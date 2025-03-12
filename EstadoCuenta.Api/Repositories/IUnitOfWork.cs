namespace EstadoCuenta.Api.Repositories
{
    public class IUnitOfWork : IDisposable
    {
        ITarjetaCreditoRepository TarjetasCredito { get; }
        ITransaccionRepository Transacciones { get; }
        Task<int> SaveChangesAsync();
    }
}
