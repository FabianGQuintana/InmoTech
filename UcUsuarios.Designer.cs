using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcUsuarios
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlRoot;
        private Panel pnlHeader;
        private Label lblTitulo;

        private GroupBox gbCrear;
        private Label lblNombre;
        private Label lblApellido;
        private Label lblDni;
        private Label lblTelefono;
        private Label lblEmail;
        private Label lblPass;
        private Label lblPass2;
        private Label lblRol;

        internal TextBox txtNombre;
        internal TextBox txtApellido;
        internal TextBox txtDni;
        internal TextBox txtTelefono;
        internal TextBox txtEmail;
        internal TextBox txtPass;
        internal TextBox txtPass2;

        internal RadioButton rbOperador;
        internal RadioButton rbAdministrador;
        internal RadioButton rbPropietario;

        internal Button btnGuardar;
        internal Button btnCancelar;

        private Panel pnlListaHeader;
        private Label lblListaTitulo;
        internal DataGridView dgvUsuarios;

        private Label label1;
        private DateTimePicker dateTimePicker1;
        private PictureBox pictureBox1;

        // >>> NUEVO <<<
        private ErrorProvider ep;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            tlRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            gbCrear = new GroupBox();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            dateTimePicker1 = new DateTimePicker();
            lblNombre = new Label();
            txtNombre = new TextBox();
            lblApellido = new Label();
            txtApellido = new TextBox();
            lblDni = new Label();
            txtDni = new TextBox();
            lblTelefono = new Label();
            txtTelefono = new TextBox();
            lblEmail = new Label();
            txtEmail = new TextBox();
            lblPass = new Label();
            txtPass = new TextBox();
            lblRol = new Label();
            rbOperador = new RadioButton();
            rbAdministrador = new RadioButton();
            rbPropietario = new RadioButton();
            lblPass2 = new Label();
            txtPass2 = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            pnlListaHeader = new Panel();
            lblListaTitulo = new Label();
            dgvUsuarios = new DataGridView();
            ep = new ErrorProvider(components);
            tlRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            gbCrear.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            pnlListaHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)ep).BeginInit();
            SuspendLayout();
            // 
            // tlRoot
            // 
            tlRoot.BackColor = Color.WhiteSmoke;
            tlRoot.ColumnCount = 1;
            tlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlRoot.Controls.Add(pnlHeader, 0, 0);
            tlRoot.Controls.Add(gbCrear, 0, 1);
            tlRoot.Controls.Add(pnlListaHeader, 0, 2);
            tlRoot.Controls.Add(dgvUsuarios, 0, 3);
            tlRoot.Dock = DockStyle.Fill;
            tlRoot.Location = new Point(0, 0);
            tlRoot.Name = "tlRoot";
            tlRoot.RowCount = 4;
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 76F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 274F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 56F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlRoot.Size = new Size(980, 680);
            tlRoot.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(3, 3);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16);
            pnlHeader.Size = new Size(974, 70);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Montserrat SemiBold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(5, 10);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(163, 49);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Usuarios";
            // 
            // gbCrear
            // 
            gbCrear.BackColor = Color.White;
            gbCrear.Controls.Add(pictureBox1);
            gbCrear.Controls.Add(label1);
            gbCrear.Controls.Add(dateTimePicker1);
            gbCrear.Controls.Add(lblNombre);
            gbCrear.Controls.Add(txtNombre);
            gbCrear.Controls.Add(lblApellido);
            gbCrear.Controls.Add(txtApellido);
            gbCrear.Controls.Add(lblDni);
            gbCrear.Controls.Add(txtDni);
            gbCrear.Controls.Add(lblTelefono);
            gbCrear.Controls.Add(txtTelefono);
            gbCrear.Controls.Add(lblEmail);
            gbCrear.Controls.Add(txtEmail);
            gbCrear.Controls.Add(lblPass);
            gbCrear.Controls.Add(txtPass);
            gbCrear.Controls.Add(lblRol);
            gbCrear.Controls.Add(rbOperador);
            gbCrear.Controls.Add(rbAdministrador);
            gbCrear.Controls.Add(rbPropietario);
            gbCrear.Controls.Add(lblPass2);
            gbCrear.Controls.Add(txtPass2);
            gbCrear.Controls.Add(btnGuardar);
            gbCrear.Controls.Add(btnCancelar);
            gbCrear.Dock = DockStyle.Fill;
            gbCrear.Font = new Font("Montserrat", 9F);
            gbCrear.Location = new Point(3, 79);
            gbCrear.Name = "gbCrear";
            gbCrear.Padding = new Padding(12);
            gbCrear.Size = new Size(974, 268);
            gbCrear.TabIndex = 1;
            gbCrear.TabStop = false;
            gbCrear.Text = "Crear Usuario";
            gbCrear.Enter += gbCrear_Enter;
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.floppi;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(691, 140);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(72, 64);
            pictureBox1.TabIndex = 22;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(10, 176);
            label1.Name = "label1";
            label1.Size = new Size(178, 28);
            label1.TabIndex = 21;
            label1.Text = "Fecha Nacimiento";
            label1.Click += label1_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(10, 207);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(190, 29);
            dateTimePicker1.TabIndex = 20;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(11, 34);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(87, 28);
            lblNombre.TabIndex = 23;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(11, 65);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(190, 29);
            txtNombre.TabIndex = 24;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(230, 34);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(86, 28);
            lblApellido.TabIndex = 25;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(230, 65);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(190, 29);
            txtApellido.TabIndex = 26;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(230, 176);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(47, 28);
            lblDni.TabIndex = 27;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(230, 207);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(190, 29);
            txtDni.TabIndex = 28;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(10, 104);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(88, 28);
            lblTelefono.TabIndex = 29;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(10, 135);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(190, 29);
            txtTelefono.TabIndex = 30;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(230, 104);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(64, 28);
            lblEmail.TabIndex = 31;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(230, 135);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(190, 29);
            txtEmail.TabIndex = 32;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(456, 104);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(115, 28);
            lblPass.TabIndex = 33;
            lblPass.Text = "Contraseña";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(456, 135);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(190, 29);
            txtPass.TabIndex = 34;
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(456, 34);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(41, 28);
            lblRol.TabIndex = 35;
            lblRol.Text = "Rol";
            // 
            // rbOperador
            // 
            rbOperador.AutoSize = true;
            rbOperador.Checked = true;
            rbOperador.Location = new Point(449, 65);
            rbOperador.Name = "rbOperador";
            rbOperador.Size = new Size(122, 32);
            rbOperador.TabIndex = 36;
            rbOperador.TabStop = true;
            rbOperador.Text = "Operador";
            // 
            // rbAdministrador
            // 
            rbAdministrador.AutoSize = true;
            rbAdministrador.Location = new Point(577, 65);
            rbAdministrador.Name = "rbAdministrador";
            rbAdministrador.Size = new Size(166, 32);
            rbAdministrador.TabIndex = 37;
            rbAdministrador.Text = "Administrador";
            // 
            // rbPropietario
            // 
            rbPropietario.AutoSize = true;
            rbPropietario.Location = new Point(749, 66);
            rbPropietario.Name = "rbPropietario";
            rbPropietario.Size = new Size(136, 32);
            rbPropietario.TabIndex = 38;
            rbPropietario.Text = "Propietario";
            // 
            // lblPass2
            // 
            lblPass2.AutoSize = true;
            lblPass2.Location = new Point(449, 176);
            lblPass2.Name = "lblPass2";
            lblPass2.Size = new Size(186, 28);
            lblPass2.TabIndex = 39;
            lblPass2.Text = "Repetir Contraseña";
            // 
            // txtPass2
            // 
            txtPass2.Location = new Point(452, 206);
            txtPass2.Name = "txtPass2";
            txtPass2.Size = new Size(190, 29);
            txtPass2.TabIndex = 40;
            txtPass2.UseSystemPasswordChar = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(681, 205);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 41;
            btnGuardar.Text = "Guardar";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(802, 205);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 42;
            btnCancelar.Text = "Cancelar";
            // 
            // pnlListaHeader
            // 
            pnlListaHeader.BackColor = Color.WhiteSmoke;
            pnlListaHeader.Controls.Add(lblListaTitulo);
            pnlListaHeader.Dock = DockStyle.Fill;
            pnlListaHeader.Font = new Font("Montserrat SemiBold", 9F, FontStyle.Bold);
            pnlListaHeader.Location = new Point(3, 353);
            pnlListaHeader.Name = "pnlListaHeader";
            pnlListaHeader.Size = new Size(974, 50);
            pnlListaHeader.TabIndex = 2;
            // 
            // lblListaTitulo
            // 
            lblListaTitulo.AutoSize = true;
            lblListaTitulo.Font = new Font("Montserrat SemiBold", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblListaTitulo.Location = new Point(-3, 10);
            lblListaTitulo.Name = "lblListaTitulo";
            lblListaTitulo.Size = new Size(188, 38);
            lblListaTitulo.TabIndex = 0;
            lblListaTitulo.Text = "Lista Usuarios";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.ColumnHeadersHeight = 34;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(3, 409);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 62;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(974, 268);
            dgvUsuarios.TabIndex = 3;
            // 
            // ep
            // 
            ep.ContainerControl = this;
            // 
            // UcUsuarios
            // 
            Controls.Add(tlRoot);
            Name = "UcUsuarios";
            Size = new Size(980, 680);
            tlRoot.ResumeLayout(false);
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            gbCrear.ResumeLayout(false);
            gbCrear.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            pnlListaHeader.ResumeLayout(false);
            pnlListaHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)ep).EndInit();
            ResumeLayout(false);
        }
    }
}
