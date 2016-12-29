using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SmartAudit.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<AuditDefinition> AuditDefinitions { get; set; }
        public DbSet<SectionDefinition> SectionDefinitions { get; set; }
        public DbSet<QuestionDefinition> QuestionDefinitions { get; set; }
        public DbSet<Audit> Audit { get; set; }
        public DbSet<QuestionResult> QuestionResults { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<ActionComment> ActionComments { get; set; }
        public DbSet<AuditStatus> AuditStates { get; set; }
        public DbSet<PeriodType> PeriodTypes { get; set; }
        public DbSet<Quarter> Quarters { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}