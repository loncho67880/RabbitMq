using Common.EmailHelper;
using Common.ReadTemplateHelper;
using Core.Incapacidades;
using Microsoft.Extensions.Configuration;
using static Common.EmailHelper.EmailModel;

namespace Servicios.AnulacionServicio
{
    public class AnulacionServicio : IAnulacionServicio
    {
        private readonly IReadTemplateHelper _readTemplateHelper;
        private readonly IConfiguration _configuration;
        private readonly IEmailHelper _emailHelper;

        public AnulacionServicio(IReadTemplateHelper readTemplateHelper, IConfiguration configuration, IEmailHelper emailHelper)
        {
            _readTemplateHelper = readTemplateHelper;
            _configuration = configuration;
            _emailHelper = emailHelper;
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
                Body = new BodyMail()
                {
                    Title = "",
                    HtmlContent = templateHtmlBaja,
                },
                Type = EmailModel.MailType.Alerta,
                Addressee = new DestinationMail()
                {
                    To = _configuration["mailNotificacion"].Split(";").ToList()
                }
            };

            _emailHelper.SendEmail(emailAnulacion);
        }
    }
}