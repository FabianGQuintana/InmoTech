// Archivo: UcUsuarios.Designer.cs
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

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            gbCrear = new GroupBox();
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
            tlRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            gbCrear.SuspendLayout();
            pnlListaHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
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
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 70F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 210F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
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
            pnlHeader.Size = new Size(974, 64);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Verdana", 18F, FontStyle.Bold);
            lblTitulo.Location = new Point(16, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(195, 44);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Usuarios";
            // 
            // gbCrear
            // 
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
            gbCrear.Location = new Point(3, 73);
            gbCrear.Name = "gbCrear";
            gbCrear.Padding = new Padding(12);
            gbCrear.Size = new Size(974, 204);
            gbCrear.TabIndex = 1;
            gbCrear.TabStop = false;
            gbCrear.Text = "Crear Usuario";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(16, 26);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(78, 25);
            lblNombre.TabIndex = 0;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(16, 44);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(190, 31);
            txtNombre.TabIndex = 1;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(236, 26);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(78, 25);
            lblApellido.TabIndex = 2;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(236, 44);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(190, 31);
            txtApellido.TabIndex = 3;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(456, 26);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(43, 25);
            lblDni.TabIndex = 4;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(456, 44);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(190, 31);
            txtDni.TabIndex = 5;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(16, 72);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(79, 25);
            lblTelefono.TabIndex = 6;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(16, 90);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(190, 31);
            txtTelefono.TabIndex = 7;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(236, 72);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(54, 25);
            lblEmail.TabIndex = 8;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(236, 90);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(190, 31);
            txtEmail.TabIndex = 9;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(456, 72);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(101, 25);
            lblPass.TabIndex = 10;
            lblPass.Text = "Contraseña";
            // 
            // txtPass
            // 
            txtPass.Location = new Point(456, 90);
            txtPass.Name = "txtPass";
            txtPass.Size = new Size(190, 31);
            txtPass.TabIndex = 11;
            txtPass.UseSystemPasswordChar = true;
            // 
            // lblRol
            // 
            lblRol.AutoSize = true;
            lblRol.Location = new Point(16, 118);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(37, 25);
            lblRol.TabIndex = 12;
            lblRol.Text = "Rol";
            // 
            // rbOperador
            // 
            rbOperador.AutoSize = true;
            rbOperador.Checked = true;
            rbOperador.Location = new Point(16, 138);
            rbOperador.Name = "rbOperador";
            rbOperador.Size = new Size(114, 29);
            rbOperador.TabIndex = 13;
            rbOperador.TabStop = true;
            rbOperador.Text = "Operador";
            // 
            // rbAdministrador
            // 
            rbAdministrador.AutoSize = true;
            rbAdministrador.Location = new Point(139, 138);
            rbAdministrador.Name = "rbAdministrador";
            rbAdministrador.Size = new Size(151, 29);
            rbAdministrador.TabIndex = 14;
            rbAdministrador.Text = "Administrador";
            // 
            // rbPropietario
            // 
            rbPropietario.AutoSize = true;
            rbPropietario.Location = new Point(296, 138);
            rbPropietario.Name = "rbPropietario";
            rbPropietario.Size = new Size(124, 29);
            rbPropietario.TabIndex = 15;
            rbPropietario.Text = "Propietario";
            // 
            // lblPass2
            // 
            lblPass2.AutoSize = true;
            lblPass2.Location = new Point(456, 118);
            lblPass2.Name = "lblPass2";
            lblPass2.Size = new Size(161, 25);
            lblPass2.TabIndex = 16;
            lblPass2.Text = "Repetir Contraseña";
            // 
            // txtPass2
            // 
            txtPass2.Location = new Point(456, 136);
            txtPass2.Name = "txtPass2";
            txtPass2.Size = new Size(190, 31);
            txtPass2.TabIndex = 17;
            txtPass2.UseSystemPasswordChar = true;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(680, 132);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 30);
            btnGuardar.TabIndex = 18;
            btnGuardar.Text = "Guardar";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(790, 132);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 30);
            btnCancelar.TabIndex = 19;
            btnCancelar.Text = "Cancelar";
            // 
            // pnlListaHeader
            // 
            pnlListaHeader.BackColor = Color.White;
            pnlListaHeader.Controls.Add(lblListaTitulo);
            pnlListaHeader.Dock = DockStyle.Fill;
            pnlListaHeader.Location = new Point(3, 283);
            pnlListaHeader.Name = "pnlListaHeader";
            pnlListaHeader.Size = new Size(974, 34);
            pnlListaHeader.TabIndex = 2;
            // 
            // lblListaTitulo
            // 
            lblListaTitulo.AutoSize = true;
            lblListaTitulo.Font = new Font("Verdana", 12F, FontStyle.Bold);
            lblListaTitulo.Location = new Point(16, 10);
            lblListaTitulo.Name = "lblListaTitulo";
            lblListaTitulo.Size = new Size(129, 29);
            lblListaTitulo.TabIndex = 0;
            lblListaTitulo.Text = "Usuarios";
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvUsuarios.BackgroundColor = Color.White;
            dgvUsuarios.ColumnHeadersHeight = 34;
            dgvUsuarios.Dock = DockStyle.Fill;
            dgvUsuarios.Location = new Point(3, 323);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersVisible = false;
            dgvUsuarios.RowHeadersWidth = 62;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(974, 354);
            dgvUsuarios.TabIndex = 3;
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
            pnlListaHeader.ResumeLayout(false);
            pnlListaHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
        }
    }
}
