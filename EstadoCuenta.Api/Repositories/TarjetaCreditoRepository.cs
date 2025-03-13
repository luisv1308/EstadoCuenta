using System.Data;
using Dapper;
using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EstadoCuenta.Api.Repositories
{
    public class TarjetaCreditoRepository : ITarjetaCreditoRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IDbConnection _dbConnection;

        public TarjetaCreditoRepository(ApplicationDbContext context, IDbConnection dbConnection)
        {
            _context = context;
            _dbConnection = dbConnection;
        }

        public async Task<TarjetaCredito?> ObtenerPorIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TarjetaId", id);

            var tarjeta = await _dbConnection.QueryFirstOrDefaultAsync<TarjetaCredito>(
                "sp_ObtenerEstadoCuenta",
                parameters,
                commandType: CommandType.StoredProcedure
            );

            return tarjeta;
        }

        public async Task<IEnumerable<TarjetaCredito>> ObtenerTodosAsync()
        {
            return await _context.TarjetasCredito.Include(tc => tc.Transacciones).ToListAsync();
        }

        public async Task AgregarAsync(TarjetaCredito tarjetaCredito)
        {
            await _context.TarjetasCredito.AddAsync(tarjetaCredito);
        }

        public void Actualizar(TarjetaCredito tarjetaCredito)
        {
            _context.TarjetasCredito.Update(tarjetaCredito);
        }

        public void Eliminar(TarjetaCredito tarjetaCredito)
        {
            _context.TarjetasCredito.Remove(tarjetaCredito);
        }

    }
}
