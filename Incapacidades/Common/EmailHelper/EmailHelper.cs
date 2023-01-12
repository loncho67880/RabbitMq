namespace Common.EmailHelper
{
    public class EmailHelper : IEmailHelper
    {
        public void SendEmail(EmailModel email)
        {
            Console.WriteLine("Correo Enviado");
        }
    }
}