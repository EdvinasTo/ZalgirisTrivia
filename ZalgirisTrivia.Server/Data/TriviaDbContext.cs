using Microsoft.EntityFrameworkCore;
using Trivia.Models;
using ZalgirisTrivia.Server.Models;

namespace Trivia.Data
{
    public class TriviaDbContext : DbContext
    {
        public DbSet<Question> Questions { get; set; }      // A set of trivia's questions stored in the database
        public DbSet<Submission> Submissions { get; set; }  // A set of user submissions stored in the database
        public DbSet<User> Users { get; set; }              // a set of users stored in the database

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="options"> options </param>
        public TriviaDbContext(DbContextOptions<TriviaDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// configures the model and relationships for the database context
        /// </summary>
        /// <param name="modelBuilder"> model builder used to configure models </param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Question>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Submission>()
                .HasKey(u => u.Id);
        }

    }
}
