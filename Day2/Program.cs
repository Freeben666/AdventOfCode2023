// See https://aka.ms/new-console-template for more information

using Day2;

using (var file = File.OpenText("input.txt"))
{
    List<Game> games = [];
    
    string? line;
    while ((line = file.ReadLine()) != null)
    {
        games.Add(new Game(line));
    }

    Set constraint = new(12, 13, 14);
    int result = 0;
    int result2 = 0;

    foreach(Game game in games)
    {
        if (game.Possible(constraint))
        {
            result += game.ID;
        }

        //Console.WriteLine("Game " + game.ID + " power : " + game.Power);
        result2 += game.Power;
    }

    Console.WriteLine("Part 1 : " + result);
    Console.WriteLine("Part 2 : " + result2);
}
