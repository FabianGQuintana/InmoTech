// UcBackup.Designer.cs  (layout corregido con TableLayoutPanel, sin solapamientos)
using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcBackup
    {
        private System.ComponentModel.IContainer components = null;

        private Label lblTitulo;

        // Card
        private Panel cardRuta;
        private TableLayoutPanel tlCard;
        private Label lblDestino;
        private TextBox txtDestino;
        private Button btnElegirCarpeta;

        private Label lblNombre;
        private TextBox txtNombre;
        private CheckBox chkAgregarFecha;

        private Label lblCompresion;
        private ComboBox cmbCompresion;
        private FlowLayoutPanel pnlChecks;   // contiene verificar + sobrescribir
        private CheckBox chkVerificar;
        private CheckBox chkSobrescribir;

        // Barra de acciones
        private TableLayoutPanel tlActions;
        private FlowLayoutPanel pnlBtns;
        private Button btnProbarRuta;
        private Button btnSimular;
        private Label lblEstado;

        // Preview
        private TextBox txtPreview;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        private void InitializeComponent()
        {
            lblTitulo = new Label();
            cardRuta = new Panel();
            tlCard = new TableLayoutPanel();
            lblDestino = new Label();
            txtDestino = new TextBox();
            btnElegirCarpeta = new Button();
            lblNombre = new Label();
            txtNombre = new TextBox();
            chkAgregarFecha = new CheckBox();
            lblCompresion = new Label();
            cmbCompresion = new ComboBox();
            pnlChecks = new FlowLayoutPanel();
            chkVerificar = new CheckBox();
            chkSobrescribir = new CheckBox();
            tlActions = new TableLayoutPanel();
            pnlBtns = new FlowLayoutPanel();
            btnProbarRuta = new Button();
            btnSimular = new Button();
            lblEstado = new Label();
            txtPreview = new TextBox();
            cardRuta.SuspendLayout();
            tlCard.SuspendLayout();
            pnlChecks.SuspendLayout();
            tlActions.SuspendLayout();
            pnlBtns.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.FromArgb(20, 60, 60);
            lblTitulo.Location = new Point(18, 18);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(396, 45);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Backup de Base de Datos";
            // 
            // cardRuta
            // 
            cardRuta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cardRuta.BackColor = Color.White;
            cardRuta.BorderStyle = BorderStyle.FixedSingle;
            cardRuta.Controls.Add(tlCard);
            cardRuta.Location = new Point(22, 64);
            cardRuta.Name = "cardRuta";
            cardRuta.Padding = new Padding(16);
            cardRuta.Size = new Size(1146, 220);
            cardRuta.TabIndex = 1;
            // 
            // tlCard
            // 
            tlCard.ColumnCount = 3;
            tlCard.ColumnStyles.Add(new ColumnStyle());
            tlCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlCard.ColumnStyles.Add(new ColumnStyle());
            tlCard.Controls.Add(lblDestino, 0, 0);
            tlCard.Controls.Add(txtDestino, 1, 0);
            tlCard.Controls.Add(btnElegirCarpeta, 2, 0);
            tlCard.Controls.Add(lblNombre, 0, 1);
            tlCard.Controls.Add(txtNombre, 1, 1);
            tlCard.Controls.Add(chkAgregarFecha, 2, 1);
            tlCard.Controls.Add(lblCompresion, 0, 2);
            tlCard.Controls.Add(cmbCompresion, 1, 2);
            tlCard.Controls.Add(pnlChecks, 2, 2);
            tlCard.Dock = DockStyle.Fill;
            tlCard.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
            tlCard.Location = new Point(16, 16);
            tlCard.Name = "tlCard";
            tlCard.Padding = new Padding(4);
            tlCard.RowCount = 3;
            tlCard.RowStyles.Add(new RowStyle());
            tlCard.RowStyles.Add(new RowStyle());
            tlCard.RowStyles.Add(new RowStyle());
            tlCard.Size = new Size(1112, 186);
            tlCard.TabIndex = 0;
            // 
            // lblDestino
            // 
            lblDestino.AutoSize = true;
            lblDestino.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDestino.ForeColor = Color.FromArgb(20, 60, 60);
            lblDestino.Location = new Point(12, 10);
            lblDestino.Margin = new Padding(8, 6, 12, 6);
            lblDestino.Name = "lblDestino";
            lblDestino.Size = new Size(161, 28);
            lblDestino.TabIndex = 0;
            lblDestino.Text = "Carpeta destino";
            // 
            // txtDestino
            // 
            txtDestino.Dock = DockStyle.Fill;
            txtDestino.Location = new Point(224, 10);
            txtDestino.Margin = new Padding(0, 6, 8, 6);
            txtDestino.Name = "txtDestino";
            txtDestino.PlaceholderText = "Ej: C:\\Backups\\InmoTech";
            txtDestino.Size = new Size(499, 31);
            txtDestino.TabIndex = 1;
            // 
            // btnElegirCarpeta
            // 
            btnElegirCarpeta.AutoSize = true;
            btnElegirCarpeta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnElegirCarpeta.Location = new Point(731, 8);
            btnElegirCarpeta.Margin = new Padding(0, 4, 0, 4);
            btnElegirCarpeta.Name = "btnElegirCarpeta";
            btnElegirCarpeta.Size = new Size(78, 35);
            btnElegirCarpeta.TabIndex = 2;
            btnElegirCarpeta.Text = "Elegir…";
            // 
            // lblNombre
            // 
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNombre.ForeColor = Color.FromArgb(20, 60, 60);
            lblNombre.Location = new Point(12, 53);
            lblNombre.Margin = new Padding(8, 6, 12, 6);
            lblNombre.Name = "lblNombre";
            lblNombre.Size = new Size(200, 28);
            lblNombre.TabIndex = 3;
            lblNombre.Text = "Nombre del archivo";
            // 
            // txtNombre
            // 
            txtNombre.Dock = DockStyle.Fill;
            txtNombre.Location = new Point(224, 53);
            txtNombre.Margin = new Padding(0, 6, 8, 6);
            txtNombre.Name = "txtNombre";
            txtNombre.PlaceholderText = "backup_inmotech.bak";
            txtNombre.Size = new Size(499, 31);
            txtNombre.TabIndex = 4;
            // 
            // chkAgregarFecha
            // 
            chkAgregarFecha.AutoSize = true;
            chkAgregarFecha.Location = new Point(731, 55);
            chkAgregarFecha.Margin = new Padding(0, 8, 0, 6);
            chkAgregarFecha.Name = "chkAgregarFecha";
            chkAgregarFecha.Size = new Size(149, 29);
            chkAgregarFecha.TabIndex = 5;
            chkAgregarFecha.Text = "Agregar fecha";
            // 
            // lblCompresion
            // 
            lblCompresion.AutoSize = true;
            lblCompresion.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCompresion.ForeColor = Color.FromArgb(20, 60, 60);
            lblCompresion.Location = new Point(12, 96);
            lblCompresion.Margin = new Padding(8, 6, 12, 6);
            lblCompresion.Name = "lblCompresion";
            lblCompresion.Size = new Size(124, 28);
            lblCompresion.TabIndex = 6;
            lblCompresion.Text = "Compresión";
            // 
            // cmbCompresion
            // 
            cmbCompresion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompresion.Items.AddRange(new object[] { "Ninguna", "Baja", "Media", "Alta" });
            cmbCompresion.Location = new Point(224, 96);
            cmbCompresion.Margin = new Padding(0, 6, 8, 6);
            cmbCompresion.Name = "cmbCompresion";
            cmbCompresion.Size = new Size(180, 33);
            cmbCompresion.TabIndex = 7;
            // 
            // pnlChecks
            // 
            pnlChecks.AutoSize = true;
            pnlChecks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlChecks.Controls.Add(chkVerificar);
            pnlChecks.Controls.Add(chkSobrescribir);
            pnlChecks.Location = new Point(731, 94);
            pnlChecks.Margin = new Padding(0, 4, 0, 4);
            pnlChecks.Name = "pnlChecks";
            pnlChecks.Size = new Size(377, 37);
            pnlChecks.TabIndex = 8;
            pnlChecks.WrapContents = false;
            // 
            // chkVerificar
            // 
            chkVerificar.AutoSize = true;
            chkVerificar.Location = new Point(0, 4);
            chkVerificar.Margin = new Padding(0, 4, 16, 4);
            chkVerificar.Name = "chkVerificar";
            chkVerificar.Size = new Size(163, 29);
            chkVerificar.TabIndex = 0;
            chkVerificar.Text = "Verificar backup";
            // 
            // chkSobrescribir
            // 
            chkSobrescribir.AutoSize = true;
            chkSobrescribir.Location = new Point(179, 4);
            chkSobrescribir.Margin = new Padding(0, 4, 0, 4);
            chkSobrescribir.Name = "chkSobrescribir";
            chkSobrescribir.Size = new Size(198, 29);
            chkSobrescribir.TabIndex = 1;
            chkSobrescribir.Text = "Sobrescribir si existe";
            // 
            // tlActions
            // 
            tlActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlActions.ColumnCount = 2;
            tlActions.ColumnStyles.Add(new ColumnStyle());
            tlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlActions.Controls.Add(pnlBtns, 0, 0);
            tlActions.Controls.Add(lblEstado, 1, 0);
            tlActions.Location = new Point(22, 292);
            tlActions.Name = "tlActions";
            tlActions.RowCount = 1;
            tlActions.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tlActions.Size = new Size(1146, 44);
            tlActions.TabIndex = 2;
            // 
            // pnlBtns
            // 
            pnlBtns.AutoSize = true;
            pnlBtns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlBtns.Controls.Add(btnProbarRuta);
            pnlBtns.Controls.Add(btnSimular);
            pnlBtns.Location = new Point(0, 0);
            pnlBtns.Margin = new Padding(0);
            pnlBtns.Name = "pnlBtns";
            pnlBtns.Size = new Size(204, 35);
            pnlBtns.TabIndex = 0;
            pnlBtns.WrapContents = false;
            // 
            // btnProbarRuta
            // 
            btnProbarRuta.AutoSize = true;
            btnProbarRuta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnProbarRuta.Location = new Point(0, 0);
            btnProbarRuta.Margin = new Padding(0, 0, 12, 0);
            btnProbarRuta.Name = "btnProbarRuta";
            btnProbarRuta.Size = new Size(111, 35);
            btnProbarRuta.TabIndex = 0;
            btnProbarRuta.Text = "Probar ruta";
            // 
            // btnSimular
            // 
            btnSimular.AutoSize = true;
            btnSimular.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSimular.Location = new Point(123, 0);
            btnSimular.Margin = new Padding(0);
            btnSimular.Name = "btnSimular";
            btnSimular.Size = new Size(81, 35);
            btnSimular.TabIndex = 1;
            btnSimular.Text = "Simular";
            // 
            // lblEstado
            // 
            lblEstado.AutoEllipsis = true;
            lblEstado.Dock = DockStyle.Fill;
            lblEstado.ForeColor = Color.DimGray;
            lblEstado.Location = new Point(216, 8);
            lblEstado.Margin = new Padding(12, 8, 0, 0);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(930, 36);
            lblEstado.TabIndex = 1;
            lblEstado.Text = "—";
            lblEstado.TextAlign = ContentAlignment.MiddleRight;
            // 
            // txtPreview
            // 
            txtPreview.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtPreview.BackColor = Color.FromArgb(247, 250, 250);
            txtPreview.BorderStyle = BorderStyle.FixedSingle;
            txtPreview.Font = new Font("Consolas", 10F);
            txtPreview.Location = new Point(22, 344);
            txtPreview.Multiline = true;
            txtPreview.Name = "txtPreview";
            txtPreview.ReadOnly = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.Size = new Size(1146, 112);
            txtPreview.TabIndex = 3;
            txtPreview.Text = "La previsualización de la simulación aparecerá aquí...";
            // 
            // UcBackup
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Controls.Add(lblTitulo);
            Controls.Add(cardRuta);
            Controls.Add(tlActions);
            Controls.Add(txtPreview);
            MinimumSize = new Size(680, 400);
            Name = "UcBackup";
            Padding = new Padding(18);
            Size = new Size(1190, 492);
            cardRuta.ResumeLayout(false);
            tlCard.ResumeLayout(false);
            tlCard.PerformLayout();
            pnlChecks.ResumeLayout(false);
            pnlChecks.PerformLayout();
            tlActions.ResumeLayout(false);
            tlActions.PerformLayout();
            pnlBtns.ResumeLayout(false);
            pnlBtns.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
