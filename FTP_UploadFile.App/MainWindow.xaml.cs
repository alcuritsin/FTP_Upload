using System;
using System.IO;
using System.Windows;
using FTP_Lib;
using Microsoft.Win32;

namespace FTP_UploadFile.App
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Clear_OnClick(object sender, RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void Button_SelectFile_OnClick(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            Label_FilePath.Content = openFileDialog.FileName;
        }

        private void Button_Upload_OnClick(object sender, RoutedEventArgs e)
        {
            var filePath = Label_FilePath.Content.ToString();
            if (!File.Exists(filePath)) return;

            var fileName = Path.GetFileName(filePath);
            var login = Input_LoginFTPServer.Text;
            var password = Input_PasswordFTPServer.Password;

            try
            {
                var ftp = new Ftp($"{Input_AddressFTPServer.Text}/{fileName}", login, password);
                ftp.UploadFile(filePath, out var status);
                MessageBox.Show(status);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"{exception.Message}\n{Input_AddressFTPServer.Text}/{fileName}");
            }
        }
    }
}