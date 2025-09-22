using System.Drawing;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    partial class UcReportesOperador
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            panel1 = new Panel();
            panel2 = new Panel();
            dataGridView1 = new DataGridView();
            lblTitulo = new Label();
            dtpDia = new DateTimePicker();
            panel3 = new Panel();
            panel4 = new Panel();
            panel5 = new Panel();
            panel6 = new Panel();
            label4 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            LContratos = new Label();
            LPagos = new Label();
            LIngreso = new Label();
            LRecibos = new Label();
            label5 = new Label();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel6.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Teal;
            panel1.BorderStyle = BorderStyle.Fixed3D;
            panel1.Controls.Add(lblTitulo);
            panel1.Location = new Point(15, 16);
            panel1.Name = "panel1";
            panel1.Size = new Size(1170, 70);
            panel1.TabIndex = 0;
            // Responsive
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Teal;
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(panel6);
            panel2.Controls.Add(panel5);
            panel2.Controls.Add(panel4);
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(dtpDia);
            panel2.Location = new Point(15, 106);
            panel2.Name = "panel2";
            panel2.Size = new Size(1170, 199);
            panel2.TabIndex = 1;
            // Responsive
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = Color.Teal;
            dataGridView1.BorderStyle = BorderStyle.Fixed3D;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(15, 372);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(1170, 310);
            dataGridView1.TabIndex = 2;
            // Responsive
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitulo.Location = new Point(19, 13);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(448, 37);
            lblTitulo.TabIndex = 2;
            lblTitulo.Text = "Reportes Diarios (Operador)";
            // Responsive (se mantiene a la izquierda)
            lblTitulo.Anchor = AnchorStyles.Left | AnchorStyles.Top;
            // 
            // dtpDia
            // 
            dtpDia.Location = new Point(19, 19);
            dtpDia.Name = "dtpDia";
            dtpDia.Size = new Size(358, 31);
            dtpDia.TabIndex = 0;
            // Responsive
            dtpDia.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // panel3
            // 
            panel3.BackColor = Color.WhiteSmoke;
            panel3.Controls.Add(LContratos);
            panel3.Controls.Add(label4);
            panel3.Location = new Point(29, 71);
            panel3.Name = "panel3";
            panel3.Size = new Size(230, 99);
            panel3.TabIndex = 1;
            // Responsive
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // panel4
            // 
            panel4.BackColor = Color.WhiteSmoke;
            panel4.Controls.Add(LPagos);
            panel4.Controls.Add(label1);
            panel4.Location = new Point(316, 71);
            panel4.Name = "panel4";
            panel4.Size = new Size(230, 99);
            panel4.TabIndex = 2;
            // Responsive
            panel4.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // panel5
            // 
            panel5.BackColor = Color.WhiteSmoke;
            panel5.Controls.Add(LIngreso);
            panel5.Controls.Add(label2);
            panel5.Location = new Point(595, 71);
            panel5.Name = "panel5";
            panel5.Size = new Size(230, 99);
            panel5.TabIndex = 3;
            // Responsive
            panel5.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // panel6
            // 
            panel6.BackColor = Color.WhiteSmoke;
            panel6.Controls.Add(LRecibos);
            panel6.Controls.Add(label3);
            panel6.Location = new Point(870, 71);
            panel6.Name = "panel6";
            panel6.Size = new Size(230, 99);
            panel6.TabIndex = 2;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.Location = new Point(15, 11);
            label4.Name = "label4";
            label4.Size = new Size(194, 25);
            label4.TabIndex = 1;
            label4.Text = "Contratos Creados";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(23, 11);
            label1.Name = "label1";
            label1.Size = new Size(185, 25);
            label1.TabIndex = 2;
            label1.Text = "Pagos Generados";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(32, 11);
            label2.Name = "label2";
            label2.Size = new Size(163, 25);
            label2.TabIndex = 3;
            label2.Text = "Total Ingresado";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(28, 11);
            label3.Name = "label3";
            label3.Size = new Size(177, 25);
            label3.TabIndex = 4;
            label3.Text = "Recibos Emitidos";
            // 
            // LContratos
            // 
            LContratos.AutoSize = true;
            LContratos.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LContratos.ForeColor = Color.ForestGreen;
            LContratos.Location = new Point(90, 46);
            LContratos.Name = "LContratos";
            LContratos.Size = new Size(38, 40);
            LContratos.TabIndex = 3;
            LContratos.Text = "4";
            // 
            // LPagos
            // 
            LPagos.AutoSize = true;
            LPagos.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LPagos.ForeColor = Color.ForestGreen;
            LPagos.Location = new Point(80, 46);
            LPagos.Name = "LPagos";
            LPagos.Size = new Size(59, 40);
            LPagos.TabIndex = 4;
            LPagos.Text = "17";
            // 
            // LIngreso
            // 
            LIngreso.AutoSize = true;
            LIngreso.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LIngreso.ForeColor = Color.ForestGreen;
            LIngreso.Location = new Point(20, 46);
            LIngreso.Name = "LIngreso";
            LIngreso.Size = new Size(207, 40);
            LIngreso.TabIndex = 5;
            LIngreso.Text = "142.432,44";
            // 
            // LRecibos
            // 
            LRecibos.AutoSize = true;
            LRecibos.Font = new Font("Microsoft Sans Serif", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            LRecibos.ForeColor = Color.ForestGreen;
            LRecibos.Location = new Point(81, 46);
            LRecibos.Name = "LRecibos";
            LRecibos.Size = new Size(59, 40);
            LRecibos.TabIndex = 6;
            LRecibos.Text = "14";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(15, 337);
            label5.Name = "label5";
            label5.Size = new Size(403, 32);
            label5.TabIndex = 3;
            label5.Text = "Contratos Pagados en el Dia";
            // Responsive
            label5.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            // 
            // UcReportesOperador
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label5);
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "UcReportesOperador";
            Size = new Size(1200, 700);
            // Responsive: tamaño mínimo para que no se superpongan
            MinimumSize = new Size(900, 600);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private DataGridView dataGridView1;
        private Label lblTitulo;
        private Panel panel6;
        private Panel panel5;
        private Panel panel4;
        private Panel panel3;
        private DateTimePicker dtpDia;
        private Label label3;
        private Label label2;
        private Label label1;
        private Label LContratos;
        private Label label4;
        private Label LRecibos;
        private Label LIngreso;
        private Label LPagos;
        private Label label5;
    }
}
