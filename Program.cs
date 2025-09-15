// Program.cs
using System;
using System.Windows.Forms;

namespace InmoTech
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var login = new LoginForm())
            {
                var result = login.ShowDialog();
                if (result != DialogResult.OK)
                {
                    // Si el usuario cierra o falla el login, salimos
                    return;
                }
            }

            Application.Run(new MainForm());
        }
    }
}
