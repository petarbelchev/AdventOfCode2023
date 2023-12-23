namespace Day08;

class PartOne
{
    private class Node
    {
        public string Value { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }

    private Dictionary<string, Node> _map;

    public int GetResult(string input)
    {
        string[] data = input.Split("\r\n\r\n");
        char[] commands = data[0].ToCharArray();

        LoadMap(data[1]);
        
        int i = 0;
        int steps = 0;
        Node node = _map["AAA"];
        while (node.Value != "ZZZ")
        {
            char cmd = commands[i++];
            if (cmd == 'L')
                node = _map[node.Left];
            else
                node = _map[node.Right];
        
            if (i == commands.Length)
                i = 0;
            
            steps++;
        }

        return steps;
    }

    private void LoadMap(string input)
    {
        _map = new();
        string[] lines = input.Split(Environment.NewLine);
        
        foreach (string line in lines)
        {
            string[] data = line.Split(" = ");
            string node = data[0];
            string[] nodeDirections = data[1].Split(", ");
            string leftNode = nodeDirections[0].Trim('(');
            string rightNode = nodeDirections[1].Trim(')');
            _map.Add(node, new Node { Value = node, Left = leftNode, Right = rightNode });
        }
    }
}
