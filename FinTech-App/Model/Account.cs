namespace FinTech_App.Model
{
    public class Account:GenericModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}
