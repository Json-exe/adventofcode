namespace AdventOfCode2024.Solutions;

internal sealed class Day6 : IDay
{
    private char[,] _grid = new char[0, 0];
    private Guard _guard = new();
    private HashSet<(int, int)> _visited = [];

    public void Run()
    {
        var inputText = File.ReadLines("inputs\\day6input1.txt").ToList();
        _grid = new char[inputText.Count, inputText[0].Length];

        for (var y = 0; y < inputText.Count; y++)
        {
            for (var x = 0; x < inputText[y].Length; x++)
            {
                _grid[y, x] = inputText[y][x];
                if (inputText[y][x] != '^') continue;
                _guard.X = x;
                _guard.Y = y;
            }
        }

        _visited.Add((_guard.Y, _guard.X));
        Move();
        Console.WriteLine(_visited.Count);
    }

    private void Move()
    {
        Print();
        while (true)
        {
            _visited.Add((_guard.Y, _guard.X));

            try
            {
                switch (_guard.Direction)
                {
                    case Direction.Up:
                        if (_grid[_guard.Y - 1, _guard.X] == '#')
                        {
                            _guard.Direction = Direction.Right;
                            continue;
                        }

                        _grid[_guard.Y, _guard.X] = '.';
                        _grid[_guard.Y - 1, _guard.X] = '^';
                        _guard.Y -= 1;

                        break;
                    case Direction.Down:
                        if (_grid[_guard.Y + 1, _guard.X] == '#')
                        {
                            _guard.Direction = Direction.Left;
                            continue;
                        }

                        _grid[_guard.Y, _guard.X] = '.';
                        _grid[_guard.Y + 1, _guard.X] = '^';
                        _guard.Y += 1;
                        break;
                    case Direction.Left:
                        if (_grid[_guard.Y, _guard.X - 1] == '#')
                        {
                            _guard.Direction = Direction.Up;
                            continue;
                        }

                        _grid[_guard.Y, _guard.X] = '.';
                        _grid[_guard.Y, _guard.X - 1] = '^';
                        _guard.X -= 1;
                        break;
                    case Direction.Right:
                        if (_grid[_guard.Y, _guard.X + 1] == '#')
                        {
                            _guard.Direction = Direction.Down;
                            continue;
                        }

                        _grid[_guard.Y, _guard.X] = '.';
                        _grid[_guard.Y, _guard.X + 1] = '^';
                        _guard.X += 1;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(_guard.Direction));
                }
            }
            catch (IndexOutOfRangeException)
            {
                Print();
                break;
            }
        }
    }

    private void Print()
    {
        Console.Clear();
        for (var y = 0; y < _grid.GetLength(0); y++)
        {
            for (var x = 0; x < _grid.GetLength(1); x++)
            {
                Console.Write(_grid[y, x]);
            }

            Console.Write("\n");
        }

        // Thread.Sleep(10);
    }

    public void RunPart2()
    {
        throw new NotImplementedException();
    }
}

internal sealed record Guard
{
    public int X { get; set; }
    public int Y { get; set; }
    public Direction Direction { get; set; } = Direction.Up;
}

internal enum Direction
{
    Up,
    Down,
    Left,
    Right
}