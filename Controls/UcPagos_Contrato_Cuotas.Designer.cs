using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcPagos_Contrato_Cuotas
    {
        private System.ComponentModel.IContainer components = null;
        private Label lblContrato;
        private Label lblInquilino;
        private Label lblInmueble;
        private Label lblFechas;
        private Label lblTotalTitulo;
        private Label lblTotal;
        private Label lblAtrasoTitulo;
        private Label lblAtraso;
        private Label lblCuotasTitulo;
        private Label lblCuotas;
        private DataGridView dgvCuotas;
        private Button btnVolver;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            lblContrato = new Label();
            lblInquilino = new Label();
            lblInmueble = new Label();
            lblFechas = new Label();
            lblTotalTitulo = new Label();
            lblTotal = new Label();
            lblAtrasoTitulo = new Label();
            lblAtraso = new Label();
            lblCuotasTitulo = new Label();
            lblCuotas = new Label();
            dgvCuotas = new DataGridView();
            btnVolver = new Button();

            SuspendLayout();

            // ========== Labels cabecera ==========
            lblContrato.AutoSize = true;
            lblContrato.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblContrato.Location = new Point(20, 15);

            lblInquilino.AutoSize = true;
            lblInquilino.Location = new Point(25, 55);
            lblInquilino.Font = new Font("Segoe UI", 10F);

            lblInmueble.AutoSize = true;
            lblInmueble.Location = new Point(25, 80);
            lblInmueble.Font = new Font("Segoe UI", 10F);

            lblFechas.AutoSize = true;
            lblFechas.Location = new Point(25, 105);
            lblFechas.Font = new Font("Segoe UI", 10F);

            lblTotalTitulo.Text = "Total";
            lblTotalTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotalTitulo.Location = new Point(700, 25);
            lblTotalTitulo.AutoSize = true;

            lblTotal.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblTotal.ForeColor = Color.Black;
            lblTotal.Location = new Point(700, 50);
            lblTotal.AutoSize = true;

            lblAtrasoTitulo.Text = "Atraso";
            lblAtrasoTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblAtrasoTitulo.Location = new Point(850, 25);
            lblAtrasoTitulo.AutoSize = true;

            lblAtraso.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblAtraso.ForeColor = Color.Red;
            lblAtraso.Location = new Point(850, 50);
            lblAtraso.AutoSize = true;

            lblCuotasTitulo.Text = "Cuotas";
            lblCuotasTitulo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblCuotasTitulo.Location = new Point(580, 25);
            lblCuotasTitulo.AutoSize = true;

            lblCuotas.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblCuotas.ForeColor = Color.Black;
            lblCuotas.Location = new Point(580, 50);
            lblCuotas.AutoSize = true;

            // ========== DataGridView ==========
            dgvCuotas.Location = new Point(25, 150);
            dgvCuotas.Size = new Size(1000, 400);
            dgvCuotas.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvCuotas.BackgroundColor = Color.White;
            dgvCuotas.BorderStyle = BorderStyle.FixedSingle;
            dgvCuotas.AllowUserToAddRows = false;
            dgvCuotas.AllowUserToDeleteRows = false;
            dgvCuotas.ReadOnly = true;
            dgvCuotas.RowHeadersVisible = false;
            dgvCuotas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCuotas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // ========== Botón volver ==========
            btnVolver.Text = "← Volver";
            btnVolver.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnVolver.BackColor = Color.WhiteSmoke;
            btnVolver.FlatStyle = FlatStyle.Flat;
            btnVolver.Size = new Size(100, 36);
            btnVolver.Location = new Point(25, 570);
            btnVolver.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnVolver.Click += btnVolver_Click;

            // ========== UcPagos_Contrato_Cuotas ==========
            AutoScaleMode = AutoScaleMode.None;
            BackColor = Color.White;
            Controls.Add(lblContrato);
            Controls.Add(lblInquilino);
            Controls.Add(lblInmueble);
            Controls.Add(lblFechas);
            Controls.Add(lblTotalTitulo);
            Controls.Add(lblTotal);
            Controls.Add(lblAtrasoTitulo);
            Controls.Add(lblAtraso);
            Controls.Add(lblCuotasTitulo);
            Controls.Add(lblCuotas);
            Controls.Add(dgvCuotas);
            Controls.Add(btnVolver);

            Name = "UcPagos_Contrato_Cuotas";
            Size = new Size(1080, 620);

            ResumeLayout(false);
            PerformLayout();
        }
    }
}
