using InmoTech.Models;
using InmoTech.Repositories;
using InmoTech.Security; // Necesario para obtener el usuario actual
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ClosedXML.Excel; // Asegúrate de tener instalado el paquete NuGet ClosedXML
using System.Globalization; // Necesario para CultureInfo

namespace InmoTech.Controls
{
    public partial class UcReportesOperador : UserControl
    {
        private readonly ReporteRepository _reporteRepo = new();
        private int _dniUsuarioActual; // Guardamos el DNI del operador logueado

        public UcReportesOperador()
        {
            InitializeComponent();
            InitializeCustomComponents();
            ConfigurarEventos();
        }

        private void InitializeCustomComponents()
        {
            HabilitarDoubleBuffer(dgvClientesMorosos);
            HabilitarDoubleBuffer(dgvMisPagos);
            HabilitarDoubleBuffer(dgvMisContratos);

            // NUEVO: habilitar double buffer para la nueva grilla
            if (dgvContratosPorVencer != null)
                HabilitarDoubleBuffer(dgvContratosPorVencer);

            dtpFechaInicio.Value = DateTime.Today.AddMonths(-1);
            dtpFechaFin.Value = DateTime.Today;
        }

        private void ConfigurarEventos()
        {
            this.Load += UcReportesOperador_Load;
            btnRefrescar.Click += BtnRefrescar_Click;
            btnExportarExcel.Click += BtnExportarExcel_Click;
            tabControlReportes.SelectedIndexChanged += TabControlReportes_SelectedIndexChanged;

            // Enganchar eventos CellFormatting para las grillas
            dgvClientesMorosos.CellFormatting += DgvClientesMorosos_CellFormatting;
            dgvMisPagos.CellFormatting += DgvMisPagos_CellFormatting;
            dgvMisContratos.CellFormatting += DgvMisContratos_CellFormatting;

            // NUEVO: formateo de la grilla "Contratos por vencer"
            if (dgvContratosPorVencer != null)
                dgvContratosPorVencer.CellFormatting += DgvContratosPorVencer_CellFormatting;
        }

