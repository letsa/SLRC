using Bogus;
using ExamenProgramacion.Models;

namespace ExamenProgramacion.Service
{
    public class DataGeneratorService : IDataGeneratorService
    {
        public List<int> GenerarNumeros()
        {
			List<int> list = new List<int>();
			for (int i = 0; i < 25; i++)
			{
				list.Add(new Random().Next(0,500));
			}
			return list;
        }

        public List<PalabraDTO> GenerarPalabras()
        {
			List<PalabraDTO> list = new List<PalabraDTO>();
			for (int i = 0; i < 100; i++)
			{
				var palabra = new Faker<PalabraDTO>()
                    .RuleFor(x => x.Palabra, f => f.Lorem.Word());
				list.Add(palabra.Generate());
            }
			return list;
        }

        public List<DatosPedidos> GenerarPedidos()
        {
            var productos = new[] { "apple", "banana", "orange", "strawberry", "kiwi" };
            var pedidos = new List<DatosPedidos>();
            for (int i = 1; i < 500; i++)
            {
                var persona = new Faker<DatosPedidos>()
                    .RuleFor(x => x.Id, f=> f.Random.Number(1, 100))
                    .RuleFor(x => x.OrdenId, f=> f.Random.Number(1, 10000))
                    .RuleFor(x => x.Producto, (f, x) => f.PickRandom(productos))
                    .RuleFor(x => x.Cantidad, (f, x) => f.Random.Number(1, 10));
                pedidos.Add(persona.Generate());
            }

            return pedidos;
        }

        public List<Persona> GenerarPersonas()
        {
			try
			{
				var personas = new List<Persona>();
				for (int i = 0; i < 500; i++)
				{
					var persona = new Faker<Persona>()
						.RuleFor(x => x.Edad, (f, x) => f.Random.Number(10, 100))
						.RuleFor(x => x.Nombre, (f, x) => f.Name.FirstName());
					personas.Add(persona.Generate());
				}

				return personas;
			}
			catch (Exception ex)
			{
				return null;
			}
        }

        public List<DatosRecibidos> GenerarUsuarios()
        {
            List<DatosRecibidos> list = new List<DatosRecibidos>();
			for (int i = 1; i < 100; i++)
			{
                var persona = new Faker<DatosRecibidos>()
                        .RuleFor(x => x.Id, f=>i)
                        .RuleFor(x => x.Nombre, (f, x) => f.Name.FirstName())
                        .RuleFor(x => x.Apellido, (f, x) => f.Name.LastName());
				list.Add(persona.Generate());
            }
			return list;
        }

        public List<DatosUsuario> GenerarUsuariosDatos()
        {
            List<DatosUsuario> list = new List<DatosUsuario>();
            for (int i = 1; i < 100; i++)
            {
                var persona = new Faker<DatosUsuario>()
                        .RuleFor(x => x.Id, f => i)
                        .RuleFor(x => x.idUnique, f => Guid.NewGuid())
                        .RuleFor(x => x.Email, (f, x) => f.Internet.Email())
                        .RuleFor(x => x.Usuario, (f, x) => f.Internet.UserName());
                list.Add(persona.Generate());
            }
            return list;
        }
    }
}
