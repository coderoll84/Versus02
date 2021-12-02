using Aplicacion.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Resultados
{
    public class RestClient
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<Tabla> Get(string ctlr, int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<Tabla>($"http://localhost:5206/{ctlr}/get/{id}");
        }

        public async Task<List<Tabla>> GetSome(string ctlr, int from, int to)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<List<Tabla>>($"http://localhost:5206/{ctlr}/getsome/{from}/{to}");
        }

        public async Task<TablaDto[]> GetDto(string ctlr, int from, int to)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<TablaDto[]>($"http://localhost:5206/{ctlr}/getdto/{from}/{to}");
        }

        public async Task<Tabla> GetSp(string ctlr, int id)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<Tabla>($"http://localhost:5206/{ctlr}/GetSp/{id}");
        }

        public async Task<List<Tabla>> GetSomeSp(string ctlr, int from, int to)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<List<Tabla>>($"http://localhost:5206/{ctlr}/getsomesp/{from}/{to}");
        }

        public async Task<List<Tabla>> GetDtoSp(string ctlr, int from, int to)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return await client.GetFromJsonAsync<List<Tabla>>($"http://localhost:5206/{ctlr}/GetDtoSp/{from}/{to}");
        }
    }
}