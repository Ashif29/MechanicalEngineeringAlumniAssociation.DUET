using Autofac;
using ImageMagick;

namespace MEAlumniAssociationDUET.Web.Core
{
    public class FileSystemPhotoStorage : IPhotoStorage
    {
        private readonly IWebHostEnvironment _host;

        public FileSystemPhotoStorage()
        {
            _host = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
        }


        public string StorePhotoWithoutIForm(string uploadsFolderPath, string image)
        {
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            //using (MemoryStream stream = new MemoryStream())
            //{
            //    image.Save(stream, image.RawFormat);
            //    Convert.ToBase64String(stream.ToArray());

            //    var file = new FileStream(filePath, FileMode.Create, FileAccess.Write);
            //    stream.CopyToAsync(file);
            //}

            using (var stream = new MemoryStream(Convert.FromBase64String(image)))
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                stream.CopyToAsync(file);
            }

            return fileName;
        }
        public async Task<string> StoreFile(string uploadsFolderPath, string currentFolder, IFormFile file)
        {
            string folderFormat = "/" + DateTime.Now.Year + "/" + DateTime.Now.ToString("MMMM") + "/";
            uploadsFolderPath = uploadsFolderPath + "/" + currentFolder + folderFormat;

            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return "/uploads/" + currentFolder + folderFormat + fileName; ;
        }

        public string Base64ToImage(string base64String, string imagename, string folderpath)
        {
            // Convert base 64 string to byte[] 
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);

            var imageFolderPath = Path.Combine(_host.WebRootPath, folderpath);
            imagename = imagename + ".jpeg";
            if (!Directory.Exists(imageFolderPath))
                Directory.CreateDirectory(imageFolderPath);
            var filePath = Path.Combine(imageFolderPath, imagename);
            //Save Image to server folder
            image.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);

            return folderpath + imagename;
        }


        public async Task<string> StorePhoto(string uploadsFolderPath, IFormFile file)
        {
            if (!Directory.Exists(uploadsFolderPath))
            {
                Directory.CreateDirectory(uploadsFolderPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);

                memoryStream.Position = 0;

                using (var resizedImageStream = await ResizeAndCompressImage(memoryStream, 300, 300, 500 * 1024))
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await resizedImageStream.CopyToAsync(fileStream);
                    }
                }
            }

            return fileName;
        }

        public async Task<Stream> ResizeAndCompressImage(Stream inputStream, uint width, uint height, long maxSizeInBytes)
        {
            inputStream.Position = 0;
            using (var image = new MagickImage(inputStream))
            {
                image.Resize(width, height);

                uint quality = 85;
                MemoryStream outputStream = new MemoryStream();
                do
                {
                    outputStream.SetLength(0);

                    image.Quality = quality;
                    await Task.Run(() => image.Write(outputStream));
                    if (outputStream.Length <= maxSizeInBytes)
                    {
                        break;
                    }
                    quality -= 1;
                }
                while (outputStream.Length > maxSizeInBytes && quality > 50);

                outputStream.Position = 0;
                return outputStream;
            }
        }


    }
}
