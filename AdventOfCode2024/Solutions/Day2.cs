namespace AdventOfCode2024.Solutions;

internal sealed class Day2 : IDay
{
    private readonly List<int> _leftData = [];
    private readonly List<int> _rightData = [];

    public void Run()
    {
        var inputText = File.ReadLines("inputs\\day2input1.txt");
        var safeReports = 0;
        foreach (var line in inputText)
        {
            var splittedLine = line.Trim().Split(" ", StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToList();
            if (splittedLine.Distinct().Count() != splittedLine.Count)
            {
                continue;
            }

            if (!splittedLine.OrderDescending().SequenceEqual(splittedLine) &&
                !splittedLine.Order().SequenceEqual(splittedLine))
            {
                continue;
            }

            var safe = true;
            for (var i = 0; i < splittedLine.Count - 1; i++)
            {
                var difference = Math.Abs(splittedLine[i] - splittedLine[i + 1]);
                if (difference is <= 3 and >= 1) continue;
                safe = false;
                break;
            }

            if (!safe) continue;
            safeReports++;
        }

        Console.WriteLine(safeReports);
    }

    public void RunPart2()
    {
        var inputText = File.ReadLines("inputs\\day2input2.txt");
        var safeReports = 0;
        foreach (var line in inputText)
        {
            var splittedLine = line.Trim().Split(" ", StringSplitOptions.TrimEntries)
                .Select(int.Parse)
                .ToList();

            var safe = false;
            if (splittedLine.Distinct().Count() != splittedLine.Count)
            {
                var duplicates = splittedLine.Count - splittedLine.Distinct().Count();
                if (duplicates > 1)
                {
                    continue;
                }

                for (var i = 0; i < splittedLine.Count; i++)
                {
                    var updatedLine = splittedLine.ToList();
                    updatedLine.RemoveAt(i);
                    for (var j = 0; j < updatedLine.Count - 1; j++)
                    {
                        if (!updatedLine.OrderDescending().SequenceEqual(updatedLine) &&
                            !updatedLine.Order().SequenceEqual(updatedLine))
                        {
                            break;
                        }

                        var difference = Math.Abs(updatedLine[j] - updatedLine[j + 1]);
                        if (difference is > 3 or < 1)
                        {
                            safe = false;
                            break;
                        }

                        safe = true;
                    }

                    if (safe)
                    {
                        break;
                    }
                }

                if (!safe)
                {
                    continue;
                }
            }

            if (!splittedLine.OrderDescending().SequenceEqual(splittedLine) &&
                !splittedLine.Order().SequenceEqual(splittedLine))
            {
                for (var i = 0; i < splittedLine.Count; i++)
                {
                    var updatedLine = splittedLine.ToList();
                    updatedLine.RemoveAt(i);
                    for (var j = 0; j < updatedLine.Count - 1; j++)
                    {
                        if (!updatedLine.OrderDescending().SequenceEqual(updatedLine) &&
                            !updatedLine.Order().SequenceEqual(updatedLine))
                        {
                            break;
                        }

                        var difference = Math.Abs(updatedLine[j] - updatedLine[j + 1]);
                        if (difference is > 3 or < 1)
                        {
                            safe = false;
                            break;
                        }

                        safe = true;
                    }

                    if (safe)
                    {
                        break;
                    }
                }
            }

            if (!safe)
            {
                for (var i = 0; i < splittedLine.Count - 1; i++)
                {
                    var difference = Math.Abs(splittedLine[i] - splittedLine[i + 1]);
                    if (difference is > 3 or < 1)
                    {
                        safe = false;
                        break;
                    }

                    safe = true;
                }
            }

            if (!safe) continue;
            safeReports++;
        }

        Console.WriteLine(safeReports);
    }
}