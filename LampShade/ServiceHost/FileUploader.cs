using _0_Framework.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file, string path)
        {
            if (file == null) return string.Empty;

            var Directorypath = $"{_webHostEnvironment.WebRootPath}//ProductPictures//{path}";
            if (!Directory.Exists(Directorypath))           
                Directory.CreateDirectory(Directorypath);

            var filename = $"{DateTime.Now.ToFileName()}-{file.FileName}";

            var filepath = $"{Directorypath}//{filename}";
            using var output = File.Create(filepath);
            file.CopyToAsync(output);
            return $"{path}//{filename}";
        }
    }
}
