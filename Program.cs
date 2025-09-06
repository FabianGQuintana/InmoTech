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
                // Mostrar login como di�logo
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // Si fue correcto, abrir Form1
                    Application.Run(new Form1());
                }
            }
        }
    }
}
