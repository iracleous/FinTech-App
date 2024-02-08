using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Service;

public interface IFinTransactionService:IGenericService<FinTechTransaction, long>
{
    public Task<ActionResult<FinTechTransaction?>> CreateTransactionAsync(
        TransactionDto transactionDto);
    public Task<ActionResult<List<FinTechTransaction>>>
        GetClientTransactionsPagedAsync(ViewTransactionsDto viewTransactionsDto);
}
