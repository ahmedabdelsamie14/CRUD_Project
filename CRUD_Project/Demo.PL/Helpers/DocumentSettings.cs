using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        public static string  UploadFile(IFormFile file , string folderName)
        {
            //1.Get Path of Folder
            //string FolderPath = "C:\\Users\\passa\\Downloads\\ASP.NET\\Demo.PL Solution\\Demo.PL\\wwwroot\\files\\images\\";//da m4 sa777

            //string FolderPath = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\"+ folderName;
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files\\" + folderName);

            //2.Get FileName and make it unique

            string fileName = $"{Guid.NewGuid()}{file.FileName}";

            //3.Get File Path

            string filePath = Path.Combine(FolderPath, fileName);

            // Save file as Streams : (Date Per Time)

            var fileStream = new FileStream(filePath, FileMode.Create);

            file.CopyTo(fileStream);

            return fileName;

        }


        public static void DeleteFile(string fileName , string folderName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName ,fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
