namespace Day08;

class PartTwo
{
    class Node
    {
        public string Value { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }

    Dictionary<string, Node> _map;

    public long GetResult(string input)
    {
        string[] data = input.Split("\r\n\r\n");
        char[] commands = data[0].ToCharArray();

        LoadMap(data[1]);

        int i = 0;
        long steps = 1;
        var nodes = _map.Values.Where(n => n.Value.EndsWith('A')).ToArray();
        for (int j = 0; j < nodes.Length; j++)
        {
            int currSteps = 0;

            while (!nodes[j].Value.EndsWith('Z'))
            {
                char cmd = commands[i++];
                if (i == commands.Length)
                    i = 0;
            
                if (cmd == 'L')
                    nodes[j] = _map[nodes[j].Left];
                else
                    nodes[j] = _map[nodes[j].Right];
                
                currSteps++;
            }

            steps = Lcm(steps, currSteps);
        }

        return steps;
    }

    void LoadMap(string input)
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

    long Lcm(long a, long b) => a * b / Gcd(a, b);

    long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);
}
