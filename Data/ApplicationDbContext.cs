using Microsoft.EntityFrameworkCore;

namespace CertiPurge.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Certificate> Certificates { get; set; }
}

public class Certificate
{
    public int Id { get; set; }
    public required string CandidateNumber { get; set; }
    public required string CentreNumber { get; set; }
    public required string ULN { get; set; }
    public required string Name { get; set; }
    public required string Course { get; set; }
    public required string Grade { get; set; }
    public string? DateIssued { get; set; }
    public DateOnly DestructionDate { get; set; }
}