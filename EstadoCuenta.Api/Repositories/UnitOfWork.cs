using EstadoCuenta.Api.Data;

namespace EstadoCuenta.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ITarjetaCreditoRepository TarjetasCredito { get; }
        public ITransaccionRepository Transacciones { get; }

        public UnitOfWork(ApplicationDbContext context, ITarjetaCreditoRepository tarjetasCreditoRepository, ITransaccionRepository transaccionesRepository)
        {
            _context = context;
            TarjetasCredito = tarjetasCreditoRepository;
            Transacciones = transaccionesRepository;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
