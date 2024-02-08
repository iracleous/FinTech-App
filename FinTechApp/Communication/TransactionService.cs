using FinTech_App.Dto;
using FinTech_App.Model;
using Newtonsoft.Json;
using System.Security.Policy;
using System.Text;


namespace FinTechApp.Communication;

public class TransactionService : ITransactionService
{
    private readonly string _url = "https://localhost:7270/api/Transactions";


    public async Task<FinTechTransaction?> CreateTransactionAsync(TransactionDto transactionDto)
    {
        using HttpClient httpClient = new();
        httpClient.BaseAddress = new Uri(_url + "/customer");
        var dataRequest = JsonConvert.SerializeObject(transactionDto);
        HttpContent httpContent = new StringContent(dataRequest, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(_url + "/customer", httpContent);
        var data = await response.Content.ReadAsStringAsync();
        var dataResponse = JsonConvert.DeserializeObject<FinTechTransaction?>(data);
        return dataResponse;
    }

    public async Task<List<FinTechTransaction>?> GetTransactionAsync()
    {
        using HttpClient httpCient = new();
        httpCient.BaseAddress = new Uri(_url);
        var response = await httpCient.GetAsync("");
        var data = await response.Content.ReadAsStringAsync();
        var transactions = JsonConvert.DeserializeObject<List<FinTechTransaction>?>(data);
        return transactions;
    }
}
