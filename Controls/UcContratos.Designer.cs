using System.Windows.Forms;

namespace InmoTech
{
    partial class UcContratos
    {
        private System.ComponentModel.IContainer components = null;
        private GroupBox grpEdicion;
        private Label lblNumero;
        private TextBox txtNumero;
        private Label lblInquilino;
        private TextBox txtInquilino;
        private Label lblInmueble;
        private TextBox txtInmueble;
        private Label lblInicio;
        private DateTimePicker dtpInicio;
        private Label lblFin;
        private DateTimePicker dtpFin;
        private Label lblMonto;
        private NumericUpDown nudMonto;
        private Label lblEstado;
        private ComboBox cmbEstado;
        private CheckBox chkActivo;
        private Button btnGuardar;
        private Button btnCancelar;
        private TextBox txtBuscar;
        private Button btnBuscar;
        private DataGridView dgvContratos;
        private GroupBox grpLista;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            grpEdicion = new GroupBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblInquilino = new Label();
            txtInquilino = new TextBox();
            lblInmueble = new Label();
            txtInmueble = new TextBox();
            lblInicio = new Label();
            dtpInicio = new DateTimePicker();
            lblFin = new Label();
            dtpFin = new DateTimePicker();
            lblMonto = new Label();
            nudMonto = new NumericUpDown();
            lblEstado = new Label();
            cmbEstado = new ComboBox();
            chkActivo = new CheckBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            btnBuscar = new Button();
            txtBuscar = new TextBox();
            grpLista = new GroupBox();
            dgvContratos = new DataGridView();
            grpEdicion.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).BeginInit();
            grpLista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            SuspendLayout();
            // 
            // grpEdicion
            // 
            grpEdicion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpEdicion.Controls.Add(lblNumero);
            grpEdicion.Controls.Add(txtNumero);
            grpEdicion.Controls.Add(lblInquilino);
            grpEdicion.Controls.Add(txtInquilino);
            grpEdicion.Controls.Add(lblInmueble);
            grpEdicion.Controls.Add(txtInmueble);
            grpEdicion.Controls.Add(lblInicio);
            grpEdicion.Controls.Add(dtpInicio);
            grpEdicion.Controls.Add(lblFin);
            grpEdicion.Controls.Add(dtpFin);
            grpEdicion.Controls.Add(lblMonto);
            grpEdicion.Controls.Add(nudMonto);
            grpEdicion.Controls.Add(lblEstado);
            grpEdicion.Controls.Add(cmbEstado);
            grpEdicion.Controls.Add(chkActivo);
            grpEdicion.Controls.Add(btnGuardar);
            grpEdicion.Controls.Add(btnCancelar);
            grpEdicion.Controls.Add(btnBuscar);
            grpEdicion.Controls.Add(txtBuscar);
            grpEdicion.Location = new Point(16, 12);
            grpEdicion.Name = "grpEdicion";
            grpEdicion.Size = new Size(958, 150);
            grpEdicion.TabIndex = 1;
            grpEdicion.TabStop = false;
            grpEdicion.Text = "Crear / Editar Contrato";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(16, 30);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(88, 20);
            lblNumero.TabIndex = 0;
            lblNumero.Text = "N° Contrato";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(110, 26);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(160, 27);
            txtNumero.TabIndex = 1;
            // 
            // lblInquilino
            // 
            lblInquilino.AutoSize = true;
            lblInquilino.Location = new Point(277, 33);
            lblInquilino.Name = "lblInquilino";
            lblInquilino.Size = new Size(67, 20);
            lblInquilino.TabIndex = 2;
            lblInquilino.Text = "Inquilino";
            // 
            // txtInquilino
            // 
            txtInquilino.Location = new Point(350, 26);
            txtInquilino.Name = "txtInquilino";
            txtInquilino.Size = new Size(220, 27);
            txtInquilino.TabIndex = 3;
            // 
            // lblInmueble
            // 
            lblInmueble.AutoSize = true;
            lblInmueble.Location = new Point(576, 29);
            lblInmueble.Name = "lblInmueble";
            lblInmueble.Size = new Size(71, 20);
            lblInmueble.TabIndex = 4;
            lblInmueble.Text = "Inmueble";
            // 
            // txtInmueble
            // 
            txtInmueble.Location = new Point(660, 26);
            txtInmueble.Name = "txtInmueble";
            txtInmueble.Size = new Size(220, 27);
            txtInmueble.TabIndex = 5;
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(16, 70);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(45, 20);
            lblInicio.TabIndex = 6;
            lblInicio.Text = "Inicio";
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(110, 66);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(160, 27);
            dtpInicio.TabIndex = 7;
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(290, 70);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(28, 20);
            lblFin.TabIndex = 8;
            lblFin.Text = "Fin";
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(350, 66);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(160, 27);
            dtpFin.TabIndex = 9;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(530, 70);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(53, 20);
            lblMonto.TabIndex = 10;
            lblMonto.Text = "Monto";
            // 
            // nudMonto
            // 
            nudMonto.Location = new Point(590, 66);
            nudMonto.Name = "nudMonto";
            nudMonto.Size = new Size(120, 27);
            nudMonto.TabIndex = 11;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(730, 70);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(54, 20);
            lblEstado.TabIndex = 12;
            lblEstado.Text = "Estado";
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Location = new Point(785, 66);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(120, 28);
            cmbEstado.TabIndex = 13;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(16, 108);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(73, 24);
            chkActivo.TabIndex = 14;
            chkActivo.Text = "Activo";
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.Location = new Point(717, 106);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(131, 32);
            btnGuardar.TabIndex = 15;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(539, 107);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(150, 30);
            btnCancelar.TabIndex = 16;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // btnBuscar
            // 
            btnBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnBuscar.BackColor = Color.White;
            btnBuscar.Location = new Point(110, 105);
            btnBuscar.Name = "btnBuscar";
            btnBuscar.Size = new Size(117, 37);
            btnBuscar.TabIndex = 17;
            btnBuscar.Text = "Buscar";
            btnBuscar.UseVisualStyleBackColor = false;
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtBuscar.Location = new Point(245, 110);
            txtBuscar.Multiline = true;
            txtBuscar.Name = "txtBuscar";
            txtBuscar.PlaceholderText = "Buscar por nº, inquilino o inmueble…";
            txtBuscar.Size = new Size(265, 27);
            txtBuscar.TabIndex = 18;
            // 
            // grpLista
            // 
            grpLista.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpLista.Controls.Add(dgvContratos);
            grpLista.Location = new Point(16, 170);
            grpLista.Name = "grpLista";
            grpLista.Size = new Size(958, 478);
            grpLista.TabIndex = 0;
            grpLista.TabStop = false;
            grpLista.Text = "Lista de Contratos";
            // 
            // dgvContratos
            // 
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.BackgroundColor = SystemColors.Control;
            dgvContratos.ColumnHeadersHeight = 29;
            dgvContratos.Dock = DockStyle.Fill;
            dgvContratos.Location = new Point(3, 23);
            dgvContratos.MultiSelect = false;
            dgvContratos.Name = "dgvContratos";
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.RowHeadersWidth = 51;
            dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContratos.Size = new Size(952, 452);
            dgvContratos.TabIndex = 0;
            // 
            // UcContratos
            // 
            AutoScaleDimensions = new SizeF(120F, 120F);
            AutoScaleMode = AutoScaleMode.Dpi;
            BackColor = Color.Teal;
            Controls.Add(grpLista);
            Controls.Add(grpEdicion);
            Name = "UcContratos";
            Padding = new Padding(8);
            Size = new Size(990, 664);
            grpEdicion.ResumeLayout(false);
            grpEdicion.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).EndInit();
            grpLista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            ResumeLayout(false);
        }
    }
}
