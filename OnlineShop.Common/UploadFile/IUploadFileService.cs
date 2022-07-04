using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Common.UploadFile
{
    public interface IUploadFileService
    {
        ResultUpload ExecuteFileUpload(IFormFile file);
    }

    public class UploadFileService : IUploadFileService
    {
        private readonly IHostingEnvironment _environment;

        public UploadFileService(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public ResultUpload ExecuteFileUpload(IFormFile file)
        {

            if (file != null)
            {
               // string folder = @"images\" + DateTime.Now.ToShortDateString().Replace('/','\\') + @"\";
                //string folder = $@"images\ProductImages\";
                string folder = $@"images\" + DateTime.Now.ToShortDateString() + @"\";

                string uploadRootPath = Path.Combine(_environment.WebRootPath, folder);
                if (!Directory.Exists(uploadRootPath))
                {
                    Directory.CreateDirectory(uploadRootPath);
                }

                string fileName = DateTime.Now.Ticks.ToString() + file.FileName;
                string filePath = Path.Combine(uploadRootPath, fileName);

                using (var fileStram = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStram);
                }
                return new ResultUpload
                {
                    Status = true,
                    AddressFileName = folder + fileName

                };
            }
            else
            {
                return new ResultUpload
                {
                    Status = false,
                    AddressFileName = ""
                };
            }
        }
    }

    public class ResultUpload
    {
        public bool Status { get; set; }
        public string AddressFileName { get; set; }
    }
}
