using System.Text.RegularExpressions;
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
steps = traverseGraph(nodes, instructions, "AAA", "ZZZ");

Console.WriteLine($"Steps taken for part 1: {steps}");


var currentNodes = nodes.Keys.Where( k => k[2] == 'A').ToList();
var a = currentNodes.Count();

Console.WriteLine($"Nodes that end with 'A': {a}");

var steps2 = new Stack<ulong>();

foreach (var node in currentNodes) {
    steps2.Push((ulong)traverseGraph(nodes, instructions, node, @"^[0-9A-Z]{2}Z$"));
}

ulong x = steps2.Pop();

foreach (var step in steps2){
    x = lcm(x, step);
}

Console.WriteLine($"Steps taken for part 2: {x}");

// Functions

static int traverseGraph(Dictionary<String, Node> nodes, string instructions, string startNode, string endNodePattern){
    var currentNode = startNode;
    var steps = 0;

    Regex r = new Regex(endNodePattern);

    while (!r.Match(currentNode).Success) {
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

            if (r.Match(currentNode).Success) {
                break;
            }
        }
    }

    return steps;
}


static ulong gcd(ulong n1, ulong n2)
{
    if (n2 == 0)
    {
        return n1;
    }
    else
    {
        return gcd(n2, n1 % n2);
    }
}

static ulong lcm(ulong a, ulong b) {
    return (a * b) / gcd(a,b);
}
