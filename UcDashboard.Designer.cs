// Archivo: UcDashboard.Designer.cs
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech
{
    partial class UcDashboard
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel tlRoot;
        private Panel pnlHeader;
        private Label lblTitulo;
        private Label lblUserName;
        private Label lblRol;

        private TableLayoutPanel tlKpis;
        private Panel kpi1;
        private Panel kpi2;
        private Panel kpi3;
        private Panel kpi4;
        private Label lblKpiPropCap;
        private Label lblKpiProp;
        private Label lblKpiInqCap;
        private Label lblKpiInq;
        private Label lblKpiIngresoCap;
        private Label lblKpiIngreso;
        private Label lblKpiPendCap;
        private Label lblKpiPend;

        private Label lblPropDisp;
        private FlowLayoutPanel flPropiedades;
        private Label lblContratos;
        private DataGridView dgvContratos;

        /// <inheritdoc/>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            tlRoot = new TableLayoutPanel();
            pnlHeader = new Panel();
            lblTitulo = new Label();
            lblUserName = new Label();
            lblRol = new Label();
            tlKpis = new TableLayoutPanel();
            kpi1 = new Panel();
            lblKpiProp = new Label();
            lblKpiPropCap = new Label();
            kpi2 = new Panel();
            lblKpiInq = new Label();
            lblKpiInqCap = new Label();
            kpi3 = new Panel();
            lblKpiIngreso = new Label();
            lblKpiIngresoCap = new Label();
            kpi4 = new Panel();
            lblKpiPend = new Label();
            lblKpiPendCap = new Label();
            lblPropDisp = new Label();
            flPropiedades = new FlowLayoutPanel();
            lblContratos = new Label();
            dgvContratos = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            tlRoot.SuspendLayout();
            pnlHeader.SuspendLayout();
            tlKpis.SuspendLayout();
            kpi1.SuspendLayout();
            kpi2.SuspendLayout();
            kpi3.SuspendLayout();
            kpi4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).BeginInit();
            SuspendLayout();
            // 
            // tlRoot
            // 
            tlRoot.BackColor = Color.WhiteSmoke;
            tlRoot.ColumnCount = 1;
            tlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlRoot.Controls.Add(pnlHeader, 0, 0);
            tlRoot.Controls.Add(tlKpis, 0, 1);
            tlRoot.Controls.Add(lblPropDisp, 0, 2);
            tlRoot.Controls.Add(flPropiedades, 0, 3);
            tlRoot.Controls.Add(lblContratos, 0, 4);
            tlRoot.Controls.Add(dgvContratos, 0, 5);
            tlRoot.Dock = DockStyle.Fill;
            tlRoot.Location = new Point(0, 0);
            tlRoot.Name = "tlRoot";
            tlRoot.RowCount = 6;
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 80F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 120F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 420F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
            tlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlRoot.Size = new Size(980, 680);
            tlRoot.TabIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTitulo);
            pnlHeader.Controls.Add(lblUserName);
            pnlHeader.Controls.Add(lblRol);
            pnlHeader.Dock = DockStyle.Fill;
            pnlHeader.Location = new Point(3, 3);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16);
            pnlHeader.Size = new Size(974, 74);
            pnlHeader.TabIndex = 0;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Montserrat", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(16, 24);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(231, 56);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Dashboard";
            lblTitulo.Click += lblTitulo_Click;
            // 
            // lblUserName
            // 
            lblUserName.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 10F);
            lblUserName.Location = new Point(1394, 16);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(79, 28);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Usuario";
            // 
            // lblRol
            // 
            lblRol.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblRol.AutoSize = true;
            lblRol.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblRol.Location = new Point(1394, 40);
            lblRol.Name = "lblRol";
            lblRol.Size = new Size(41, 25);
            lblRol.TabIndex = 2;
            lblRol.Text = "Rol:";
            // 
            // tlKpis
            // 
            tlKpis.ColumnCount = 4;
            tlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlKpis.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            tlKpis.Controls.Add(kpi1, 0, 0);
            tlKpis.Controls.Add(kpi2, 1, 0);
            tlKpis.Controls.Add(kpi3, 2, 0);
            tlKpis.Controls.Add(kpi4, 3, 0);
            tlKpis.Dock = DockStyle.Fill;
            tlKpis.Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tlKpis.Location = new Point(3, 83);
            tlKpis.Name = "tlKpis";
            tlKpis.Padding = new Padding(12);
            tlKpis.RowCount = 1;
            tlKpis.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlKpis.Size = new Size(974, 114);
            tlKpis.TabIndex = 1;
            // 
            // kpi1
            // 
            kpi1.BackColor = Color.White;
            kpi1.BorderStyle = BorderStyle.FixedSingle;
            kpi1.Controls.Add(lblKpiProp);
            kpi1.Controls.Add(lblKpiPropCap);
            kpi1.Dock = DockStyle.Fill;
            kpi1.Location = new Point(20, 20);
            kpi1.Margin = new Padding(8);
            kpi1.Name = "kpi1";
            kpi1.Padding = new Padding(12);
            kpi1.Size = new Size(221, 74);
            kpi1.TabIndex = 0;
            // 
            // lblKpiProp
            // 
            lblKpiProp.AutoSize = true;
            lblKpiProp.Dock = DockStyle.Top;
            lblKpiProp.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiProp.Location = new Point(12, 40);
            lblKpiProp.Name = "lblKpiProp";
            lblKpiProp.Size = new Size(33, 38);
            lblKpiProp.TabIndex = 0;
            lblKpiProp.Text = "0";
            // 
            // lblKpiPropCap
            // 
            lblKpiPropCap.AutoSize = true;
            lblKpiPropCap.Dock = DockStyle.Top;
            lblKpiPropCap.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiPropCap.Location = new Point(12, 12);
            lblKpiPropCap.Name = "lblKpiPropCap";
            lblKpiPropCap.Size = new Size(125, 28);
            lblKpiPropCap.TabIndex = 1;
            lblKpiPropCap.Text = "Propiedades";
            // 
            // kpi2
            // 
            kpi2.BackColor = Color.White;
            kpi2.BorderStyle = BorderStyle.FixedSingle;
            kpi2.Controls.Add(lblKpiInq);
            kpi2.Controls.Add(lblKpiInqCap);
            kpi2.Dock = DockStyle.Fill;
            kpi2.Location = new Point(257, 20);
            kpi2.Margin = new Padding(8);
            kpi2.Name = "kpi2";
            kpi2.Padding = new Padding(12);
            kpi2.Size = new Size(221, 74);
            kpi2.TabIndex = 1;
            // 
            // lblKpiInq
            // 
            lblKpiInq.AutoSize = true;
            lblKpiInq.Dock = DockStyle.Top;
            lblKpiInq.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiInq.Location = new Point(12, 40);
            lblKpiInq.Name = "lblKpiInq";
            lblKpiInq.Size = new Size(33, 38);
            lblKpiInq.TabIndex = 0;
            lblKpiInq.Text = "0";
            // 
            // lblKpiInqCap
            // 
            lblKpiInqCap.AutoSize = true;
            lblKpiInqCap.Dock = DockStyle.Top;
            lblKpiInqCap.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiInqCap.Location = new Point(12, 12);
            lblKpiInqCap.Name = "lblKpiInqCap";
            lblKpiInqCap.Size = new Size(101, 28);
            lblKpiInqCap.TabIndex = 1;
            lblKpiInqCap.Text = "Inquilinos";
            // 
            // kpi3
            // 
            kpi3.BackColor = Color.White;
            kpi3.BorderStyle = BorderStyle.FixedSingle;
            kpi3.Controls.Add(lblKpiIngreso);
            kpi3.Controls.Add(lblKpiIngresoCap);
            kpi3.Dock = DockStyle.Fill;
            kpi3.Location = new Point(494, 20);
            kpi3.Margin = new Padding(8);
            kpi3.Name = "kpi3";
            kpi3.Padding = new Padding(12);
            kpi3.Size = new Size(221, 74);
            kpi3.TabIndex = 2;
            // 
            // lblKpiIngreso
            // 
            lblKpiIngreso.AutoSize = true;
            lblKpiIngreso.Dock = DockStyle.Top;
            lblKpiIngreso.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiIngreso.Location = new Point(12, 40);
            lblKpiIngreso.Name = "lblKpiIngreso";
            lblKpiIngreso.Size = new Size(48, 38);
            lblKpiIngreso.TabIndex = 0;
            lblKpiIngreso.Text = "$0";
            // 
            // lblKpiIngresoCap
            // 
            lblKpiIngresoCap.AutoSize = true;
            lblKpiIngresoCap.Dock = DockStyle.Top;
            lblKpiIngresoCap.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiIngresoCap.Location = new Point(12, 12);
            lblKpiIngresoCap.Name = "lblKpiIngresoCap";
            lblKpiIngresoCap.Size = new Size(157, 28);
            lblKpiIngresoCap.TabIndex = 1;
            lblKpiIngresoCap.Text = "Ingreso del mes";
            // 
            // kpi4
            // 
            kpi4.BackColor = Color.White;
            kpi4.BorderStyle = BorderStyle.FixedSingle;
            kpi4.Controls.Add(lblKpiPend);
            kpi4.Controls.Add(lblKpiPendCap);
            kpi4.Dock = DockStyle.Fill;
            kpi4.Location = new Point(731, 20);
            kpi4.Margin = new Padding(8);
            kpi4.Name = "kpi4";
            kpi4.Padding = new Padding(12);
            kpi4.Size = new Size(223, 74);
            kpi4.TabIndex = 3;
            // 
            // lblKpiPend
            // 
            lblKpiPend.AutoSize = true;
            lblKpiPend.Dock = DockStyle.Top;
            lblKpiPend.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiPend.Location = new Point(12, 40);
            lblKpiPend.Name = "lblKpiPend";
            lblKpiPend.Size = new Size(33, 38);
            lblKpiPend.TabIndex = 0;
            lblKpiPend.Text = "0";
            // 
            // lblKpiPendCap
            // 
            lblKpiPendCap.AutoSize = true;
            lblKpiPendCap.Dock = DockStyle.Top;
            lblKpiPendCap.Font = new Font("Montserrat Medium", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblKpiPendCap.Location = new Point(12, 12);
            lblKpiPendCap.Name = "lblKpiPendCap";
            lblKpiPendCap.Size = new Size(175, 28);
            lblKpiPendCap.TabIndex = 1;
            lblKpiPendCap.Text = "Pagos pendientes";
            // 
            // lblPropDisp
            // 
            lblPropDisp.AutoSize = true;
            lblPropDisp.Dock = DockStyle.Fill;
            lblPropDisp.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblPropDisp.Location = new Point(3, 200);
            lblPropDisp.Name = "lblPropDisp";
            lblPropDisp.Padding = new Padding(12, 0, 0, 0);
            lblPropDisp.Size = new Size(974, 36);
            lblPropDisp.TabIndex = 2;
            lblPropDisp.Text = "Propiedades Disponibles";
            // 
            // flPropiedades
            // 
            flPropiedades.AutoScroll = true;
            flPropiedades.BackColor = Color.WhiteSmoke;
            flPropiedades.Dock = DockStyle.Fill;
            flPropiedades.Location = new Point(3, 239);
            flPropiedades.Name = "flPropiedades";
            flPropiedades.Padding = new Padding(8);
            flPropiedades.Size = new Size(974, 414);
            flPropiedades.TabIndex = 3;
            // 
            // lblContratos
            // 
            lblContratos.AutoSize = true;
            lblContratos.Dock = DockStyle.Fill;
            lblContratos.Font = new Font("Montserrat", 11.999999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblContratos.Location = new Point(3, 656);
            lblContratos.Name = "lblContratos";
            lblContratos.Padding = new Padding(12, 0, 0, 0);
            lblContratos.Size = new Size(974, 36);
            lblContratos.TabIndex = 4;
            lblContratos.Text = "Contratos Por Vencer";
            // 
            // dgvContratos
            // 
            dgvContratos.AllowUserToAddRows = false;
            dgvContratos.AllowUserToDeleteRows = false;
            dgvContratos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvContratos.BackgroundColor = Color.White;
            dgvContratos.ColumnHeadersHeight = 34;
            dgvContratos.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4 });
            dgvContratos.Dock = DockStyle.Fill;
            dgvContratos.Location = new Point(3, 695);
            dgvContratos.Name = "dgvContratos";
            dgvContratos.ReadOnly = true;
            dgvContratos.RowHeadersVisible = false;
            dgvContratos.RowHeadersWidth = 62;
            dgvContratos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvContratos.Size = new Size(974, 1);
            dgvContratos.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "Contrato";
            dataGridViewTextBoxColumn1.MinimumWidth = 8;
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Inquilino";
            dataGridViewTextBoxColumn2.MinimumWidth = 8;
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            dataGridViewTextBoxColumn3.HeaderText = "Inmueble";
            dataGridViewTextBoxColumn3.MinimumWidth = 8;
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            dataGridViewTextBoxColumn4.HeaderText = "Estado";
            dataGridViewTextBoxColumn4.MinimumWidth = 8;
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // UcDashboard
            // 
            Controls.Add(tlRoot);
            Name = "UcDashboard";
            Size = new Size(980, 680);
            tlRoot.ResumeLayout(false);
            tlRoot.PerformLayout();
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tlKpis.ResumeLayout(false);
            kpi1.ResumeLayout(false);
            kpi1.PerformLayout();
            kpi2.ResumeLayout(false);
            kpi2.PerformLayout();
            kpi3.ResumeLayout(false);
            kpi3.PerformLayout();
            kpi4.ResumeLayout(false);
            kpi4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvContratos).EndInit();
            ResumeLayout(false);
        }
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    }
}
