using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SchoolBackoffice.Models;

namespace SchoolBackoffice.Data
{
	public class SchoolBackofficeContext : IdentityDbContext
    {
		public SchoolBackofficeContext(DbContextOptions<SchoolBackofficeContext> options)
                : base(options)
        {
        }
		public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Lesson>()
                .HasRequired<Teacher>(s => s.Teacher)
                .WithMany(g => g.Lessons)
                .HasForeignKey<int>(s => s.TeacherId);
        }*/
    }
}
