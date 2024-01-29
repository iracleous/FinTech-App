using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Service;

public interface IFinTransactionService:IGenericService<FinTechTransaction, long>
{
    public Task<ActionResult<long>> WithdrawTransactionAsync(TransactionDto transactionDto);
    public Task<ActionResult<List<FinTechTransaction>>>
        GetClientTransactionsPagedAsync(int pageCount,int pageSize, long CustomerId,
        DateOnly startingDate, DateOnly endingDate);

}
