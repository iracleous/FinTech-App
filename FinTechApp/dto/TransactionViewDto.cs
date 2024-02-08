using FinTech_App.Dto;
using FinTech_App.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinTechApp.dto
{
    public class TransactionViewDto
    {
        public required List<SelectListItem> Clients {  get; set; }
        public required List<SelectListItem> Accounts { get; set; }
        public required TransactionDto TransactionDto { get; set; }
    }
}
