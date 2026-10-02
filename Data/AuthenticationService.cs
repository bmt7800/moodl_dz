using System.Security.Cryptography;
using System.Text;
using InvoiceLedger.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace InvoiceLedger.Data;

public sealed class AuthenticationService
{
    private readonly string _connectionString;

    public AuthenticationService()
    {
        var configuration = new ConfigurationBuilder().SetBasePath(AppContext.BaseDirectory).AddJsonFile("appsettings.json").Build();
        _connectionString = configuration.GetConnectionString("InvoiceLedger") ?? throw new InvalidOperationException("Строка подключения InvoiceLedger не найдена.");
    }

    public async Task<User?> SignInAsync(string email, string password)
    {
        var options = new DbContextOptionsBuilder<InvoiceLedgerContext>().UseSqlServer(_connectionString).Options;
        await using var context = new InvoiceLedgerContext(options);
        await context.Database.EnsureCreatedAsync();
        await SeedAdminAsync(context);
        var hash = HashPassword(password);
        return await context.Users.SingleOrDefaultAsync(user => user.Email == email && user.PasswordHash == hash);
    }

    private static async Task SeedAdminAsync(InvoiceLedgerContext context)
    {
        if (await context.Users.AnyAsync()) return;
        context.Users.Add(new User { Email = "admin@finflow.local", FullName = "Администратор", PasswordHash = HashPassword("Admin123!") });
        await context.SaveChangesAsync();
    }

    private static string HashPassword(string password) => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password)));
}
