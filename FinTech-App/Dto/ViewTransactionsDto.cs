namespace FinTech_App.Dto
{
    public class ViewTransactionsDto
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public long ClientId { get; set; }
        public DateOnly StartingDate { get; set; }
        public DateOnly EndingDate { get; set; }
    }
} 
