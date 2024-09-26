using Day8;

var input = File.ReadAllLines("input.txt");
var instructions = input[0];

var nodes = new Dictionary<String, Node>();

for (int i = 2; i < input.Length; i++) {
    var nodeName = input[i].Substring(0, 3);
    var left = input[i].Substring(7, 3);
    var right = input[i].Substring(12, 3);

    nodes.Add(nodeName, new Node(nodeName, left, right));
}

Console.WriteLine($"Nodes read: {nodes.Count()}");


int steps = 0;
/*
string currentNode = "AAA";

while (currentNode != "ZZZ") {
    for (int i = 0; i < instructions.Length; i++) {
        steps++;

        var instruction = instructions[i];
        
        switch (instruction) {
            case 'L':
               currentNode = nodes[currentNode].Left;
               break;
            case 'R':
               currentNode = nodes[currentNode].Right;
               break;
            default:
               throw new Exception("Invalid instruction");
        }

        if (currentNode == "ZZZ") {
            break;
        }
    }
}

Console.WriteLine($"Steps taken for part 1: {steps}");
*/

var currentNodes = nodes.Keys.Where( k => k[2] == 'A').ToList();
var a = currentNodes.Count();
steps = 0;

Console.WriteLine($"Nodes that end with 'A': {a}");

while (currentNodes.Where(x => x[2] == 'Z').Count() != a) {
    //Console.WriteLine(currentNodes.Where(x => x[2] == 'Z').Count());
    for (int i = 0; i < instructions.Length; i++) {
        steps++;

        var instruction = instructions[i];
        
        for (int j = 0; j < currentNodes.Count(); j++) {
            switch (instruction) {
                case 'L':
                currentNodes[j] = nodes[currentNodes[j]].Left;
                break;
                case 'R':
                currentNodes[j] = nodes[currentNodes[j]].Right;
                break;
                default:
                throw new Exception("Invalid instruction");
            }
        }

        if (currentNodes.Where(x => x[2] == 'Z').Count() == a) {
            break;
        }
    }
}

Console.WriteLine($"Steps taken for part 2: {steps}");