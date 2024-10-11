using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace NotificationChannelParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var allowedChannels = new HashSet<string> { "BE", "FE", "QA", "Urgent" };
            bool isRunning = true;
            while (isRunning){
                Console.Write("Enter a notification title:");
                string notificationTitle = Console.ReadLine();
                string result = ParseNotificationChannels(notificationTitle, allowedChannels);
                Console.WriteLine(result);
                //Ask user to continue or quit
                Console.Write("Another notification title or quit? (y/n): ");
                string response = Console.ReadLine();
                isRunning = response.Equals("y", StringComparison.OrdinalIgnoreCase);
                Console.WriteLine("\n");
            }
        }

        static string ParseNotificationChannels(string title, HashSet<string> allowedChannels)
        {
            // Find all tags enclosed in square brackets using regex
            var tags = Regex.Matches(title, @"\[(.*?)\]")
                            .Cast<Match>()
                            .Select(m => m.Groups[1].Value)
                            .ToList();

            // Filter out the tags that match the allowed channels
            var channels = tags.Where(tag => allowedChannels.Contains(tag)).Distinct();

            return $"Receive channels: {string.Join(", ", channels)}";
        }
    }
}
