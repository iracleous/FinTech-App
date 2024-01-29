﻿using Microsoft.EntityFrameworkCore;

namespace FinTech_App.Model;

public class FinTechDbContext:DbContext
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<FinTechTransaction> Transactions { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //  string connectionString = "Data Source=(local);Initial Catalog=finTech-2024; Integrated Security = True;TrustServerCertificate=True;";

        var MyConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
        var connectionString = MyConfig.GetValue<string>("AppSettings:MyConn");

        optionsBuilder.UseSqlServer(connectionString);
    }

    public FinTechDbContext(DbContextOptions<FinTechDbContext> options)
       : base(options)
    {
    }

    public FinTechDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>()
            .Property(account => account.Balance)
            .HasPrecision(12, 2);
        base.OnModelCreating(modelBuilder);
    }


}
