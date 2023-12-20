using Day6;

// Input:
// Time:        35      69      68      87
// Distance:    213     1168    1086    1248

List<Race> races = new List<Race>()
{
    new(35,213),
    new(69,1168),
    new(68,1086),
    new(87,1248)
};

long part1 = 1;

foreach(Race race in races)
{
    part1 *= race.NumberOfWays;
}

Console.WriteLine("Part 1 : {0}", part1);

// Part2
Race r2 = new(35696887, 213116810861248);

Console.WriteLine("Part 2 : {0}", r2.NumberOfWays);