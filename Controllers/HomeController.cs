using ExamenProgramacion.Models;
using ExamenProgramacion.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.ComponentModel.DataAnnotations;

namespace ExamenProgramacion.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataGeneratorService _dataGeneratorService;

        public HomeController(IDataGeneratorService dataGeneratorService)
        {
            _dataGeneratorService = dataGeneratorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Agrupar y contar numeros
        public IActionResult Prueba1()
        {
            //Con este array de numeros 
            //Agrupar y ordenar descendentemente por numero de repeticiones 
            //Ejemplo de respueta [{"valor":#numero,"contador":#Repeticiones},{"valor":#numero,"contador":#Repeticiones}]
            int[] arr1 = new int[] { 5, 9, 1, 2, 3, 7, 5, 6, 7, 3, 7, 6, 8, 5, 4, 9, 6, 2 };
            var agrupados = arr1
                .GroupBy(n => n)
                .Select(x => new { valor = x.Key, contador = x.Count() }).ToList();
            string jsonDatos = JsonSerializer.Serialize(agrupados);
            return Ok(jsonDatos);
        }

        //Buscar por Rango de Edad >25 & <65 y las personas que inicien con la letra enviada  
        public IActionResult Prueba2(string letra)
        {
            //500 personas
            List<Persona> listaPersonas = _dataGeneratorService.GenerarPersonas();
            var resultado = listaPersonas.Where(x => (x.Edad > 25 && x.Edad < 65) && (x.Nombre.StartsWith(letra))).ToList();
            string jsonPersonas = JsonSerializer.Serialize(resultado);
            return Ok(jsonPersonas);
        }

        //Separar lista de numeros en pares o impares
        public IActionResult Prueba3()
        {
            //Resultado esperado {"pares":[300,34],"impares":[81,3]}
            List<int> numeros = _dataGeneratorService.GenerarNumeros();
            var pares = numeros.Where(valor => valor % 2 == 0).ToList();
            var impares = numeros.Where(valor => valor % 2 != 0).ToList();

            var ParesImpares = new
            {
                Pares = pares,
                Impares = impares,
            };

            string jsonParesImpares = JsonSerializer.Serialize(ParesImpares, new JsonSerializerOptions { WriteIndented = false });

            return Ok(jsonParesImpares);
        }

        //Agrupar palabras por mayor numero de vocales
        public IActionResult Prueba4()
        {
            //resultado Esperado {"a":["palabra1","Palabra2"],"e":["pelebre1"],"i":["pilibri1"]}
            Dictionary<char, List<string>> VocalesAgrupadas = new Dictionary<char, List<string>>();

            List<PalabraDTO> palabras = _dataGeneratorService.GenerarPalabras();

            foreach (var Palabra in palabras)
            {
                var vocal = vocalMayor(Palabra.Palabra);

                if (!VocalesAgrupadas.ContainsKey(vocal))
                {
                    VocalesAgrupadas[vocal] = new List<string>();
                }

                VocalesAgrupadas[vocal].Add(Palabra.Palabra);


            }
            VocalesAgrupadas.OrderBy(v => v.Key);
            string jsonVocales = JsonSerializer.Serialize(VocalesAgrupadas, new JsonSerializerOptions { WriteIndented = true });

            return Ok(jsonVocales);
        }

        private char vocalMayor(string palabra)
        {
            int cantA = palabra.Count(a => a == 'a' || a == 'A');
            int cantE = palabra.Count(e => e == 'e' || e == 'E');
            int cantI = palabra.Count(i => i == 'i' || i == 'I');
            int cantO = palabra.Count(o => o == 'o' || o == 'O');
            int cantU = palabra.Count(u => u == 'u' || u == 'U');

            int mayor = Math.Max(Math.Max(cantA, cantE), Math.Max(Math.Max(cantI, cantO), cantU));

            if (cantA == mayor) return 'A';
            if (cantE == mayor) return 'E';
            if (cantI == mayor) return 'I';
            if (cantO == mayor) return 'O';
            if (cantU == mayor) return 'U';


            throw new NotImplementedException();
        }




        //Combinar los datos de usuarios con los datos de Pedidos
        [HttpPost]
        public IActionResult Prueba5()
        {
            /*
             * Ejemplo de Respuesta:
             * [{
                    "id": 1,
                    "nombre": "Lolita",
                    "apellido": "Kreiger",
                    "pedidos": [
                        {
                            "ordenId": 4568,
                            "producto": "kiwi",
                            "cantidad": 2
                        }
                    ],
                    "usuario": "Clementina81",
                    "email": "Sherman.Jacobi@hotmail.com",
                    "idUnique": "7a04e5d6-594a-44a6-87ee-3f7f6cf72ffb"
                }]
             */

            List<DatosUsuario> usuarios = _dataGeneratorService.GenerarUsuariosDatos();
            List<DatosPedidos> usuariosPedidos = _dataGeneratorService.GenerarPedidos();
            List<DatosRecibidos> usuario = _dataGeneratorService.GenerarUsuarios();

            var Resultado = from nombreUsuario in usuario join  datoUsuario in usuarios on nombreUsuario.Id equals datoUsuario.Id
                            join pedido in usuariosPedidos on datoUsuario.Id equals pedido.Id into pedidoUsuario
                            select new
                            {
                                Nombre= nombreUsuario.Nombre.ToString(),
                                apellido= nombreUsuario.Apellido.ToString(),
                                pedido = pedidoUsuario.ToList(),
                                Usuario = datoUsuario.Usuario,
                                Email = datoUsuario.Email,
                                IdUnique= datoUsuario.idUnique

                            };

            string jsonPedidosUsuarios = JsonSerializer.Serialize(Resultado, new JsonSerializerOptions { WriteIndented=true});
            return Ok(jsonPedidosUsuarios);
}
    }
}