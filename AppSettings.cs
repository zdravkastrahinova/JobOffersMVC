namespace JobOffersMVC
{
    public class AppSettings
    {
        public FileUploadSettings FileUploadSettings { get; set; }
    }

    public class FileUploadSettings
    {
        public string UploadFolder { get; set; }
    }
}
