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
            tableLayoutPanel1 = new TableLayoutPanel();
            PanelLateral = new Panel();
            BSalir = new Button();
            PanelPrincipal = new Panel();
            PanelDashboard = new Panel();
            BEditarPerfil = new Button();
            BCerrarSesion = new Button();
            LDashboard = new Label();
            BInmuebles = new Button();
            BUsuarios = new Button();
            BDashboard = new Button();
            BInquilinos = new Button();
            BContratos = new Button();
            BPagos = new Button();
            BReportes = new Button();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            LRol = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            PPropiedades = new Panel();
            pictureBox3 = new PictureBox();
            LPropiedades = new Label();
            label1 = new Label();
            LInquilinos = new Label();
            panel1 = new Panel();
            pictureBox4 = new PictureBox();
            panel2 = new Panel();
            panel3 = new Panel();
            LIngresoMes = new Label();
            pictureBox5 = new PictureBox();
            LPagos = new Label();
            LPagosPendientes = new Label();
            pictureBox6 = new PictureBox();
            LPropiedadesDisponibles = new Label();
            tableLayoutPanel3 = new TableLayoutPanel();
            PPropiedadesDisponibles1 = new Panel();
            pictureBox7 = new PictureBox();
            pictureBox8 = new PictureBox();
            panel4 = new Panel();
            panel5 = new Panel();
            pictureBox10 = new PictureBox();
            pictureBox11 = new PictureBox();
            LContratosDisponibles = new Label();
            tableLayoutPanel4 = new TableLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            PanelLateral.SuspendLayout();
            PanelPrincipal.SuspendLayout();
            PanelDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            tableLayoutPanel2.SuspendLayout();
            PPropiedades.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).BeginInit();
            tableLayoutPanel3.SuspendLayout();
            PPropiedadesDisponibles1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).BeginInit();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.BackColor = Color.Teal;
            tableLayoutPanel1.ColumnCount = 2;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 18.577076F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 81.42293F));
            tableLayoutPanel1.Controls.Add(PanelLateral, 0, 0);
            tableLayoutPanel1.Controls.Add(PanelPrincipal, 1, 0);
            tableLayoutPanel1.Location = new Point(12, 12);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(1339, 743);
            tableLayoutPanel1.TabIndex = 1;
            tableLayoutPanel1.Paint += tableLayoutPanel1_Paint;
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
            PanelLateral.Location = new Point(3, 3);
            PanelLateral.Name = "PanelLateral";
            PanelLateral.Size = new Size(242, 737);
            PanelLateral.TabIndex = 0;
            // 
            // BSalir
            // 
            BSalir.BackgroundImage = Properties.Resources.botonSalir;
            BSalir.Image = Properties.Resources.botonSalir;
            BSalir.ImageAlign = ContentAlignment.MiddleLeft;
            BSalir.Location = new Point(20, 665);
            BSalir.Name = "BSalir";
            BSalir.Size = new Size(178, 34);
            BSalir.TabIndex = 0;
            BSalir.Text = "Salir";
            BSalir.UseVisualStyleBackColor = true;
            // 
            // PanelPrincipal
            // 
            PanelPrincipal.BackColor = Color.Teal;
            PanelPrincipal.BackgroundImageLayout = ImageLayout.None;
            PanelPrincipal.Controls.Add(tableLayoutPanel4);
            PanelPrincipal.Controls.Add(LContratosDisponibles);
            PanelPrincipal.Controls.Add(tableLayoutPanel3);
            PanelPrincipal.Controls.Add(LPropiedadesDisponibles);
            PanelPrincipal.Controls.Add(tableLayoutPanel2);
            PanelPrincipal.Controls.Add(PanelDashboard);
            PanelPrincipal.Dock = DockStyle.Fill;
            PanelPrincipal.Location = new Point(251, 3);
            PanelPrincipal.Name = "PanelPrincipal";
            PanelPrincipal.Size = new Size(1085, 737);
            PanelPrincipal.TabIndex = 1;
            // 
            // PanelDashboard
            // 
            PanelDashboard.BorderStyle = BorderStyle.Fixed3D;
            PanelDashboard.Controls.Add(LRol);
            PanelDashboard.Controls.Add(pictureBox2);
            PanelDashboard.Controls.Add(BEditarPerfil);
            PanelDashboard.Controls.Add(BCerrarSesion);
            PanelDashboard.Controls.Add(LDashboard);
            PanelDashboard.Location = new Point(43, 20);
            PanelDashboard.Name = "PanelDashboard";
            PanelDashboard.Size = new Size(1023, 103);
            PanelDashboard.TabIndex = 0;
            // 
            // BEditarPerfil
            // 
            BEditarPerfil.Location = new Point(879, 52);
            BEditarPerfil.Name = "BEditarPerfil";
            BEditarPerfil.Size = new Size(127, 33);
            BEditarPerfil.TabIndex = 2;
            BEditarPerfil.Text = "Editar Perfil";
            BEditarPerfil.UseVisualStyleBackColor = true;
            // 
            // BCerrarSesion
            // 
            BCerrarSesion.BackColor = SystemColors.MenuText;
            BCerrarSesion.ForeColor = SystemColors.Control;
            BCerrarSesion.Location = new Point(879, 3);
            BCerrarSesion.Name = "BCerrarSesion";
            BCerrarSesion.Size = new Size(127, 38);
            BCerrarSesion.TabIndex = 1;
            BCerrarSesion.Text = "Cerrar Sesion";
            BCerrarSesion.UseVisualStyleBackColor = false;
            // 
            // LDashboard
            // 
            LDashboard.AutoSize = true;
            LDashboard.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LDashboard.Location = new Point(24, 30);
            LDashboard.Name = "LDashboard";
            LDashboard.Size = new Size(224, 41);
            LDashboard.TabIndex = 0;
            LDashboard.Text = "Dashboard";
            // 
            // BInmuebles
            // 
            BInmuebles.Location = new Point(30, 208);
            BInmuebles.Name = "BInmuebles";
            BInmuebles.Size = new Size(168, 29);
            BInmuebles.TabIndex = 1;
            BInmuebles.Text = "Inmuebles";
            BInmuebles.UseVisualStyleBackColor = true;
            // 
            // BUsuarios
            // 
            BUsuarios.Location = new Point(30, 151);
            BUsuarios.Name = "BUsuarios";
            BUsuarios.Size = new Size(168, 29);
            BUsuarios.TabIndex = 2;
            BUsuarios.Text = "Usuarios";
            BUsuarios.UseVisualStyleBackColor = true;
            // 
            // BDashboard
            // 
            BDashboard.Location = new Point(30, 94);
            BDashboard.Name = "BDashboard";
            BDashboard.Size = new Size(168, 29);
            BDashboard.TabIndex = 3;
            BDashboard.Text = "Dashboard";
            BDashboard.UseVisualStyleBackColor = true;
            // 
            // BInquilinos
            // 
            BInquilinos.Location = new Point(30, 260);
            BInquilinos.Name = "BInquilinos";
            BInquilinos.Size = new Size(168, 29);
            BInquilinos.TabIndex = 4;
            BInquilinos.Text = "Inquilinos";
            BInquilinos.UseVisualStyleBackColor = true;
            // 
            // BContratos
            // 
            BContratos.Location = new Point(30, 318);
            BContratos.Name = "BContratos";
            BContratos.Size = new Size(168, 29);
            BContratos.TabIndex = 5;
            BContratos.Text = "Contratos";
            BContratos.UseVisualStyleBackColor = true;
            // 
            // BPagos
            // 
            BPagos.Location = new Point(30, 371);
            BPagos.Name = "BPagos";
            BPagos.Size = new Size(168, 29);
            BPagos.TabIndex = 6;
            BPagos.Text = "Pagos";
            BPagos.UseVisualStyleBackColor = true;
            // 
            // BReportes
            // 
            BReportes.Location = new Point(30, 428);
            BReportes.Name = "BReportes";
            BReportes.Size = new Size(168, 29);
            BReportes.TabIndex = 7;
            BReportes.Text = "Reportes";
            BReportes.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.InmoTech_Logo;
            pictureBox1.Location = new Point(-2, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(244, 74);
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            pictureBox1.Click += pictureBox1_Click;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources.UsuarioIcon;
            pictureBox2.Location = new Point(522, 14);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(125, 62);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 3;
            pictureBox2.TabStop = false;
            // 
            // LRol
            // 
            LRol.AutoSize = true;
            LRol.Location = new Point(653, 56);
            LRol.Name = "LRol";
            LRol.Size = new Size(34, 20);
            LRol.TabIndex = 4;
            LRol.Text = "Rol:";
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.BackColor = Color.AliceBlue;
            tableLayoutPanel2.ColumnCount = 9;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 3.088803F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 96.9111938F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 37F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 233F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 27F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 223F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 204F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
            tableLayoutPanel2.Controls.Add(label1, 0, 0);
            tableLayoutPanel2.Controls.Add(PPropiedades, 1, 0);
            tableLayoutPanel2.Controls.Add(panel1, 3, 0);
            tableLayoutPanel2.Controls.Add(panel2, 5, 0);
            tableLayoutPanel2.Controls.Add(panel3, 7, 0);
            tableLayoutPanel2.Location = new Point(43, 164);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(1023, 163);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // PPropiedades
            // 
            PPropiedades.Controls.Add(LPropiedades);
            PPropiedades.Controls.Add(pictureBox3);
            PPropiedades.Dock = DockStyle.Fill;
            PPropiedades.Location = new Point(10, 3);
            PPropiedades.Name = "PPropiedades";
            PPropiedades.Size = new Size(232, 157);
            PPropiedades.TabIndex = 0;
            // 
            // pictureBox3
            // 
            pictureBox3.Image = Properties.Resources.HomeIcon;
            pictureBox3.Location = new Point(0, 30);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(98, 92);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.TabIndex = 0;
            pictureBox3.TabStop = false;
            // 
            // LPropiedades
            // 
            LPropiedades.AutoSize = true;
            LPropiedades.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LPropiedades.Location = new Point(3, 125);
            LPropiedades.Name = "LPropiedades";
            LPropiedades.Size = new Size(171, 28);
            LPropiedades.TabIndex = 1;
            LPropiedades.Text = "Propiedades";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(1, 28);
            label1.TabIndex = 2;
            label1.Text = "Propiedades";
            // 
            // LInquilinos
            // 
            LInquilinos.AutoSize = true;
            LInquilinos.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LInquilinos.Location = new Point(3, 125);
            LInquilinos.Name = "LInquilinos";
            LInquilinos.Size = new Size(143, 28);
            LInquilinos.TabIndex = 2;
            LInquilinos.Text = "Inquilinos";
            // 
            // panel1
            // 
            panel1.Controls.Add(pictureBox4);
            panel1.Controls.Add(LInquilinos);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(285, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(227, 157);
            panel1.TabIndex = 3;
            // 
            // pictureBox4
            // 
            pictureBox4.Image = Properties.Resources.InquilinosIcon;
            pictureBox4.Location = new Point(3, 30);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(98, 92);
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.TabIndex = 3;
            pictureBox4.TabStop = false;
            // 
            // panel2
            // 
            panel2.Controls.Add(pictureBox5);
            panel2.Controls.Add(LIngresoMes);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(545, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(217, 157);
            panel2.TabIndex = 4;
            // 
            // panel3
            // 
            panel3.Controls.Add(pictureBox6);
            panel3.Controls.Add(LPagosPendientes);
            panel3.Controls.Add(LPagos);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(801, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(198, 157);
            panel3.TabIndex = 5;
            // 
            // LIngresoMes
            // 
            LIngresoMes.AutoSize = true;
            LIngresoMes.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LIngresoMes.Location = new Point(3, 125);
            LIngresoMes.Name = "LIngresoMes";
            LIngresoMes.Size = new Size(221, 28);
            LIngresoMes.TabIndex = 3;
            LIngresoMes.Text = "Ingreso del mes";
            // 
            // pictureBox5
            // 
            pictureBox5.Image = Properties.Resources.DollarIcon;
            pictureBox5.Location = new Point(3, 30);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(98, 92);
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.TabIndex = 4;
            pictureBox5.TabStop = false;
            // 
            // LPagos
            // 
            LPagos.AutoSize = true;
            LPagos.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LPagos.Location = new Point(3, 90);
            LPagos.Name = "LPagos";
            LPagos.Size = new Size(90, 28);
            LPagos.TabIndex = 0;
            LPagos.Text = "Pagos";
            // 
            // LPagosPendientes
            // 
            LPagosPendientes.AutoSize = true;
            LPagosPendientes.Font = new Font("Verdana", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LPagosPendientes.Location = new Point(3, 125);
            LPagosPendientes.Name = "LPagosPendientes";
            LPagosPendientes.Size = new Size(154, 28);
            LPagosPendientes.TabIndex = 1;
            LPagosPendientes.Text = "Pendientes";
            // 
            // pictureBox6
            // 
            pictureBox6.Image = Properties.Resources.PagosPendientesIcon;
            pictureBox6.Location = new Point(-5, 0);
            pictureBox6.Name = "pictureBox6";
            pictureBox6.Size = new Size(98, 92);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.TabIndex = 5;
            pictureBox6.TabStop = false;
            // 
            // LPropiedadesDisponibles
            // 
            LPropiedadesDisponibles.AutoSize = true;
            LPropiedadesDisponibles.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LPropiedadesDisponibles.Location = new Point(33, 371);
            LPropiedadesDisponibles.Name = "LPropiedadesDisponibles";
            LPropiedadesDisponibles.Size = new Size(490, 41);
            LPropiedadesDisponibles.TabIndex = 2;
            LPropiedadesDisponibles.Text = "Propiedades Disponibles";
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 7;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 4.012346F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 95.9876556F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 33F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 280F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 66F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 296F));
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 45F));
            tableLayoutPanel3.Controls.Add(pictureBox8, 0, 0);
            tableLayoutPanel3.Controls.Add(PPropiedadesDisponibles1, 1, 0);
            tableLayoutPanel3.Controls.Add(panel4, 3, 0);
            tableLayoutPanel3.Controls.Add(panel5, 5, 0);
            tableLayoutPanel3.Location = new Point(43, 415);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 1;
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel3.Size = new Size(1023, 138);
            tableLayoutPanel3.TabIndex = 3;
            // 
            // PPropiedadesDisponibles1
            // 
            PPropiedadesDisponibles1.Controls.Add(pictureBox7);
            PPropiedadesDisponibles1.Dock = DockStyle.Fill;
            PPropiedadesDisponibles1.Location = new Point(15, 3);
            PPropiedadesDisponibles1.Name = "PPropiedadesDisponibles1";
            PPropiedadesDisponibles1.Size = new Size(284, 132);
            PPropiedadesDisponibles1.TabIndex = 0;
            // 
            // pictureBox7
            // 
            pictureBox7.Location = new Point(3, 3);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(125, 126);
            pictureBox7.TabIndex = 0;
            pictureBox7.TabStop = false;
            // 
            // pictureBox8
            // 
            pictureBox8.Location = new Point(3, 3);
            pictureBox8.Name = "pictureBox8";
            pictureBox8.Size = new Size(6, 126);
            pictureBox8.TabIndex = 1;
            pictureBox8.TabStop = false;
            // 
            // panel4
            // 
            panel4.Controls.Add(pictureBox11);
            panel4.Dock = DockStyle.Fill;
            panel4.Location = new Point(338, 3);
            panel4.Name = "panel4";
            panel4.Size = new Size(274, 132);
            panel4.TabIndex = 2;
            // 
            // panel5
            // 
            panel5.Controls.Add(pictureBox10);
            panel5.Dock = DockStyle.Fill;
            panel5.Location = new Point(684, 3);
            panel5.Name = "panel5";
            panel5.Size = new Size(290, 132);
            panel5.TabIndex = 3;
            // 
            // pictureBox10
            // 
            pictureBox10.Location = new Point(3, 3);
            pictureBox10.Name = "pictureBox10";
            pictureBox10.Size = new Size(139, 126);
            pictureBox10.TabIndex = 3;
            pictureBox10.TabStop = false;
            // 
            // pictureBox11
            // 
            pictureBox11.Location = new Point(0, 3);
            pictureBox11.Name = "pictureBox11";
            pictureBox11.Size = new Size(125, 126);
            pictureBox11.TabIndex = 4;
            pictureBox11.TabStop = false;
            // 
            // LContratosDisponibles
            // 
            LContratosDisponibles.AutoSize = true;
            LContratosDisponibles.Font = new Font("Verdana", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LContratosDisponibles.Location = new Point(33, 569);
            LContratosDisponibles.Name = "LContratosDisponibles";
            LContratosDisponibles.Size = new Size(425, 41);
            LContratosDisponibles.TabIndex = 4;
            LContratosDisponibles.Text = "Contratos Por Vencer";
            LContratosDisponibles.Click += label2_Click;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 5;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 221F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 196F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 134F));
            tableLayoutPanel4.Location = new Point(43, 609);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 2;
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel4.Size = new Size(1023, 125);
            tableLayoutPanel4.TabIndex = 5;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1363, 767);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Form1";
            Text = "Form1";
            tableLayoutPanel1.ResumeLayout(false);
            PanelLateral.ResumeLayout(false);
            PanelPrincipal.ResumeLayout(false);
            PanelPrincipal.PerformLayout();
            PanelDashboard.ResumeLayout(false);
            PanelDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            PPropiedades.ResumeLayout(false);
            PPropiedades.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox6).EndInit();
            tableLayoutPanel3.ResumeLayout(false);
            PPropiedadesDisponibles1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox8).EndInit();
            panel4.ResumeLayout(false);
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox10).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox11).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Panel PanelLateral;
        private Panel PanelPrincipal;
        private Panel PanelDashboard;
        private Label LDashboard;
        private Button BEditarPerfil;
        private Button BCerrarSesion;
        private Button BSalir;
        private Button BReportes;
        private Button BPagos;
        private Button BContratos;
        private Button BInquilinos;
        private Button BDashboard;
        private Button BUsuarios;
        private Button BInmuebles;
        private PictureBox pictureBox1;
        private Label LRol;
        private PictureBox pictureBox2;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel PPropiedades;
        private PictureBox pictureBox3;
        private Label label1;
        private Label LPropiedades;
        private Panel panel1;
        private PictureBox pictureBox4;
        private Label LInquilinos;
        private Panel panel2;
        private Panel panel3;
        private PictureBox pictureBox5;
        private Label LIngresoMes;
        private PictureBox pictureBox6;
        private Label LPagosPendientes;
        private Label LPagos;
        private TableLayoutPanel tableLayoutPanel3;
        private Panel PPropiedadesDisponibles1;
        private Label LPropiedadesDisponibles;
        private Label LContratosDisponibles;
        private PictureBox pictureBox8;
        private PictureBox pictureBox7;
        private Panel panel4;
        private PictureBox pictureBox11;
        private Panel panel5;
        private PictureBox pictureBox10;
        private TableLayoutPanel tableLayoutPanel4;
    }
}
