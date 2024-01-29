using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FinTech_App.Service
{
    public class FinTransactionService : GenericService<FinTechTransaction, long>
        , IFinTransactionService
    {
       
        public FinTransactionService
            (FinTechDbContext context, ILogger<GenericService<FinTechTransaction, long>> logger) 
            : base(context, logger)
        { 
        }

        public async Task<ActionResult<List<FinTechTransaction>>> GetClientTransactionsPagedAsync(
            int pageCount, int pageSize, long clientId, DateOnly startingDate, 
            DateOnly endingDate)
        {
            if(pageCount <= 0) pageCount = 1;
            if(pageSize <= 0 || pageSize>50) pageSize = 50;
            try {
            return await _context.Transactions
                 .Where(trans => trans.Client.Id == clientId)
                 .Where(trans => DateOnly.FromDateTime(trans.DateTime) > startingDate
                                        && DateOnly.FromDateTime(trans.DateTime) <endingDate )
                 .Skip(pageSize*(pageCount-1))
                 .Take(pageSize)
                 .ToListAsync(); }
            catch (Exception)
            {
                return new NotFoundResult();
            }
        }

        public async Task<ActionResult<long>> WithdrawTransactionAsync(TransactionDto transactionDto)
        {
            var account = await _context.Accounts.FindAsync(transactionDto.AccountId);
            var client = await _context.Clients.FindAsync(transactionDto.ClientId);

            FinTechTransaction finTechTransaction = new FinTechTransaction
            {
                Account = account,
                Client = client,
                DateTime = DateTime.Now,
                TransactionCategory = transactionDto.Category,
            };
        }


    }
}
