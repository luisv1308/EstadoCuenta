﻿using System;
using System.Threading.Tasks;

namespace EstadoCuenta.Api.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        ITarjetaCreditoRepository TarjetasCredito { get; }
        ITransaccionRepository Transacciones { get; }
        IEstadoCuentaRepository EstadoCuenta { get; }
        Task<int> SaveChangesAsync();
    }
}
