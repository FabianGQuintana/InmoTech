using InmoTech.Models;
using InmoTech.Repositories;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel; // Asegúrate de tener instalado el paquete NuGet ClosedXML

namespace InmoTech.Controls
{
    public partial class UcReportesAdmin : UserControl
    {
        private readonly ReporteRepository _reporteRepo = new();
        // private readonly UsuarioRepository _usuarioRepo = new(); // No necesario si no filtramos creador

        public UcReportesAdmin()
        {
            InitializeComponent();
            InitializeCustomComponents();
            ConfigurarEventos();
        }

        private void InitializeCustomComponents()
        {
            HabilitarDoubleBuffer(dgvReporteInquilinos);
            HabilitarDoubleBuffer(dgvReporteInmuebles);
            HabilitarDoubleBuffer(dgvReporteUsuarios);

            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void ConfigurarEventos()
        {
            this.Load += UcReportesAdmin_Load;
            btnRefrescar.Click += BtnRefrescar_Click;
            btnExportarExcel.Click += BtnExportarExcel_Click;

            // Eventos de filtros específicos por pestaña
            cboInquilinosEstado.SelectedIndexChanged += (s, e) => CargarReporteInquilinos();
            cboInmueblesTipo.SelectedIndexChanged += (s, e) => CargarReporteInmuebles();
            cboUsuariosRol.SelectedIndexChanged += (s, e) => CargarReporteUsuarios();
            cboUsuariosActivo.SelectedIndexChanged += (s, e) => CargarReporteUsuarios();

            tabControlReportes.SelectedIndexChanged += TabControlReportes_SelectedIndexChanged;

            // *** NUEVO: Enganchar eventos CellFormatting ***
            dgvReporteInquilinos.CellFormatting += DgvReporteInquilinos_CellFormatting;
            dgvReporteInmuebles.CellFormatting += DgvReporteInmuebles_CellFormatting;
            dgvReporteUsuarios.CellFormatting += DgvReporteUsuarios_CellFormatting;
        }

        private void UcReportesAdmin_Load(object? sender, EventArgs e)
        {
            if (DesignMode || (Site?.DesignMode ?? false)) return;

            ConfigurarDataGrids(); // Llama a la configuración del Designer
            CargarCombosFiltros();
            CargarTodosLosReportes();
        }

        private void ConfigurarDataGrids()
        {
            // La configuración de columnas ahora está principalmente en el Designer.
            // Solo asignamos fuentes de datos vacías aquí.
            dgvReporteInquilinos.DataSource = new BindingList<InquilinoReporte>();
            dgvReporteInmuebles.DataSource = new BindingList<InmuebleReporte>();
            dgvReporteUsuarios.DataSource = new BindingList<UsuarioReporte>();

            // Estilos adicionales (alternating rows, etc.) que quieras aplicar en código
            dgvReporteInquilinos.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8F9FA");
            dgvReporteInmuebles.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8F9FA");
            dgvReporteUsuarios.AlternatingRowsDefaultCellStyle.BackColor = System.Drawing.ColorTranslator.FromHtml("#F8F9FA");
        }

        private void CargarCombosFiltros()
        {
            // Combo de estado para Inquilinos
            cboInquilinosEstado.Items.Clear();
            cboInquilinosEstado.Items.Add("Todos");
            cboInquilinosEstado.Items.Add("Activo");
            cboInquilinosEstado.Items.Add("Inactivo");
            cboInquilinosEstado.SelectedIndex = 0;

            // *** CAMBIO: Combo de tipo para Inmuebles (lista actualizada) ***
            cboInmueblesTipo.Items.Clear();
            cboInmueblesTipo.Items.Add("Todos");
            cboInmueblesTipo.Items.Add("Casa");
            cboInmueblesTipo.Items.Add("Departamento");
            cboInmueblesTipo.Items.Add("PH");
            cboInmueblesTipo.Items.Add("Local");
            cboInmueblesTipo.Items.Add("Galpón");
            cboInmueblesTipo.Items.Add("Oficina");
            cboInmueblesTipo.Items.Add("Terreno");
            cboInmueblesTipo.SelectedIndex = 0;

            // Combo de rol para Usuarios
            cboUsuariosRol.Items.Clear();
            cboUsuariosRol.Items.Add("Todos");
            cboUsuariosRol.Items.Add("Administrador");
            cboUsuariosRol.Items.Add("Operador");
            cboUsuariosRol.Items.Add("Propietario");
            cboUsuariosRol.SelectedIndex = 0;

            // Combo de estado para Usuarios
            cboUsuariosActivo.Items.Clear();
            cboUsuariosActivo.Items.Add("Todos");
            cboUsuariosActivo.Items.Add("Activo");
            cboUsuariosActivo.Items.Add("Inactivo");
            cboUsuariosActivo.SelectedIndex = 0;
        }


        private void BtnRefrescar_Click(object? sender, EventArgs e)
        {
            CargarTodosLosReportes();
        }

        private void CargarTodosLosReportes()
        {
            DateTime? fechaInicio = dtpFechaInicio.Value.Date;
            DateTime? fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

            CargarDashboard(fechaInicio, fechaFin);
            CargarReporteInquilinos(fechaInicio, fechaFin);
            CargarReporteInmuebles(fechaInicio, fechaFin);
            CargarReporteUsuarios(fechaInicio, fechaFin);
        }

        private void TabControlReportes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            // Carga perezosa (opcional)
        }

