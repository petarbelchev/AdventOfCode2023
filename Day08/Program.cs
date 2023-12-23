using Day08;

string test = File.ReadAllText("../../../data/test.txt");
string test2 = File.ReadAllText("../../../data/test2.txt");
string input = File.ReadAllText("../../../data/input.txt");

// Part 1
var partOne = new PartOne();
Console.WriteLine("Part One test: " + partOne.GetResult(test));
Console.WriteLine("Part One: " + partOne.GetResult(input));

// Part 2
var partTwo = new PartTwo();
Console.WriteLine("Part Two test: " + partTwo.GetResult(test2));
Console.WriteLine("Part Two: " + partTwo.GetResult(input));
