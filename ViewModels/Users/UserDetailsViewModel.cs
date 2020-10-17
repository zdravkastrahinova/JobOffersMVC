namespace JobOffersMVC.ViewModels.Users
{
    public class UserDetailsViewModel
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string FullName => $"{this.FirstName} {this.LastName}";
    }
}
