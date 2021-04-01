using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebServer.Utils
{
    public class FileUtil
    {

        public static bool MoveFile(IWebHostEnvironment _environment, string ImageFile, string moveFolder)
        {

            try
            {

                if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
                {
                    _environment.WebRootPath = Directory.GetCurrentDirectory();
                }

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var uploadFolder = Path.Combine(_environment.WebRootPath, "Files"); //실제 사용 폴더
                    var uploadTempFolder = Path.Combine(uploadFolder, "Temp");//임시폴더
                    var uploadProductFolder = Path.Combine(uploadFolder, moveFolder);

                    if (!Directory.Exists(uploadTempFolder))
                    {
                        Directory.CreateDirectory(uploadTempFolder);
                    }

                    if (!Directory.Exists(uploadProductFolder))
                    {
                        Directory.CreateDirectory(uploadProductFolder);
                    }

                    var orginTempFilePath = Path.Combine(uploadTempFolder, ImageFile);
                    var destTempFilePath = Path.Combine(uploadProductFolder, ImageFile);
                    if (System.IO.File.Exists(orginTempFilePath))
                    {
                        System.IO.File.Move(orginTempFilePath, destTempFilePath);
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }
    }
}
