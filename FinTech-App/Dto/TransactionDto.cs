using FinTech_App.Model;
using System.ComponentModel.DataAnnotations;

namespace FinTech_App.Dto;

public class TransactionDto
{
    [Required]
    public long ClientId { get; set; }
    [Required]
    public long AccountId { get; set; }
    public TransactionCategory Category { get; set; }
    public decimal Amount { get; set; }
    public required string  EmployeeName {  get; set; }
}
