using System.Windows.Forms;

namespace InmoTech.Forms
{
    partial class FrmBuscarInmueble
    {
        private System.ComponentModel.IContainer components = null;

        private Panel pnlTop;
        private Label lblTitulo;
        private Label lblBuscar;
        private TextBox txtBuscar;
        private Label lblEstado;
        private ComboBox cboEstado;
        private DataGridView dgv;
        private Panel pnlBottom;
        private Label lblInfo;
        private Button btnAnterior;
        private Button btnSiguiente;
        private Button btnElegir;
        private Button btnCancelar;
        private System.Windows.Forms.Timer timerBuscar;


        // Columnas del DataGridView (todas TextBoxColumn)
        private DataGridViewTextBoxColumn colDireccion;
        private DataGridViewTextBoxColumn colTipo;
        private DataGridViewTextBoxColumn colAmb;
        private DataGridViewTextBoxColumn colAmueblado;
        private DataGridViewTextBoxColumn colActivo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            pnlTop = new Panel();
            lblTitulo = new Label();
            lblBuscar = new Label();
            txtBuscar = new TextBox();
            lblEstado = new Label();
            cboEstado = new ComboBox();
            dgv = new DataGridView();
            pnlBottom = new Panel();
            lblInfo = new Label();
            btnAnterior = new Button();
            btnSiguiente = new Button();
            btnElegir = new Button();
            btnCancelar = new Button();
            timerBuscar = new System.Windows.Forms.Timer(components);
            pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            pnlBottom.SuspendLayout();
            SuspendLayout();
            // 
            // pnlTop
            // 
            pnlTop.BackColor = Color.Teal;
            pnlTop.Controls.Add(lblTitulo);
            pnlTop.Dock = DockStyle.Top;
            pnlTop.Location = new Point(0, 0);
            pnlTop.Margin = new Padding(4, 5, 4, 5);
            pnlTop.Name = "pnlTop";
            pnlTop.Size = new Size(1286, 80);
            pnlTop.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(17, 20);
            lblTitulo.Margin = new Padding(4, 0, 4, 0);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(242, 32);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Buscar un Inmueble";
            // 
            // lblBuscar
            // 
            lblBuscar.AutoSize = true;
            lblBuscar.Location = new Point(17, 100);
            lblBuscar.Margin = new Padding(4, 0, 4, 0);
            lblBuscar.Name = "lblBuscar";
            lblBuscar.Size = new Size(122, 25);
            lblBuscar.TabIndex = 1;
            lblBuscar.Text = "Buscar (texto):";
            // 
            // txtBuscar
            // 
            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            txtBuscar.Location = new Point(159, 95);
            txtBuscar.Margin = new Padding(4, 5, 4, 5);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(770, 31);
            txtBuscar.TabIndex = 0;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(953, 100);
            lblEstado.Margin = new Padding(4, 0, 4, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(70, 25);
            lblEstado.TabIndex = 3;
            lblEstado.Text = "Estado:";
            // 
            // cboEstado
            // 
            cboEstado.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.FormattingEnabled = true;
            cboEstado.Items.AddRange(new object[] { "Todos", "Activos", "Inactivos" });
            cboEstado.Location = new Point(1026, 95);
            cboEstado.Margin = new Padding(4, 5, 4, 5);
            cboEstado.Name = "cboEstado";
            cboEstado.Size = new Size(241, 33);
            cboEstado.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.AllowUserToAddRows = false;
            dgv.AllowUserToDeleteRows = false;
            dgv.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.BackgroundColor = Color.White;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Location = new Point(17, 153);
            dgv.Margin = new Padding(4, 5, 4, 5);
            dgv.MultiSelect = false;
            dgv.Name = "dgv";
            dgv.ReadOnly = true;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 62;
            dgv.RowTemplate.Height = 28;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(1251, 663);
            dgv.TabIndex = 2;
            // 
            // pnlBottom
            // 
            pnlBottom.Controls.Add(lblInfo);
            pnlBottom.Controls.Add(btnAnterior);
            pnlBottom.Controls.Add(btnSiguiente);
            pnlBottom.Controls.Add(btnElegir);
            pnlBottom.Controls.Add(btnCancelar);
            pnlBottom.Dock = DockStyle.Bottom;
            pnlBottom.Location = new Point(0, 827);
            pnlBottom.Margin = new Padding(4, 5, 4, 5);
            pnlBottom.Name = "pnlBottom";
            pnlBottom.Size = new Size(1286, 100);
            pnlBottom.TabIndex = 6;
            // 
            // lblInfo
            // 
            lblInfo.AutoSize = true;
            lblInfo.Location = new Point(17, 35);
            lblInfo.Margin = new Padding(4, 0, 4, 0);
            lblInfo.Name = "lblInfo";
            lblInfo.Size = new Size(220, 25);
            lblInfo.TabIndex = 0;
            lblInfo.Text = "Mostrando 0-0 de 0 ítems";
            // 
            // btnAnterior
            // 
            btnAnterior.Anchor = AnchorStyles.Bottom;
            btnAnterior.Location = new Point(517, 27);
            btnAnterior.Margin = new Padding(4, 5, 4, 5);
            btnAnterior.Name = "btnAnterior";
            btnAnterior.Size = new Size(126, 47);
            btnAnterior.TabIndex = 3;
            btnAnterior.Text = "Anterior";
            btnAnterior.UseVisualStyleBackColor = true;
            // 
            // btnSiguiente
            // 
            btnSiguiente.Anchor = AnchorStyles.Bottom;
            btnSiguiente.Location = new Point(651, 27);
            btnSiguiente.Margin = new Padding(4, 5, 4, 5);
            btnSiguiente.Name = "btnSiguiente";
            btnSiguiente.Size = new Size(126, 47);
            btnSiguiente.TabIndex = 4;
            btnSiguiente.Text = "Siguiente";
            btnSiguiente.UseVisualStyleBackColor = true;
            // 
            // btnElegir
            // 
            btnElegir.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnElegir.Location = new Point(986, 27);
            btnElegir.Margin = new Padding(4, 5, 4, 5);
            btnElegir.Name = "btnElegir";
            btnElegir.Size = new Size(131, 47);
            btnElegir.TabIndex = 5;
            btnElegir.Text = "Elegir";
            btnElegir.UseVisualStyleBackColor = true;
            // 
            // btnCancelar
            // 
            btnCancelar.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancelar.DialogResult = DialogResult.Cancel;
            btnCancelar.Location = new Point(1126, 27);
            btnCancelar.Margin = new Padding(4, 5, 4, 5);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.Size = new Size(131, 47);
            btnCancelar.TabIndex = 6;
            btnCancelar.Text = "Cancelar";
            btnCancelar.UseVisualStyleBackColor = true;
            // 
            // timerBuscar
            // 
            timerBuscar.Interval = 350;
            // 
            // FrmBuscarInmueble
            // 
            AcceptButton = btnElegir;
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancelar;
            ClientSize = new Size(1286, 927);
            Controls.Add(pnlBottom);
            Controls.Add(dgv);
            Controls.Add(cboEstado);
            Controls.Add(lblEstado);
            Controls.Add(txtBuscar);
            Controls.Add(lblBuscar);
            Controls.Add(pnlTop);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Margin = new Padding(4, 5, 4, 5);
            MinimumSize = new Size(1076, 696);
            Name = "FrmBuscarInmueble";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Buscar Inmueble";
            pnlTop.ResumeLayout(false);
            pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            pnlBottom.ResumeLayout(false);
            pnlBottom.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
    }
}
