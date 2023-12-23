using Day11;

string[] test = File.ReadAllLines("../../../data/test.txt");
string[] input = File.ReadAllLines("../../../data/input.txt");

// Part 1
Console.WriteLine("Part One test: " + GetResult(test, 2));
Console.WriteLine("Part One: " + GetResult(input, 2));

// Part 2
Console.WriteLine("Part Two test: " + GetResult(test, 100));
Console.WriteLine("Part Two: " + GetResult(input, 1000000));

long GetResult(string[] input, int space)
{
    var galaxies = new List<Galaxy>();
    var emptyRows = new Stack<long>();
    var notEmptyCols = new HashSet<long>();

    for (long row = 0; row < input.Length; row++)
    {
        bool isEmptyRow = true;
        string line = input[row];
    
        for (int col = 0; col < line.Length; col++)
        {
            if (line[col] == '#')
            {
                galaxies.Add(new Galaxy { Row = row, Col = col });
                isEmptyRow = false;
                notEmptyCols.Add(col);
            }
        }
        
        if (isEmptyRow)
            emptyRows.Push(row);
    }

    var emptyCols = new Stack<int>(Enumerable.Range(0, input[0].Length).Where(x => !notEmptyCols.Contains(x)));
    while (emptyCols.Count != 0)
    {
        long emptyCol = emptyCols.Pop();
        
        for (int i = 0; i < galaxies.Count; i++)
        {
            if (galaxies[i].Col > emptyCol)
                galaxies[i].Col += space - 1;
        }
    }

    while (emptyRows.Count != 0)
    {
        long emptyRow = emptyRows.Pop();
    
        for (int i = 0; i < galaxies.Count; i++)
        {
            if (galaxies[i].Row > emptyRow)
                galaxies[i].Row += space - 1;
        }
    }

    long sumOfLengths = 0;

    for (int i = 0; i < galaxies.Count; i++)
    {
        Galaxy current = galaxies[i];
    
        for (int j = i + 1; j < galaxies.Count; j++)
        {
            Galaxy other = galaxies[j];
            long length = Math.Abs(current.Row - other.Row) + Math.Abs(current.Col - other.Col);
            sumOfLengths += length;
        }
    }

    return sumOfLengths;
}
