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

        public TarjetaCreditoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TarjetaCredito?> ObtenerPorIdAsync(int id)
        {
            return await _context.TarjetasCredito.FirstOrDefaultAsync(tc => tc.Id == id);
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
