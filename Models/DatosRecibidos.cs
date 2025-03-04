namespace ExamenProgramacion.Models
{
    public class DatosRecibidos :DatosUsuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public List<object> Pedidos { get; set; }
    }
}
