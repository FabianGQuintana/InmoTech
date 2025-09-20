// InmoTech/LoginForm.cs
using InmoTech.Repositories;
using InmoTech.Security;
using InmoTech.Models;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace InmoTech
{
    public partial class LoginForm : Form
    {
        private readonly UsuarioRepository _usuarioRepository = new();

        public LoginForm()
        {
            InitializeComponent();

            txtPassword.UseSystemPasswordChar = true; 

            btnLogin.Click += BtnLogin_Click;
            chkMostrarPassword.CheckedChanged += ChkMostrarPassword_CheckedChanged;

            txtEmail.KeyDown += OnKeyDownEnterLogin;
            txtPassword.KeyDown += OnKeyDownEnterLogin;
        }


        private void ChkMostrarPassword_CheckedChanged(object? sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkMostrarPassword.Checked;
        }

        private void OnKeyDownEnterLogin(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                IntentarLogin();
            }
        }

        private void BtnLogin_Click(object? sender, EventArgs e) => IntentarLogin();

        private void IntentarLogin()
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(email))
            {
                MostrarError("Ingresá tu email.");
                txtEmail.Focus();
                return;
            }
            if (!EsEmailValido(email))
            {
                MostrarError("El formato de email no es válido.");
                txtEmail.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MostrarError("Ingresá tu contraseña.");
                txtPassword.Focus();
                return;
            }

            try
            {
                var usuario = _usuarioRepository.ValidarCredenciales(email, password);

                if (usuario is null)
                {
                    MostrarError("Usuario o contraseña incorrectos");
                    return;
                }

                // OK → guardar sesión y cerrar con OK
                AuthService.SignIn(usuario);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MostrarError("Ocurrió un error al iniciar sesión. " + ex.Message);
            }
        }

        private static bool EsEmailValido(string email)
        {
            // Regex simple para email
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private void MostrarError(string mensaje)
        {
            // Si tenés un label de error
            var lbl = this.Controls.Find("lblError", true);
            if (lbl.Length > 0 && lbl[0] is Label lblError)
            {
                lblError.Text = mensaje;
                lblError.Visible = true;
            }
            else
            {
                MessageBox.Show(mensaje, "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
