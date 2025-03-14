using Dapper;
using EstadoCuenta.Api.DTOs;
using System.Data;

namespace EstadoCuenta.Api.Repositories
{
    public class EstadoCuentaRepository : IEstadoCuentaRepository
    {
        private readonly IDbConnection _dbConnection;

        public EstadoCuentaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<EstadoCuentaDTO> ObtenerEstadoCuentaAsync(int tarjetaId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TarjetaId", tarjetaId);

            using (var multi = await _dbConnection.QueryMultipleAsync(
                "sp_ObtenerEstadoCuenta",
                parameters,
                commandType: CommandType.StoredProcedure
            ))
            {
                var estadoCuenta = await multi.ReadFirstOrDefaultAsync<EstadoCuentaDTO>();
                if (estadoCuenta == null)
                {
                    throw new InvalidOperationException("No se encontró el estado de cuenta");
                }
                estadoCuenta.Compras = (await multi.ReadAsync<TransaccionDTO>()).AsList();

                return estadoCuenta;
            }
        }
    }
}
