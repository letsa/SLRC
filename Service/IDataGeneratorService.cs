using ExamenProgramacion.Models;

namespace ExamenProgramacion.Service
{
    public interface IDataGeneratorService
    {
        List<int> GenerarNumeros();
        List<PalabraDTO> GenerarPalabras();
        List<DatosPedidos> GenerarPedidos();
        List<Persona> GenerarPersonas();
        List<DatosRecibidos> GenerarUsuarios();
        List<DatosUsuario> GenerarUsuariosDatos();
    }
}
