using Day3;

using (var file = File.OpenText("input.txt"))
{
    List<String> input = [];

    String? line = null;
    while ((line = file.ReadLine()) != null)
    {
        input.Add(line);
    }

    Day3.Day3 Day = new(input);

    Console.WriteLine(Day.Part1());

    Console.WriteLine(Day.Part2());
}