using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.DbContexts
{
    public class HighScoreDbContext : DbContext
    {
        public HighScoreDbContext(DbContextOptions<HighScoreDbContext> options) : base(options) { }

        public DbSet<Highscore> Highscores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database = HighScoreDb;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Adding some constraints
            modelBuilder.Entity<Highscore>().HasKey(h => new { h.Id });
            modelBuilder.Entity<Highscore>().Property(h => h.Name).IsRequired();
            modelBuilder.Entity<Highscore>().Property(h => h.Time).IsRequired();
            modelBuilder.Entity<Highscore>().Property(p => p.Name).HasMaxLength(100);

            //Seeding
            var HattoriHanzoGuid = Guid.Parse("{B0788D2F-8003-43C1-92A4-EDC76A7C5DDE}");
            var VincentVegaGuid = Guid.Parse("{6313179F-7837-473A-A4D5-A5571B43E6A6}");
            var BeatrixKiddoGuid = Guid.Parse("{BF3F3002-7E53-441E-8B76-F6280BE284AA}");
            var CalvinCandieGuid = Guid.Parse("{FE98F549-E790-4E9F-AA16-18C2292A2EE9}");
            var BillGuid = Guid.Parse("{1098F549-E790-4E9F-AA16-18C2292A2EE9}");
            var WinstonWolfGuid = Guid.Parse("{1198F549-E790-4E9F-AA16-18C2292A2EE9}");
            var JackieBrownGuid = Guid.Parse("{1298F549-E790-4E9F-AA16-18C2292A2EE9}");
            var MarsellusWallaceGuid = Guid.Parse("{1398F549-E790-4E9F-AA16-18C2292A2EE9}");
            var MrPinkGuid = Guid.Parse("{1498F549-E790-4E9F-AA16-18C2292A2EE9}");
            var ZedGuid = Guid.Parse("{1598F549-E790-4E9F-AA16-18C2292A2EE9}");
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = HattoriHanzoGuid, Name = "Hattori Hanzo", Time = 250 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = VincentVegaGuid, Name = "Vincent Vega", Time = 300 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = BeatrixKiddoGuid, Name = "Beatrix Kiddo", Time = 350 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = CalvinCandieGuid, Name = "Calvin Candie", Time = 400 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = BillGuid, Name = "Bill", Time = 450 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = WinstonWolfGuid, Name = "Winston Wolf", Time = 500 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = JackieBrownGuid, Name = "Jackie Brown", Time = 550 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = MarsellusWallaceGuid, Name = "Marsellus Wallace", Time = 600 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = MrPinkGuid, Name = "Mr Pink", Time = 650 });
            modelBuilder.Entity<Highscore>().HasData(new Highscore { Id = ZedGuid, Name = "Zed", Time = 700 });
        }
    }
}