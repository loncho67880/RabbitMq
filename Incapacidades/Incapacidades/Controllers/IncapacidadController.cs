using Core.Incapacidades;
using Incapacidades.Producer;
using Microsoft.AspNetCore.Mvc;

namespace Incapacidades.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncapacidadController : ControllerBase
    {
        private static readonly string[] Tipos = new[]
        {
            "Maternidad", "Paternidad", "Enfermedad", "Terapia"
        };

        private readonly ILogger<IncapacidadController> _logger;
        private readonly IIncapacidadProducer _incapacidadProducer;

        public IncapacidadController(ILogger<IncapacidadController> logger, IIncapacidadProducer incapacidadProducer)
        {
            _logger = logger;
            _incapacidadProducer = incapacidadProducer;
        }

        [HttpPost(Name = "AnularIncapacidad")]
        public Incapacidad Get()
        {
            var incapacidad = new Incapacidad
            {
                Id = Random.Shared.Next(50, 100),
                Nombre = Tipos[Random.Shared.Next(Tipos.Length)],
                causa_anulacion = "Error Digitación",
                fecha_anulacion = DateTime.Today,
                observacion = "Observación Anulación"
            };

            _incapacidadProducer.SendNotificatinoIncapacidadMessage<Incapacidad>(incapacidad);
            return incapacidad;
        }
    }
}