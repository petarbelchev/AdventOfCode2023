string[] test = File.ReadAllLines("../../../data/test.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
Console.WriteLine("Part One test: " + PartOne(test));
Console.WriteLine("Part One: " + PartOne(input));

// Part 2
Console.WriteLine("Part Two test: " + PartTwo(test));
Console.WriteLine("Part Two: " + PartTwo(input));

int PartOne(string[] input)
{
    int valuesSum = 0;

    foreach (string line in input)
    {
        var history = new Stack<int[]>();
        history.Push(line.Split().Select(int.Parse).ToArray());

        bool toBeContinue = true;
        while (toBeContinue)
        {
            toBeContinue = false;
            int[] values = history.Peek();
            int[] nextValues = new int[values.Length - 1];

            for (int i = 1; i < values.Length; i++)
            {
                int nextValue = values[i] - values[i - 1];
                nextValues[i - 1] = nextValue;

                if (nextValue != 0)
                    toBeContinue = true;
            }

            history.Push(nextValues);
        }

        int value = 0;

        while (history.Count != 0)
        {
            int[] values = history.Pop();
            value += values[values.Length - 1];
        }

        valuesSum += value;
    }

    return valuesSum;
}

int PartTwo(string[] input)
{
    int valuesSum = 0;

    foreach (string line in input)
    {
        var history = new Stack<int[]>();
        history.Push(line.Split().Select(int.Parse).ToArray());

        bool toBeContinue = true;
        while (toBeContinue)
        {
            toBeContinue = false;
            int[] values = history.Peek();
            int[] nextValues = new int[values.Length - 1];

            for (int i = 1; i < values.Length; i++)
            {
                int nextValue = values[i] - values[i - 1];
                nextValues[i - 1] = nextValue;

                if (nextValue != 0)
                    toBeContinue = true;
            }

            history.Push(nextValues);
        }

        int value = 0;

        while (history.Count != 0)
        {
            int[] values = history.Pop();
            value = values[0] - value;
        }

        valuesSum += value;
    }

    return valuesSum;
}