        private void UcReportesOperador_Load(object? sender, EventArgs e) // sender es object?
        {
            if (DesignMode || (Site?.DesignMode ?? false)) return;

            // Obtener DNI del usuario actual al cargar
            if (AuthService.CurrentUser == null)
            {
                MessageBox.Show("No se pudo identificar al usuario. Por favor, inicie sesión de nuevo.", "Error de Autenticación", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Enabled = false;
                return;
            }
            _dniUsuarioActual = AuthService.CurrentUser.Dni;

            ConfigurarDataGrids();
            CargarTodosLosReportes();
        }

        private void ConfigurarDataGrids()
        {
            dgvClientesMorosos.DataSource = new BindingList<ClienteMorosoViewModel>();
            dgvMisPagos.DataSource = new BindingList<PagoOperadorReporte>();
            dgvMisContratos.DataSource = new BindingList<ContratoOperadorReporte>();

            // NUEVO: datasource para la grilla de Contratos por Vencer
            if (dgvContratosPorVencer != null)
                dgvContratosPorVencer.DataSource = new BindingList<ContratoPorVencerViewModel>();

            dgvClientesMorosos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#FFF0F0");
            dgvMisPagos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8F9FA");
            dgvMisContratos.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8F9FA");

            // NUEVO: alternating para CPV
            if (dgvContratosPorVencer != null)
                dgvContratosPorVencer.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F8F9FA");
        }

        private void BtnRefrescar_Click(object? sender, EventArgs e) // sender es object?
        {
            CargarTodosLosReportes();
        }

        private void CargarTodosLosReportes()
        {
            if (!this.Enabled) return;

            DateTime? fechaInicio = dtpFechaInicio.Value.Date;
            DateTime? fechaFin = dtpFechaFin.Value.Date.AddDays(1).AddSeconds(-1);

            CargarDashboardOperador(fechaInicio, fechaFin);
            CargarClientesMorosos(fechaInicio, fechaFin);
            CargarMisPagos(fechaInicio, fechaFin);
            CargarMisContratos(fechaInicio, fechaFin);

            // NUEVO: los contratos por vencer son relativos a HOY y próximos 60 días (no al filtro)
            CargarContratosPorVencer(60);
        }

        private void TabControlReportes_SelectedIndexChanged(object? sender, EventArgs e) // sender es object?
        {
            // Carga perezosa (opcional)
        }

        private void CargarDashboardOperador(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var kpis = _reporteRepo.ObtenerKpisOperador(_dniUsuarioActual, fechaInicio, fechaFin);
                lblTotalIngresos.Text = kpis.IngresosGenerados.ToString("C", CultureInfo.GetCultureInfo("es-AR")); // Aplicar cultura aquí
                lblContratosCreados.Text = kpis.ContratosCreados.ToString();
                lblPagosRegistrados.Text = kpis.PagosRegistrados.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar KPIs del Dashboard:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarClientesMorosos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var morosos = _reporteRepo.ObtenerClientesMorosos(_dniUsuarioActual, fechaInicio, fechaFin);
                var bindingList = dgvClientesMorosos.DataSource as BindingList<ClienteMorosoViewModel>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in morosos) bindingList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Clientes Morosos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMisPagos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var pagos = _reporteRepo.ObtenerPagosPorOperador(_dniUsuarioActual, fechaInicio, fechaFin);
                var bindingList = dgvMisPagos.DataSource as BindingList<PagoOperadorReporte>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in pagos) bindingList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Mis Pagos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarMisContratos(DateTime? fechaInicio, DateTime? fechaFin)
        {
            try
            {
                var contratos = _reporteRepo.ObtenerContratosPorOperador(_dniUsuarioActual, fechaInicio, fechaFin);
                var bindingList = dgvMisContratos.DataSource as BindingList<ContratoOperadorReporte>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in contratos) bindingList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Mis Contratos:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // NUEVO: carga de Contratos por Vencer (<= X días desde hoy)
        private void CargarContratosPorVencer(int diasHaciaAdelante)
        {
            try
            {
                var lista = _reporteRepo.ObtenerContratosPorVencer(_dniUsuarioActual, diasHaciaAdelante);
                var bindingList = dgvContratosPorVencer?.DataSource as BindingList<ContratoPorVencerViewModel>;
                bindingList?.Clear();
                if (bindingList != null) foreach (var item in lista) bindingList.Add(item);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar Contratos por vencer:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- INICIO: MANEJADORES CellFormatting ---
        private void DgvClientesMorosos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || sender == null || e.Value == null) return;
            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].Name;

            // ** FORMATO "dd/MM/yyyy" **
            if (colName == "colMorProxVenc" && e.Value is DateTime dt)
            {
                if (dt != DateTime.MinValue)
                {
                    e.Value = dt.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
                else
                {
                    e.Value = "";
                    e.FormattingApplied = true;
                }
            }
        }

        private void DgvMisPagos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || sender == null || e.Value == null) return;
            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].Name;

