using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Service;


//synchronous
public interface IClientService2
{
    //CRUD 5 services
    public Client CreateClient(Client client);
    public Client ReadClient(long clientId);
    public List<Client> ReadClient();
    public Client UpdateClient(long clientId, Client client);
    public bool DeleteClient(long clientId);
}

// asynchronous call
public interface IClientService
{
    //CRUD 5 services
    public Task<ActionResult<Client>> CreateClient(Client client);
    public Task<ActionResult<Client>> ReadClient(long clientId);
    public Task<ActionResult<IEnumerable<Client>>> ReadClient();
    public Task<IActionResult> UpdateClient(long clientId, Client client);
    public Task<IActionResult> DeleteClient(long clientId);
}
