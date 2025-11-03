// MainForm.Designer.cs — agrega botón BBackup antes de Salir y respeta pnlInfoUsuario
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class MainForm
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
            pnlInfoUsuario = new Panel();
            LUsuarioRol = new Label();
            LUsuarioEmail = new Label();
            LUsuarioNombre = new Label();
            LTituloLogo = new Label();
            pictureBox1 = new PictureBox();
            BReportes = new Button();
            BPagos = new Button();
            BContratos = new Button();
            BInquilinos = new Button();
            BDashboard = new Button();
            BUsuarios = new Button();
            BInmuebles = new Button();
            BBackup = new Button();
            BSalir = new Button();
            tableLayoutPanel1 = new TableLayoutPanel();
            PanelLateral.SuspendLayout();
            pnlInfoUsuario.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tableLayoutPanel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnlContent
            // 
            pnlContent.BackColor = Color.DarkSlateGray;
            pnlContent.BackgroundImageLayout = ImageLayout.None;
            pnlContent.Dock = DockStyle.Fill;
            pnlContent.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            pnlContent.Location = new Point(248, 3);
            pnlContent.Name = "pnlContent";
            pnlContent.Size = new Size(1115, 732);
            pnlContent.TabIndex = 1;
            // 
            // PanelLateral
            // 
            PanelLateral.BackColor = Color.White;
            PanelLateral.Controls.Add(pnlInfoUsuario);
            PanelLateral.Controls.Add(LTituloLogo);
            PanelLateral.Controls.Add(pictureBox1);
            PanelLateral.Controls.Add(BReportes);
            PanelLateral.Controls.Add(BPagos);
            PanelLateral.Controls.Add(BContratos);
            PanelLateral.Controls.Add(BInquilinos);
            PanelLateral.Controls.Add(BDashboard);
            PanelLateral.Controls.Add(BUsuarios);
            PanelLateral.Controls.Add(BInmuebles);
            PanelLateral.Controls.Add(BBackup);
            PanelLateral.Controls.Add(BSalir);
            PanelLateral.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            PanelLateral.Location = new Point(3, 3);
            PanelLateral.Name = "PanelLateral";
            PanelLateral.Size = new Size(239, 732);
            PanelLateral.TabIndex = 0;
            // 
            // pnlInfoUsuario
            // 
            pnlInfoUsuario.BackColor = Color.FromArgb(248, 248, 248);
            pnlInfoUsuario.Controls.Add(LUsuarioRol);
            pnlInfoUsuario.Controls.Add(LUsuarioEmail);
            pnlInfoUsuario.Controls.Add(LUsuarioNombre);
            pnlInfoUsuario.Location = new Point(20, 494);
            pnlInfoUsuario.Margin = new Padding(2, 2, 2, 2);
            pnlInfoUsuario.Name = "pnlInfoUsuario";
            pnlInfoUsuario.Size = new Size(178, 106);
            pnlInfoUsuario.TabIndex = 10;
            // 
            // LUsuarioRol
            // 
            LUsuarioRol.BackColor = Color.FromArgb(230, 240, 230);
            LUsuarioRol.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LUsuarioRol.ForeColor = Color.FromArgb(30, 30, 30);
            LUsuarioRol.Location = new Point(8, 52);
            LUsuarioRol.Margin = new Padding(2, 0, 2, 0);
            LUsuarioRol.Name = "LUsuarioRol";
            LUsuarioRol.Size = new Size(160, 20);
            LUsuarioRol.TabIndex = 2;
            LUsuarioRol.Text = "Sin rol";
            LUsuarioRol.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // LUsuarioEmail
            // 
            LUsuarioEmail.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            LUsuarioEmail.ForeColor = Color.DimGray;
            LUsuarioEmail.Location = new Point(8, 29);
            LUsuarioEmail.Margin = new Padding(2, 0, 2, 0);
            LUsuarioEmail.Name = "LUsuarioEmail";
            LUsuarioEmail.Size = new Size(160, 17);
            LUsuarioEmail.TabIndex = 1;
            LUsuarioEmail.Text = "—";
            LUsuarioEmail.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LUsuarioNombre
            // 
            LUsuarioNombre.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LUsuarioNombre.Location = new Point(8, 8);
            LUsuarioNombre.Margin = new Padding(2, 0, 2, 0);
            LUsuarioNombre.Name = "LUsuarioNombre";
            LUsuarioNombre.Size = new Size(160, 18);
            LUsuarioNombre.TabIndex = 0;
            LUsuarioNombre.Text = "Invitado";
            LUsuarioNombre.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // LTituloLogo
            // 
            LTituloLogo.AutoSize = true;
            LTituloLogo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LTituloLogo.Location = new Point(70, 11);
            LTituloLogo.Margin = new Padding(2, 0, 2, 0);
            LTituloLogo.Name = "LTituloLogo";
            LTituloLogo.Size = new Size(134, 30);
            LTituloLogo.TabIndex = 9;
            LTituloLogo.Text = "InmoTech";
            // 
            // pictureBox1
            // 
            pictureBox1.BackgroundImage = Properties.Resources.logoImnoTech;
            pictureBox1.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(66, 58);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // BReportes
            // 
            BReportes.Location = new Point(30, 413);
            BReportes.Name = "BReportes";
            BReportes.Size = new Size(168, 47);
            BReportes.TabIndex = 7;
            BReportes.Text = "Reportes";
            BReportes.UseVisualStyleBackColor = true;
            BReportes.Click += BReportes_Click;
            // 
            // BPagos
            // 
            BPagos.Location = new Point(30, 359);
            BPagos.Name = "BPagos";
            BPagos.Size = new Size(168, 47);
            BPagos.TabIndex = 6;
            BPagos.Text = "Pagos";
            BPagos.UseVisualStyleBackColor = true;
            BPagos.Click += BPagos_Click;
            // 
            // BContratos
            // 
            BContratos.Location = new Point(30, 306);
            BContratos.Name = "BContratos";
            BContratos.Size = new Size(168, 47);
            BContratos.TabIndex = 5;
            BContratos.Text = "Contratos";
            BContratos.UseVisualStyleBackColor = true;
            BContratos.Click += BContratos_Click;
            // 
            // BInquilinos
            // 
            BInquilinos.Location = new Point(30, 252);
            BInquilinos.Name = "BInquilinos";
            BInquilinos.Size = new Size(168, 47);
            BInquilinos.TabIndex = 4;
            BInquilinos.Text = "Inquilinos";
            BInquilinos.UseVisualStyleBackColor = true;
            BInquilinos.Click += BInquilinos_Click;
            // 
            // BDashboard
            // 
            BDashboard.BackColor = SystemColors.Window;
            BDashboard.FlatStyle = FlatStyle.Flat;
            BDashboard.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BDashboard.ImageAlign = ContentAlignment.MiddleLeft;
            BDashboard.Location = new Point(30, 90);
            BDashboard.Name = "BDashboard";
            BDashboard.Padding = new Padding(10, 0, 10, 0);
            BDashboard.Size = new Size(168, 47);
            BDashboard.TabIndex = 3;
            BDashboard.Text = "Dashboard";
            BDashboard.TextImageRelation = TextImageRelation.ImageBeforeText;
            BDashboard.UseVisualStyleBackColor = false;
            BDashboard.Click += BDashboard_Click;
            // 
            // BUsuarios
            // 
            BUsuarios.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BUsuarios.Location = new Point(30, 144);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(168, 47);
            BUsuarios.TabIndex = 2;
            BUsuarios.Text = "Usuarios";
            BUsuarios.UseVisualStyleBackColor = true;
            BUsuarios.Click += BUsuarios_Click;
            // 
            // BInmuebles
            // 
            BInmuebles.Location = new Point(30, 198);
            BInmuebles.Name = "BInmuebles";
            BInmuebles.Size = new Size(168, 47);
            BInmuebles.TabIndex = 1;
            BInmuebles.Text = "Inmuebles";
            BInmuebles.UseVisualStyleBackColor = true;
            BInmuebles.Click += BInmuebles_Click;
            // 
            // BBackup
            // 
            BBackup.Location = new Point(20, 616);
            BBackup.Name = "BBackup";
            BBackup.Size = new Size(178, 47);
            BBackup.TabIndex = 11;
            BBackup.Text = "Backup";
            BBackup.UseVisualStyleBackColor = true;
            // 
            // BSalir
            // 
            BSalir.BackColor = Color.White;
            BSalir.ImageAlign = ContentAlignment.MiddleLeft;
            BSalir.Location = new Point(20, 670);
            BSalir.Name = "BSalir";
            BSalir.Size = new Size(178, 47);
            BSalir.TabIndex = 0;
            BSalir.Text = "Salir";
            BSalir.UseVisualStyleBackColor = false;
            BSalir.Click += BSalir_Click;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.White;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 17.9808846F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 82.01912F));
            tableLayoutPanel1.Controls.Add(PanelLateral, 0, 0);
            tableLayoutPanel1.Controls.Add(pnlContent, 1, 0);
            tableLayoutPanel1.Location = new Point(1, -5);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1366, 738);
            tableLayoutPanel1.TabIndex = 1;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 19F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1363, 729);
            Controls.Add(tableLayoutPanel1);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Gestor De Inmuebles";
            PanelLateral.ResumeLayout(false);
            PanelLateral.PerformLayout();
            pnlInfoUsuario.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tableLayoutPanel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pnlContent;
        private Panel PanelLateral;
        private Panel pnlInfoUsuario;
        private Label LUsuarioNombre;
        private Label LUsuarioEmail;
        private Label LUsuarioRol;
        private Button BReportes;
        private Button BPagos;
        private Button BContratos;
        private Button BInquilinos;
        private Button BDashboard;
        private Button BUsuarios;
        private Button BInmuebles;
        private Button BBackup;   // ⬅️ NUEVO
        private Button BSalir;
        private TableLayoutPanel tableLayoutPanel1;
        private PictureBox pictureBox1;
        private Label LTituloLogo;
    }
}
