using FinTech_App.Model;
using Newtonsoft.Json;
using System.Text;

namespace FinTechApp.Communication;

public class ClientDataService: IClientDataService
{
    private  readonly string url = "https://localhost:7270/api/Client";

    public  Client? CreateClient(Client client)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = new Uri(url);
        var dataRequest = JsonConvert.SerializeObject(client);
        HttpContent httpContent = new StringContent(dataRequest, Encoding.UTF8, "application/json");
        var response = httpClient.PostAsync(url, httpContent);
        response.Wait();
        var result = response.Result;
        Client? clientResult = null;
        if (result.IsSuccessStatusCode)
        {
            var EmpResponse = result.Content.ReadAsStringAsync().Result;
            var dataResponse = JsonConvert.DeserializeObject<Client>(EmpResponse);
            clientResult = dataResponse;
        }
        return clientResult;
    }
    public  async Task<Client?> CreateClientAsync(Client client) 
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
    public  async Task DeleteClientAsync(long clientId)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = new Uri(url);
        var response = await httpClient.DeleteAsync(url + "/" + clientId);
        var EmpResponse = await response.Content.ReadAsStringAsync();
    }

   

    public  async Task<Client?> GetClientAsync(int clientId)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = new Uri(url + "/" + clientId);
        var response = await httpClient.GetAsync("");
        var empResponse = await response.Content.ReadAsStringAsync();
        var client = JsonConvert.DeserializeObject<Client>(empResponse);
        return client;
    }
    public  async Task<List<Client>?> GetClientsAsync()
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri(url);
        var response = await client.GetAsync("");
        var data = await response.Content.ReadAsStringAsync();
        var clients = JsonConvert.DeserializeObject<List<Client>>(data);
        return clients;
    }

    public async Task<List<Account>> GetAccountsAsync()
    {
        using HttpClient client = new();
        client.BaseAddress = new Uri("https://localhost:7270/api/Account");
        var response = await client.GetAsync("");
        var data = await response.Content.ReadAsStringAsync();
        var accounts = JsonConvert.DeserializeObject<List<Account>>(data);
        return accounts;
    }
}