using Day9;

List <Sequence> sequences = new List<Sequence>();

using (var input = File.OpenText("input.txt")) {
    string? line;

    while((line = input.ReadLine()) != null) {
        sequences.Add(new Sequence(line));
    }
}

