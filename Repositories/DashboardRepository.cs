// DashboardRepository.cs
using InmoTech.Data; // Para BDGeneral
using InmoTech.Models; // Para DashboardData, InmuebleCard
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace InmoTech.Repositories
{
    public class DashboardRepository
    {
        public DashboardData GetDashboardData()
        {
            var data = new DashboardData();

            // La conexión ya viene abierta de BDGeneral.GetConnection()
            using var cn = BDGeneral.GetConnection();

            // ======================================================
            // 1. CONSULTA DE KPIS CORREGIDA POR ERRORES ANTERIORES
            // ======================================================

            const string sqlKpis = @"
                -- KPI 1: Total Propiedades Disponibles (Usamos estado=1 [BIT] para activo/disponible)
                SELECT COUNT(id_inmueble) FROM dbo.inmueble WHERE estado = 1;

                -- KPI 2: Total Inquilinos (CORRECCIÓN FINAL: Contar personas únicas con contratos activos)
                SELECT COUNT(DISTINCT c.id_persona) 
                FROM dbo.contrato c 
                WHERE c.estado = 1;

                -- KPI 3: Ingreso Total del Mes (Pagos Completados)
                SELECT ISNULL(SUM(monto_total), 0) FROM dbo.pago 
                WHERE YEAR(fecha_pago) = YEAR(GETDATE()) AND MONTH(fecha_pago) = MONTH(GETDATE()) 
                AND estado = 'Completado' AND activo = 1; 

                -- KPI 4: Pagos Pendientes
                SELECT COUNT(id_pago) FROM dbo.pago 
                WHERE estado = 'Pendiente' AND activo = 1; 

                -- CONSULTA 5: Propiedades Disponibles (para Cards)
                SELECT TOP 4 id_inmueble, direccion, tipo, nro_ambientes, estado 
                FROM dbo.inmueble 
                WHERE estado = 1 
                ORDER BY fecha_creacion DESC;

                -- CONSULTA 6: Contratos Por Vencer (para DataGridView)
                SELECT TOP 5 
                    c.id_contrato, 
                    p.nombre + ' ' + p.apellido AS InquilinoNombre,
                    i.direccion AS InmuebleDireccion,
                    c.estado, 
                    c.fecha_fin
                FROM dbo.contrato c
                INNER JOIN dbo.inmueble i ON c.id_inmueble = i.id_inmueble
                INNER JOIN dbo.persona p ON c.id_persona = p.id_persona
                WHERE c.estado = 1 
                AND c.fecha_fin BETWEEN GETDATE() AND DATEADD(day, 90, GETDATE());
            ";

            using var cmd = new SqlCommand(sqlKpis, cn);
            using var reader = cmd.ExecuteReader();

            // 1. Leer KPI: Total Propiedades
            if (reader.Read()) data.TotalPropiedades = reader.GetInt32(0);

            // 2. Leer KPI: Total Inquilinos
            reader.NextResult();
            if (reader.Read()) data.TotalInquilinos = reader.GetInt32(0);

            // 3. Leer KPI: Ingreso Total Mes
            reader.NextResult();
            if (reader.Read()) data.IngresoTotalMes = reader.IsDBNull(0) ? 0 : reader.GetDecimal(0);

            // 4. Leer KPI: Pagos Pendientes
            reader.NextResult();
            if (reader.Read()) data.PagosPendientes = reader.GetInt32(0);

            // 5. Leer Propiedades Disponibles (Cards) 
            reader.NextResult();
            while (reader.Read())
            {
                // NOTA: estado (index 4) es BIT en la BD, se convierte a texto para la UI
                string estadoInmueble = reader.GetBoolean(4) ? "Activo" : "Inactivo";

                data.InmueblesDisponibles.Add(new InmuebleCard
                {
                    IdInmueble = reader.GetInt32(0),
                    Direccion = reader.GetString(1),
                    Tipo = reader.GetString(2),
                    Ambientes = reader.GetInt32(3),
                    Estado = estadoInmueble,
                    Titulo = $"{reader.GetString(2)} {reader.GetString(1)}"
                });
            }

            // 6. Leer Contratos Por Vencer (DataGridView)
            reader.NextResult();
            var contratosVencer = new DataTable();
            contratosVencer.Load(reader);
            data.ContratosVencer = contratosVencer;

            return data;
        }
    }
}