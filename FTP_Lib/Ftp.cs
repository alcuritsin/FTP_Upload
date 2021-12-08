using System.IO;
using System.Net;

namespace FTP_Lib
{
    public class Ftp
    {
        private readonly string _ftpPath;

        public Ftp(string ftpPath)
        {
            _ftpPath = ftpPath;
        }

        public void UploadFile(string filePath, out string responseStatus)
        {
            var request = (FtpWebRequest)WebRequest.Create(_ftpPath);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            
            using var fs = new FileStream(filePath, FileMode.Open);
            var fileContents = new byte[fs.Length];
            fs.Read(fileContents, 0, fileContents.Length);
            request.ContentLength = fileContents.Length;
            
            using var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);

            using var response = (FtpWebResponse)request.GetResponse();
            responseStatus = response.StatusDescription;
        }
    }
}

