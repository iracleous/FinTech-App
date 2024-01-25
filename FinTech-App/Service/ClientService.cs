using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTech_App.Service;

public class ClientService : IClientService
{
    private readonly FinTechDbContext _context;
    private ILogger<ClientService> _logger;

    public ClientService(FinTechDbContext context, ILogger<ClientService> logger)
    {
        _context = context;
        _logger = logger;
    }


    public async Task<ActionResult<Client>> CreateClient(Client client)
    {
        _logger.LogInformation("Method CreateClient starting");
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<IActionResult> DeleteClient(long id)
    {
        _logger.LogInformation("Method DeleteClient starting");
        var client = await _context.Clients.FindAsync(id);
        if (client == null)
        {
            return new NotFoundResult();
        }
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return new NoContentResult();
    }

    public async Task<ActionResult<Client>> ReadClient(long clientId)
    {
        _logger.LogInformation("Method ReadClient starting");
        var client = await _context.Clients.FindAsync(clientId);
        if (client == null)
        {
            return new NotFoundResult();
        }
         
        return client;
    }

    public async Task<ActionResult<IEnumerable<Client>>> ReadClient()
    {
        _logger.LogInformation("Method ReadClient starting");
        return await _context.Clients.ToListAsync();
    }

    public async Task<IActionResult> UpdateClient(long id, Client client)
    {
        _logger.LogInformation("Method UpdateClient starting");
        if (id != client.Id)
        {
            return new BadRequestResult();
        }

        _context.Entry(client).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ClientExists(id))
            {
                return new NotFoundResult();
            }
            else
            {
                throw;
            }
        }

        return new NoContentResult();
    }


    private bool ClientExists(long id)
    {
        return _context.Clients.Any(e => e.Id == id);
    }
}

    

 
