string[] test = File.ReadAllLines("../../../data/test.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
Console.WriteLine("Part One test: " + PartOne(test));
Console.WriteLine("Part One result: " + PartOne(input));

// Part 2
Console.WriteLine("Part Two test: " + PartTwo(test));
Console.WriteLine("Part Two result: " + PartTwo(input));

static int PartOne(string[] input)
{
    Dictionary<string, int> bag = new() { { "red", 12 }, { "green", 13 }, { "blue", 14 } };
    int result = 0;

    foreach (string game in input)
    {
        bool isPossibleGame = true;
        string[] data = game.Split(": ");
        int gameNum = int.Parse(data[0].Split()[1]);
        string[] subsets = data[1].Split("; ");

        foreach (string subset in subsets)
        {
            string[] cubes = subset.Split(", ");

            foreach (string cube in cubes)
            {
                string[] cubeData = cube.Split();
                int cubeCount = int.Parse(cubeData[0]);
                string cubeColor = cubeData[1];

                if (bag[cubeColor] < cubeCount)
                {
                    isPossibleGame = false;
                    break;
                }
            }

            if (!isPossibleGame)
                break;
        }

        if (isPossibleGame)
            result += gameNum;
    }

    return result;
}

static int PartTwo(string[] input)
{
    var red = "red";
    var green = "green";
    var blue = "blue";
    int result = 0;

    foreach (string game in input)
    {
        var bag = new Dictionary<string, int>() { { red, 0 }, { green, 0 }, { blue, 0 } };
        string[] data = game.Split(": ");
        int gameNum = int.Parse(data[0].Split()[1]);
        string[] subsets = data[1].Split("; ");

        foreach (string subset in subsets)
        {
            string[] cubes = subset.Split(", ");

            foreach (string cube in cubes)
            {
                string[] cubeData = cube.Split();
                int cubeCount = int.Parse(cubeData[0]);
                string cubeColor = cubeData[1];

                if (bag[cubeColor] < cubeCount)
                    bag[cubeColor] = cubeCount;
            }
        }

        int power = bag[red] * bag[green] * bag[blue];
        result += power;
    }

    return result;
}
