using System.IO;
using System.Net;

namespace FTP_Lib
{
    /// <summary>
    /// Основной класс библиотеки для работы с FTP
    /// </summary>
    public class Ftp
    {
        private readonly string _ftpPath;
        private readonly string _login;
        private readonly string _password;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="ftpPath">Путь к FTP-серверу</param>
        /// <param name="login">Логин пользователя</param>
        /// <param name="password">Пароль пользователя</param>
        public Ftp(string ftpPath, string login, string password)
        {
            _ftpPath = ftpPath;
            _login = login;
            _password = password;
        }

        /// <summary>
        /// Загрузка любого файла на FTP-сервер
        /// </summary>
        /// <param name="filePath">Путь к файлу</param>
        /// <param name="responseStatus">Ответ FTP-сервера</param>
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

