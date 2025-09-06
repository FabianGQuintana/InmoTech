namespace InmoTech
{
    partial class UcInquilinos
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
            label9 = new Label();
            panel2 = new Panel();
            label8 = new Label();
            button2 = new Button();
            button1 = new Button();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            dateTimePicker1 = new DateTimePicker();
            TEmail = new TextBox();
            TTelefono = new TextBox();
            TDireccion = new TextBox();
            TDni = new TextBox();
            TApellido = new TextBox();
            TNombre = new TextBox();
            dataGridView1 = new DataGridView();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(label9);
            panel1.Location = new Point(13, 21);
            panel1.Name = "panel1";
            panel1.Size = new Size(975, 70);
            panel1.TabIndex = 0;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Montserrat SemiBold", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(3, 11);
            label9.Name = "label9";
            label9.Size = new Size(184, 49);
            label9.TabIndex = 0;
            label9.Text = "Inquilinos";
            // 
            // panel2
            // 
            panel2.BackColor = Color.White;
            panel2.Controls.Add(label8);
            panel2.Controls.Add(button2);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(label7);
            panel2.Controls.Add(label6);
            panel2.Controls.Add(label5);
            panel2.Controls.Add(label4);
            panel2.Controls.Add(label3);
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(dateTimePicker1);
            panel2.Controls.Add(TEmail);
            panel2.Controls.Add(TTelefono);
            panel2.Controls.Add(TDireccion);
            panel2.Controls.Add(TDni);
            panel2.Controls.Add(TApellido);
            panel2.Controls.Add(TNombre);
            panel2.Font = new Font("Montserrat", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            panel2.Location = new Point(13, 106);
            panel2.Name = "panel2";
            panel2.Size = new Size(975, 231);
            panel2.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(265, 10);
            label8.Name = "label8";
            label8.Size = new Size(88, 28);
            label8.TabIndex = 16;
            label8.Text = "Telefono";
            // 
            // button2
            // 
            button2.Location = new Point(638, 172);
            button2.Name = "button2";
            button2.Size = new Size(112, 34);
            button2.TabIndex = 15;
            button2.Text = "Cancelar";
            button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(510, 172);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 14;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(510, 16);
            label7.Name = "label7";
            label7.Size = new Size(67, 28);
            label7.TabIndex = 13;
            label7.Text = "label7";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(265, 150);
            label6.Name = "label6";
            label6.Size = new Size(98, 28);
            label6.TabIndex = 12;
            label6.Text = "Direccion";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(265, 83);
            label5.Name = "label5";
            label5.Size = new Size(64, 28);
            label5.TabIndex = 11;
            label5.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(296, 16);
            label4.Name = "label4";
            label4.Size = new Size(0, 28);
            label4.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(16, 150);
            label3.Name = "label3";
            label3.Size = new Size(47, 28);
            label3.TabIndex = 9;
            label3.Text = "DNI";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(16, 83);
            label2.Name = "label2";
            label2.Size = new Size(86, 28);
            label2.TabIndex = 8;
            label2.Text = "Apellido";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(16, 10);
            label1.Name = "label1";
            label1.Size = new Size(87, 28);
            label1.TabIndex = 7;
            label1.Text = "Nombre";
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Format = DateTimePickerFormat.Short;
            dateTimePicker1.Location = new Point(510, 52);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(183, 29);
            dateTimePicker1.TabIndex = 6;
            // 
            // TEmail
            // 
            TEmail.Location = new Point(265, 114);
            TEmail.Name = "TEmail";
            TEmail.Size = new Size(183, 29);
            TEmail.TabIndex = 5;
            // 
            // TTelefono
            // 
            TTelefono.Location = new Point(265, 51);
            TTelefono.Name = "TTelefono";
            TTelefono.Size = new Size(183, 29);
            TTelefono.TabIndex = 4;
            // 
            // TDireccion
            // 
            TDireccion.Location = new Point(265, 177);
            TDireccion.Name = "TDireccion";
            TDireccion.Size = new Size(183, 29);
            TDireccion.TabIndex = 3;
            TDireccion.TextChanged += TDireccion_TextChanged;
            // 
            // TDni
            // 
            TDni.Location = new Point(17, 177);
            TDni.Name = "TDni";
            TDni.Size = new Size(183, 29);
            TDni.TabIndex = 2;
            // 
            // TApellido
            // 
            TApellido.Location = new Point(16, 114);
            TApellido.Name = "TApellido";
            TApellido.Size = new Size(184, 29);
            TApellido.TabIndex = 1;
            // 
            // TNombre
            // 
            TNombre.Location = new Point(16, 51);
            TNombre.Name = "TNombre";
            TNombre.Size = new Size(183, 29);
            TNombre.TabIndex = 0;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(13, 429);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.Size = new Size(975, 225);
            dataGridView1.TabIndex = 2;
            // 
            // UcInquilinos
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.WhiteSmoke;
            Controls.Add(dataGridView1);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "UcInquilinos";
            Size = new Size(1000, 700);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private DateTimePicker dateTimePicker1;
        private TextBox TEmail;
        private TextBox TTelefono;
        private TextBox TDireccion;
        private TextBox TDni;
        private TextBox TApellido;
        private TextBox TNombre;
        private Button button2;
        private Button button1;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private DataGridView dataGridView1;
        private Label label9;
        private Label label8;
    }
}
