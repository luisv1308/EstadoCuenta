using AutoMapper;
using EstadoCuenta.Api.CQRS.Commands;
using EstadoCuenta.Api.CQRS.Queries;
using EstadoCuenta.Api.DTOs;
using EstadoCuenta.Api.Hubs;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace EstadoCuenta.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public HistorialController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("{tarjetaId}")]
        public async Task<IActionResult> ObtenerTransacciones(int tarjetaId)
        {
            IEnumerable<TransaccionDTO> transacciones = await _mediator.Send(new GetTransaccionesByTarjetaQuery(tarjetaId));
            if (transacciones == null)
                return NotFound();

            return Ok(transacciones);
        }
        
    }
}
