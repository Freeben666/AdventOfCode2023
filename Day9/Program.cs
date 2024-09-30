using Day9;

List <Sequence> sequences = new List<Sequence>();

using (var input = File.OpenText("input.txt")) {
    string? line;

    while((line = input.ReadLine()) != null) {
        sequences.Add(new Sequence(line));
    }
}

foreach (Sequence sequence in sequences) {
    Sequence current = sequence;

    while (!current.IsAllZero()){
        current = current.GetDifferences();
    }

    while (current.Parent != null){
        Sequence next = current.Parent;

        next.Add(next.Last + current.Last);
        next.AddFirst(next.First - current.First);

        current = next;
    }
}

Console.WriteLine("Part 1: " + sequences.Sum(s => s.Last));
Console.WriteLine("Part 2: " + sequences.Sum(s => s.First));