namespace AdventOfCode2024.Solutions;

internal sealed class Day5 : IDay
{
    private List<(int Before, int After)> _rules = [];
    private readonly List<List<int>> _pageUpdates = [];

    public void Run()
    {
        ReadInput();
        var sum = 0;
        foreach (var pageUpdate in _pageUpdates)
        {
            var requiredRules = _rules.Where(r => pageUpdate.Contains(r.Before) && pageUpdate.Contains(r.After))
                .ToList();
            List<int> safeUpdates = [];
            foreach (var update in pageUpdate)
            {
                var applyingRules = requiredRules.Where(r => r.Before == update || r.After == update)
                    .Except(requiredRules.Where(r => safeUpdates.Contains(r.Before)))
                    .ToList();
                if (applyingRules.TrueForAll(ar => ar.After != update))
                {
                    safeUpdates.Add(update);
                }
                else
                {
                    safeUpdates.Clear();
                    break;
                }
            }

            if (safeUpdates.Count > 0)
            {
                sum += GetListMiddle(safeUpdates);
            }
        }

        Console.WriteLine(sum);
    }

    public void RunPart2()
    {
        ReadInput();
        var sum = 0;
        foreach (var pageUpdate in _pageUpdates)
        {
            var requiredRules = _rules.Where(r => pageUpdate.Contains(r.Before) && pageUpdate.Contains(r.After))
                .ToList();
            List<int> unsafeUpdates = [];
            List<int> safeUpdate = [];
            foreach (var update in pageUpdate)
            {
                var applyingRules = requiredRules.Where(r => r.Before == update || r.After == update)
                    .Except(requiredRules.Where(r => safeUpdate.Contains(r.Before)))
                    .ToList();
                if (!applyingRules.TrueForAll(ar => ar.After != update))
                {
                    unsafeUpdates = pageUpdate.ToList();
                }
                else
                {
                    safeUpdate.Add(update);
                }
            }

            if (unsafeUpdates.Count <= 0) continue;
            unsafeUpdates = OrderUnsafeUpdate(unsafeUpdates, requiredRules);
            sum += GetListMiddle(unsafeUpdates);
        }

        Console.WriteLine(sum);
    }

    private List<int> OrderUnsafeUpdate(List<int> update, List<(int Before, int After)> requiredRules)
    {
        update.Sort((i, i1) => OrderByRules(i, i1, requiredRules));
        
        return update;
    }

    private static int OrderByRules(int before, int after, List<(int Before, int After)> requiredRules)
    {
        if (requiredRules.Any(rr => rr.Before == before && rr.After == after))
        {
            return 0;
        }

        if (requiredRules.Any(rr => rr.After == before && rr.Before == after))
        {
            return -1;
        }

        return -1;
    }

    private void ReadInput(bool part2 = false)
    {
        var inputRules = File.ReadAllLines($"inputs/day5input{(part2 ? "2" : "1")}_1.txt");
        var inputPageUpdates = File.ReadAllLines($"inputs/day5input{(part2 ? "2" : "1")}_2.txt");
        foreach (var inputRule in inputRules)
        {
            var splittedRules = inputRule.Split("|");
            _rules.Add((int.Parse(splittedRules[0]), int.Parse(splittedRules[1])));
        }

        foreach (var pageUpdateLine in inputPageUpdates.Select(i => i.Split(',', StringSplitOptions.TrimEntries)))
        {
            List<int> pageUpdate = [];
            pageUpdate.AddRange(pageUpdateLine.Select(int.Parse));
            _pageUpdates.Add(pageUpdate);
        }

        _rules = _rules.OrderBy(r => r.Before).ThenBy(r => r.After).ToList();
    }

    private int GetListMiddle(List<int> list)
    {
        return int.IsOddInteger(list.Count)
            ? list[list.Count / 2]
            : throw new NotImplementedException("The list length is not an odd number. So there are 2 middle values.");
    }
}