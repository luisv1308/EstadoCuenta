using EstadoCuenta.Api.Data;

namespace EstadoCuenta.Api.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public ITarjetaCreditoRepository TarjetasCredito { get; }
        public ITransaccionRepository Transacciones { get; }
        public IEstadoCuentaRepository EstadoCuenta { get; }

        public UnitOfWork(ApplicationDbContext context, ITarjetaCreditoRepository tarjetasCreditoRepository, ITransaccionRepository transaccionesRepository, IEstadoCuentaRepository estadoCuenta)
        {
            _context = context;
            TarjetasCredito = tarjetasCreditoRepository;
            Transacciones = transaccionesRepository;
            EstadoCuenta = estadoCuenta;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

        public void Dispose() => _context.Dispose();
    }
}
