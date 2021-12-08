using System.IO;
using System.Net;

namespace FTP_Lib
{
    public class Ftp
    {
        private readonly string _ftpPath;
        private readonly string _login;
        private readonly string _password;

        public Ftp(string ftpPath, string login, string password)
        {
            _ftpPath = ftpPath;
            _login = login;
            _password = password;
        }

        public void UploadFile(string filePath, out string responseStatus)
        {
            var request = (FtpWebRequest)WebRequest.Create(_ftpPath);
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(_login, _password);
            
            using var fs = new FileStream(filePath, FileMode.Open);
            var fileContents = new byte[fs.Length];
            fs.Read(fileContents, 0, fileContents.Length);
            request.ContentLength = fileContents.Length;
            request.EnableSsl = true;
            
            using var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);

            using var response = (FtpWebResponse)request.GetResponse();
            responseStatus = response.StatusDescription;
        }
    }
}

