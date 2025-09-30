using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Certificate> Certificates { get; set; }
}

public class Certificate
{
    public int Id { get; set; }
    public string CandidateNumber { get; set; }
    public string CentreNumber { get; set; }
    public string ULN { get; set; }
    public string Name { get; set; }
    public string Course { get; set; }
    public string Grade { get; set; }
    public string DateIssued { get; set; }
    public string DestructionDate { get; set; }
    public bool Destroyed { get; set; }
}