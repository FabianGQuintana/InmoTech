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
    /// <summary>
    /// Reportes operativos por usuario (operador).
    /// Muestra KPIs, clientes morosos, pagos y contratos asociados al operador logueado;
    /// permite exportación a Excel y visualización formateada de grillas.
    /// </summary>
    public partial class UcReportesOperador : UserControl
    {
        private readonly ReporteRepository _reporteRepo = new();
        private int _dniUsuarioActual; // Guardamos el DNI del operador logueado

        /// <summary>
        /// Constructor del control: inicializa componentes, configura UI personalizada y registra eventos.
        /// </summary>
        public UcReportesOperador()
        {
            InitializeComponent();
            InitializeCustomComponents();
            ConfigurarEventos();
        }

        /// <summary>
        /// Ajustes visuales (DoubleBuffer), fuentes de datos iniciales y rango de fechas por defecto.
        /// </summary>
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

        /// <summary>
        /// Registra handlers de ciclo de vida y de interacción, incluido formateo de celdas para cada grilla.
        /// </summary>
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

        /// <summary>
        /// Handler de carga: verifica autenticación, obtiene el DNI del operador,
        /// configura grillas y realiza la primera carga de reportes.
        /// </summary>
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

        /// <summary>
        /// Inicializa las fuentes (BindingList) de cada DataGridView y estilos alternados.
        /// </summary>
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

        /// <summary>
        /// Actualiza todos los reportes según el rango de fechas seleccionado.
        /// </summary>
        private void BtnRefrescar_Click(object? sender, EventArgs e) // sender es object?
        {
            CargarTodosLosReportes();
        }

        /// <summary>
        /// Orquesta la carga de dashboard, morosos, pagos, contratos y contratos por vencer (estos últimos relativos a hoy).
        /// </summary>
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

        /// <summary>
        /// Cambio de pestaña (reservado para futuras cargas perezosas).
        /// </summary>
        private void TabControlReportes_SelectedIndexChanged(object? sender, EventArgs e) // sender es object?
        {
            // Carga perezosa (opcional)
        }

        /// <summary>
        /// Obtiene KPIs del operador (ingresos, contratos, pagos) y los muestra con formato regional.
        /// </summary>
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

        /// <summary>
        /// Carga y vincula la lista de clientes morosos del operador, respetando el rango de fechas.
        /// </summary>
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

        /// <summary>
        /// Carga y vincula los pagos registrados por el operador en el período indicado.
        /// </summary>
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

        /// <summary>
        /// Carga y vincula los contratos creados por el operador dentro del rango de fechas.
        /// </summary>
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

        /// <summary>
        /// Carga y vincula los contratos que vencen en los próximos <paramref name="diasHaciaAdelante"/> días (relativo a hoy).
        /// </summary>
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

        /// <summary>
        /// Formato de columnas de Clientes Morosos (fechas dd/MM/yyyy y vacío si MinValue).
        /// </summary>
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

        /// <summary>
        /// Formato de columnas en Mis Pagos: fecha, monto (C2 es-AR), y fecha/hora de registro.
        /// </summary>
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

        /// <summary>
        /// Formato de columnas en Mis Contratos: estado legible, fechas y montos.
        /// </summary>
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

        /// <summary>
        /// Formato de columnas en Contratos por Vencer: fecha fin (dd/MM/yyyy) y días restantes en texto.
        /// </summary>
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

        /// <summary>
        /// Exporta a Excel la grilla correspondiente a la pestaña activa.
        /// Si no hay datos, muestra un aviso.
        /// </summary>
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

        /// <summary>
        /// Exporta el contenido de un <see cref="DataGridView"/> a un archivo XLSX usando ClosedXML,
        /// respetando formatos básicos (fechas, moneda, números) y autoajustando columnas.
        /// </summary>
        /// <param name="dgv">Grilla a exportar.</param>
        /// <param name="sheetName">Nombre de la hoja.</param>
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

        /// <summary>
        /// Activa <c>DoubleBuffered</c> por reflexión para reducir parpadeo en controles.
        /// </summary>
        /// <param name="ctl">Control objetivo.</param>
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
