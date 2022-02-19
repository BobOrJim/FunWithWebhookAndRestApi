
namespace ReactionGame
{
    public class Program
    {
        static async Task Main()
        {
            Game game = new Game();
            try
            {
                await game.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }
    }
}