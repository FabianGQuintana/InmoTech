using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace InmoTech.Controls
{
    internal static class UiTheme
    {
        // Paleta
        public static readonly Color Primary = Color.FromArgb(0, 153, 169);
        public static readonly Color PrimaryDark = Color.FromArgb(0, 123, 137);
        public static readonly Color Success = Color.FromArgb(0, 166, 81);
        public static readonly Color Warning = Color.FromArgb(255, 166, 0);
        public static readonly Color Danger = Color.FromArgb(203, 67, 53);
        public static readonly Color Paper = Color.FromArgb(245, 247, 250);
        public static readonly Color CardBorder = Color.FromArgb(210, 215, 219);
        public static readonly Color TextMain = Color.FromArgb(40, 40, 40);

        // Para pantallas con 125% / 150% / 175%
        public static void EnableHighDpi(ContainerControl c)
        {
            // Estas propiedades existen en ContainerControl (UserControl/Form), no en Control
            c.AutoScaleMode = AutoScaleMode.Dpi;
            c.AutoScaleDimensions = new SizeF(96f, 96f); // base 100%

            // Fuente y fondo base (opcional)
            c.Font = new Font("Segoe UI", 9.5f, FontStyle.Regular);
            c.BackColor = Paper;
        }


        public static Button PrimaryButton(string text)
        {
            var b = new Button
            {
                Text = text,
                BackColor = Primary,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Height = 38,
                Width = 150,
                Margin = new Padding(6)
            };
            b.FlatAppearance.BorderSize = 0;
            b.FlatAppearance.MouseOverBackColor = PrimaryDark;
            b.Cursor = Cursors.Hand;
            return b;
        }

        public static Panel KpiCard(string title, out Label valueLabel, Color? valueColor = null)
        {
            var panel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(16, 12, 16, 12),
                Margin = new Padding(8),
                MinimumSize = new Size(280, 120)
            };

            var lblTitle = new Label
            {
                Text = title,
                AutoSize = true,
                Font = new Font("Segoe UI", 11, FontStyle.Regular),
                ForeColor = TextMain,
                Location = new Point(6, 6)
            };

            valueLabel = new Label
            {
                Text = "—",
                AutoSize = true,                        // 👈 evita recortes en DPI altos
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = valueColor ?? TextMain
            };

            // ⚠️ NO usar "valueLabel" (out) dentro de lambdas/funciones locales
            //    Usamos una copia local:
            var val = valueLabel;

            // Posición inicial + reposición en Resize (sin Dock)
            void Place()
            {
                val.Location = new Point(10, Math.Max(40, lblTitle.Bottom + 10));
            }
            Place();
            panel.Resize += (s, e) => Place();

            panel.Controls.Add(val);
            panel.Controls.Add(lblTitle);
            return panel;
        }



        public static void StyleGrid(DataGridView grid, Color headerColor)
        {
            grid.SuspendLayout();

            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.AutoGenerateColumns = true;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.RowHeadersVisible = false;
            grid.Dock = DockStyle.Fill;
            grid.Margin = new Padding(0);

            grid.ColumnHeadersDefaultCellStyle.BackColor = headerColor;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersHeight = 36;

            grid.DefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Regular);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 247, 249);
            grid.DefaultCellStyle.SelectionForeColor = TextMain;
            grid.GridColor = CardBorder;

            // Double buffer (reduce parpadeo)
            grid.GetType().GetProperty("DoubleBuffered",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)!
                .SetValue(grid, true, null);

            grid.ResumeLayout();
        }
    }

    internal static class GridExporter
    {
        // Exportación rápida a CSV (Excel lo abre directo)
        public static void ExportToCsv(DataGridView grid, string suggestedFileName = "reporte.csv")
        {
            using var sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = suggestedFileName
            };
            if (sfd.ShowDialog() != DialogResult.OK) return;

            var sb = new StringBuilder();

            // Encabezados
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                if (i > 0) sb.Append(';');
                sb.Append(Escape(grid.Columns[i].HeaderText));
            }
            sb.AppendLine();

            // Filas
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;
                for (int i = 0; i < grid.Columns.Count; i++)
                {
                    if (i > 0) sb.Append(';');
                    var val = row.Cells[i].Value?.ToString() ?? "";
                    sb.Append(Escape(val));
                }
                sb.AppendLine();
            }

            File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);
            MessageBox.Show("Archivo exportado correctamente.", "Exportación", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static string Escape(string s)
        {
            if (s.Contains(';') || s.Contains('"') || s.Contains('\n'))
                return "\"" + s.Replace("\"", "\"\"") + "\"";
            return s;
        }
    }
}
