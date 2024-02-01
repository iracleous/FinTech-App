namespace FinTech_App.Model;

public class FinTechTransaction: IGenericModel<long>
{
    public long Id { get; set; }
 
    public DateTime DateTime { get; set; }
    public TransactionCategory TransactionCategory { get; set; }

    public virtual required Client Client { get; set; }
    public virtual required Account Account { get; set; }
}
