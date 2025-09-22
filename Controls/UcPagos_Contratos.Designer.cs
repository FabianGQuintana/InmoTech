using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos_Contratos
    {
        private System.ComponentModel.IContainer components = null;
        private Panel headerPanel;
        private Label lblTitle;
        private Label lblSubTitle;
        private TextBox txtBuscar;
        private Button btnFiltrar;
        private DataGridView dgvContratos;

        private DataGridViewTextBoxColumn colContrato;
        private DataGridViewTextBoxColumn colInquilino;
        private DataGridViewTextBoxColumn colInmueble;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewButtonColumn colAcciones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms
        private void InitializeComponent()
        {
            headerPanel = new Panel();
            lblTitle = new Label();
            lblSubTitle = new Label();
            txtBuscar = new TextBox();
            btnFiltrar = new Button();
            dgvContratos = new DataGridView();
            colContrato = new DataGridViewTextBoxColumn();
            colInquilino = new DataGridViewTextBoxColumn();
            colInmueble = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colAcciones = new DataGridViewButtonColumn();
            headerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            SuspendLayout();
            // 
            // headerPanel
            // 
            headerPanel.BackColor = Color.Teal;
            headerPanel.Controls.Add(lblTitle);
            headerPanel.Controls.Add(lblSubTitle);
            headerPanel.Controls.Add(txtBuscar);
            headerPanel.Controls.Add(btnFiltrar);
            headerPanel.Dock = DockStyle.Top;
            headerPanel.Location = new Point(0, 0);
            headerPanel.Name = "headerPanel";
            headerPanel.Padding = new Padding(24, 16, 24, 16);
            headerPanel.Size = new Size(1000, 96);
            headerPanel.TabIndex = 1;
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblTitle.Location = new Point(24, 20);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(101, 41);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "Pagos";
            // 
            // lblSubTitle
            // 
            lblSubTitle.AutoSize = true;
            lblSubTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSubTitle.ForeColor = Color.FromArgb(30, 30, 30);
            lblSubTitle.Location = new Point(24, 58);
            lblSubTitle.Name = "lblSubTitle";
            lblSubTitle.Size = new Size(126, 32);
            lblSubTitle.TabIndex = 1;
            lblSubTitle.Text = "Contratos";
            // 
            // txtBuscar
            // 
            txtBuscar.BorderStyle = BorderStyle.FixedSingle;
            txtBuscar.Font = new Font("Segoe UI", 10F);
            txtBuscar.Location = new Point(180, 60);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar contratos…";
            txtBuscar.Size = new Size(280, 30);
            txtBuscar.TabIndex = 2;
            // 
            // btnFiltrar
            // 
            btnFiltrar.Font = new Font("Segoe UI", 10F);
            btnFiltrar.Location = new Point(470, 60);
            btnFiltrar.Name = "btnFiltrar";
            btnFiltrar.Size = new Size(90, 28);
            btnFiltrar.TabIndex = 3;
            btnFiltrar.Text = "Filtrar";
            // 
            // dgvContratos
            // 
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContratos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvContratos.Columns.AddRange(new DataGridViewColumn[] { colContrato, colInquilino, colInmueble, colEstado, colAcciones });
            dgvContratos.Dock = DockStyle.Fill;
            dgvContratos.GridColor = Color.FromArgb(235, 235, 235);
            dgvContratos.Location = new Point(0, 96);
            dgvContratos.Name = "dgvContratos";
            dgvContratos.ReadOnly = true;
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.Size = new Size(1000, 544);
            dgvContratos.TabIndex = 0;
            // 
            // colContrato
            // 
            colContrato.DataPropertyName = "Contrato";
            colContrato.FillWeight = 90F;
            colContrato.HeaderText = "Contrato";
            colContrato.MinimumWidth = 6;
            colContrato.Name = "colContrato";
            colContrato.ReadOnly = true;
            // 
            // colInquilino
            // 
            colInquilino.DataPropertyName = "Inquilino";
            colInquilino.FillWeight = 140F;
            colInquilino.HeaderText = "Inquilino";
            colInquilino.MinimumWidth = 6;
            colInquilino.Name = "colInquilino";
            colInquilino.ReadOnly = true;
            // 
            // colInmueble
            // 
            colInmueble.DataPropertyName = "Inmueble";
            colInmueble.FillWeight = 170F;
            colInmueble.HeaderText = "Inmueble";
            colInmueble.MinimumWidth = 6;
            colInmueble.Name = "colInmueble";
            colInmueble.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.DataPropertyName = "Estado";
            colEstado.FillWeight = 90F;
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 6;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colAcciones
            // 
            colAcciones.FillWeight = 110F;
            colAcciones.HeaderText = "Acciones";
            colAcciones.MinimumWidth = 6;
            colAcciones.Name = "colAcciones";
            colAcciones.ReadOnly = true;
            colAcciones.Text = "Ver cuotas";
            colAcciones.UseColumnTextForButtonValue = true;
            // 
            // UcPagos_Contratos
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.White;
            Controls.Add(dgvContratos);
            Controls.Add(headerPanel);
            Name = "UcPagos_Contratos";
            Size = new Size(1000, 640);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            ResumeLayout(false);
        }
        #endregion
    }
}
