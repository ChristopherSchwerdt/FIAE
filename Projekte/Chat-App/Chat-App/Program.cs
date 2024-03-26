using ConnectionObject;
using System.Net.Sockets;

namespace Chat_App
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            LoginForm loginForm = new LoginForm();
            Application.Run(loginForm);

            if(loginForm.userSuccessfullyAuthenticated)
            {
                ChatClient client = loginForm.GetClientConnection();
                Application.Run(new MainForm(client));
            }
           
        }
    }
}