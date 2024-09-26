using System;

namespace Day8;

public struct Node
{
    public String Name { get; }
    public String Left { get; }
    public String Right { get; }

    public Node(String name, String left, String right) => (Name, Left, Right) = (name, left, right);
}
