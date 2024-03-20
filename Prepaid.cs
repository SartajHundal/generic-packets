using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

// Define the User entity
public class User
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public List<PrepaidAccount> PrepaidAccounts { get; set; }
}

// Define the PrepaidAccount entity
public class PrepaidAccount
{
    public int PrepaidAccountId { get; set; }
    public decimal Balance { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public List<Transaction> Transactions { get; set; }
}

// Define the Transaction entity
public class Transaction
{
    public int TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }
    public int PrepaidAccountId { get; set; }
    public PrepaidAccount PrepaidAccount { get; set; }
}

// Define the database context
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<PrepaidAccount> PrepaidAccounts { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("your_connection_string_here");
    }
}

// Example usage:
class Program
{
    static void Main(string[] args)
    {
        using (var context = new AppDbContext())
        {
            // Create a user with a prepaid account
            var user = new User { UserName = "JohnDoe" };
            var prepaidAccount = new PrepaidAccount { Balance = 100.00m };
            user.PrepaidAccounts = new List<PrepaidAccount> { prepaidAccount };
            context.Users.Add(user);
            context.SaveChanges();

            // Perform a transaction
            var transaction = new Transaction { Amount = 50.00m, Timestamp = DateTime.Now, PrepaidAccountId = prepaidAccount.PrepaidAccountId };
            prepaidAccount.Transactions = new List<Transaction> { transaction };
            prepaidAccount.Balance -= transaction.Amount;
            context.Transactions.Add(transaction);
            context.SaveChanges();

            // Retrieve user's prepaid account balance
            var userWithPrepaidAccount = context.Users.Include(u => u.PrepaidAccounts).ThenInclude(p => p.Transactions).FirstOrDefault();
            if (userWithPrepaidAccount != null)
            {
                foreach (var account in userWithPrepaidAccount.PrepaidAccounts)
                {
                    Console.WriteLine($"User: {userWithPrepaidAccount.UserName}, Prepaid Account Balance: {account.Balance}");
                    foreach (var trans in account.Transactions)
                    {
                        Console.WriteLine($"Transaction ID: {trans.TransactionId}, Amount: {trans.Amount}, Timestamp: {trans.Timestamp}");
                    }
                }
            }
        }
    }
}
