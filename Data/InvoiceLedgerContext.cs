using InvoiceLedger.Models;
using Microsoft.EntityFrameworkCore;

namespace InvoiceLedger.Data;

public class InvoiceLedgerContext(DbContextOptions<InvoiceLedgerContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Invoice> Invoices => Set<Invoice>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(user => user.Email).IsUnique();
            entity.Property(user => user.Email).HasMaxLength(256);
            entity.Property(user => user.PasswordHash).HasMaxLength(64);
            entity.Property(user => user.FullName).HasMaxLength(200);
        });
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.Property(invoice => invoice.Number).HasMaxLength(50);
            entity.Property(invoice => invoice.Counterparty).HasMaxLength(200);
            entity.Property(invoice => invoice.Status).HasMaxLength(50);
            entity.Property(invoice => invoice.Amount).HasPrecision(18, 2);
        });
    }
}
