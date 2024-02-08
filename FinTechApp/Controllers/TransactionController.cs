using FinTech_App.Dto;
using FinTech_App.Model;
using FinTechApp.Communication;
using FinTechApp.dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinTechApp.Controllers;

public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    private readonly IClientDataService _clientService;

    public TransactionController (ITransactionService transactionService, IClientDataService clientDataService)
    {
        _transactionService = transactionService;
        _clientService = clientDataService;
    }


    public async Task<IActionResult> Index()
    {
        var transactions = await _transactionService.GetTransactionAsync();
        return View(transactions);
    }

    public async Task< IActionResult> CreateTransaction() 
    {
        var clients = await _clientService.GetClientsAsync();
        var accounts = await _clientService.GetAccountsAsync();
        return View(
                new TransactionViewDto
                {
                    Accounts = accounts.Select(
                      account => new SelectListItem
                        {
                            Text = account.Name,
                            Value = account.Id.ToString()
                        }).ToList(),
                    Clients = clients.Select(
                        client => new SelectListItem 
                        { 
                            Text=client.Name,
                            Value = client.Id.ToString()
                        }).ToList(),
                    TransactionDto = new TransactionDto 
                    { 
                         EmployeeName=""
                    }
                }
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateTransaction(TransactionDto transactionDto)
    {
       await _transactionService.CreateTransactionAsync(transactionDto);

        return Redirect("Index");
    }

}
