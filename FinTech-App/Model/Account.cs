namespace FinTech_App.Model;

public class Account:IGenericModel<long>
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public decimal Balance { get; set; }
}
