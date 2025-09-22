using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcInquilinos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblTitulo = new Label();
            panel2 = new Panel();
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
            txtDireccion = new TextBox();
            btnGuardar = new Button();
            btnCancelar = new Button();
            dataGridInquilinos = new DataGridView();
            LListaInquilinos = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((ISupportInitialize)dataGridInquilinos).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(15, 24);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 70);
            panel1.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(14, 15);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(162, 37);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Inquilinos";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Teal;
            panel2.Controls.Add(label1);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(lblNombre);
            panel2.Controls.Add(txtNombre);
            panel2.Controls.Add(lblApellido);
            panel2.Controls.Add(txtApellido);
            panel2.Controls.Add(lblDni);
            panel2.Controls.Add(txtDni);
            panel2.Controls.Add(lblTelefono);
            panel2.Controls.Add(txtTelefono);
            panel2.Controls.Add(lblEmail);
            panel2.Controls.Add(txtEmail);
            panel2.Controls.Add(lblPass);
            panel2.Controls.Add(txtDireccion);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(btnCancelar);
            panel2.Location = new Point(15, 112);
            panel2.Name = "panel2";
            panel2.Size = new Size(1170, 270);
            panel2.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(24, 165);
            label1.Name = "label1";
            label1.Size = new Size(152, 25);
            label1.TabIndex = 44;
            label1.Text = "Fecha Nacimiento";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(24, 196);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(190, 31);
            dateTimePicker1.TabIndex = 43;
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Location = new Point(25, 23);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(78, 25);
            lblNombre.TabIndex = 45;
            lblNombre.Text = "Nombre";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(25, 54);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(190, 31);
            txtNombre.TabIndex = 46;
            // 
            // lblApellido
            // 
            lblApellido.AutoSize = true;
            lblApellido.Location = new Point(244, 23);
            lblApellido.Name = "lblApellido";
            lblApellido.Size = new Size(78, 25);
            lblApellido.TabIndex = 47;
            lblApellido.Text = "Apellido";
            // 
            // txtApellido
            // 
            txtApellido.Location = new Point(244, 54);
            txtApellido.Name = "txtApellido";
            txtApellido.Size = new Size(190, 31);
            txtApellido.TabIndex = 48;
            // 
            // lblDni
            // 
            lblDni.AutoSize = true;
            lblDni.Location = new Point(244, 165);
            lblDni.Name = "lblDni";
            lblDni.Size = new Size(43, 25);
            lblDni.TabIndex = 49;
            lblDni.Text = "DNI";
            // 
            // txtDni
            // 
            txtDni.Location = new Point(244, 196);
            txtDni.Name = "txtDni";
            txtDni.Size = new Size(190, 31);
            txtDni.TabIndex = 50;
            // 
            // lblTelefono
            // 
            lblTelefono.AutoSize = true;
            lblTelefono.Location = new Point(24, 93);
            lblTelefono.Name = "lblTelefono";
            lblTelefono.Size = new Size(79, 25);
            lblTelefono.TabIndex = 51;
            lblTelefono.Text = "Teléfono";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(24, 124);
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(190, 31);
            txtTelefono.TabIndex = 52;
            // 
            // lblEmail
            // 
            lblEmail.AutoSize = true;
            lblEmail.Location = new Point(244, 93);
            lblEmail.Name = "lblEmail";
            lblEmail.Size = new Size(54, 25);
            lblEmail.TabIndex = 53;
            lblEmail.Text = "Email";
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(244, 124);
            txtEmail.Name = "txtEmail";
            txtEmail.Size = new Size(190, 31);
            txtEmail.TabIndex = 54;
            // 
            // lblPass
            // 
            lblPass.AutoSize = true;
            lblPass.Location = new Point(463, 23);
            lblPass.Name = "lblPass";
            lblPass.Size = new Size(85, 25);
            lblPass.TabIndex = 55;
            lblPass.Text = "Direccion";
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(463, 54);
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(190, 31);
            txtDireccion.TabIndex = 62;
            txtDireccion.UseSystemPasswordChar = true;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.Location = new Point(688, 198);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(100, 36);
            btnGuardar.TabIndex = 63;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(823, 198);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(100, 36);
            btnCancelar.TabIndex = 64;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            btnCancelar.Click += BtnCancelar_Click;
            // 
            // dataGridInquilinos
            // 
            dataGridInquilinos.AllowUserToAddRows = false;
            dataGridInquilinos.AllowUserToDeleteRows = false;
            dataGridInquilinos.BackgroundColor = Color.Teal;
            dataGridInquilinos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridInquilinos.Location = new Point(15, 422);
            dataGridInquilinos.MultiSelect = false;
            dataGridInquilinos.Name = "dataGridInquilinos";
            dataGridInquilinos.ReadOnly = true;
            dataGridInquilinos.RowHeadersWidth = 62;
            dataGridInquilinos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridInquilinos.Size = new Size(1170, 258);
            dataGridInquilinos.TabIndex = 2;
            dataGridInquilinos.CellContentClick += dataGridView1_CellContentClick;
            dataGridInquilinos.CellDoubleClick += dataGridInquilinos_CellDoubleClick;
            // 
            // LListaInquilinos
            // 
            LListaInquilinos.AutoSize = true;
            LListaInquilinos.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LListaInquilinos.Location = new Point(15, 385);
            LListaInquilinos.Name = "LListaInquilinos";
            LListaInquilinos.Size = new Size(188, 29);
            LListaInquilinos.TabIndex = 3;
            LListaInquilinos.Text = "Lista Inquilinos";
            LListaInquilinos.Click += label1_Click;
            // 
            // UcInquilinos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(LListaInquilinos);
            Controls.Add(dataGridInquilinos);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "UcInquilinos";
            Size = new Size(1200, 699);
            Load += UcInquilinos_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((ISupportInitialize)dataGridInquilinos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private DataGridView dataGridInquilinos;
        private Label LListaInquilinos;
        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Label lblNombre;
        internal TextBox txtNombre;
        private Label lblApellido;
        internal TextBox txtApellido;
        private Label lblDni;
        internal TextBox txtDni;
        private Label lblTelefono;
        internal TextBox txtTelefono;
        private Label lblEmail;
        internal TextBox txtEmail;
        private Label lblPass;
        internal TextBox txtDireccion;
        internal Button btnGuardar;
        internal Button btnCancelar;
        private Label lblTitulo;

        // Columnas del grid
        private DataGridViewTextBoxColumn colDni;
        private DataGridViewTextBoxColumn colNombre;
        private DataGridViewTextBoxColumn colApellido;
        private DataGridViewTextBoxColumn colTelefono;
        private DataGridViewTextBoxColumn colEmail;
        private DataGridViewTextBoxColumn colDireccion;
        private DataGridViewTextBoxColumn colFechaNacimiento;
    }
}
