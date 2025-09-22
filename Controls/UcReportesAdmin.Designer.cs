// UcReportesAdmin.Designer.cs
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcReportesAdmin
    {
        private IContainer components = null;

        private TableLayoutPanel root;
        private Panel header;
        private Label lblTitulo;

        private Panel filtros;
        private DateTimePicker dtpFecha;
        private Button btnHoy;
        private Button btnActualizar;
        private Button btnExportar;

        private TableLayoutPanel cards;
        private Panel cardUsuarios;
        private Label lblUsuariosTitulo;
        private Label lblUsuariosValor;

        private Panel cardInmuebles;
        private Label lblInmueblesTitulo;
        private Label lblInmueblesValor;

        private Panel cardInquilinos;
        private Label lblInquilinosTitulo;
        private Label lblInquilinosValor;

        private TableLayoutPanel lists;
        private Panel panelUsuariosRecientes;
        private Label lblUsuariosRecientes;
        private DataGridView dgvUsuarios;

        private Panel panelInmueblesRecientes;
        private Label lblInmueblesRecientes;
        private DataGridView dgvInmuebles;

        private BindingSource bsUsuarios;
        private BindingSource bsInmuebles;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();

            // === Root ===
            root = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                ColumnCount = 1,
                RowCount = 5,
                Padding = new Padding(12)
            };
            root.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // header
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // filtros
            root.RowStyles.Add(new RowStyle(SizeType.AutoSize));          // cards
            root.RowStyles.Add(new RowStyle(SizeType.Percent, 100));      // lists
            root.RowStyles.Add(new RowStyle(SizeType.Absolute, 0));       // spacer

            // === Header ===
            header = new Panel { Dock = DockStyle.Top, Height = 52, Padding = new Padding(4, 6, 4, 6) };
            lblTitulo = new Label
            {
                AutoSize = true,
                Text = "Reportes diarios (Administrador)",
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 41, 59)
            };
            header.Controls.Add(lblTitulo);

            // === Filtros ===
            filtros = new Panel { Dock = DockStyle.Top, Height = 48, Padding = new Padding(0, 6, 0, 6) };
            dtpFecha = new DateTimePicker
            {
                Format = DateTimePickerFormat.Short,
                Width = 160,
                Location = new Point(4, 10)
            };
            btnHoy = new Button { Text = "Hoy", AutoSize = true, Location = new Point(176, 8) };
            btnActualizar = new Button { Text = "Actualizar", AutoSize = true, Location = new Point(240, 8) };
            btnExportar = new Button { Text = "Exportar", AutoSize = true, Location = new Point(340, 8) };
            filtros.Controls.AddRange(new Control[] { dtpFecha, btnHoy, btnActualizar, btnExportar });

            // === Cards (3 columnas responsivas) ===
            cards = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 120,
                ColumnCount = 3,
                RowCount = 1,
                Margin = new Padding(0, 6, 0, 6)
            };
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333f));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333f));
            cards.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.333f));
            cards.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            cardUsuarios = CrearCard(out lblUsuariosTitulo, out lblUsuariosValor, "Usuarios creados");
            cardInmuebles = CrearCard(out lblInmueblesTitulo, out lblInmueblesValor, "Inmuebles creados");
            cardInquilinos = CrearCard(out lblInquilinosTitulo, out lblInquilinosValor, "Inquilinos creados");

            cards.Controls.Add(cardUsuarios, 0, 0);
            cards.Controls.Add(cardInmuebles, 1, 0);
            cards.Controls.Add(cardInquilinos, 2, 0);

            // === Lists (dos columnas responsivas) ===
            lists = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            lists.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            lists.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            lists.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            // Usuarios recientes
            panelUsuariosRecientes = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6, 0, 6, 6) };
            lblUsuariosRecientes = new Label
            {
                Text = "Usuarios recientes",
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(0, 6, 0, 6)
            };
            dgvUsuarios = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            // columnas usuarios
            dgvUsuarios.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn{ DataPropertyName="Usuario", HeaderText="Usuario", FillWeight=120 },
                new DataGridViewTextBoxColumn{ DataPropertyName="Nombre", HeaderText="Nombre", FillWeight=160 },
                new DataGridViewTextBoxColumn{ DataPropertyName="Rol", HeaderText="Rol", FillWeight=90 },
                new DataGridViewTextBoxColumn{
                    DataPropertyName="FechaCreacion",
                    HeaderText="Fecha creación",
                    DefaultCellStyle = new DataGridViewCellStyle { Format="dd/MM/yyyy HH:mm" },
                    FillWeight=130
                }
            });
            // estilo teal
            dgvUsuarios.EnableHeadersVisualStyles = false;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvUsuarios.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 242, 241);

            panelUsuariosRecientes.Controls.Add(dgvUsuarios);
            panelUsuariosRecientes.Controls.Add(lblUsuariosRecientes);

            // Inmuebles recientes
            panelInmueblesRecientes = new Panel { Dock = DockStyle.Fill, Padding = new Padding(6, 0, 6, 6) };
            lblInmueblesRecientes = new Label
            {
                Text = "Inmuebles recientes",
                Dock = DockStyle.Top,
                AutoSize = true,
                Font = new Font("Segoe UI", 11f, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),
                Padding = new Padding(0, 6, 0, 6)
            };
            dgvInmuebles = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoGenerateColumns = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false
            };
            // columnas inmuebles
            dgvInmuebles.Columns.AddRange(new DataGridViewColumn[] {
                new DataGridViewTextBoxColumn{ DataPropertyName="Descripcion", HeaderText="Descripción", FillWeight=250 },
                new DataGridViewTextBoxColumn{
                    DataPropertyName="Hora",
                    HeaderText="Hora",
                    DefaultCellStyle = new DataGridViewCellStyle { Format="HH:mm" },
                    FillWeight=70
                }
            });
            // estilo teal
            dgvInmuebles.EnableHeadersVisualStyles = false;
            dgvInmuebles.ColumnHeadersDefaultCellStyle.BackColor = Color.Teal;
            dgvInmuebles.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInmuebles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(224, 242, 241);

            panelInmueblesRecientes.Controls.Add(dgvInmuebles);
            panelInmueblesRecientes.Controls.Add(lblInmueblesRecientes);

            lists.Controls.Add(panelUsuariosRecientes, 0, 0);
            lists.Controls.Add(panelInmueblesRecientes, 1, 0);

            // BindingSources
            bsUsuarios = new BindingSource();
            bsInmuebles = new BindingSource();
            dgvUsuarios.DataSource = bsUsuarios;
            dgvInmuebles.DataSource = bsInmuebles;

            // Ensamble
            root.Controls.Add(header, 0, 0);
            root.Controls.Add(filtros, 0, 1);
            root.Controls.Add(cards, 0, 2);
            root.Controls.Add(lists, 0, 3);

            Controls.Add(root);

            // Eventos básicos
            btnHoy.Click += (s, e) => dtpFecha.Value = System.DateTime.Today;
        }

        private static Panel CrearCard(out Label titulo, out Label valor, string texto)
        {
            var p = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Teal,
                Padding = new Padding(14),
                Margin = new Padding(6),
                BorderStyle = BorderStyle.FixedSingle
            };
            titulo = new Label
            {
                AutoSize = true,
                Text = texto,
                Font = new Font("Segoe UI", 9.5f, FontStyle.Regular),
                ForeColor = Color.White
            };
            valor = new Label
            {
                AutoSize = true,
                Text = "—",
                Font = new Font("Segoe UI", 20f, FontStyle.Bold),
                ForeColor = Color.White
            };
            p.Controls.Add(titulo);
            p.Controls.Add(valor);
            titulo.Location = new Point(6, 6);
            valor.Location = new Point(6, 34);
            return p;
        }
    }
}
