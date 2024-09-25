using System.Security.Cryptography.X509Certificates;
using Day7;

var lines = File.ReadAllLines("input.txt");
        var hands = new List<Hand>();

        foreach (var line in lines)
        {
            var parts = line.Split(' ');
            hands.Add(new Hand( parts[0],int.Parse(parts[1]) ));
        }

        hands.Sort();

        int totalWinnings = 0;
        for (int i = 0; i < hands.Count; i++)
        {
            totalWinnings += hands[i].Bid * (i + 1);
        }

        Console.WriteLine($"Total winnings: {totalWinnings}");