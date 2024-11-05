namespace MEAlumniAssociationDUET.Web.Core
{
    public interface IPhotoStorage
    {
        Task<string> StorePhoto(string uploadsFolderPath, IFormFile file);
        string StorePhotoWithoutIForm(string uploadsFolderPath, string image);
        Task<string> StoreFile(string uploadsFolderPath, string currentFolder, IFormFile file);
        string Base64ToImage(string base64String, string imagename, string folderpath);
    }
}
