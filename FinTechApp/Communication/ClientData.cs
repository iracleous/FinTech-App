using FinTech_App.Model;
using Newtonsoft.Json;
using System.Text;

namespace FinTechApp.Communication
{
    public static class ClientData
    {
        private static readonly string url = "https://localhost:7270/api/Client";


        public static async Task<Client?> GetClientAsync(int clientId)
        {
            using HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri(url+ "/" + clientId);
            var response = await httpClient.GetAsync("");
            var empResponse = await response.Content.ReadAsStringAsync();
            var client =  JsonConvert.DeserializeObject<Client>(empResponse);
            return client;
        }


            public static List<Client>? GetClients()
        {
            using HttpClient client = new();
            client.BaseAddress = new Uri(url);
            var response = client.GetAsync("");
            response.Wait();
            var result = response.Result;

            List<Client>? clients = [];
            if (result.IsSuccessStatusCode)
            {
                var EmpResponse = result.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<List<Client>>(EmpResponse);
                clients = data;
            }
            return clients;
        }

        public static Client? CreateClient(Client client) {
            using HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri(url);
            var dataRequest = JsonConvert.SerializeObject(client);
            HttpContent httpContent = new StringContent(dataRequest, Encoding.UTF8,  "application/json");
            var response = httpClient.PostAsync(url, httpContent);
            response.Wait();
            var result = response.Result;

            Client? clientResult = null ;
            if (result.IsSuccessStatusCode)
            {
                var EmpResponse = result.Content.ReadAsStringAsync().Result;
                var dataResponse = JsonConvert.DeserializeObject<Client>(EmpResponse);
                clientResult = dataResponse;
            }
            return clientResult;
        }


        public static async Task<Client?> CreateClientAsync(Client client)
        {
            using HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri(url);
            var dataRequest = JsonConvert.SerializeObject(client);
            HttpContent httpContent = new StringContent(dataRequest, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, httpContent);
            var EmpResponse = await response.Content.ReadAsStringAsync();
            var dataResponse = JsonConvert.DeserializeObject<Client>(EmpResponse);
            return dataResponse;
        }

        public static void DeleteClient(long clientId)
        {
            using HttpClient httpClient = new();
            httpClient.BaseAddress = new Uri(url );


            var response = httpClient.DeleteAsync(url+ "/" + clientId);
            response.Wait();
            var result = response.Result;


            if (result.IsSuccessStatusCode)
            {
                var EmpResponse = result.Content.ReadAsStringAsync().Result;

            }
        }    
    }
}