            if (colName == "colPagoFecha" && e.Value is DateTime dt)
            {
                e.Value = dt != DateTime.MinValue ? dt.ToString("dd/MM/yyyy") : "";
                e.FormattingApplied = true;
            }
            else if (colName == "colPagoMonto" && e.Value is decimal monto)
            {
                e.Value = monto.ToString("C2", CultureInfo.GetCultureInfo("es-AR"));
                e.FormattingApplied = true;
            }
            else if (colName == "colPagoFecReg" && e.Value is DateTime dtr)
            {
                e.Value = dtr != DateTime.MinValue ? dtr.ToString("dd/MM/yyyy HH:mm") : "";
                e.FormattingApplied = true;
            }
        }

        private void DgvMisContratos_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || sender == null || e.Value == null) return;
            var dgv = (DataGridView)sender;
            string colName = dgv.Columns[e.ColumnIndex].Name;

            if (colName == "colContEstado" && e.Value is bool estado)
            {
                e.Value = estado ? "Activo" : "Inactivo";
                e.FormattingApplied = true;
            }
            else if ((colName == "colContInicio" || colName == "colContFin") && e.Value is DateTime dt)
            {
                e.Value = dt != DateTime.MinValue ? dt.ToString("dd/MM/yyyy") : "";
                e.FormattingApplied = true;
            }
            else if (colName == "colContMonto" && e.Value is decimal monto)
            {
                e.Value = monto.ToString("C2", CultureInfo.GetCultureInfo("es-AR"));
                e.FormattingApplied = true;
            }
            else if (colName == "colContFecCrea" && e.Value is DateTime dtc)
            {
                e.Value = dtc != DateTime.MinValue ? dtc.ToString("dd/MM/yyyy HH:mm") : "";
                e.FormattingApplied = true;
            }
        }

        // NUEVO: formateo de la grilla "Contratos por vencer"
        private void DgvContratosPorVencer_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || sender == null || e.Value == null) return;
            var dgv = (DataGridView)sender;
            string col = dgv.Columns[e.ColumnIndex].Name;

            if (col == "colCpvFechaFin" && e.Value is DateTime fin)
            {
                e.Value = fin != DateTime.MinValue ? fin.ToString("dd/MM/yyyy") : "";
                e.FormattingApplied = true;
            }
            else if (col == "colCpvDiasRestantes" && e.Value is int dias)
            {
                // centrar ya está en el DefaultCellStyle del designer; acá podrías colorear si querés
                e.Value = dias.ToString();
                e.FormattingApplied = true;
            }
        }
        // --- FIN: MANEJADORES CellFormatting ---

        private void BtnExportarExcel_Click(object? sender, EventArgs e) // sender es object?
        {
            DataGridView? dgvToExport = null;
            string defaultFileName = "Reporte_Operador";

            if (tabControlReportes.SelectedTab == tabPageResumen)
            {
                // Por defecto exportamos los Morosos (como antes).
                // Si preferís exportar "Contratos por vencer", cambiá acá a dgvContratosPorVencer.
                dgvToExport = dgvClientesMorosos;
                defaultFileName = "Clientes_Morosos";
            }
            else if (tabControlReportes.SelectedTab == tabPageMisPagos)
            {
                dgvToExport = dgvMisPagos;
                defaultFileName = "Mis_Pagos";
            }
            else if (tabControlReportes.SelectedTab == tabPageMisContratos)
            {
                dgvToExport = dgvMisContratos;
                defaultFileName = "Mis_Contratos";
            }

            if (dgvToExport != null && dgvToExport.Rows.Count > 0)
            {
                ExportToExcel(dgvToExport, defaultFileName);
            }
            else
            {
                MessageBox.Show("No hay datos para exportar en la pestaña actual.", "Exportar a Excel", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // --- ExportToExcel se mantiene igual que la versión anterior ---
        private void ExportToExcel(DataGridView dgv, string sheetName)
        {
            try
            {
                using var saveFileDialog = new SaveFileDialog
                {
                    Filter = "Archivos de Excel (*.xlsx)|*.xlsx",
                    FileName = $"{sheetName}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx"
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
                            string formattedValue = dgv.Rows[i].Cells[j].FormattedValue?.ToString() ?? "";

                            // Usar valor formateado para booleanos de texto y fechas
                            bool isFormatted = (dataPropertyName == "Estado" || dataPropertyName == "Activo" || dataPropertyName == "Amueblado" ||
                                               cellValueObj is DateTime)
                                               && !string.IsNullOrEmpty(formattedValue);

                            if (isFormatted)
                            {
                                if (cellValueObj is DateTime dtValue && dtValue != DateTime.MinValue)
                                {
                                    targetCell.Value = dtValue;
                                    targetCell.Style.NumberFormat.Format = dtValue.TimeOfDay.TotalSeconds == 0 ? "dd/MM/yyyy" : "dd/MM/yyyy HH:mm";
                                }
                                else if (cellValueObj is DateTime dtEmpty && dtEmpty == DateTime.MinValue)
                                {
                                    targetCell.Value = string.Empty;
                                }
                                else
                                {
                                    targetCell.Value = formattedValue;
                                }
                            }
                            else if (cellValueObj is decimal decimalValue)
                            {
                                targetCell.Value = decimalValue;
                                if (dgv.Columns[j].DefaultCellStyle.Format == "C2")
                                {
                                    targetCell.Style.NumberFormat.Format = "$ #,##0.00";
                                }
                            }
                            else if (cellValueObj is int intValue) { targetCell.Value = intValue; }
                            else if (cellValueObj is double doubleValue) { targetCell.Value = doubleValue; }
                            else if (cellValueObj is float floatValue) { targetCell.Value = floatValue; }
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

        private static void HabilitarDoubleBuffer(Control ctl)
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
