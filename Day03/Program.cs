using Day03;

string[] test = File.ReadAllLines("../../../data/test.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
Console.WriteLine("Part One test: " + PartOne.GetResult(test));
Console.WriteLine("Part One: " + PartOne.GetResult(input));

// Part 2
Console.WriteLine("Part Two test: " + PartTwo.GetResult(test));
Console.WriteLine("Part Two: " + PartTwo.GetResult(input));
