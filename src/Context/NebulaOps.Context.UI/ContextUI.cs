using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using NebulaOps.Context.UI.Entity;

namespace NebulaOps.Context.UI;

public class ContextUI : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=nebulaops_ui;Username=postgres;Password=123"
            );
        }
    }


    public DbSet<Agent> Agents{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agent>(entity =>
        {
            entity.ToTable("agents");

            entity.HasKey(e => e.Id);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.Hostname)
                .HasColumnName("hostname")
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.IpAddress)
                .HasColumnName("ip_address")
                .HasMaxLength(45)
                .IsRequired();

            entity.Property(e => e.Port)
                .HasColumnName("port");

            entity.Property(e => e.Username)
                .HasColumnName("username")
                .HasMaxLength(50);

            entity.Property(e => e.Password)
                .HasColumnName("password");

            entity.Property(e => e.SshKey)
                .HasColumnName("ssh_key");

            entity.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasIndex(e => e.IpAddress)
                .IsUnique();
        });
    }

}
