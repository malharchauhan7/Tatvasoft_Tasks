using Microsoft.EntityFrameworkCore;

namespace Books.DataContext
{
    //DbContext is the primary class for interacting with database
    public class BooksContext : DbContext  //DbContext act as a bridge between our class and db 
    {
        public BooksContext(DbContextOptions<BooksContext> options) : base(options)
        {
            // DbContextOptions<BooksContext> used to configure the context 
            // base(options) yeh saare options base class (DbContext) ko bhej deta h 
        }

        //DbSet is a class representing collection of entities. (here Book)
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ForgotPassword> ForgotPasswords { get; set; }
        public DbSet<UserDetail> UserDetails { get; set; }

        public DbSet<MissionSkill> MissionSkills { get; set; }
        public DbSet<MissionTheme> MissionThemes { get; set; }
        public DbSet<Mission> Missions { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<MissionApplication> MissionApplications { get; set; } // Added this line for MissionApplication


    }
}
