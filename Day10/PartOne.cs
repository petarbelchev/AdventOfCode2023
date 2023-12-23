namespace Day10;

class PartOne
{
    enum Direction
    {
        North,
        South,
        West,
        East
    }

    private Dictionary<char, Direction[]> _pipes = new()
    {
        { '|', new Direction[] { Direction.North, Direction.South } },
        { '-', new Direction[] { Direction.East, Direction.West} },
        { 'L', new Direction[] { Direction.North, Direction.East} },
        { 'J', new Direction[] { Direction.North, Direction.West} },
        { '7', new Direction[] { Direction.South, Direction.West} },
        { 'F', new Direction[] { Direction.South, Direction.East} },
    };
    private (int Row, int Col) _start;
    private char[,] _sketch;
    private int _pipeLength;

    public int GetResult(string[] input)
    {
        _pipeLength = 0;

        DrawSketch(input);

        if (FindDirection(Direction.West, _start.Row, _start.Col + 1))
            Go(Direction.West, _start.Row, _start.Col + 1);
        else if (FindDirection(Direction.North, _start.Row + 1, _start.Col))
            Go(Direction.North, _start.Row + 1, _start.Col);
        else if (FindDirection(Direction.East, _start.Row, _start.Col - 1))
            Go(Direction.East, _start.Row, _start.Col - 1);
        else
            Go(Direction.North, _start.Row - 1, _start.Col);

        int steps = _pipeLength / 2;

        if (_pipeLength % 2 != 0)
            steps++;

        return steps;
    }

    private void DrawSketch(string[] input)
    {
        _sketch = new char[input.GetLength(0), input[0].Length];

        for (int row = 0; row < _sketch.GetLength(0); row++)
        {
            string line = input[row];
        
            for (int col = 0; col < _sketch.GetLength(1); col++)
            {
                _sketch[row, col] = line[col];
            
                if (line[col] == 'S')
                {
                    _start.Row = row;
                    _start.Col = col;
                }
            }
        }
    }

    private bool FindDirection(Direction position, int row, int col)
        => ValidCoordinates(row, col) && _pipes[_sketch[row, col]].Contains(position);

    private bool ValidCoordinates(int row, int col)
        => row >= 0 && row < _sketch.GetLength(0) && col >= 0 && col < _sketch.GetLength(1);

    private void Go(Direction from, int row, int col)
    {
        while (row != _start.Row || col != _start.Col)
        {
            Direction to = _pipes[_sketch[row, col]].First(d => d != from);

            if (to == Direction.North)
            {
                from = Direction.South;
                row--;
            }
            else if (to == Direction.East)
            {
                from = Direction.West;
                col++;
            }
            else if (to == Direction.South)
            {
                from = Direction.North;
                row++;
            }
            else
            {
                from = Direction.East;
                col--;
            }
            
            _pipeLength++;
        }
    }
}
