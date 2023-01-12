namespace Core.Incapacidades
{
    public class Incapacidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string causa_anulacion { get; set; }
        public string observacion { get; set; }
        public DateTime fecha_anulacion { get; set; }
        public string mailNotificacion { get; set; }
    }
}