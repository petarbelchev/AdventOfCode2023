using System.Text.RegularExpressions;

string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
string[] test = File.ReadAllLines("../../../data/test.txt");
Console.WriteLine(PartOne(test));
Console.WriteLine(PartOne(input));

// Part 2
string[] test2 = File.ReadAllLines("../../../data/test2.txt");
Console.WriteLine(PartTwo(test2));
Console.WriteLine(PartTwo(input));

static int PartOne(string[] input)
{
    int sum = 0;

    foreach (string line in input)
    {
        string first = string.Empty;
        string last = string.Empty;

        for (int i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                first = line[i].ToString();
                break;
            }
        }

        for (int i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                last = line[i].ToString();
                break;
            }
        }

        sum += int.Parse(first + last);
    }

    return sum;
}

static int PartTwo(string[] input)
{
    int result = 0;
    string pattern = @"\d|one|two|three|four|five|six|seven|eight|nine";
    
    foreach (string line in input)
    {
        string first = Regex.Match(line, pattern).Value;
        string last = Regex.Match(line, pattern, RegexOptions.RightToLeft).Value;
        
        result += (Parse(first) * 10) + Parse(last);
    }

    return result;
}

static int Parse(string value)
{
    return value switch
    {
        "one" => 1,
        "two" => 2,
        "three" => 3,
        "four" => 4,
        "five" => 5,
        "six" => 6,
        "seven" => 7,
        "eight" => 8,
        "nine" => 9,
        _ => int.Parse(value)
    };
}
