﻿using Dapper;
using System.Data;
using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EstadoCuenta.Api.Repositories
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;

        public TransaccionRepository(ApplicationDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorTarjetaAsync(int tarjetaId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TarjetaId", tarjetaId);

            return await _dbConnection.QueryAsync<Transaccion>(
                "sp_ObtenerTransaccionesPorTarjeta",
                parameters,
                commandType: CommandType.StoredProcedure
            );
        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorTipoAsync(int tarjetaId, string tipo)
        {
            var mesActual = DateTime.Now.Month;
            var anioActual = DateTime.Now.Year;

            var tarjeta = await _context.Transacciones
                .Where(t => t.TarjetaCreditoId == tarjetaId
                    && t.Tipo == tipo
                    && t.Fecha.Month == mesActual
                    && t.Fecha.Year == anioActual)
                .OrderByDescending(t => t.Fecha)
                .ThenByDescending(t => t.Id)
                .ToListAsync();

            return tarjeta;
        }

        public async Task AgregarAsync(Transaccion transaccion)
        {
            await _context.Transacciones.AddAsync(transaccion);
        }

        public void Eliminar(Transaccion transaccion)
        {
            _context.Transacciones.Remove(transaccion);
        }
    }
}
