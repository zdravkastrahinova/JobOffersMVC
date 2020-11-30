namespace JobOffersMVC.ViewModels
{
    public class EmailViewModel
    {
        // Name of the user we want to send email
        public string UserName { get; set; }

        // Email of the user we want to send email
        public string UserEmail { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }
    }
}