        private void CargarDashboard(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var kpis = _reporteRepo.ObtenerDashboardKpis(fechaInicio, fechaFin);
                lblTotalPropiedades.Text = kpis.TotalPropiedades.ToString();
                lblTotalInquilinos.Text = kpis.TotalInquilinos.ToString();
                lblTotalUsuarios.Text = kpis.TotalUsuarios.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el Dashboard:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteInquilinos(DateTime? globalFechaInicio = null, DateTime? globalFechaFin = null)
        {
            try
            {
                DateTime? fechaInicio = globalFechaInicio ?? dtpFechaInicio.Value.Date;
                DateTime? fechaFin = globalFechaFin ?? dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                bool? estado = null;
                if (cboInquilinosEstado.SelectedItem?.ToString() == "Activo") estado = true;
                if (cboInquilinosEstado.SelectedItem?.ToString() == "Inactivo") estado = false;

                var inquilinos = _reporteRepo.ObtenerReporteInquilinos(fechaInicio, fechaFin, estado);
                var bindingList = dgvReporteInquilinos.DataSource as BindingList<InquilinoReporte>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in inquilinos) bindingList.Add(item);
                //dgvReporteInquilinos.Refresh(); // No suele ser necesario con BindingList
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el Reporte de Inquilinos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteInmuebles(DateTime? globalFechaInicio = null, DateTime? globalFechaFin = null)
        {
            try
            {
                DateTime? fechaInicio = globalFechaInicio ?? dtpFechaInicio.Value.Date;
                DateTime? fechaFin = globalFechaFin ?? dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                string? tipoInmueble = cboInmueblesTipo.SelectedItem?.ToString();

                var inmuebles = _reporteRepo.ObtenerReporteInmuebles(fechaInicio, fechaFin, tipoInmueble);
                var bindingList = dgvReporteInmuebles.DataSource as BindingList<InmuebleReporte>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in inmuebles) bindingList.Add(item);
                //dgvReporteInmuebles.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el Reporte de Inmuebles:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarReporteUsuarios(DateTime? globalFechaInicio = null, DateTime? globalFechaFin = null)
        {
            try
            {
                DateTime? fechaInicio = globalFechaInicio ?? dtpFechaInicio.Value.Date;
                DateTime? fechaFin = globalFechaFin ?? dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

                string? rol = cboUsuariosRol.SelectedItem?.ToString();
                bool? activo = null;
                if (cboUsuariosActivo.SelectedItem?.ToString() == "Activo") activo = true;
                if (cboUsuariosActivo.SelectedItem?.ToString() == "Inactivo") activo = false;

                var usuarios = _reporteRepo.ObtenerReporteUsuarios(fechaInicio, fechaFin, rol, activo);
                var bindingList = dgvReporteUsuarios.DataSource as BindingList<UsuarioReporte>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in usuarios) bindingList.Add(item);
                //dgvReporteUsuarios.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el Reporte de Usuarios:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- INICIO: NUEVOS MANEJADORES CellFormatting ---
        private void DgvReporteInquilinos_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return; // Ignorar cabecera

            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].DataPropertyName; // Usar DataPropertyName es más robusto

            if (colName == nameof(InquilinoReporte.Estado) && e.Value is bool estadoValue)
            {
                e.Value = estadoValue ? "Activo" : "Inactivo";
                e.FormattingApplied = true; // Indicar que ya formateamos
            }
        }

        private void DgvReporteInmuebles_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].DataPropertyName;

            if (colName == nameof(InmuebleReporte.Estado) && e.Value is bool estadoValue)
            {
                e.Value = estadoValue ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
            else if (colName == nameof(InmuebleReporte.Amueblado) && e.Value is bool amuebladoValue)
            {
                e.Value = amuebladoValue ? "Sí" : "No";
                e.FormattingApplied = true;
            }
        }

        private void DgvReporteUsuarios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].DataPropertyName;

