namespace FinTech_App.Model
{
    public class Client:GenericModel<long>
    {
        public long Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public DateOnly DateOfBirth { get; set; }
        public Guid Identidier { get; set; } = Guid.NewGuid();
    }
}
