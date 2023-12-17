using Day4;

using (var file = File.OpenText("input.txt"))
{
    List<ScratchCard> cards = [];

    String? line = null;
    while ((line = file.ReadLine()) != null)
    {
        cards.Add(new ScratchCard(line));
    }

    double Part1 = cards.Sum(c => c.Points);

    Console.WriteLine("Part 1 : " + Part1);

    for (int i = 0; i < cards.Count; i++)
    {
        var card = cards[i];

        for (int j = 1; j <= card.WinningCount; j++) // Should probably add an out of bounds check...
        {
            cards[i + j].Copy(card.Copies);
        }
    }

    int Part2 = cards.Sum(c => c.Copies);
    Console.WriteLine("Part 2 : " + Part2);
}