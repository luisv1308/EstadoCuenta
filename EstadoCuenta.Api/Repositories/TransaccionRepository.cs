using EstadoCuenta.Api.Data;
using EstadoCuenta.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EstadoCuenta.Api.Repositories
{
    public class TransaccionRepository : ITransaccionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransaccionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Transaccion>> ObtenerPorTarjetaAsync(int tarjetaId)
        {
            return await _context.Transacciones
                .Where(t => t.TarjetaCreditoId == tarjetaId)
                .OrderByDescending(t => t.Fecha)
                .ToListAsync();
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
