using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    public partial class UcReportesOperador : UserControl
    {
        // ======================================================
        //  REGIÓN: propiedades
        // ======================================================
        #region Propiedades
        private DataTable _tabla;
        #endregion

        // ======================================================
        //  REGIÓN: Constructor y Configuración Inicial
        // ======================================================
        #region Constructor y Configuración Inicial
        public UcReportesOperador()
        {
            InitializeComponent();

            // Anti-parpadeo
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
          ControlStyles.AllPaintingInWmPaint |
          ControlStyles.UserPaint, true);
            UpdateStyles();

            dataGridView1.ScrollBars = ScrollBars.Both;

            // Configurar columnas y estilos del grid
            ConfigureGrid();

            // Datos de muestra
            LoadDemoData();

            // Fuerza layout en carga
            Load += (s, e) => PerformLayout();
        }
        #endregion

        // ======================================================
        //  REGIÓN: Configuración de DataGridView 
        // ======================================================
        #region Configuración de DataGridView
        private void ConfigureGrid()
        {
            // No autogenerar, definimos a mano
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            // Estilos generales
            dataGridView1.ReadOnly = true;
            dataGridView1.MultiSelect = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Formato regional (AR para moneda con coma)
            var ar = CultureInfo.GetCultureInfo("es-AR");
            dataGridView1.DefaultCellStyle.FormatProvider = ar;
            dataGridView1.ColumnHeadersDefaultCellStyle.Font =
              new System.Drawing.Font(dataGridView1.Font, System.Drawing.FontStyle.Bold);

            // ----- Columnas -----
            // Contrato
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colContrato",
                HeaderText = "Contrato",
                DataPropertyName = "Contrato",
                FillWeight = 110
            });

            // Inquilino
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colInquilino",
                HeaderText = "Inquilino",
                DataPropertyName = "Inquilino",
                FillWeight = 150
            });

            // Inmueble
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colInmueble",
                HeaderText = "Inmueble",
                DataPropertyName = "Inmueble",
                FillWeight = 160
            });

            // Fecha Pago (dd/MM/yyyy)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colFechaPago",
                HeaderText = "Fecha Pago",
                DataPropertyName = "FechaPago",
                DefaultCellStyle = { Format = "dd/MM/yyyy", Alignment = DataGridViewContentAlignment.MiddleCenter },
                FillWeight = 110
            });

            // Importe (moneda)
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colImporte",
                HeaderText = "Importe",
                DataPropertyName = "Importe",
                DefaultCellStyle = { Format = "C2", Alignment = DataGridViewContentAlignment.MiddleRight },
                FillWeight = 110
            });

            // Medio
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colMedio",
                HeaderText = "Medio",
                DataPropertyName = "Medio",
                FillWeight = 100
            });
        }
        #endregion

        // ======================================================
        //  REGIÓN: Carga de Datos
        // ======================================================
        #region Carga de Datos
        private void LoadDemoData()
        {
            _tabla = new DataTable();
            _tabla.Columns.Add("Contrato", typeof(string));
            _tabla.Columns.Add("Inquilino", typeof(string));
            _tabla.Columns.Add("Inmueble", typeof(string));
            _tabla.Columns.Add("FechaPago", typeof(DateTime));
            _tabla.Columns.Add("Importe", typeof(decimal));
            _tabla.Columns.Add("Medio", typeof(string));

            // --- filas demo ---
            _tabla.Rows.Add("CT-00045", "María López", "Depto 2A - San Martín 1234", DateTime.Today, 120000.00m, "Transferencia");
            _tabla.Rows.Add("CT-00012", "Juan Pérez", "Casa - Belgrano 456", DateTime.Today, 95000.50m, "Efectivo");
            _tabla.Rows.Add("CT-00077", "Ana Gómez", "PH - Rivadavia 789", DateTime.Today.AddDays(-1), 135500.75m, "Tarjeta");
            _tabla.Rows.Add("CT-00088", "Carlos Ruiz", "Depto 7C - Lavalle 321", DateTime.Today.AddDays(-2), 88000.00m, "Transferencia");

            dataGridView1.DataSource = _tabla;
        }
        #endregion
    }
}