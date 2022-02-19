using Game.Models;
using System.Diagnostics;
using Presentation;
using ReactionGame.Http;

namespace ReactionGame
{
    public class Game
    {
        private List<Highscore> highscoreList = new();
        private Random random = new();
        private HighscoreService highscoreService = new();

        public async Task Start()
        {
            while (true) //This is the game loop
            {
                highscoreList = (await highscoreService.GetHighscoresFromHighcoreApiAsync()).OrderByDescending(h => h.Time).ToList();
                PrintHighscoreListToConsole(highscoreList);

                long reactionTime = PlayReactionGameRound();

                if (reactionTime > 0 && IsNewHighscore(highscoreList, reactionTime))
                    await highscoreService.PostHighscoreToHighcoreApiAsync(RegisterNewHighscore(reactionTime));
            }
        }


        private long PlayReactionGameRound()
        {
            long time;
            //Console.Clear();
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Tryck valfri tangent för att starta spelet!");
            Console.ReadKey(true);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("\nVänta lite");

            float waitTime = random.Next(500, 3000);
            while (Console.KeyAvailable != true && waitTime > 0)
            {
                Thread.Sleep(100);
                waitTime -= 100;
                Console.Write(".");
            }

            if (waitTime > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nTjuvstart! Prova igen.");
                Console.ReadKey(true);
                time = - 1;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\nTryck NU!!\n");
                stopwatch.Start();
                Console.ReadKey(true);
                stopwatch.Stop();

                Console.ResetColor();
                Console.Write("Det tog: " + stopwatch.ElapsedMilliseconds + " millisekunder! ");

                time = stopwatch.ElapsedMilliseconds;
            }

            Console.ResetColor();
            Console.WriteLine("\nTryck på valfri tangent för att börja om, eller Q för att avsluta.");
            if (Console.ReadKey(true).Key == ConsoleKey.Q) Environment.Exit(0);
            return time;
        }

        private void PrintHighscoreListToConsole(List<Highscore> highscores)
        {
            Console.WriteLine("\n\nHIGHSCORE:");
            for (int i = highscores.Count; i > 0; i--)
            {
                Console.WriteLine(highscores[i - 1].Name + " " + highscores[i - 1].Time);
            }
        }


        private bool IsNewHighscore(List<Highscore> highscores, long elapsedMilliseconds) => (highscores.Count <= 10) ? true : (elapsedMilliseconds < highscores.ElementAt(10).Time);

        private Highscore RegisterNewHighscore(long time) => new Highscore {Name = ConsoleUtilities.AskCLIForString("Nytt rekord \nSkriv ditt namn: "), Time = time};

    }
}
