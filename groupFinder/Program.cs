using System.Diagnostics;
using System.Net;
using Newtonsoft.Json;
namespace RobloxGroupFinder;

public static class GroupFinder
{

    public static void GroupGet()
    {
        Random r = new Random();
        int id = r.Next(1, 15000000);
        Uri url = new Uri($"https://groups.roblox.com/v1/groups/{id}");
        try
        {
            string urlText = new WebClient().DownloadString(url);
            dynamic group = JsonConvert.DeserializeObject(urlText);

            try
            {
                if (group.owner.username != null)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"{group.name} | {group.id} | {group.owner.username} | {group.publicEntryAllowed}");
                }
            }
            catch
            {
                if (group.publicEntryAllowed == "False")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine($"{group.name} | {group.id} | {group.publicEntryAllowed}");
                    return;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{group.name} | {group.id} | {group.publicEntryAllowed}");
                using (Process web = new Process())
                {
                    web.StartInfo.FileName = $"https://www.roblox.com/groups/{group.id}";
                    web.StartInfo.UseShellExecute = true;
                    web.Start();
                }
            }
        }
        catch { }
    }

    public static void Main()
    {
        Console.Title = "Roblox Group Finder  | kitsuki.moe";
        for (; ; )
        {
            GroupFinder.GroupGet();
            Thread.Sleep(1000);
        }
    }

}