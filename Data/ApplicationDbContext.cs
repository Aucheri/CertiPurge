using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CertiPurge.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Certificate> Certificates { get; set; }
}

public class Certificate
{
    public int Id { get; set; }
    public required string ExamBoard { get; set; }
    public required string CandidateNumber { get; set; }
    public required string CentreNumber { get; set; }
    public required string ULN { get; set; }
    public required string Name { get; set; }
    public required string Course { get; set; }
    public required string Grade { get; set; }
    public string? AwardingDate { get; set; }
    public DateOnly DestructionDate { get; set; }
}