string[] test = File.ReadAllLines("../../../data/test.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
Console.WriteLine("Part One test: " + PartOne(test));
Console.WriteLine("Part One " + PartOne(input));

// Part 2
Console.WriteLine("Part Two test: " + PartTwo(test));
Console.WriteLine("Part Two: " + PartTwo(input));

static double PartOne(string[] input)
{
    double points = 0;

    foreach (var line in input)
    {
        (int[] win, int[] nums) = GetCard(line);

        int matches = nums.Count(n => win.Contains(n));

        if (matches == 1)
            points++;
        else if (matches > 1)
        {
            int point = 1;

            for (int i = 0; i < matches - 1; i++)
                point *= 2;

            points += point;
        }
    }

    return points;
}

static int PartTwo(string[] input)
{
    var cards = new Dictionary<int, int>();

    for (int cardNum = 1; cardNum <= input.Length; cardNum++)
    {
        if (!cards.ContainsKey(cardNum))
            cards[cardNum] = 1;
        else
            cards[cardNum]++;

        (int[] win, int[] nums) = GetCard(input[cardNum - 1]);
        int matches = nums.Count(n => win.Contains(n));

        for (int i = 1; i <= matches; i++)
        {
            if (!cards.ContainsKey(cardNum + i))
                cards[cardNum + i] = 0;

            cards[cardNum + i] += cards[cardNum];
        }
    }

    return cards.Where(c => c.Key <= input.Length).Sum(c => c.Value);
}

static (int[] win, int[] nums) GetCard(string line)
{
    string[] sides = line.Split(" | ");
    int[] win = sides[0].Split(": ")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
    int[] nums = sides[1].Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();

    return (win, nums);
}
