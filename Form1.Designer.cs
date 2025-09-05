namespace InmoTech
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            pnlContent = new Panel();
            PanelLateral = new Panel();
            BSalir = new Button();
            BInmuebles = new Button();
            BUsuarios = new Button();
            BDashboard = new Button();
            BInquilinos = new Button();
            BContratos = new Button();
            BPagos = new Button();
            BReportes = new Button();
            pictureBox1 = new PictureBox();
            tableLayoutPanel1 = new TableLayoutPanel();
            PanelLateral.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.White;
            pnlContent.BackgroundImageLayout = ImageLayout.None;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Location = new Point(311, 4);
            pnlContent.Margin = new Padding(4);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1359, 921);
            pnlContent.TabIndex = 1;
            // 
            // PanelLateral
            // 
            PanelLateral.BackColor = Color.LightSlateGray;
            PanelLateral.Controls.Add(pictureBox1);
            PanelLateral.Controls.Add(BReportes);
            PanelLateral.Controls.Add(BPagos);
            PanelLateral.Controls.Add(BContratos);
            PanelLateral.Controls.Add(BInquilinos);
            PanelLateral.Controls.Add(BDashboard);
            PanelLateral.Controls.Add(BUsuarios);
            PanelLateral.Controls.Add(BInmuebles);
            PanelLateral.Controls.Add(BSalir);
            PanelLateral.Location = new Point(4, 4);
            PanelLateral.Margin = new Padding(4);
            PanelLateral.Name = "PanelLateral";
            PanelLateral.Size = new Size(299, 921);
            PanelLateral.TabIndex = 0;
            // 
            // BSalir
            // 
            BSalir.BackgroundImage = Properties.Resources.botonSalir;
            BSalir.Image = Properties.Resources.botonSalir;
            BSalir.ImageAlign = ContentAlignment.MiddleLeft;
            BSalir.Location = new Point(25, 831);
            BSalir.Margin = new Padding(4);
            BSalir.Name = "BSalir";
            BSalir.Size = new Size(222, 42);
            BSalir.TabIndex = 0;
            BSalir.Text = "Salir";
            BSalir.UseVisualStyleBackColor = true;
            // 
            // BInmuebles
            // 
            BInmuebles.Location = new Point(38, 260);
            BInmuebles.Margin = new Padding(4);
            BInmuebles.Name = "BInmuebles";
            BInmuebles.Size = new Size(210, 36);
            BInmuebles.TabIndex = 1;
            BInmuebles.Text = "Inmuebles";
            BInmuebles.UseVisualStyleBackColor = true;
            BInmuebles.Click += BInmuebles_Click;
            // 
            // BUsuarios
            // 
            BUsuarios.Location = new Point(38, 189);
            BUsuarios.Margin = new Padding(4);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(210, 36);
            BUsuarios.TabIndex = 2;
            BUsuarios.Text = "Usuarios";
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click;
            // 
            // BDashboard
            // 
            BDashboard.Location = new Point(38, 118);
            BDashboard.Margin = new Padding(4);
            BDashboard.Name = "BDashboard";
            BDashboard.Size = new Size(210, 36);
            BDashboard.TabIndex = 3;
            BDashboard.Text = "Dashboard";
            BDashboard.UseVisualStyleBackColor = true;
            BDashboard.Click += BDashboard_Click;
            // 
            // BInquilinos
            // 
            BInquilinos.Location = new Point(38, 325);
            BInquilinos.Margin = new Padding(4);
            BInquilinos.Name = "BInquilinos";
            BInquilinos.Size = new Size(210, 36);
            BInquilinos.TabIndex = 4;
            BInquilinos.Text = "Inquilinos";
            BInquilinos.UseVisualStyleBackColor = true;
            BInquilinos.Click += BInquilinos_Click;
            // 
            // BContratos
            // 
            BContratos.Location = new Point(38, 398);
            BContratos.Margin = new Padding(4);
            BContratos.Name = "BContratos";
            BContratos.Size = new Size(210, 36);
            BContratos.TabIndex = 5;
            BContratos.Text = "Contratos";
            BContratos.UseVisualStyleBackColor = true;
            // 
            // BPagos
            // 
            BPagos.Location = new Point(38, 464);
            BPagos.Margin = new Padding(4);
            BPagos.Name = "BPagos";
            BPagos.Size = new Size(210, 36);
            BPagos.TabIndex = 6;
            BPagos.Text = "Pagos";
            BPagos.UseVisualStyleBackColor = true;
            BPagos.Click += BPagos_Click;
            // 
            // BReportes
            // 
            BReportes.Location = new Point(38, 535);
            BReportes.Margin = new Padding(4);
            BReportes.Name = "BReportes";
            BReportes.Size = new Size(210, 36);
            BReportes.TabIndex = 7;
            BReportes.Text = "Reportes";
            BReportes.UseVisualStyleBackColor = true;
            BReportes.Click += BReportes_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.InmoTech_Logo;
            pictureBox1.Location = new Point(-2, 0);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(305, 92);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.3393078F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.66069F));
            tableLayoutPanel1.Controls.Add(PanelLateral, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlContent, 1, 0);
            tableLayoutPanel1.Location = new Point(15, 15);
            tableLayoutPanel1.Margin = new Padding(4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1674, 929);
            tableLayoutPanel1.TabIndex = 1;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1704, 959);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            PanelLateral.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContent;
        private Panel PanelLateral;
        private PictureBox pictureBox1;
        private Button BReportes;
        private Button BPagos;
        private Button BContratos;
        private Button BInquilinos;
        private Button BDashboard;
        private Button BUsuarios;
        private Button BInmuebles;
        private Button BSalir;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
