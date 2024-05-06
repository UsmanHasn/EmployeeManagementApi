using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using Microsoft.AspNetCore.Hosting;

namespace BusinessLogicLayer.Helper
{
    public interface IGeneralFunctions
    {
        string? GetConfigValue(string Section, string Key);
        int GetLoggedInUserId();
        string GetLoggedInUserEmail();
        string GetLoggedInUserName();
        Task<string> UploadMediaFile(HttpRequest httpRequest, string uploadPath);
        string GetProfileImageDirectory();
    }

    public class GeneralFunctions : IGeneralFunctions
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _IConfiguration;
        private readonly IBLL_LoggedInUser _IBLL_LoggedInUser;
        public GeneralFunctions(IConfiguration IConfiguration, IBLL_LoggedInUser IBLL_LoggedInUser,IWebHostEnvironment IWebHostEnvironment)
        {
            _IConfiguration = IConfiguration;
            _IBLL_LoggedInUser = IBLL_LoggedInUser;
            _webHostEnvironment = IWebHostEnvironment;
        }

        public string? GetConfigValue(string Section, string Key)
        {
            return _IConfiguration[Section + ":" + Key];
        }

        public int GetLoggedInUserId()
        {
            return _IBLL_LoggedInUser.GetLoggedinUserId();
        }

        public string GetLoggedInUserEmail()
        {
            return _IBLL_LoggedInUser.GetLoggedInUserEmail();
        }

        public string GetLoggedInUserName()
        {
            return _IBLL_LoggedInUser.GetLoggedInUserName();
        }

        public async Task<string> UploadMediaFile(HttpRequest httpRequest, string uploadPath)
        {
            var formFile = httpRequest.Form.Files.FirstOrDefault();
            if (formFile == null || formFile.Length == 0)
            {
                throw new ArgumentException("File is empty or null");
            }
            string folderpath = _webHostEnvironment.WebRootPath + uploadPath;
        //E:\Classes\Update Project 2022\SchoolManagementApi\PresentationLayer\wwwroot\
        //Upload\ProfilePictures\
            if (!Directory.Exists(folderpath))
            {
                Directory.CreateDirectory(folderpath);
            }
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(formFile.FileName);

            // Combine the upload path with the file name
            var filePath = Path.Combine(folderpath, fileName);

            // Copy the file to the server
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }

            // Resize the image (if it's an image)
            if (IsImage(formFile))
            {
                var resizedFilePath = Path.Combine(folderpath, "resized_" + fileName);
                using (Image<Rgba32> image = Image.Load<Rgba32>(filePath))
                {
                    // Resize the image to half its dimensions
                    image.Mutate(x => x.Resize(image.Width / 2, image.Height / 2));
                    image.Save(resizedFilePath); // Save the resized image
                }
                // Delete the original file
                File.Delete(filePath);
                return resizedFilePath;
            }
            else
            {
                // Return the original file path if it's not an image
                return filePath;
            }


            // Generate a unique file name

        }

        public string GetProfileImageDirectory()
        {
            return "\\Upload\\ProfilePictures";
        }
        private bool IsImage(IFormFile file)
        {
            return file.ContentType.ToLower().StartsWith("image/");
        }
    }
}