            if (colName == nameof(UsuarioReporte.Activo) && e.Value is bool activoValue)
            {
                e.Value = activoValue ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
        }
        // --- FIN: NUEVOS MANEJADORES CellFormatting ---


        private void BtnExportarExcel_Click(object? sender, EventArgs e)
        {
            DataGridView? dgvToExport = null;

            if (tabControlReportes.SelectedTab == tabPageDashboard)
            {
                MessageBox.Show("El Dashboard no tiene una tabla para exportar. Seleccione la pestaña de Inquilinos, Inmuebles o Usuarios.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (tabControlReportes.SelectedTab == tabPageInquilinos)
            {
                dgvToExport = dgvReporteInquilinos;
            }
            else if (tabControlReportes.SelectedTab == tabPageInmuebles)
            {
                dgvToExport = dgvReporteInmuebles;
            }
            else if (tabControlReportes.SelectedTab == tabPageUsuarios)
            {
                dgvToExport = dgvReporteUsuarios;
            }

            if (dgvToExport != null && dgvToExport.Rows.Count > 0)
            {
                ExportToExcel(dgvToExport, tabControlReportes.SelectedTab.Text);
            }
            else
            {
                MessageBox.Show("No hay datos para exportar en la pestaña actual.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ExportToExcel(DataGridView dgv, string sheetName)
        {
            try
            {
                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos de Excel (*.xlsx)|*.xlsx",
                    FileName = $"{sheetName.Replace(" ", "_")}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    using var workbook = new XLWorkbook();
                    var worksheet = workbook.Worksheets.Add(sheetName);

                    // Headers
                    for (int i = 0; i < dgv.ColumnCount; i++)
                    {
                        worksheet.Cell(1, i + 1).Value = dgv.Columns[i].HeaderText;
                    }

                    // Data
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        for (int j = 0; j < dgv.ColumnCount; j++)
                        {
                            object cellValueObj = dgv.Rows[i].Cells[j].Value;
                            var targetCell = worksheet.Cell(i + 2, j + 1);

                            if (cellValueObj == null || cellValueObj == DBNull.Value)
                            {
                                targetCell.Value = string.Empty;
                                continue;
                            }

                            string dataPropertyName = dgv.Columns[j].DataPropertyName;

                            // *** CAMBIO: Usar el valor ya formateado por CellFormatting ***
                            string formattedValue = dgv.Rows[i].Cells[j].FormattedValue?.ToString() ?? "";

                            if ((dataPropertyName == "Estado" || dataPropertyName == "Activo" || dataPropertyName == "Amueblado") && !string.IsNullOrEmpty(formattedValue))
                            {
                                targetCell.Value = formattedValue; // Usar "Activo"/"Inactivo" o "Sí"/"No"
                            }
                            // Resto del código igual...
                            else if (cellValueObj is DateTime dateTimeValue)
                            {
                                targetCell.Value = dateTimeValue;
                                // Aplicar formato si no lo toma solo
                                if (dgv.Columns[j].DefaultCellStyle.Format == "dd/MM/yyyy")
                                    targetCell.Style.NumberFormat.Format = "dd/MM/yyyy";

                            }
                            else if (cellValueObj is decimal decimalValue)
                            {
                                targetCell.Value = decimalValue;
                                if (dgv.Columns[j].DefaultCellStyle.Format == "C")
                                {
                                    targetCell.Style.NumberFormat.Format = "$ #,##0.00"; // Formato ARS
                                }
                            }
                            else if (cellValueObj is int intValue)
                            {
                                targetCell.Value = intValue;
                            }
                            else if (cellValueObj is double doubleValue)
                            {
                                targetCell.Value = doubleValue;
                            }
                            else if (cellValueObj is float floatValue)
                            {
                                targetCell.Value = floatValue;
                            }
                            else
                            {
                                targetCell.Value = cellValueObj.ToString();
                            }
                        }
                    }

                    worksheet.Columns().AdjustToContents();

                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Datos exportados exitosamente a Excel.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al exportar a Excel:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private static void HabilitarDoubleBuffer(Control ctl) // Funciona para DGV también
        {
            try
            {
                typeof(Control).InvokeMember("DoubleBuffered",
                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
                    null, ctl, new object[] { true });
            }
            catch { /* Ignorar si falla */ }
        }
    }
}