namespace JobOffersMVC
{
    public class AppSettings
    {
        public FileUploadSettings FileUploadSettings { get; set; }

        public EmailSettings EmailSettings { get; set; }
    }

    public class FileUploadSettings
    {
        public string UploadFolder { get; set; }
    }

    public class EmailSettings
    {
        public string EmailName { get; set; }

        public string EmailAccount { get; set; }

        public string EmailPassword { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }
    }
}
