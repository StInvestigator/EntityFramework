using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp2
{

    public class Game
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Creator { set; get; }
        public string Style { set; get; }
        public DateTime ReleaseDate { set; get; }

        public Game()
        {

        }
        public Game(int id, string name, string creator, string style, DateTime releaseDate)
        {
            Id = id;
            Name = name;
            Creator = creator;
            Style = style;
            ReleaseDate = releaseDate;
        }

        public void Print()
        {
            Console.WriteLine($"Id: {Id}, Name: {Name}, Creator: {Creator}, Style: {Style}, Release Date: {ReleaseDate}");
        }
    }
    public class GameContext : DbContext
    {
        public string connectionString = @"Data Source=DESKTOP-OF66R01\SQLEXPRESS;Initial Catalog=GamesDB;Integrated Security=True";
        public DbSet<Game> Game { set; get; }

        public GameContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
    //public string connectionString = @"Data Source=AVL_Laptop\SQLEXPRESS;Initial Catalog=AnimalDB;Integrated Security=True;Encrypt=False";
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (GameContext db = new GameContext())
                {
                    //Game game1 = new Game(1, "MarioBattles","SuperStudio","ActionRPG", DateTime.UtcNow);
                    Game game2 = new Game(2, "TetrisWars", "UltraStudio", "ActionRPG", DateTime.UtcNow);

                    //db.Game.Add(game1);
                    db.Game.Add(game2);
                    db.SaveChanges();

                    var games = db.Game.ToList();
                    foreach (var game in games)
                    {
                        game.Print();
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