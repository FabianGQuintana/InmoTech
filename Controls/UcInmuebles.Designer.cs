using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcInmuebles
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlRoot;
        private Panel pnlHeader;

        private GroupBox gbCrear;
        private Label lblDireccion;
        private Label lblTipo;
        private Label lblDescripcion;
        private Label lblEstado;
        private Label lblAmbientes;
        private Label lblAmueblado;
        private Label lblImagen;

        private TextBox txtDireccion;
        private ComboBox cboTipo;
        private TextBox txtDescripcion;
        private ComboBox cboEstado;
        private NumericUpDown nudAmbientes;
        private CheckBox chkAmueblado;

        private PictureBox pbFoto;
        private Button btnCargarImagen;
        private Button btnQuitarImagen;

        private Button btnGuardar;
        private Button btnCancelar;

        private Label lblLista;
        private DataGridView dgvInmuebles;

        // >>> MODIFICADO <<<: Cambian los tipos de las columnas
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colDir;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colAmb;
        private DataGridViewTextBoxColumn colAmueb; // Antes era CheckBoxColumn
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colActivo; // Antes era CheckBoxColumn
        private DataGridViewImageColumn colImg;
        // Se eliminan las de botones

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            label1 = new Label();
            gbCrear = new GroupBox();
            BEstado = new Button();
            lblDireccion = new Label();
            txtDireccion = new TextBox();
            lblTipo = new Label();
            cboTipo = new ComboBox();
            lblAmbientes = new Label();
            nudAmbientes = new NumericUpDown();
            lblDescripcion = new Label();
            txtDescripcion = new TextBox();
            lblEstado = new Label();
            cboEstado = new ComboBox();
            lblAmueblado = new Label();
            chkAmueblado = new CheckBox();
            lblImagen = new Label();
            pbFoto = new PictureBox();
            btnCargarImagen = new Button();
            btnQuitarImagen = new Button();
            btnGuardar = new Button();
            btnCancelar = new Button();
            lblLista = new Label();
            dgvInmuebles = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colDir = new DataGridViewTextBoxColumn();
            colTipo = new DataGridViewTextBoxColumn();
            colAmb = new DataGridViewTextBoxColumn();
            colAmueb = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colActivo = new DataGridViewTextBoxColumn();
            colImg = new DataGridViewImageColumn();
            tlRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            gbCrear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudAmbientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pbFoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvInmuebles).BeginInit();
            SuspendLayout();
            // 
            // tlRoot
            // 
            tlRoot.BackColor = Color.WhiteSmoke;
            tlRoot.ColumnCount = 1;
            tlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlRoot.Controls.Add(pnlHeader, 0, 0);
            tlRoot.Controls.Add(gbCrear, 0, 1);
            tlRoot.Controls.Add(lblLista, 0, 2);
            tlRoot.Controls.Add(dgvInmuebles, 0, 3);
            tlRoot.Dock = DockStyle.Fill;
            tlRoot.Location = new Point(0, 0);
            tlRoot.Name = "tlRoot";
            tlRoot.Padding = new Padding(12);
            tlRoot.RowCount = 4;
            tlRoot.RowStyles.Add(new RowStyle());
            tlRoot.RowStyles.Add(new RowStyle());
            tlRoot.RowStyles.Add(new RowStyle());
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlRoot.Size = new Size(1200, 700);
            tlRoot.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.Teal;
            pnlHeader.Controls.Add(label1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(15, 15);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(8, 0, 8, 6);
            pnlHeader.Size = new Size(1170, 69);
            pnlHeader.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(0, 17);
            label1.Name = "label1";
            label1.Size = new Size(172, 37);
            label1.TabIndex = 0;
            label1.Text = "Inmuebles";
            // 
            // gbCrear
            // 
            gbCrear.BackColor = Color.Teal;
            gbCrear.Controls.Add(BEstado);
            gbCrear.Controls.Add(lblDireccion);
            gbCrear.Controls.Add(txtDireccion);
            gbCrear.Controls.Add(lblTipo);
            gbCrear.Controls.Add(cboTipo);
            gbCrear.Controls.Add(lblAmbientes);
            gbCrear.Controls.Add(nudAmbientes);
            gbCrear.Controls.Add(lblDescripcion);
            gbCrear.Controls.Add(txtDescripcion);
            gbCrear.Controls.Add(lblEstado);
            gbCrear.Controls.Add(cboEstado);
            gbCrear.Controls.Add(lblAmueblado);
            gbCrear.Controls.Add(chkAmueblado);
            gbCrear.Controls.Add(lblImagen);
            gbCrear.Controls.Add(pbFoto);
            gbCrear.Controls.Add(btnCargarImagen);
            gbCrear.Controls.Add(btnQuitarImagen);
            gbCrear.Controls.Add(btnGuardar);
            gbCrear.Controls.Add(btnCancelar);
            gbCrear.Dock = DockStyle.Top;
            gbCrear.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            gbCrear.Location = new Point(12, 95);
            gbCrear.Margin = new Padding(0, 8, 0, 8);
            gbCrear.Name = "gbCrear";
            gbCrear.Padding = new Padding(12);
            gbCrear.Size = new Size(1176, 299);
            gbCrear.TabIndex = 1;
            gbCrear.TabStop = false;
            gbCrear.Text = "Crear / Editar Inmueble";
            gbCrear.Enter += gbCrear_Enter;
            // 
            // BEstado
            // 
            BEstado.BackColor = Color.White;
            BEstado.Location = new Point(942, 230);
            BEstado.Name = "BEstado";
            BEstado.Size = new Size(120, 32);
            BEstado.TabIndex = 18;
            BEstado.Text = "Baja";
            BEstado.UseVisualStyleBackColor = false;
            // 
            // lblDireccion
            // 
            lblDireccion.AutoSize = true;
            lblDireccion.Location = new Point(16, 28);
            lblDireccion.Name = "lblDireccion";
            lblDireccion.Size = new Size(85, 22);
            lblDireccion.TabIndex = 0;
            lblDireccion.Text = "Dirección";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(16, 56);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(280, 28);
            txtDireccion.TabIndex = 1;
            // 
            // lblTipo
            // 
            lblTipo.AutoSize = true;
            lblTipo.Location = new Point(330, 28);
            lblTipo.Name = "lblTipo";
            lblTipo.Size = new Size(46, 22);
            lblTipo.TabIndex = 2;
            lblTipo.Text = "Tipo";
            // 
            // cboTipo
            // 
            cboTipo.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTipo.Location = new Point(330, 56);
            cboTipo.Name = "cboTipo";
            cboTipo.Size = new Size(280, 30);
            cboTipo.TabIndex = 3;
            // 
            // lblAmbientes
            // 
            lblAmbientes.AutoSize = true;
            lblAmbientes.Location = new Point(330, 108);
            lblAmbientes.Name = "lblAmbientes";
            lblAmbientes.Size = new Size(131, 22);
            lblAmbientes.TabIndex = 4;
            lblAmbientes.Text = "Nro. ambientes";
            // 
            // nudAmbientes
            // 
            nudAmbientes.Location = new Point(330, 138);
            nudAmbientes.Maximum = new decimal(new int[] { 50, 0, 0, 0 });
            nudAmbientes.Name = "nudAmbientes";
            nudAmbientes.Size = new Size(120, 28);
            nudAmbientes.TabIndex = 5;
            // 
            // lblDescripcion
            // 
            lblDescripcion.AutoSize = true;
            lblDescripcion.Location = new Point(15, 180);
            lblDescripcion.Name = "lblDescripcion";
            lblDescripcion.Size = new Size(104, 22);
            lblDescripcion.TabIndex = 6;
            lblDescripcion.Text = "Descripción";
            // 
            // txtDescripcion
            // 
            txtDescripcion.Location = new Point(15, 216);
            txtDescripcion.MaxLength = 300;
            txtDescripcion.Multiline = true;
            txtDescripcion.Name = "txtDescripcion";
            txtDescripcion.ScrollBars = ScrollBars.Vertical;
            txtDescripcion.Size = new Size(595, 60);
            txtDescripcion.TabIndex = 7;
            txtDescripcion.TextChanged += txtDescripcion_TextChanged;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(15, 108);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(109, 22);
            lblEstado.TabIndex = 8;
            lblEstado.Text = "Condiciones";
            // 
            // cboEstado
            // 
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.Location = new Point(16, 136);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(280, 30);
            cboEstado.TabIndex = 9;
            // 
            // lblAmueblado
            // 
            lblAmueblado.AutoSize = true;
            lblAmueblado.Location = new Point(510, 108);
            lblAmueblado.Name = "lblAmueblado";
            lblAmueblado.Size = new Size(100, 22);
            lblAmueblado.TabIndex = 10;
            lblAmueblado.Text = "Amueblado";
            // 
            // chkAmueblado
            // 
            chkAmueblado.AutoSize = true;
            chkAmueblado.Location = new Point(546, 141);
            chkAmueblado.Name = "chkAmueblado";
            chkAmueblado.Size = new Size(22, 21);
            chkAmueblado.TabIndex = 11;
            // 
            // lblImagen
            // 
            lblImagen.AutoSize = true;
            lblImagen.Location = new Point(708, 28);
            lblImagen.Name = "lblImagen";
            lblImagen.Size = new Size(68, 22);
            lblImagen.TabIndex = 12;
            lblImagen.Text = "Imagen";
            // 
            // pbFoto
            // 
            pbFoto.BackColor = Color.Honeydew;
            pbFoto.BorderStyle = BorderStyle.FixedSingle;
            pbFoto.Location = new Point(655, 56);
            pbFoto.Name = "pbFoto";
            pbFoto.Size = new Size(180, 120);
            pbFoto.SizeMode = PictureBoxSizeMode.Zoom;
            pbFoto.TabIndex = 13;
            pbFoto.TabStop = false;
            // 
            // btnCargarImagen
            // 
            btnCargarImagen.BackColor = Color.White;
            btnCargarImagen.Location = new Point(655, 192);
            btnCargarImagen.Name = "btnCargarImagen";
            btnCargarImagen.Size = new Size(180, 30);
            btnCargarImagen.TabIndex = 14;
            btnCargarImagen.Text = "Cargar imagen…";
            btnCargarImagen.UseVisualStyleBackColor = false;
            // 
            // btnQuitarImagen
            // 
            btnQuitarImagen.BackColor = Color.White;
            btnQuitarImagen.Location = new Point(655, 228);
            btnQuitarImagen.Name = "btnQuitarImagen";
            btnQuitarImagen.Size = new Size(180, 30);
            btnQuitarImagen.TabIndex = 15;
            btnQuitarImagen.Text = "Quitar";
            btnQuitarImagen.UseVisualStyleBackColor = false;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.ForeColor = SystemColors.ControlText;
            btnGuardar.Location = new Point(942, 155);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(120, 32);
            btnGuardar.TabIndex = 16;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(942, 192);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(120, 32);
            btnCancelar.TabIndex = 17;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // lblLista
            // 
            lblLista.AutoSize = true;
            lblLista.Font = new Font("Microsoft Sans Serif", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblLista.Location = new Point(12, 410);
            lblLista.Margin = new Padding(0, 8, 0, 6);
            lblLista.Name = "lblLista";
            lblLista.Size = new Size(196, 29);
            lblLista.TabIndex = 2;
            lblLista.Text = "Lista Inmuebles";
            // 
            // dgvInmuebles
            // 
            dgvInmuebles.AllowUserToAddRows = false;
            dgvInmuebles.AllowUserToDeleteRows = false;
            dgvInmuebles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInmuebles.BackgroundColor = Color.Teal;
            dgvInmuebles.ColumnHeadersHeight = 34;
            dgvInmuebles.Columns.AddRange(new DataGridViewColumn[] { colId, colDir, colTipo, colAmb, colAmueb, colEstado, colActivo, colImg });
            dgvInmuebles.Dock = DockStyle.Fill;
            dgvInmuebles.GridColor = Color.White;
            dgvInmuebles.Location = new Point(15, 448);
            dgvInmuebles.MultiSelect = false;
            dgvInmuebles.Name = "dgvInmuebles";
            dgvInmuebles.ReadOnly = true;
            dgvInmuebles.RowHeadersVisible = false;
            dgvInmuebles.RowHeadersWidth = 62;
            dgvInmuebles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInmuebles.Size = new Size(1170, 237);
            dgvInmuebles.TabIndex = 3;
            // 
            // colId
            // 
            colId.DataPropertyName = "IdInmueble";
            colId.HeaderText = "ID";
            colId.MinimumWidth = 8;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colDir
            // 
            colDir.DataPropertyName = "Direccion";
            colDir.HeaderText = "Dirección";
            colDir.MinimumWidth = 8;
            colDir.Name = "colDir";
            colDir.ReadOnly = true;
            // 
            // colTipo
            // 
            colTipo.DataPropertyName = "Tipo";
            colTipo.FillWeight = 90F;
            colTipo.HeaderText = "Tipo";
            colTipo.MinimumWidth = 8;
            colTipo.Name = "colTipo";
            colTipo.ReadOnly = true;
            // 
            // colAmb
            // 
            colAmb.DataPropertyName = "NroAmbientes";
            colAmb.FillWeight = 70F;
            colAmb.HeaderText = "Ambientes";
            colAmb.MinimumWidth = 8;
            colAmb.Name = "colAmb";
            colAmb.ReadOnly = true;
            // 
            // colAmueb
            // 
            colAmueb.DataPropertyName = "Amueblado";
            colAmueb.FillWeight = 70F;
            colAmueb.HeaderText = "Amueblado";
            colAmueb.MinimumWidth = 8;
            colAmueb.Name = "colAmueb";
            colAmueb.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.DataPropertyName = "Condiciones";
            colEstado.FillWeight = 90F;
            colEstado.HeaderText = "Condiciones";
            colEstado.MinimumWidth = 8;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colActivo
            // 
            colActivo.DataPropertyName = "Estado";
            colActivo.FillWeight = 55F;
            colActivo.HeaderText = "Activo";
            colActivo.MinimumWidth = 8;
            colActivo.Name = "colActivo";
            colActivo.ReadOnly = true;
            // 
            // colImg
            // 
            colImg.DataPropertyName = "Imagen";
            colImg.FillWeight = 80F;
            colImg.HeaderText = "Imagen";
            colImg.ImageLayout = DataGridViewImageCellLayout.Zoom;
            colImg.MinimumWidth = 8;
            colImg.Name = "colImg";
            colImg.ReadOnly = true;
            // 
            // UcInmuebles
            // 
            Controls.Add(tlRoot);
            Name = "UcInmuebles";
            Size = new Size(1200, 700);
            tlRoot.ResumeLayout(false);
            tlRoot.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            gbCrear.ResumeLayout(false);
            gbCrear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudAmbientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)pbFoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvInmuebles).EndInit();
            ResumeLayout(false);
        }
        private Label label1;
        private Button BEstado;
    }
}