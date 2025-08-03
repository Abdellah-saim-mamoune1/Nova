using bankApI.BusinessLayer.Dto_s;
using bankApI.BusinessLayer.Dto_s.ClientDto_s.DClientMain;
using bankApI.Models.ClientModels;
using bankApI.Models.ClientXEmployeeModels;
using bankApI.Models.EmployeeModels;
using Microsoft.EntityFrameworkCore;


namespace bankApI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Person> Persons { get; set; }

        public DbSet<Employee>Employees{ get; set; }

        public DbSet<Role> Roles { get; set; }
        public DbSet<Account>Accounts { get; set; }

        public DbSet<EmployeeType> EmployeeType { get; set; }

        public DbSet<EmployeeAccount> EmployeeAccount { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<TransactionsHistory> TransactionsHistory { get; set; }

        public DbSet<TransactionsType> TransactionsTypes { get; set; }


        public DbSet<ClientXNotifications> ClientXNotifications { get; set; }

        public DbSet<EmployeeNotifications> EmployeeNotifications { get; set; }

        public DbSet<NotificationsTypes> NotificationsTypes { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<LoginRegister> LoginRegister { get; set; }

        public DbSet<BankRevenue> BankRevenues { get; set; }

        public DbSet<TransferFundHistory> TransferFundHistory { get; set; }

        public DbSet<GetHelp> GetHelp { get; set; }
        public DbSet<Token> Tokens { get; set; }


        //DTOs 
        public DbSet<GetClientInfoDto> MainClientInfo { get; set; }
       // public DbSet<AccountGetDto> GetAccounts { get; set; }
        //public DbSet<CardGetDto> GetCards { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //DTOs

            modelBuilder.Entity<MainClientInfoDto>().HasNoKey().ToView(null);
         //   modelBuilder.Entity<AccountGetDto>().HasNoKey().ToView(null);
           // modelBuilder.Entity<CardGetDto>().HasNoKey().ToView(null);


            modelBuilder.Entity<Person>()
                .HasOne(p => p.Client)
                .WithOne(c => c.Person)
                .HasForeignKey<Client>(c => c.PersonId) 
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Person>()
              .HasOne(p => p.Employee)
              .WithOne(c => c.Person)
              .HasForeignKey<Employee>(c => c.PersonId)
              .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Account>()
            .HasOne(p => p.Person)
            .WithMany(c => c.Accounts)
            .HasForeignKey(c => c.PersonId)
            .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Card>()
               .HasOne(p => p.Account)
               .WithOne(c => c.Card)
               .HasForeignKey<Account>(c => c.CardId)
               .OnDelete(DeleteBehavior.Restrict);



            modelBuilder.Entity<Employee>()
        .HasOne(p => p.Role)
        .WithMany(c => c.Employees)
        .HasForeignKey(c => c.RoleTypeId)
        .OnDelete(DeleteBehavior.Restrict);

      

            modelBuilder.Entity<Employee>()
            .HasOne(p => p.EmployeeType)
            .WithMany(c => c.Employees)
            .HasForeignKey(c => c.TypeId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Person>()
              .HasOne(p => p.EmployeeAccount)
              .WithOne(c => c.Person)
              .HasForeignKey<EmployeeAccount>(c=>c.EmployeeId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Notification>()
           .HasOne(p => p.types)
           .WithMany(c => c.Notifications)
           .HasForeignKey(c => c.TypeId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionsHistory>()
                .HasOne(p => p.TransactionsType)
           .WithMany(c => c.TransactionHistory)
           .HasForeignKey(c => c.TypeId)
           .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransactionsHistory>()
              .HasOne(p => p.Account)
         .WithMany(c => c.AccountTransactionHistory)
         .HasForeignKey(c => c.AccountId)
         .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferFundHistory>()
          .HasOne(p => p.SenderAccountIds)
     .WithMany(c => c.SenderAccount)
     .HasForeignKey(c => c.SenderAccountId)
     .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TransferFundHistory>()
       .HasOne(p => p.RecipientAccountIds)
  .WithMany(c => c.RecipientAccount)
  .HasForeignKey(c => c.RecipientAccountId)
  .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GetHelp>()
  .HasOne(p => p.Account)
.WithMany(c => c.GetHelp)
.HasForeignKey(c => c.ClientAccountId)
.OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Employee>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<TransactionsHistory>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<GetHelp>()
.Property(l => l.Date)
.HasDefaultValueSql("GETDATE()");



            modelBuilder.Entity<TransferFundHistory>()
     .Property(l => l.CreatedAt)
     .HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<Client>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Account>()
.Property(l => l.CreatedAt)
.HasDefaultValueSql("GETDATE()");



            modelBuilder.Entity<LoginRegister>()
.Property(l => l.Date)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<BankRevenue>()
.Property(l => l.Date)
.HasDefaultValueSql("GETDATE()");


            modelBuilder.Entity<ClientXNotifications>()
.Property(l => l.Date)
.HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<EmployeeNotifications>()
.Property(l => l.Date)
.HasDefaultValueSql("GETDATE()");




            base.OnModelCreating(modelBuilder);
        }


    }
}
