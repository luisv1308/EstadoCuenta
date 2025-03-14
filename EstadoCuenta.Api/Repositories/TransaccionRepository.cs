using Dapper;
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
            var tarjeta = await _context.Transacciones
                .Where(t => t.TarjetaCreditoId == tarjetaId && t.Tipo == tipo)
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
