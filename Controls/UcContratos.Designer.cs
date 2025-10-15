using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcContratos
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            panel1 = new Panel();
            lblTitulo = new Label();
            dataGridView1 = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colNumero = new DataGridViewTextBoxColumn();
            colInquilino = new DataGridViewTextBoxColumn();
            colInmueble = new DataGridViewTextBoxColumn();
            colInicio = new DataGridViewTextBoxColumn();
            colFin = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colUsuario = new DataGridViewTextBoxColumn();
            colFechaCreacion = new DataGridViewTextBoxColumn();
            LListaInquilinos = new Label();
            panel2 = new Panel();
            BEstado = new Button();
            btnBuscarInquilino = new Button();
            txtInquilino = new Label();
            label1 = new Label();
            btnBuscarInmueble = new Button();
            btnCancelar = new Button();
            dtpInicio = new DateTimePicker();
            chkActivo = new CheckBox();
            lblInquilino = new Label();
            txtInmueble = new Label();
            lblInicio = new Label();
            lblFin = new Label();
            dtpFin = new DateTimePicker();
            lblMonto = new Label();
            nudMonto = new NumericUpDown();
            btnGuardar = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel1.BackColor = Color.Teal;
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(16, 18);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 70);
            panel1.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(3, 19);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(166, 37);
            lblTitulo.TabIndex = 1;
            lblTitulo.Text = "Contratos";
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.BackgroundColor = Color.Teal;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colId, colNumero, colInquilino, colInmueble, colInicio, colFin, colMonto, colEstado, colUsuario, colFechaCreacion });
            dataGridView1.Location = new Point(16, 366);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1170, 318);
            dataGridView1.TabIndex = 4;
            // 
            // colId
            // 
            colId.DataPropertyName = "IdContrato";
            colId.HeaderText = "Id";
            colId.MinimumWidth = 50;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colNumero
            // 
            colNumero.DataPropertyName = "IdContrato";
            colNumero.HeaderText = "N° Contrato";
            colNumero.MinimumWidth = 80;
            colNumero.Name = "colNumero";
            colNumero.ReadOnly = true;
            // 
            // colInquilino
            // 
            colInquilino.DataPropertyName = "NombreInquilino";
            colInquilino.HeaderText = "Inquilino";
            colInquilino.MinimumWidth = 120;
            colInquilino.Name = "colInquilino";
            colInquilino.ReadOnly = true;
            // 
            // colInmueble
            // 
            colInmueble.DataPropertyName = "DireccionInmueble";
            colInmueble.HeaderText = "Inmueble";
            colInmueble.MinimumWidth = 120;
            colInmueble.Name = "colInmueble";
            colInmueble.ReadOnly = true;
            // 
            // colInicio
            // 
            colInicio.DataPropertyName = "FechaInicio";
            colInicio.HeaderText = "Inicio";
            colInicio.MinimumWidth = 80;
            colInicio.Name = "colInicio";
            colInicio.ReadOnly = true;
            // 
            // colFin
            // 
            colFin.DataPropertyName = "FechaFin";
            colFin.HeaderText = "Fin";
            colFin.MinimumWidth = 80;
            colFin.Name = "colFin";
            colFin.ReadOnly = true;
            // 
            // colMonto
            // 
            colMonto.DataPropertyName = "Monto";
            colMonto.HeaderText = "Monto";
            colMonto.MinimumWidth = 80;
            colMonto.Name = "colMonto";
            colMonto.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.HeaderText = "Estado";
            colEstado.MinimumWidth = 90;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colUsuario
            // 
            colUsuario.DataPropertyName = "NombreUsuario";
            colUsuario.HeaderText = "Usuario creador";
            colUsuario.MinimumWidth = 120;
            colUsuario.Name = "colUsuario";
            colUsuario.ReadOnly = true;
            // 
            // colFechaCreacion
            // 
            colFechaCreacion.DataPropertyName = "FechaCreacion";
            colFechaCreacion.HeaderText = "Fecha Creación";
            colFechaCreacion.MinimumWidth = 120;
            colFechaCreacion.Name = "colFechaCreacion";
            colFechaCreacion.ReadOnly = true;
            // 
            // LListaInquilinos
            // 
            LListaInquilinos.AutoSize = true;
            LListaInquilinos.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            LListaInquilinos.Location = new Point(16, 324);
            LListaInquilinos.Name = "LListaInquilinos";
            LListaInquilinos.Size = new Size(187, 29);
            LListaInquilinos.TabIndex = 5;
            LListaInquilinos.Text = "Lista Contratos";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panel2.BackColor = Color.Teal;
            panel2.Controls.Add(BEstado);
            panel2.Controls.Add(btnBuscarInquilino);
            panel2.Controls.Add(txtInquilino);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(btnBuscarInmueble);
            panel2.Controls.Add(btnCancelar);
            panel2.Controls.Add(dtpInicio);
            panel2.Controls.Add(chkActivo);
            panel2.Controls.Add(lblInquilino);
            panel2.Controls.Add(txtInmueble);
            panel2.Controls.Add(lblInicio);
            panel2.Controls.Add(lblFin);
            panel2.Controls.Add(dtpFin);
            panel2.Controls.Add(lblMonto);
            panel2.Controls.Add(nudMonto);
            panel2.Controls.Add(btnGuardar);
            panel2.Location = new Point(16, 103);
            panel2.Name = "panel2";
            panel2.Size = new Size(1170, 200);
            panel2.TabIndex = 6;
            // 
            // BEstado
            // 
            BEstado.BackColor = Color.White;
            BEstado.Location = new Point(904, 139);
            BEstado.Name = "BEstado";
            BEstado.Size = new Size(157, 36);
            BEstado.TabIndex = 15;
            BEstado.Text = "Anular";
            BEstado.UseVisualStyleBackColor = false;
            // 
            // btnBuscarInquilino
            // 
            btnBuscarInquilino.Location = new Point(129, 21);
            btnBuscarInquilino.Name = "btnBuscarInquilino";
            btnBuscarInquilino.Size = new Size(112, 34);
            btnBuscarInquilino.TabIndex = 0;
            btnBuscarInquilino.Text = "Buscar";
            btnBuscarInquilino.UseVisualStyleBackColor = true;
            btnBuscarInquilino.Click += btnBuscarInquilino_Click;
            // 
            // txtInquilino
            // 
            txtInquilino.AutoSize = true;
            txtInquilino.Location = new Point(261, 26);
            txtInquilino.Name = "txtInquilino";
            txtInquilino.Size = new Size(24, 25);
            txtInquilino.TabIndex = 1;
            txtInquilino.Text = "...";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(428, 26);
            label1.Name = "label1";
            label1.Size = new Size(86, 25);
            label1.TabIndex = 2;
            label1.Text = "Inmueble";
            // 
            // btnBuscarInmueble
            // 
            btnBuscarInmueble.Location = new Point(519, 21);
            btnBuscarInmueble.Name = "btnBuscarInmueble";
            btnBuscarInmueble.Size = new Size(112, 34);
            btnBuscarInmueble.TabIndex = 3;
            btnBuscarInmueble.Text = "Buscar";
            btnBuscarInmueble.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(904, 97);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(157, 36);
            btnCancelar.TabIndex = 14;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(127, 81);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(236, 31);
            dtpInicio.TabIndex = 4;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(519, 142);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(88, 29);
            chkActivo.TabIndex = 5;
            chkActivo.Text = "Activo";
            // 
            // lblInquilino
            // 
            lblInquilino.AutoSize = true;
            lblInquilino.Location = new Point(41, 26);
            lblInquilino.Name = "lblInquilino";
            lblInquilino.Size = new Size(81, 25);
            lblInquilino.TabIndex = 6;
            lblInquilino.Text = "Inquilino";
            // 
            // txtInmueble
            // 
            txtInmueble.AutoSize = true;
            txtInmueble.Location = new Point(649, 26);
            txtInmueble.Name = "txtInmueble";
            txtInmueble.Size = new Size(24, 25);
            txtInmueble.TabIndex = 7;
            txtInmueble.Text = "...";
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(68, 81);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(54, 25);
            lblInicio.TabIndex = 8;
            lblInicio.Text = "Inicio";
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(477, 86);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(35, 25);
            lblFin.TabIndex = 9;
            lblFin.Text = "Fin";
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(519, 81);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(260, 31);
            dtpFin.TabIndex = 10;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(56, 139);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(66, 25);
            lblMonto.TabIndex = 11;
            lblMonto.Text = "Monto";
            // 
            // nudMonto
            // 
            nudMonto.DecimalPlaces = 2;
            nudMonto.Location = new Point(127, 139);
            nudMonto.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudMonto.Name = "nudMonto";
            nudMonto.Size = new Size(120, 31);
            nudMonto.TabIndex = 12;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.Location = new Point(904, 54);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(157, 37);
            btnGuardar.TabIndex = 13;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // UcContratos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel2);
            Controls.Add(LListaInquilinos);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            Name = "UcContratos";
            Size = new Size(1200, 700);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudMonto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Label lblTitulo;
        private DataGridView dataGridView1;
        private Label LListaInquilinos;
        private Panel panel2;
        private DateTimePicker dtpInicio;
        private CheckBox chkActivo;
        private Label lblInquilino;
        private Label txtInmueble;
        private Label lblInicio;
        private Label lblFin;
        private DateTimePicker dtpFin;
        private Label lblMonto;
        private NumericUpDown nudMonto;
        private Button btnGuardar;
        private Button btnCancelar;
        private Button btnBuscarInmueble;
        private Label label1;
        private Button btnBuscarInquilino;
        private Label txtInquilino;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colNumero;
        private DataGridViewTextBoxColumn colInquilino;
        private DataGridViewTextBoxColumn colInmueble;
        private DataGridViewTextBoxColumn colInicio;
        private DataGridViewTextBoxColumn colFin;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colUsuario;
        private DataGridViewTextBoxColumn colFechaCreacion;
        // La declaración de colAccion también ha sido eliminada
        private Button BEstado;
    }
}