// UcInquilinos.Designer.cs
using System.ComponentModel;
using SWF = System.Windows.Forms;
using SD = System.Drawing;

namespace InmoTech
{
    partial class UcInquilinos
    {
        private IContainer components = null!;

        // Root layout
        private SWF.TableLayoutPanel root;
        private SWF.Label lblTitle;

        // Card Form
        private SWF.Panel cardForm;
        private SWF.TableLayoutPanel formGrid;
        private SWF.TextBox txtName;
        private SWF.TextBox txtLastName;
        private SWF.TextBox txtDni;
        private SWF.TextBox txtPhone;
        private SWF.TextBox txtEmail;
        private SWF.TextBox txtAddress;
        private SWF.DateTimePicker dtpDate;
        private SWF.CheckBox chkActive;
        private SWF.Button btnNew;
        private SWF.Button btnSave;
        private SWF.Button btnCancel;

        // Card List
        private SWF.Panel cardList;
        private SWF.TableLayoutPanel listHeader;
        private SWF.TextBox txtSearch;
        private SWF.ComboBox cbFilter;
        private SWF.Label lblCount;
        private SWF.DataGridView grid;

        private SWF.ErrorProvider err;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            err = new SWF.ErrorProvider();

            // === Root Table ===
            root = new SWF.TableLayoutPanel();
            root.ColumnCount = 1;
            root.RowCount = 3;
            root.Dock = SWF.DockStyle.Fill;
            root.Padding = new SWF.Padding(16);
            root.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize));          // header
            root.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize));          // form
            root.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.Percent, 100));      // list fill

            // Title
            lblTitle = new SWF.Label();
            lblTitle.Text = "Inquilinos";
            lblTitle.AutoSize = true;
            lblTitle.Margin = new SWF.Padding(0, 0, 0, 12);
            root.Controls.Add(lblTitle, 0, 0);

            // === Card Form ===
            cardForm = new SWF.Panel();
            cardForm.BackColor = SD.Color.White;
            cardForm.BorderStyle = SWF.BorderStyle.FixedSingle;
            cardForm.Dock = SWF.DockStyle.Top;
            cardForm.Padding = new SWF.Padding(12);
            cardForm.Margin = new SWF.Padding(0, 0, 0, 12);
            cardForm.AutoSize = true;
            cardForm.AutoSizeMode = SWF.AutoSizeMode.GrowAndShrink;


            // Grid de formulario (8 cols x 4 filas)
            formGrid = new SWF.TableLayoutPanel();
            formGrid.ColumnCount = 8;
            formGrid.RowCount = 4;
            formGrid.Dock = SWF.DockStyle.Top;
            formGrid.AutoSize = true;
            formGrid.GrowStyle = SWF.TableLayoutPanelGrowStyle.AddRows;

            // 👇 sin bucles aquí (Designer-friendly)
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));
            formGrid.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 12.5f));

            formGrid.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize)); // fila 0: labels 1ª línea
            formGrid.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize)); // fila 1: inputs 1ª línea
            formGrid.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize)); // fila 2: labels 2ª línea
            formGrid.RowStyles.Add(new SWF.RowStyle(SWF.SizeType.AutoSize)); // fila 3: inputs 2ª línea

            // ---- Fila 1: Nombre, Apellido, DNI, Teléfono ----
            formGrid.Controls.Add(new SWF.Label { Text = "Nombre", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 0, 0);
            txtName = new SWF.TextBox { Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 6) };
            formGrid.Controls.Add(txtName, 0, 1); formGrid.SetColumnSpan(txtName, 2);

            formGrid.Controls.Add(new SWF.Label { Text = "Apellido", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 2, 0);
            txtLastName = new SWF.TextBox { Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 6) };
            formGrid.Controls.Add(txtLastName, 2, 1); formGrid.SetColumnSpan(txtLastName, 2);

            formGrid.Controls.Add(new SWF.Label { Text = "DNI", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 4, 0);
            txtDni = new SWF.TextBox { MaxLength = 10, Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 6) };
            formGrid.Controls.Add(txtDni, 4, 1);

            formGrid.Controls.Add(new SWF.Label { Text = "Teléfono", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 5, 0);
            txtPhone = new SWF.TextBox { Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 0, 6) };
            formGrid.Controls.Add(txtPhone, 5, 1); formGrid.SetColumnSpan(txtPhone, 3);

            // ---- Fila 2: Email, Dirección, Fecha, Activo ----
            formGrid.Controls.Add(new SWF.Label { Text = "Email", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 0, 2);
            txtEmail = new SWF.TextBox { Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 0) };
            formGrid.Controls.Add(txtEmail, 0, 3); formGrid.SetColumnSpan(txtEmail, 3);

            formGrid.Controls.Add(new SWF.Label { Text = "Dirección", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 3, 2);
            txtAddress = new SWF.TextBox { Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 0) };
            formGrid.Controls.Add(txtAddress, 3, 3); formGrid.SetColumnSpan(txtAddress, 3);

            formGrid.Controls.Add(new SWF.Label { Text = "Fecha de entrada", AutoSize = true, Margin = new SWF.Padding(0, 0, 0, 2) }, 6, 2);
            dtpDate = new SWF.DateTimePicker { Format = SWF.DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy", Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right, Margin = new SWF.Padding(0, 0, 6, 0) };
            formGrid.Controls.Add(dtpDate, 6, 3);

            chkActive = new SWF.CheckBox { Text = "Activo", Checked = true, AutoSize = true, Anchor = SWF.AnchorStyles.Left, Margin = new SWF.Padding(0, 2, 0, 0) };
            formGrid.Controls.Add(chkActive, 7, 3);

            // Botones
            var btnPanel = new SWF.FlowLayoutPanel { Dock = SWF.DockStyle.Top, AutoSize = true, FlowDirection = SWF.FlowDirection.LeftToRight, Margin = new SWF.Padding(0, 8, 0, 0) };
            btnNew = new SWF.Button { Text = "Nuevo", Width = 90, FlatStyle = SWF.FlatStyle.Flat };
            btnSave = new SWF.Button { Text = "Guardar", Width = 110, FlatStyle = SWF.FlatStyle.Flat };
            btnCancel = new SWF.Button { Text = "Cancelar", Width = 110, FlatStyle = SWF.FlatStyle.Flat };
            btnPanel.Controls.AddRange(new SWF.Control[] { btnNew, btnSave, btnCancel });

            cardForm.Controls.Add(btnPanel);
            cardForm.Controls.Add(formGrid);

            root.Controls.Add(cardForm, 0, 1);

            // === Card List ===
            cardList = new SWF.Panel();
            cardList.BackColor = SD.Color.White;
            cardList.BorderStyle = SWF.BorderStyle.FixedSingle;
            cardList.Dock = SWF.DockStyle.Fill;
            cardList.Padding = new SWF.Padding(12);

            // Encabezado listado
            listHeader = new SWF.TableLayoutPanel();
            listHeader.ColumnCount = 4;
            listHeader.RowCount = 1;
            listHeader.Dock = SWF.DockStyle.Top;
            listHeader.AutoSize = true;
            listHeader.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.AutoSize));        // "Buscar"
            listHeader.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.Percent, 100f));   // textbox
            listHeader.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.AutoSize));        // combo
            listHeader.ColumnStyles.Add(new SWF.ColumnStyle(SWF.SizeType.AutoSize));        // contador

            listHeader.Controls.Add(new SWF.Label { Text = "Buscar", AutoSize = true, Anchor = SWF.AnchorStyles.Left }, 0, 0);
            txtSearch = new SWF.TextBox { Width = 260, Anchor = SWF.AnchorStyles.Left | SWF.AnchorStyles.Right };
            listHeader.Controls.Add(txtSearch, 1, 0);

            cbFilter = new SWF.ComboBox { DropDownStyle = SWF.ComboBoxStyle.DropDownList, Width = 130, Anchor = SWF.AnchorStyles.Left };
            listHeader.Controls.Add(cbFilter, 2, 0);

            lblCount = new SWF.Label { Text = "0 inquilino(s)", AutoSize = true, Anchor = SWF.AnchorStyles.Right, ForeColor = SD.Color.FromArgb(100, 116, 139) };
            listHeader.Controls.Add(lblCount, 3, 0);

            // Grid
            grid = new SWF.DataGridView { Dock = SWF.DockStyle.Fill };

            cardList.Controls.Add(grid);
            cardList.Controls.Add(listHeader);

            root.Controls.Add(cardList, 0, 2);

            // Control root
            this.Controls.Add(root);
            this.Name = "UcInquilinos";

            // ErrorProvider
            err.ContainerControl = this;
        }
    }
}
