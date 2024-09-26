using System.Security.Cryptography.X509Certificates;
using Day7;

var lines = File.ReadAllLines("input.txt");
        var hands = new List<Hand>();
        var hands2 = new List<Hand2>();

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            hands.Add(new Hand( parts[0],int.Parse(parts[1]) ));
            hands2.Add(new Hand2( parts[0],int.Parse(parts[1]) ));
        }

        hands.Sort();

        int totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Bid * (i + 1);
        }

        Console.WriteLine($"Total winnings: {totalWinnings}");

        hands2.Sort();

        int totalWinnings2 = 0;
        for (int i = 0; i < hands2.Count; i++)
        {
            totalWinnings2 += hands2[i].Bid * (i + 1);
        }

        Console.WriteLine($"Total winnings using Jokers: {totalWinnings2}");