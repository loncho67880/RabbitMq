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

        [HttpGet(Name = "GetIncapacidad")]
        public IEnumerable<Incapacidad> Get()
        {
            var list = Enumerable.Range(0, 5).Select(index => new Incapacidad
            {
                Id = Random.Shared.Next(50, 100)
                ,
                Nombre = Tipos[Random.Shared.Next(Tipos.Length)]
            }).ToArray();

            _incapacidadProducer.SendNotificatinoIncapacidadMessage<Incapacidad>(list.First());
            return list;
        }
    }
}