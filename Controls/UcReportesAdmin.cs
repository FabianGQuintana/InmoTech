// UcReportesAdmin.cs
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReportesAdmin : UserControl
    {
        // ==== ViewModels ====
        public sealed class UsuarioRecienteVm
        {
            public string Usuario { get; set; } = "";
            public string Nombre { get; set; } = "";
            public string Rol { get; set; } = "";
            public DateTime FechaCreacion { get; set; }
        }
        public sealed class InmuebleRecienteVm
        {
            public string Descripcion { get; set; } = "";
            public DateTime Hora { get; set; }
        }

        private readonly BindingList<UsuarioRecienteVm> _usuarios = new();
        private readonly BindingList<InmuebleRecienteVm> _inmuebles = new();

        public UcReportesAdmin()
        {
            InitializeComponent();

            // DS
            bsUsuarios.DataSource = _usuarios;
            bsInmuebles.DataSource = _inmuebles;

            // Eventos
            Load += (_, __) => CargarReporte(dtpFecha.Value.Date);
            btnActualizar.Click += (_, __) => CargarReporte(dtpFecha.Value.Date);
            btnExportar.Click += (_, __) => ExportarCsv();
            btnHoy.Click += (_, __) => CargarReporte(DateTime.Today);
        }

        /// <summary>
        /// Carga cards + grillas. Reemplazá mocks por llamadas reales a tus repos.
        /// </summary>
        public void CargarReporte(DateTime fecha)
        {
            // ===== Reemplazar por repos reales =====
            // int usuariosCreados   = repoUsuarios.ContarCreadosEn(fecha);
            // int inmueblesCreados  = repoInmuebles.ContarCreadosEn(fecha);
            // int inquilinosCreados = repoInquilinos.ContarCreadosEn(fecha);
            // var ultUsuarios       = repoUsuarios.ListarRecientes(fecha, top:20);
            // var ultInmuebles      = repoInmuebles.ListarRecientes(fecha, top:20);

            // ---- MOCK para demo UI ----
            var rnd = new Random(fecha.GetHashCode());
            int usuariosCreados = rnd.Next(5, 20);
            int inmueblesCreados = rnd.Next(3, 12);
            int inquilinosCreados = rnd.Next(6, 18);

            var ultUsuarios = MockUsuarios(fecha, rnd, 8);
            var ultInmuebles = MockInmuebles(fecha, rnd, 8);
            // ---------------------------

            // Cards
            lblUsuariosValor.Text = usuariosCreados.ToString();
            lblInmueblesValor.Text = inmueblesCreados.ToString();
            lblInquilinosValor.Text = inquilinosCreados.ToString();

            // Grillas
            _usuarios.Clear();
            foreach (var u in ultUsuarios) _usuarios.Add(u);

            _inmuebles.Clear();
            foreach (var i in ultInmuebles) _inmuebles.Add(i);
        }

        // ==== Mocks ====
        private static IEnumerable<UsuarioRecienteVm> MockUsuarios(DateTime fecha, Random rnd, int n)
        {
            string[] roles = { "Admin", "Operador", "Operador", "Propietario" };
            for (int i = 1; i <= n; i++)
            {
                yield return new UsuarioRecienteVm
                {
                    Usuario = $"usuario{i}",
                    Nombre = $"Usuario {i}",
                    Rol = roles[rnd.Next(roles.Length)],
                    FechaCreacion = fecha.AddHours(rnd.Next(8, 18)).AddMinutes(rnd.Next(0, 60))
                };
            }
        }

        private static IEnumerable<InmuebleRecienteVm> MockInmuebles(DateTime fecha, Random rnd, int n)
        {
            for (int i = 1; i <= n; i++)
            {
                yield return new InmuebleRecienteVm
                {
                    Descripcion = $"Depto {rnd.Next(1, 60)} - Piso {rnd.Next(1, 10)}",
                    Hora = fecha.AddHours(rnd.Next(8, 20)).AddMinutes(rnd.Next(0, 60))
                };
            }
        }

        // ==== Exportación simple a CSV de ambas grillas ====
        private void ExportarCsv()
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = $"Admin_Report_{dtpFecha.Value:yyyyMMdd}.csv"
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            try
            {
                using var w = new System.IO.StreamWriter(sfd.FileName);
                // Usuarios
                w.WriteLine("=== Usuarios recientes ===");
                w.WriteLine("Usuario,Nombre,Rol,FechaCreacion");
                foreach (var u in _usuarios)
                    w.WriteLine($"{u.Usuario},{Esc(u.Nombre)},{u.Rol},{u.FechaCreacion:dd/MM/yyyy HH:mm}");
                w.WriteLine();

                // Inmuebles
                w.WriteLine("=== Inmuebles recientes ===");
                w.WriteLine("Descripcion,Hora");
                foreach (var i in _inmuebles)
                    w.WriteLine($"{Esc(i.Descripcion)},{i.Hora:HH:mm}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exportando CSV: " + ex.Message, "Exportar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static string Esc(string s) => "\"" + (s ?? "").Replace("\"", "\"\"") + "\"";
    }
}
