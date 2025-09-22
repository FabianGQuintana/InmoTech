// UcContratos.Designer.cs
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
            colNumero = new DataGridViewTextBoxColumn();
            colInquilino = new DataGridViewTextBoxColumn();
            colInmueble = new DataGridViewTextBoxColumn();
            colInicio = new DataGridViewTextBoxColumn();
            colFin = new DataGridViewTextBoxColumn();
            colMonto = new DataGridViewTextBoxColumn();
            colEstado = new DataGridViewTextBoxColumn();
            colActivo = new DataGridViewTextBoxColumn();
            LListaInquilinos = new Label();
            panel2 = new Panel();
            cmbInmueble = new ComboBox();
            cmbInquilino = new ComboBox();
            lblEstado = new Label();
            dtpInicio = new DateTimePicker();
            cmbEstado = new ComboBox();
            chkActivo = new CheckBox();
            lblNumero = new Label();
            txtNumero = new TextBox();
            lblInquilino = new Label();
            lblInmueble = new Label();
            lblInicio = new Label();
            lblFin = new Label();
            dtpFin = new DateTimePicker();
            lblMonto = new Label();
            nudMonto = new NumericUpDown();
            btnGuardar = new Button();
            btnCancelar = new Button();
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
            lblTitulo.Location = new Point(14, 15);
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { colNumero, colInquilino, colInmueble, colInicio, colFin, colMonto, colEstado, colActivo });
            dataGridView1.Location = new Point(16, 366);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(1170, 318);
            dataGridView1.TabIndex = 4;
            // 
            // colNumero
            // 
            colNumero.MinimumWidth = 8;
            colNumero.Name = "colNumero";
            colNumero.ReadOnly = true;
            // 
            // colInquilino
            // 
            colInquilino.MinimumWidth = 8;
            colInquilino.Name = "colInquilino";
            colInquilino.ReadOnly = true;
            // 
            // colInmueble
            // 
            colInmueble.MinimumWidth = 8;
            colInmueble.Name = "colInmueble";
            colInmueble.ReadOnly = true;
            // 
            // colInicio
            // 
            colInicio.MinimumWidth = 8;
            colInicio.Name = "colInicio";
            colInicio.ReadOnly = true;
            // 
            // colFin
            // 
            colFin.MinimumWidth = 8;
            colFin.Name = "colFin";
            colFin.ReadOnly = true;
            // 
            // colMonto
            // 
            colMonto.MinimumWidth = 8;
            colMonto.Name = "colMonto";
            colMonto.ReadOnly = true;
            // 
            // colEstado
            // 
            colEstado.MinimumWidth = 8;
            colEstado.Name = "colEstado";
            colEstado.ReadOnly = true;
            // 
            // colActivo
            // 
            colActivo.MinimumWidth = 8;
            colActivo.Name = "colActivo";
            colActivo.ReadOnly = true;
            // 
            // LListaInquilinos
            // 
            LListaInquilinos.AutoSize = true;
            LListaInquilinos.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
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
            panel2.Controls.Add(cmbInmueble);
            panel2.Controls.Add(cmbInquilino);
            panel2.Controls.Add(lblEstado);
            panel2.Controls.Add(dtpInicio);
            panel2.Controls.Add(cmbEstado);
            panel2.Controls.Add(chkActivo);
            panel2.Controls.Add(lblNumero);
            panel2.Controls.Add(txtNumero);
            panel2.Controls.Add(lblInquilino);
            panel2.Controls.Add(lblInmueble);
            panel2.Controls.Add(lblInicio);
            panel2.Controls.Add(lblFin);
            panel2.Controls.Add(dtpFin);
            panel2.Controls.Add(lblMonto);
            panel2.Controls.Add(nudMonto);
            panel2.Controls.Add(btnGuardar);
            panel2.Controls.Add(btnCancelar);
            panel2.Location = new Point(16, 103);
            panel2.Name = "panel2";
            panel2.Size = new Size(1170, 200);
            panel2.TabIndex = 6;
            // 
            // cmbInmueble
            // 
            cmbInmueble.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInmueble.Location = new Point(801, 22);
            cmbInmueble.Margin = new Padding(4);
            cmbInmueble.Name = "cmbInmueble";
            cmbInmueble.Size = new Size(234, 33);
            cmbInmueble.TabIndex = 36;
            // 
            // cmbInquilino
            // 
            cmbInquilino.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbInquilino.Location = new Point(460, 22);
            cmbInquilino.Margin = new Padding(4);
            cmbInquilino.Name = "cmbInquilino";
            cmbInquilino.Size = new Size(234, 33);
            cmbInquilino.TabIndex = 33;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(386, 139);
            lblEstado.Margin = new Padding(4, 0, 4, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(66, 25);
            lblEstado.TabIndex = 30;
            lblEstado.Text = "Estado";
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(127, 81);
            dtpInicio.Margin = new Padding(4);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(236, 31);
            dtpInicio.TabIndex = 25;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbEstado.Location = new Point(460, 137);
            cmbEstado.Margin = new Padding(4);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(234, 33);
            cmbEstado.TabIndex = 31;
            // 
            // chkActivo
            // 
            chkActivo.AutoSize = true;
            chkActivo.Checked = true;
            chkActivo.CheckState = CheckState.Checked;
            chkActivo.Location = new Point(781, 77);
            chkActivo.Margin = new Padding(4);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(88, 29);
            chkActivo.TabIndex = 32;
            chkActivo.Text = "Activo";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.Location = new Point(12, 25);
            lblNumero.Margin = new Padding(4, 0, 4, 0);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(107, 25);
            lblNumero.TabIndex = 20;
            lblNumero.Text = "N° Contrato";
            // 
            // txtNumero
            // 
            txtNumero.Location = new Point(127, 23);
            txtNumero.Margin = new Padding(4);
            txtNumero.Name = "txtNumero";
            txtNumero.Size = new Size(236, 31);
            txtNumero.TabIndex = 21;
            // 
            // lblInquilino
            // 
            lblInquilino.AutoSize = true;
            lblInquilino.Location = new Point(371, 25);
            lblInquilino.Margin = new Padding(4, 0, 4, 0);
            lblInquilino.Name = "lblInquilino";
            lblInquilino.Size = new Size(81, 25);
            lblInquilino.TabIndex = 22;
            lblInquilino.Text = "Inquilino";
            // 
            // lblInmueble
            // 
            lblInmueble.AutoSize = true;
            lblInmueble.Location = new Point(716, 26);
            lblInmueble.Margin = new Padding(4, 0, 4, 0);
            lblInmueble.Name = "lblInmueble";
            lblInmueble.Size = new Size(86, 25);
            lblInmueble.TabIndex = 23;
            lblInmueble.Text = "Inmueble";
            // 
            // lblInicio
            // 
            lblInicio.AutoSize = true;
            lblInicio.Location = new Point(68, 81);
            lblInicio.Margin = new Padding(4, 0, 4, 0);
            lblInicio.Name = "lblInicio";
            lblInicio.Size = new Size(54, 25);
            lblInicio.TabIndex = 24;
            lblInicio.Text = "Inicio";
            // 
            // lblFin
            // 
            lblFin.AutoSize = true;
            lblFin.Location = new Point(417, 81);
            lblFin.Margin = new Padding(4, 0, 4, 0);
            lblFin.Name = "lblFin";
            lblFin.Size = new Size(35, 25);
            lblFin.TabIndex = 26;
            lblFin.Text = "Fin";
            // 
            // dtpFin
            // 
            dtpFin.Location = new Point(460, 81);
            dtpFin.Margin = new Padding(4);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(234, 31);
            dtpFin.TabIndex = 27;
            // 
            // lblMonto
            // 
            lblMonto.AutoSize = true;
            lblMonto.Location = new Point(56, 139);
            lblMonto.Margin = new Padding(4, 0, 4, 0);
            lblMonto.Name = "lblMonto";
            lblMonto.Size = new Size(66, 25);
            lblMonto.TabIndex = 28;
            lblMonto.Text = "Monto";
            // 
            // nudMonto
            // 
            nudMonto.Location = new Point(127, 139);
            nudMonto.Margin = new Padding(4);
            nudMonto.Name = "nudMonto";
            nudMonto.Size = new Size(236, 31);
            nudMonto.TabIndex = 29;
            // 
            // btnGuardar
            // 
            btnGuardar.BackColor = Color.White;
            btnGuardar.Location = new Point(927, 133);
            btnGuardar.Margin = new Padding(4);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(157, 37);
            btnGuardar.TabIndex = 34;
            btnGuardar.Text = "Guardar";
            btnGuardar.UseVisualStyleBackColor = false;
            // 
            // btnCancelar
            // 
            btnCancelar.BackColor = Color.White;
            btnCancelar.Location = new Point(743, 135);
            btnCancelar.Margin = new Padding(4);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(157, 36);
            btnCancelar.TabIndex = 35;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = false;
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
        private ComboBox cmbInmueble;
        private ComboBox cmbInquilino;
        private Label lblEstado;
        private DateTimePicker dtpInicio;
        private ComboBox cmbEstado;
        private CheckBox chkActivo;
        private Label lblNumero;
        private TextBox txtNumero;
        private Label lblInquilino;
        private Label lblInmueble;
        private Label lblInicio;
        private Label lblFin;
        private DateTimePicker dtpFin;
        private Label lblMonto;
        private NumericUpDown nudMonto;
        private Button btnGuardar;
        private Button btnCancelar;
        private DataGridViewTextBoxColumn colNumero;
        private DataGridViewTextBoxColumn colInquilino;
        private DataGridViewTextBoxColumn colInmueble;
        private DataGridViewTextBoxColumn colInicio;
        private DataGridViewTextBoxColumn colFin;
        private DataGridViewTextBoxColumn colMonto;
        private DataGridViewTextBoxColumn colEstado;
        private DataGridViewTextBoxColumn colActivo;
    }
}
