using AutoMapper;
using JobOffersMVC.Models;
using JobOffersMVC.Repositories.Abstractions;
using JobOffersMVC.Services.Helpers;
using JobOffersMVC.Services.ModelServices.Abstractions;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Users;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace JobOffersMVC.Services.ModelServices.Implementations
{
    public class UsersService : BaseService<User, UserDetailsViewModel, UserEditViewModel>, IUsersService
    {
        private readonly IFileHelperService fileHelperService;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly AppSettings appSettings;

        public UsersService(IUsersRepository usersRepository, IMapper mapper, IFileHelperService fileHelperService, IWebHostEnvironment webHostEnvironment, AppSettings appSettings)
            : base (usersRepository, mapper)
        {
            this.fileHelperService = fileHelperService;
            this.webHostEnvironment = webHostEnvironment;
            this.appSettings = appSettings;
        }

        public UserDetailsViewModel GetByUsernameAndPassword(string username, string password)
        {
            User user = ((IUsersRepository)repository).GetByUsernameAndPassword(username, password);

            return mapper.Map<User, UserDetailsViewModel>(user);
        }

        public void Insert(RegisterViewModel viewModel)
        {
            User user = mapper.Map<User>(viewModel);

            repository.Insert(user);
        }

        public void UploadImage(int userId, IFormFile formFile)
        {
            // 1. Get user by id
            User user = repository.GetById(userId);

            // 2. Set image name
            string fileExtension = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.')).ToLower();

            user.ImagePath = $"{Guid.NewGuid()}{fileExtension}";

            string folderPath = Path.Combine(webHostEnvironment.WebRootPath, appSettings.FileUploadSettings.UploadFolder);
            string filePath = fileHelperService.BuildFilePath(folderPath, user.ImagePath);

            fileHelperService.CreateFile(formFile, filePath);

            // 3. Update user
            repository.Update(user);
        }
    }
}
