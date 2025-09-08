using System.Windows.Forms;

namespace InmoTech
{
    partial class UcPagos
    {
        private System.ComponentModel.IContainer components = null;

        private GroupBox grpEdicion;
        private Label lblContrato;
        private TextBox txtContrato;
        private Label lblInquilino;
        private TextBox txtInquilino;
        private Label lblFecha;
        private DateTimePicker dtpFecha;
        private Label lblImporte;
        private NumericUpDown nudImporte;
        private Label lblMetodo;
        private ComboBox cmbMetodo;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private Label lblObs;
        private TextBox txtObs;
        private Button btnGuardar;
        private Button btnCancelar;

        private GroupBox grpFiltro;
        private TextBox txtBuscar;
        private ComboBox cmbFiltroEstado;
        private Button btnBuscar;
        private Label lblFiltroEstado;

        private GroupBox grpLista;
        private DataGridView dgvPagos;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpEdicion = new GroupBox();
            pictureBox1 = new PictureBox();
            lblContrato = new Label();
            txtContrato = new TextBox();
            lblInquilino = new Label();
            txtInquilino = new TextBox();
            lblFecha = new Label();
            dtpFecha = new DateTimePicker();
            lblImporte = new Label();
            nudImporte = new NumericUpDown();
            lblMetodo = new Label();
            cmbMetodo = new ComboBox();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            lblObs = new Label();
            txtObs = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            grpFiltro = new GroupBox();
            txtBuscar = new TextBox();
            lblFiltroEstado = new Label();
            cmbFiltroEstado = new ComboBox();
            btnBuscar = new Button();
            grpLista = new GroupBox();
            dgvPagos = new DataGridView();
            grpEdicion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudImporte).BeginInit();
            grpFiltro.SuspendLayout();
            grpLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPagos).BeginInit();
            SuspendLayout();
            // 
            // grpEdicion
            // 
            grpEdicion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpEdicion.Controls.Add(pictureBox1);
            grpEdicion.Controls.Add(lblContrato);
            grpEdicion.Controls.Add(txtContrato);
            grpEdicion.Controls.Add(lblInquilino);
            grpEdicion.Controls.Add(txtInquilino);
            grpEdicion.Controls.Add(lblFecha);
            grpEdicion.Controls.Add(dtpFecha);
            grpEdicion.Controls.Add(lblImporte);
            grpEdicion.Controls.Add(nudImporte);
            grpEdicion.Controls.Add(lblMetodo);
            grpEdicion.Controls.Add(cmbMetodo);
            grpEdicion.Controls.Add(lblEstado);
            grpEdicion.Controls.Add(cmbEstado);
            grpEdicion.Controls.Add(lblObs);
            grpEdicion.Controls.Add(txtObs);
            grpEdicion.Controls.Add(btnGuardar);
            grpEdicion.Controls.Add(btnCancelar);
            grpEdicion.Location = new Point(16, 12);
            grpEdicion.Name = "grpEdicion";
            grpEdicion.Size = new Size(980, 170);
            grpEdicion.TabIndex = 2;
            grpEdicion.TabStop = false;
            grpEdicion.Text = "Registrar / Editar Pago";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.tarjetaIcon;
            pictureBox1.Location = new Point(833, 17);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(125, 101);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 16;
            pictureBox1.TabStop = false;
            // 
            // lblContrato
            // 
            lblContrato.AutoSize = true;
            lblContrato.Location = new Point(3, 29);
            lblContrato.Name = "lblContrato";
            lblContrato.Size = new Size(91, 20);
            lblContrato.TabIndex = 0;
            lblContrato.Text = "N° Contrato:";
            // 
            // txtContrato
            // 
            txtContrato.Location = new Point(113, 26);
            txtContrato.Name = "txtContrato";
            txtContrato.Size = new Size(150, 27);
            txtContrato.TabIndex = 1;
            // 
            // lblInquilino
            // 
            lblInquilino.AutoSize = true;
            lblInquilino.Location = new Point(264, 33);
            lblInquilino.Name = "lblInquilino";
            lblInquilino.Size = new Size(70, 20);
            lblInquilino.TabIndex = 2;
            lblInquilino.Text = "Inquilino:";
            // 
            // txtInquilino
            // 
            txtInquilino.Location = new Point(340, 26);
            txtInquilino.Name = "txtInquilino";
            txtInquilino.Size = new Size(220, 27);
            txtInquilino.TabIndex = 3;
            // 
            // lblFecha
            // 
            lblFecha.AutoSize = true;
            lblFecha.Location = new Point(566, 29);
            lblFecha.Name = "lblFecha";
            lblFecha.Size = new Size(50, 20);
            lblFecha.TabIndex = 4;
            lblFecha.Text = "Fecha:";
            // 
            // dtpFecha
            // 
            dtpFecha.Location = new Point(630, 26);
            dtpFecha.Name = "dtpFecha";
            dtpFecha.Size = new Size(160, 27);
            dtpFecha.TabIndex = 5;
            // 
            // lblImporte
            // 
            lblImporte.AutoSize = true;
            lblImporte.Location = new Point(3, 70);
            lblImporte.Name = "lblImporte";
            lblImporte.Size = new Size(65, 20);
            lblImporte.TabIndex = 6;
            lblImporte.Text = "Importe:";
            // 
            // nudImporte
            // 
            nudImporte.Location = new Point(88, 65);
            nudImporte.Name = "nudImporte";
            nudImporte.Size = new Size(150, 27);
            nudImporte.TabIndex = 7;
            // 
            // lblMetodo
            // 
            lblMetodo.AutoSize = true;
            lblMetodo.Location = new Point(264, 70);
            lblMetodo.Name = "lblMetodo";
            lblMetodo.Size = new Size(65, 20);
            lblMetodo.TabIndex = 8;
            lblMetodo.Text = "Método:";
            // 
            // cmbMetodo
            // 
            cmbMetodo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbMetodo.Location = new Point(351, 62);
            cmbMetodo.Name = "cmbMetodo";
            cmbMetodo.Size = new Size(209, 28);
            cmbMetodo.TabIndex = 9;
            cmbMetodo.SelectedIndexChanged += cmbMetodo_SelectedIndexChanged;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(566, 70);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(57, 20);
            lblEstado.TabIndex = 10;
            lblEstado.Text = "Estado:";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Location = new Point(639, 65);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(151, 28);
            cmbEstado.TabIndex = 11;
            // 
            // lblObs
            // 
            lblObs.AutoSize = true;
            lblObs.Location = new Point(3, 124);
            lblObs.Name = "lblObs";
            lblObs.Size = new Size(108, 20);
            lblObs.TabIndex = 12;
            lblObs.Text = "Observaciones:";
            // 
            // txtObs
            // 
            txtObs.Location = new Point(141, 114);
            txtObs.Multiline = true;
            txtObs.Name = "txtObs";
            txtObs.Size = new Size(440, 50);
            txtObs.TabIndex = 13;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.Location = new Point(639, 124);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(127, 40);
            btnGuardar.TabIndex = 14;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(798, 124);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(130, 40);
            btnCancelar.TabIndex = 15;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // grpFiltro
            // 
            grpFiltro.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpFiltro.Controls.Add(txtBuscar);
            grpFiltro.Controls.Add(lblFiltroEstado);
            grpFiltro.Controls.Add(cmbFiltroEstado);
            grpFiltro.Controls.Add(btnBuscar);
            grpFiltro.Location = new Point(16, 188);
            grpFiltro.Name = "grpFiltro";
            grpFiltro.Size = new Size(980, 64);
            grpFiltro.TabIndex = 1;
            grpFiltro.TabStop = false;
            grpFiltro.Text = "Búsqueda / Filtros";
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(16, 26);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por contrato, inquilino u observaciones…";
            txtBuscar.Size = new Size(420, 27);
            txtBuscar.TabIndex = 0;
            // 
            // lblFiltroEstado
            // 
            lblFiltroEstado.AutoSize = true;
            lblFiltroEstado.Location = new Point(472, 29);
            lblFiltroEstado.Name = "lblFiltroEstado";
            lblFiltroEstado.Size = new Size(57, 20);
            lblFiltroEstado.TabIndex = 1;
            lblFiltroEstado.Text = "Estado:";
            // 
            // cmbFiltroEstado
            // 
            cmbFiltroEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFiltroEstado.Location = new Point(544, 25);
            cmbFiltroEstado.Name = "cmbFiltroEstado";
            cmbFiltroEstado.Size = new Size(140, 28);
            cmbFiltroEstado.TabIndex = 2;
            // 
            // btnBuscar
            // 
            btnBuscar.BackColor = Color.White;
            btnBuscar.Location = new Point(801, 21);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(127, 32);
            btnBuscar.TabIndex = 3;
            btnBuscar.Text = "Aplicar";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // grpLista
            // 
            grpLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpLista.Controls.Add(dgvPagos);
            grpLista.Location = new Point(16, 258);
            grpLista.Name = "grpLista";
            grpLista.Size = new Size(980, 332);
            grpLista.TabIndex = 0;
            grpLista.TabStop = false;
            grpLista.Text = "Lista de Pagos";
            // 
            // dgvPagos
            // 
            dgvPagos.AllowUserToAddRows = false;
            dgvPagos.AllowUserToDeleteRows = false;
            dgvPagos.BackgroundColor = SystemColors.Control;
            dgvPagos.ColumnHeadersHeight = 29;
            dgvPagos.Dock = DockStyle.Fill;
            dgvPagos.Location = new Point(3, 23);
            dgvPagos.MultiSelect = false;
            dgvPagos.Name = "dgvPagos";
            dgvPagos.RowHeadersVisible = false;
            dgvPagos.RowHeadersWidth = 51;
            dgvPagos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPagos.Size = new Size(974, 306);
            dgvPagos.TabIndex = 0;
            // 
            // UcPagos
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Teal;
            Controls.Add(grpLista);
            Controls.Add(grpFiltro);
            Controls.Add(grpEdicion);
            Name = "UcPagos";
            Padding = new Padding(8);
            Size = new Size(1012, 606);
            grpEdicion.ResumeLayout(false);
            grpEdicion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudImporte).EndInit();
            grpFiltro.ResumeLayout(false);
            grpFiltro.PerformLayout();
            grpLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPagos).EndInit();
            ResumeLayout(false);
        }
        private PictureBox pictureBox1;
    }
}
