using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2
{

    public class TournamentTop
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int Top { set; get; }
        public int Wins { set; get; }
        public int Loses { set; get; }
        public int Draws { set; get; }

        public TournamentTop()
        {

        }
        public TournamentTop(int id, string name, int top, int wins, int loses, int draws)
        {
            Id = id;
            Name = name;
            Top = top;
            Wins = wins;
            Loses = loses;
            Draws = draws;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Top: {Top}, Wins: {Wins}, Loses: {Loses}, Draws: {Draws}");
        }
    }
    public class TournamentTopContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=TournamentTopDB;Integrated Security=True";
        public DbSet<TournamentTop> TournamentTop { set; get; }

        public TournamentTopContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (TournamentTopContext db = new TournamentTopContext())
                {
                    TournamentTop tournamentTop1 = new TournamentTop(1, "Best team ever",1,20,0,1);
                    TournamentTop tournamentTop2 = new TournamentTop(2, "Not the best team ever", 1, 10, 10, 1);
                    TournamentTop tournamentTop3 = new TournamentTop(3, "Chempions", 1, 0, 20, 0);

                    db.TournamentTop.Add(tournamentTop1);
                    db.TournamentTop.Add(tournamentTop2);
                    db.TournamentTop.Add(tournamentTop3);
                    db.SaveChanges();

                    var tournamentTops = db.TournamentTop.ToList();
                    foreach (var tournamentTop in tournamentTops)
                    {
                        tournamentTop.Print();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}