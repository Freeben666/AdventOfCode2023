using System.Text.RegularExpressions;
using Day5;
using static Day5.Map;

const int MAPCOUNT = 7;

List<String> input = [];
List<uint> seeds = [];
Dictionary<MapType,Map> maps = [];

// initializing maps dict
for (int x = 0; x < MAPCOUNT; x++)
{
    MapType t = (MapType)x;
    maps.Add(t, new(t));
}

// Loading the input file
using (var file = File.OpenText("input.txt"))
{
    String? line = null;
    while ((line = file.ReadLine()) != null)
    {
        input.Add(line);
    }
}

// Seeds numbers init (for Part 1)
Match m = Regex.Match(input[0], @"seeds: ([\d\s]+)");
if (m.Success)
{
    foreach(String seedNumber in m.Groups[1].Value.Split(' '))
    {
        seeds.Add(uint.Parse(seedNumber));
    }
}
else
{
    throw new Exception("Could not parse seeds numbers");
}



// Start at the seed to soil map
int i = 3; //Data starts on the fourth line of the input file
MapType type = MapType.SeedToSoil;

while (i < input.Count)
{
    if (input[i] == String.Empty)   // When changing map (empty line in input)
    {
        type += 1;                  // Increment the MapType
        i += 2;                     // Jump to start of data of next map, skipping header
        continue;                   // Next loop
    }
    
    // Parse the input
    String[] map = input[i].Split(' ');
    uint dest_start = uint.Parse(map[0]);
    uint source_start = uint.Parse(map[1]);
    uint length = uint.Parse(map[2]);
    // Add the parsed info into a new range in the current map
    maps[type].AddRange(source_start, dest_start, length);

    i++;
}



// Looking for the nearest location (Part 1)
Console.WriteLine("Nearest location 1 : " + NearestLocation(seeds, maps));

// Seeds numbers init (for Part 1)
var seeds2 = new List<(uint, uint)>();
foreach (Match match in Regex.Matches(input[0], @"(\d+\s\d+)"))
{
    String[] seedRange = match.Groups[1].Value.Split(' ');

    uint rangeStart = uint.Parse(seedRange[0]);
    uint rangeLen = uint.Parse(seedRange[1]);

    seeds2.Add((rangeStart, rangeLen));
}
// Looking for the nearest location (Part 2)
Console.WriteLine("Nearest location 2 : " + NearestLocation2(seeds2, maps));

static uint NearestLocation(List<uint> seeds, Dictionary<MapType, Map> maps)
{
    uint location = uint.MaxValue;
    foreach (uint seed in seeds)
    {
        uint value = seed;
        for (int x = 0; x < MAPCOUNT; x++)
        {
            value = maps[(MapType)x].Convert(value);
        }

        if (value < location) { location = value; }
    }
    return location;
}

static uint NearestLocation2(List<(uint, uint)> seeds, Dictionary<MapType, Map> maps)
{
    uint location = uint.MaxValue;
    foreach ((uint start, uint length) in seeds)
    {
        for (uint seed = start; seed< start + length; seed++) {
            uint value = seed;
            for (int x = 0; x < MAPCOUNT; x++)
            {
                value = maps[(MapType)x].Convert(value);
            }

            if (value < location) { location = value; }
        }
    }
    return location;
}