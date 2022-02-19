using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    public static class ConsoleUtilities
    {
        public static int AskCLIForInt(string ToConsole)
        {
            try
            {
                Console.WriteLine(ToConsole);
                return int.TryParse(Console.ReadLine(), out int value) ? value : -1;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception in ConsoleUtilities/AskCLIForInt. ExceptionType = {e.GetType().FullName} ExceptionMessage = {e.Message}");
                return -1;
            }
        }

        public static string AskCLIForString(string ToConsole)
        {
            try
            {
                Console.WriteLine(ToConsole);
                return Convert.ToString(Console.ReadLine());
            }
            catch (Exception e)
            {
                Console.WriteLine($"Exception in ConsoleUtilities/AskCLIForString. ExceptionType = {e.GetType().FullName} ExceptionMessage = {e.Message}");
                return "";
            }
        }
    }
}