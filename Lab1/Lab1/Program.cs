using System.Diagnostics;
using System.Numerics;
using System.Reflection;

namespace Lab1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string answer;
            int rating;
            Game gameplay = new Game();

            GameAccount Gamer1 = new GameAccount();
            Console.WriteLine("Введіть ім'я: ");
            Gamer1.UserName = Console.ReadLine();
            Gamer1.CurrentRating = 100;
            GameAccount Gamer2 = new GameAccount();
            Console.WriteLine("Введіть ім'я: ");
            Gamer2.UserName = Console.ReadLine();
            Gamer2.CurrentRating = 100;

            Gamer1.OutGamers();
            Gamer2.OutGamers();
            do
            {
                Console.WriteLine("Введіть рейтинг на який грають:");
                rating = int.Parse(Console.ReadLine());
                while (rating <= 0 || Gamer1.CurrentRating < rating || Gamer2.CurrentRating < rating)
                {
                    Console.WriteLine("Рейтинг не може бути меньше 0, або рейтинг одного із гравців менше ніж заданий рейтинг, введіть інший рейтинг");
                    rating = int.Parse(Console.ReadLine());
                }
                if (Gamer1.CurrentRating <= 0 || Gamer2.CurrentRating <= 0)
                {
                    Console.WriteLine("Рейтинг одно з гравців меньше 0. Гру закінчено");
                    return;
                }
                else
                {
                    gameplay.PlayGame(Gamer1, Gamer2, rating);
                }
                Console.WriteLine("Зіграти ще раз? (Y/N)");
                answer = Console.ReadLine();
            } while (answer == "Y" || answer == "y");
            Gamer1.GetStats();
        }
    }
    class GameAccount
    {
        public string UserName { get; set; }
        public int CurrentRating { get; set; }
        public int GamesCount { get; set; }
        public List<GameResult> GameResults { get; set; } = new List<GameResult>();

        public void OutGamers()
        {
            Console.WriteLine($"\nІм'я: {UserName}\nРейтинг: {CurrentRating} \nІгор зіграно: {GamesCount}\n");
        }
        public void WinGame(int rating, string player, string winner, int gameIndex)
        {
            GamesCount++;
            CurrentRating += rating;
            GameResults.Add(new GameResult(player, winner, CurrentRating, gameIndex));
        }
        public void LoseGame(int rating, string player, string winner, int gameIndex)
        {
            GamesCount++;
            if (CurrentRating > 1)
            {
                CurrentRating -= rating;
                if (CurrentRating < 1)
                {
                    CurrentRating = 1;
                }
            }
            GameResults.Add(new GameResult(player, winner, CurrentRating, gameIndex));
        }
        public void DrawGame(int rating, string player, string winner, int gameIndex)
        {
            GamesCount++;
            GameResults.Add(new GameResult(player, winner, CurrentRating, gameIndex));
        }
        public void GetStats()
        {
            foreach (GameResult result in GameResults)
            {
                Console.WriteLine($"Проти {result.Opponent}, {result.IsWin}, Рейтинг: {result.Rating}, Гра #{result.GameIndex}");
            }
        }
    }
    class Game
    {
        GameAccount game = new GameAccount();
        int gameIndex = 0;
        public void PlayGame(GameAccount player1, GameAccount player2, int rating)
        {
            Random random = new Random();
            Console.WriteLine("Введіть число між 1 і 10: ");
            int a = Convert.ToInt32(Console.ReadLine());
            int b = random.Next(1, 11);
            gameIndex += 1;
            if (a > b)
            {
                Console.WriteLine("Гравець 1 виграв");
                player1.WinGame(rating, player2.UserName, "виграв",gameIndex);
                Console.WriteLine("Гравець 2 програв");
                player2.LoseGame(rating, player1.UserName, "програв", gameIndex);
            }
            if (a < b)
            {
                Console.WriteLine("Гравець 2 виграв");
                player2.WinGame(rating, player1.UserName, "виграв", gameIndex);
                Console.WriteLine("Гравець 1 програв");
                player1.LoseGame(rating, player2.UserName, "програв", gameIndex);
            }
            if (a == b)
            {
                Console.WriteLine("Нічия");
                game.DrawGame(rating, player1.UserName, "нічия", gameIndex);
                game.DrawGame(rating, player2.UserName, "нічия", gameIndex);
            }
            
        }
    }
    class GameResult
    {
        public string Opponent { get; }
        public string IsWin { get; }
        public int Rating { get; }
        public int GameIndex { get; }

        public GameResult(string opponent, string isWin, int rating, int gameIndex)
        {
            Opponent = opponent;
            IsWin = isWin;
            Rating = rating;
            GameIndex = gameIndex;
        }
    }
}