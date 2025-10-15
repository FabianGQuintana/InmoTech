using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcPagos_Contratos
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlTop;
        private Label lblTitulo;

        private Panel pnlFiltros;
        private TableLayoutPanel tlpFiltros;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Label lblEstado;
        private ComboBox cboEstado;

        private DataGridView dgv;

        private Panel pnlBottom;
        private TableLayoutPanel tlpBottom;
        private Label lblInfo;
        private FlowLayoutPanel flpNav;
        private Button btnAnterior;
        private Button btnSiguiente;
        private FlowLayoutPanel flpAcciones;
        private Button btnElegir;
        private Button btnCancelar;

        private System.Windows.Forms.Timer timerBuscar;

        // Columnas
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colInquilino;
        private DataGridViewTextBoxColumn colInmueble;
        private DataGridViewTextBoxColumn colInicio;
        private DataGridViewTextBoxColumn colFin;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colUsuario;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            lblTitulo = new Label();
            pnlFiltros = new Panel();
            tlpFiltros = new TableLayoutPanel();
            lblBuscar = new Label();
            lblEstado = new Label();
            cboEstado = new ComboBox();
            txtBuscar = new TextBox();
            dgv = new DataGridView();
            pnlBottom = new Panel();
            tlpBottom = new TableLayoutPanel();
            lblInfo = new Label();
            flpNav = new FlowLayoutPanel();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            flpAcciones = new FlowLayoutPanel();
            btnElegir = new Button();
            btnCancelar = new Button();
            timerBuscar = new System.Windows.Forms.Timer(components);
            pnlTop.SuspendLayout();
            pnlFiltros.SuspendLayout();
            tlpFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            pnlBottom.SuspendLayout();
            tlpBottom.SuspendLayout();
            flpNav.SuspendLayout();
            flpAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.Teal;
            pnlTop.Controls.Add(lblTitulo);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Name = "pnlTop";
            pnlTop.Padding = new Padding(16);
            pnlTop.Size = new Size(900, 75);
            pnlTop.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(8, 16);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(447, 40);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Seleccionar contrato para pago";
            // 
            // pnlFiltros
            // 
            pnlFiltros.Controls.Add(tlpFiltros);
            pnlFiltros.Dock = DockStyle.Top;
            pnlFiltros.Location = new Point(0, 75);
            pnlFiltros.Name = "pnlFiltros";
            pnlFiltros.Padding = new Padding(12, 8, 12, 8);
            pnlFiltros.Size = new Size(900, 68);
            pnlFiltros.TabIndex = 2;
            // 
            // tlpFiltros
            // 
            tlpFiltros.ColumnCount = 4;
            tlpFiltros.ColumnStyles.Add(new ColumnStyle());
            tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFiltros.ColumnStyles.Add(new ColumnStyle());
            tlpFiltros.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 170F));
            tlpFiltros.Controls.Add(lblBuscar, 0, 0);
            tlpFiltros.Controls.Add(lblEstado, 2, 0);
            tlpFiltros.Controls.Add(cboEstado, 3, 0);
            tlpFiltros.Controls.Add(txtBuscar, 1, 0);
            tlpFiltros.Dock = DockStyle.Fill;
            tlpFiltros.Location = new Point(12, 8);
            tlpFiltros.Name = "tlpFiltros";
            tlpFiltros.Padding = new Padding(0, 6, 0, 6);
            tlpFiltros.RowCount = 1;
            tlpFiltros.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFiltros.Size = new Size(876, 52);
            tlpFiltros.TabIndex = 0;
            tlpFiltros.Paint += tlpFiltros_Paint;
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblBuscar.Location = new Point(0, 16);
            lblBuscar.Margin = new Padding(0, 10, 8, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(138, 25);
            lblBuscar.TabIndex = 0;
            lblBuscar.Text = "Buscar (texto):";
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblEstado.Location = new Point(624, 16);
            lblEstado.Margin = new Padding(0, 10, 8, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(74, 25);
            lblEstado.TabIndex = 2;
            lblEstado.Text = "Estado:";
            // 
            // cboEstado
            // 
            cboEstado.Anchor = AnchorStyles.Right;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cboEstado.Location = new Point(706, 12);
            cboEstado.Margin = new Padding(0, 6, 0, 0);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(170, 33);
            cboEstado.TabIndex = 3;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(146, 13);
            txtBuscar.Margin = new Padding(0, 6, 16, 0);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(462, 31);
            txtBuscar.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 143);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 62;
            dgv.RowTemplate.Height = 28;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(900, 329);
            dgv.TabIndex = 0;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(tlpBottom);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 472);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Padding = new Padding(12, 10, 12, 10);
            pnlBottom.Size = new Size(900, 84);
            pnlBottom.TabIndex = 1;
            // 
            // tlpBottom
            // 
            tlpBottom.ColumnCount = 3;
            tlpBottom.ColumnStyles.Add(new ColumnStyle());
            tlpBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpBottom.ColumnStyles.Add(new ColumnStyle());
            tlpBottom.Controls.Add(lblInfo, 0, 0);
            tlpBottom.Controls.Add(flpNav, 1, 0);
            tlpBottom.Controls.Add(flpAcciones, 2, 0);
            tlpBottom.Dock = DockStyle.Fill;
            tlpBottom.Location = new Point(12, 10);
            tlpBottom.Name = "tlpBottom";
            tlpBottom.RowCount = 1;
            tlpBottom.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBottom.Size = new Size(876, 64);
            tlpBottom.TabIndex = 0;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(0, 6);
            lblInfo.Margin = new Padding(0, 6, 0, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(220, 25);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Mostrando 0-0 de 0 ítems";
            // 
            // flpNav
            // 
            flpNav.Anchor = AnchorStyles.None;
            flpNav.AutoSize = true;
            flpNav.Controls.Add(btnAnterior);
            flpNav.Controls.Add(btnSiguiente);
            flpNav.Location = new Point(334, 12);
            flpNav.Margin = new Padding(0);
            flpNav.Name = "flpNav";
            flpNav.Size = new Size(208, 40);
            flpNav.TabIndex = 1;
            flpNav.WrapContents = false;
            // 
            // btnAnterior
            // 
            btnAnterior.Location = new Point(0, 0);
            btnAnterior.Margin = new Padding(0, 0, 8, 0);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(98, 40);
            btnAnterior.TabIndex = 0;
            btnAnterior.Text = "Anterior";
            // 
            // btnSiguiente
            // 
            btnSiguiente.Location = new Point(106, 0);
            btnSiguiente.Margin = new Padding(0);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(102, 40);
            btnSiguiente.TabIndex = 1;
            btnSiguiente.Text = "Siguiente";
            // 
            // flpAcciones
            // 
            flpAcciones.Anchor = AnchorStyles.Right;
            flpAcciones.AutoSize = true;
            flpAcciones.Controls.Add(btnElegir);
            flpAcciones.Controls.Add(btnCancelar);
            flpAcciones.Location = new Point(656, 12);
            flpAcciones.Margin = new Padding(0);
            flpAcciones.Name = "flpAcciones";
            flpAcciones.Size = new Size(220, 40);
            flpAcciones.TabIndex = 2;
            flpAcciones.WrapContents = false;
            // 
            // btnElegir
            // 
            btnElegir.Location = new Point(0, 0);
            btnElegir.Margin = new Padding(0, 0, 8, 0);
            btnElegir.Name = "btnElegir";
            btnElegir.Size = new Size(104, 40);
            btnElegir.TabIndex = 0;
            btnElegir.Text = "Elegir";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(112, 0);
            btnCancelar.Margin = new Padding(0);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(108, 40);
            btnCancelar.TabIndex = 1;
            btnCancelar.Text = "Cancelar";
            // 
            // timerBuscar
            // 
            timerBuscar.Interval = 350;
            // 
            // UcPagos_Contratos
            // 
            BackColor = Color.White;
            Controls.Add(dgv);
            Controls.Add(pnlBottom);
            Controls.Add(pnlFiltros);
            Controls.Add(pnlTop);
            MinimumSize = new Size(760, 440);
            Name = "UcPagos_Contratos";
            Size = new Size(900, 556);
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            pnlFiltros.ResumeLayout(false);
            tlpFiltros.ResumeLayout(false);
            tlpFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            pnlBottom.ResumeLayout(false);
            tlpBottom.ResumeLayout(false);
            tlpBottom.PerformLayout();
            flpNav.ResumeLayout(false);
            flpAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion
    }
}
