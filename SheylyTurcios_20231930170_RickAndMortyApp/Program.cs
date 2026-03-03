using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("=== API Rick and Morty ===");
        Console.WriteLine("1. Mostrar lista de personajes");
        Console.WriteLine("2. Buscar personaje por ID");
        Console.Write("Seleccione una opción: ");

        string opcion = Console.ReadLine();

        if (opcion == "1")
        {
            await ObtenerPersonajes();
        }
        else if (opcion == "2")
        {
            Console.Write("Ingrese el ID del personaje: ");
            int id = int.Parse(Console.ReadLine());
            await ObtenerPersonajePorId(id);
        }
        else
        {
            Console.WriteLine("Opción inválida.");
        }

        Console.WriteLine("\nPresione cualquier tecla para salir...");
        Console.ReadKey();
    }

    static async Task ObtenerPersonajes()
    {
        using (HttpClient client = new HttpClient())
        {
            string url = "https://rickandmortyapi.com/api/character";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                Root data = JsonConvert.DeserializeObject<Root>(json);

                foreach (var personaje in data.results)
                {
                    Console.WriteLine("Nombre: " + personaje.name);
                    Console.WriteLine("Especie: " + personaje.species);
                    Console.WriteLine("Estado: " + personaje.status);
                    Console.WriteLine("------------------------");
                }
            }
            else
            {
                Console.WriteLine("Error al conectar con el API");
            }
        }
    }

    static async Task ObtenerPersonajePorId(int id)
    {
        using (HttpClient client = new HttpClient())
        {
            string url = $"https://rickandmortyapi.com/api/character/{id}";

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();

                Character personaje = JsonConvert.DeserializeObject<Character>(json);

                Console.WriteLine("\n=== Personaje Encontrado ===");
                Console.WriteLine("Nombre: " + personaje.name);
                Console.WriteLine("Especie: " + personaje.species);
                Console.WriteLine("Estado: " + personaje.status);
            }
            else
            {
                Console.WriteLine("No se encontró el personaje.");
            }
        }
    }
}

public class Root
{
    public List<Character> results { get; set; }
}

public class Character
{
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
}