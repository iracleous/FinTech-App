using FinTech_App.Dto;
using FinTech_App.Model;
using FinTech_App.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinTech_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : GenericController<FinTechTransaction, long>
    {
        private readonly IFinTransactionService _service;
        public TransactionsController(IFinTransactionService service) 
            : base(service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("customer")]
        public async Task<ActionResult<List<FinTechTransaction>>> GetTransactions(
            ViewTransactionsDto viewTransactionsDto)
        {
            return await _service.GetClientTransactionsPagedAsync(viewTransactionsDto);
        }


        [HttpPost]
        [Route("customer")]
        public async Task<ActionResult<long>> WithdrawTransactionAsync(
            TransactionDto transactionDto)
        {
            return await _service.WithdrawTransactionAsync(transactionDto);
        }
    }
}
