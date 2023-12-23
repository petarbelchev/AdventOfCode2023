using Day10;

string[] test = File.ReadAllLines("../../../data/test.txt");
string[] test2 = File.ReadAllLines("../../../data/test2.txt");
string[] test3 = File.ReadAllLines("../../../data/test3.txt");
string[] test4 = File.ReadAllLines("../../../data/test4.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

//Part 1
var partOne = new PartOne();
Console.WriteLine("Part One test: " + partOne.GetResult(test));
Console.WriteLine("Part One test 2: " + partOne.GetResult(test2));
Console.WriteLine("Part One: " + partOne.GetResult(input));

//Part 2
var partTwo = new PartTwo();
Console.WriteLine("Part Two test: " + partTwo.GetResult(test));
Console.WriteLine("Part Two test 3: " + partTwo.GetResult(test3));
Console.WriteLine("Part Two test 4: " + partTwo.GetResult(test4));
Console.WriteLine("Part Two: " + partTwo.GetResult(input));
