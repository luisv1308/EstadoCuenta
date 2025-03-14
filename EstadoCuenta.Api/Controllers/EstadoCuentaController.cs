using AutoMapper;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EstadoCuenta.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCuentaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public EstadoCuentaController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerEstadoCuenta(int tarjetaId)
        {
            var estadoCuenta = await _mediator.Send(new GetEstadoCuentaByTarjetaQuery(tarjetaId));
            if (estadoCuenta == null)
                return NotFound();
            var estadoCuentaDTO = _mapper.Map<EstadoCuentaDTO>(estadoCuenta);

            return Ok(estadoCuentaDTO);
        }
    }
}
