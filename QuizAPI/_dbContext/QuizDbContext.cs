using Microsoft.EntityFrameworkCore;
using QuizAPI.Models;

namespace QuizAPI._dbContext
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options)
        { }

        public DbSet<Questions> Questions { get; set; }
        public DbSet<Participants> Participants { get; set; }
    }
}
