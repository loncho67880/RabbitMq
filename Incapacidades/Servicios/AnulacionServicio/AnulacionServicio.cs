using Common.EmailHelper;
using Common.ReadTemplateHelper;
using Core.Incapacidades;
using Microsoft.Extensions.Configuration;

namespace Servicios.AnulacionServicio
{
    public class AnulacionServicio : IAnulacionServicio
    {
        private readonly IReadTemplateHelper _readTemplateHelper;
        private readonly IConfiguration _configuration;

        public AnulacionServicio(IReadTemplateHelper readTemplateHelper, IConfiguration configuration)
        {
            _readTemplateHelper = readTemplateHelper;
        }

        public void Anularincapacidad(Incapacidad incapacidad)
        {
            //Logica de Anular Incapacidad

            //Enviar Correo de notificación Asincrono
            string templateHtmlBaja = _readTemplateHelper.ReadTemplate(_configuration["MailAnulacion"]);
            templateHtmlBaja = templateHtmlBaja.Replace("{0}", incapacidad.Id.ToString());

            var emailAnulacion = new EmailModel()
            {
                Subject = "Anulación Incapacidad",
                FromDisplayName = "MinSalud",
                Body = new EmailModel.BodyMail()
                {
                    Title = "",
                    HtmlContent = templateHtmlBaja,
                },
                Type = EmailModel.MailType.Alerta,
                Addressee = new EmailModel.DestinationMail()
                {
                    To = _configuration["mailNotificacion"].Split(";").ToList()
                }
            };
        }
    }
}