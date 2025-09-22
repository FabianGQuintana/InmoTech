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
            components = new System.ComponentModel.Container();

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

            SuspendLayout();

            // ===== Root =====
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            Dock = DockStyle.Fill;
            Padding = new Padding(18);
            MinimumSize = new Size(680, 400);

            // ===== Título =====
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            lblTitulo.ForeColor = Color.FromArgb(20, 60, 60);
            lblTitulo.Location = new Point(18, 18);
            lblTitulo.Text = "Backup de Base de Datos";

            // ===== Card =====
            cardRuta.BackColor = Color.White;
            cardRuta.BorderStyle = BorderStyle.FixedSingle;
            cardRuta.Padding = new Padding(16);
            cardRuta.Location = new Point(22, 64);
            cardRuta.Size = new Size(860, 220);
            cardRuta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // TableLayout de la card
            tlCard.ColumnCount = 3;
            tlCard.RowCount = 3;
            tlCard.Dock = DockStyle.Fill;
            tlCard.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Etiquetas
            tlCard.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));     // Campo elástico
            tlCard.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));          // Botón / checks
            tlCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));                // Destino
            tlCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));                // Nombre
            tlCard.RowStyles.Add(new RowStyle(SizeType.AutoSize));                // Compresión + checks
            tlCard.Padding = new Padding(4);
            tlCard.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;

            // —— Destino
            lblDestino.AutoSize = true;
            lblDestino.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblDestino.ForeColor = Color.FromArgb(20, 60, 60);
            lblDestino.Margin = new Padding(8, 6, 12, 6);
            lblDestino.Text = "Carpeta destino";

            txtDestino.Margin = new Padding(0, 6, 8, 6);
            txtDestino.Dock = DockStyle.Fill;
            txtDestino.PlaceholderText = "Ej: C:\\Backups\\InmoTech";

            btnElegirCarpeta.AutoSize = true;
            btnElegirCarpeta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnElegirCarpeta.Margin = new Padding(0, 4, 0, 4);
            btnElegirCarpeta.Text = "Elegir…";

            // —— Nombre
            lblNombre.AutoSize = true;
            lblNombre.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblNombre.ForeColor = Color.FromArgb(20, 60, 60);
            lblNombre.Margin = new Padding(8, 6, 12, 6);
            lblNombre.Text = "Nombre del archivo";

            txtNombre.Margin = new Padding(0, 6, 8, 6);
            txtNombre.Dock = DockStyle.Fill;
            txtNombre.PlaceholderText = "backup_inmotech.bak";

            chkAgregarFecha.AutoSize = true;
            chkAgregarFecha.Margin = new Padding(0, 8, 0, 6);
            chkAgregarFecha.Text = "Agregar fecha";

            // —— Compresión + checks
            lblCompresion.AutoSize = true;
            lblCompresion.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            lblCompresion.ForeColor = Color.FromArgb(20, 60, 60);
            lblCompresion.Margin = new Padding(8, 6, 12, 6);
            lblCompresion.Text = "Compresión";

            cmbCompresion.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbCompresion.Items.AddRange(new object[] { "Ninguna", "Baja", "Media", "Alta" });
            cmbCompresion.Margin = new Padding(0, 6, 8, 6);
            cmbCompresion.Width = 180; // ancho razonable; el resto del espacio queda al textbox superior

            pnlChecks.FlowDirection = FlowDirection.LeftToRight;
            pnlChecks.WrapContents = false;
            pnlChecks.AutoSize = true;
            pnlChecks.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlChecks.Margin = new Padding(0, 4, 0, 4);
            pnlChecks.Padding = new Padding(0);

            chkVerificar.AutoSize = true;
            chkVerificar.Margin = new Padding(0, 4, 16, 4);
            chkVerificar.Text = "Verificar backup";

            chkSobrescribir.AutoSize = true;
            chkSobrescribir.Margin = new Padding(0, 4, 0, 4);
            chkSobrescribir.Text = "Sobrescribir si existe";

            pnlChecks.Controls.Add(chkVerificar);
            pnlChecks.Controls.Add(chkSobrescribir);

            // Agregar filas a tlCard
            tlCard.Controls.Add(lblDestino, 0, 0);
            tlCard.Controls.Add(txtDestino, 1, 0);
            tlCard.Controls.Add(btnElegirCarpeta, 2, 0);

            tlCard.Controls.Add(lblNombre, 0, 1);
            tlCard.Controls.Add(txtNombre, 1, 1);
            tlCard.Controls.Add(chkAgregarFecha, 2, 1);

            tlCard.Controls.Add(lblCompresion, 0, 2);
            tlCard.Controls.Add(cmbCompresion, 1, 2);
            tlCard.Controls.Add(pnlChecks, 2, 2);

            cardRuta.Controls.Add(tlCard);

            // ===== Barra de acciones =====
            tlActions.ColumnCount = 2;
            tlActions.RowCount = 1;
            tlActions.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));     // botones
            tlActions.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F)); // estado ocupa resto, alineado der
            tlActions.Dock = DockStyle.None;
            tlActions.Location = new Point(22, 292);
            tlActions.Size = new Size(860, 44);
            tlActions.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlActions.Padding = new Padding(0);

            pnlBtns.FlowDirection = FlowDirection.LeftToRight;
            pnlBtns.WrapContents = false;
            pnlBtns.AutoSize = true;
            pnlBtns.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pnlBtns.Margin = new Padding(0, 0, 0, 0);

            btnProbarRuta.AutoSize = true;
            btnProbarRuta.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnProbarRuta.Margin = new Padding(0, 0, 12, 0);
            btnProbarRuta.Text = "Probar ruta";

            btnSimular.AutoSize = true;
            btnSimular.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            btnSimular.Margin = new Padding(0);
            btnSimular.Text = "Simular";

            pnlBtns.Controls.Add(btnProbarRuta);
            pnlBtns.Controls.Add(btnSimular);

            lblEstado.AutoEllipsis = true;
            lblEstado.ForeColor = Color.DimGray;
            lblEstado.Dock = DockStyle.Fill;
            lblEstado.TextAlign = ContentAlignment.MiddleRight;
            lblEstado.Margin = new Padding(12, 8, 0, 0);
            lblEstado.Text = "—";

            tlActions.Controls.Add(pnlBtns, 0, 0);
            tlActions.Controls.Add(lblEstado, 1, 0);

            // ===== Preview =====
            txtPreview.BorderStyle = BorderStyle.FixedSingle;
            txtPreview.Multiline = true;
            txtPreview.ScrollBars = ScrollBars.Vertical;
            txtPreview.ReadOnly = true;
            txtPreview.Font = new Font("Consolas", 10F, FontStyle.Regular, GraphicsUnit.Point);
            txtPreview.BackColor = Color.FromArgb(247, 250, 250);
            txtPreview.Location = new Point(22, 344);
            txtPreview.Size = new Size(860, 310);
            txtPreview.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
            txtPreview.Text = "La previsualización de la simulación aparecerá aquí...";

            // ===== Compose =====
            Controls.Add(lblTitulo);
            Controls.Add(cardRuta);
            Controls.Add(tlActions);
            Controls.Add(txtPreview);

            Name = "UcBackup";
            Size = new Size(904, 690);

            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
    }
}
