using FinTech_App.Model;

namespace FinTech_App.Dto;

public class TransactionDto
{
    public long ClientId { get; set; }
    public long AccountId { get; set; }
    public TransactionCategory Category { get; set; }
    public decimal Amount { get; set; }
    public required string  EmployeeName {  get; set; }
}
