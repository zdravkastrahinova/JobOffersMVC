using AutoMapper;
using JobOffersMVC.Enums;
using JobOffersMVC.Models;
using JobOffersMVC.ViewModels.Auth;
using JobOffersMVC.ViewModels.Comments;
using JobOffersMVC.ViewModels.JobOffers;
using JobOffersMVC.ViewModels.UserApplications;
using JobOffersMVC.ViewModels.Users;
using System;

namespace JobOffersMVC.Services.AutoMapper
{
    public class AutoMapperConfiguration : Profile
    {
        public AutoMapperConfiguration()
        {
            // Model to View Model
            CreateMap<User, UserDetailsViewModel>();
            CreateMap<User, UserEditViewModel>();

            CreateMap<JobOffer, JobOfferDetailsViewModel>()
                .ForMember(offer => offer.UserName, options => options.MapFrom(x => $"{x.User.FirstName} {x.User.LastName}"))
                .ForMember(offer => offer.Comments, options => options.MapFrom(x => x.Comments))
                .ForMember(offer => offer.UserApplications, options => options.MapFrom(x => x.UserApplications));
            CreateMap<JobOffer, JobOfferEditViewModel>();

            CreateMap<UserApplication, UserApplicationEditViewModel>();
            CreateMap<UserApplication, UserApplicationDetailsViewModel>()
                .ForMember(ua => ua.Status, options => options.MapFrom(app => Enum.GetName(typeof(ApplicationStatusEnum), app.Status)))
                .ForMember(ua => ua.UserName, options => options.MapFrom(app => $"{app.User.FirstName} {app.User.LastName}"))
                .ForMember(ua => ua.JobOfferTitle, options => options.MapFrom(app => app.JobOffer.Title));

            CreateMap<Comment, CommentEditViewModel>();
            CreateMap<Comment, CommentDetailsViewModel>()
                .ForMember(c => c.JobOferTitle, options => options.MapFrom(comment => comment.JobOffer.Title))
                .ForMember(c => c.UserName, options => options.MapFrom(comment => $"{comment.User.FirstName} {comment.User.LastName}"));

            // ViewModel to Model
            CreateMap<UserEditViewModel, User>();
            CreateMap<RegisterViewModel, User>();

            CreateMap<JobOfferEditViewModel, JobOffer>()
                .ForMember(offer => offer.UserApplications, options => options.Ignore())
                .ForMember(offer => offer.Comments, options => options.Ignore())
                .ForMember(offer => offer.User, options => options.Ignore());

            CreateMap<UserApplicationEditViewModel, UserApplication>();

            CreateMap<CommentEditViewModel, Comment>();
        }
    }
}
