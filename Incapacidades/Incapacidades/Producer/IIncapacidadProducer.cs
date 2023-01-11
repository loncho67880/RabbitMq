namespace Incapacidades.Producer
{
    public interface IIncapacidadProducer
    {
        /// <summary>
        /// Enviar mensaje
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        void SendNotificatinoIncapacidadMessage<T>(T message);
    }
}
