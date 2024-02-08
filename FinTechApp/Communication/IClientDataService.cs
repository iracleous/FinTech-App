using FinTech_App.Dto;
using FinTech_App.Model;
using Newtonsoft.Json;
using System.Text;

namespace FinTechApp.Communication;

public interface IClientDataService  
{
    public Task<List<Client>> GetClientsAsync();
    public Task<Client?> CreateClientAsync(Client client);
    public Task<Client?> GetClientAsync(int clientId);
    public Task DeleteClientAsync(long clientId);

    public Task<List<Account>> GetAccountsAsync();

}


public interface ITransactionService
{
    public Task<FinTechTransaction?> CreateTransactionAsync(TransactionDto transactionDto);
    public Task<List<FinTechTransaction>?> GetTransactionAsync();
}
