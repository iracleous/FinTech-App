using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTech_App.Service;

public class FinTransactionService : GenericService<FinTechTransaction, long>
    , IFinTransactionService
{
    public FinTransactionService
        (FinTechDbContext context, ILogger<GenericService<FinTechTransaction, long>> logger) 
        : base(context, logger)
    { 

    }

    public async Task<ActionResult<List<FinTechTransaction>>> GetClientTransactionsPagedAsync(
        ViewTransactionsDto viewTransactionsDto)
    {
        _logger.LogInformation("Method GetClientTransactionsPagedAsync starting");
        if (viewTransactionsDto.PageCount <= 0) viewTransactionsDto.PageCount = 1;
        if(viewTransactionsDto.PageSize <= 0 || viewTransactionsDto.PageSize > 50)
            viewTransactionsDto.PageSize = 50;
        try {
        return await _context.Transactions
             .Where(trans => trans.Client.Id == viewTransactionsDto.ClientId)
             .Where(trans => DateOnly.FromDateTime(trans.DateTime) > viewTransactionsDto.StartingDate
                                    && DateOnly.FromDateTime(trans.DateTime) < viewTransactionsDto.EndingDate )
             .Skip(viewTransactionsDto.PageSize *(viewTransactionsDto.PageCount -1))
             .Take(viewTransactionsDto.PageSize)
             .ToListAsync(); }
        catch (Exception)
        {
            return new NotFoundResult();
        }
    }

    public async Task<ActionResult<long>> WithdrawTransactionAsync(TransactionDto transactionDto)
    {
        _logger.LogInformation("Method WithdrawTransactionAsync starting");
        var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
        var client = await _context.Clients.FindAsync(transactionDto.ClientId);
        if (account==null || client ==null) return new NotFoundResult();
        try 
        { 
            FinTechTransaction finTechTransaction = new FinTechTransaction
            {
                Account = account,
                Client = client,
                DateTime = DateTime.Now,
                TransactionCategory = transactionDto.Category,
            };
            _context.Transactions.Add(finTechTransaction);
            await _context.SaveChangesAsync();
            return finTechTransaction.Id;
        }
        catch (Exception)
        {
            return new NoContentResult();
        }
    }
}
