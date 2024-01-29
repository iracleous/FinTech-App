using Microsoft.EntityFrameworkCore;

namespace FinTech_App.Model;

public interface IFinTechDbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public Task<int> SaveChangesAsync();

    //Set Entry
}
