using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // Wczytanie danych wejściowych
        string input = Console.ReadLine();
        string[] times = input.Split(',');

        // Konwersja czasów na sekundy
        int[] seconds = times.Select(t =>
        {
            var parts = t.Split(':');
            return int.Parse(parts[0]) * 60 + int.Parse(parts[1]);
        }).ToArray();

        // 1. Liczba okrążeń
        int lapCount = seconds.Length;
        Console.WriteLine(lapCount);

        // 2. Czasy kolejnych okrążeń
        var lapTimes = seconds.Zip(seconds.Skip(1).Append(seconds[0]), (curr, next) => next - curr)
                            .Take(lapCount)
                            .Select(s => $"{s/60:D2}:{s%60:D2}");
        Console.WriteLine(string.Join(" ", lapTimes));

        // 3. Najszybsze okrążenie
        var minLapTime = seconds.Zip(seconds.Skip(1), (curr, next) => next - curr)
                              .Take(lapCount)
                              .Min();
        var minLapIndices = seconds.Zip(seconds.Skip(1), (curr, next) => next - curr)
                                 .Take(lapCount)
                                 .Select((time, index) => new { Time = time, Index = index + 1 })
                                 .Where(x => x.Time == minLapTime)
                                 .Select(x => x.Index);
        Console.WriteLine($"{minLapTime/60:D2}:{minLapTime%60:D2} {string.Join(" ", minLapIndices)}");

        // 4. Najwolniejsze okrążenie
        var maxLapTime = seconds.Zip(seconds.Skip(1), (curr, next) => next - curr)
                              .Take(lapCount)
                              .Max();
        var maxLapIndices = seconds.Zip(seconds.Skip(1), (curr, next) => next - curr)
                                 .Take(lapCount)
                                 .Select((time, index) => new { Time = time, Index = index + 1 })
                                 .Where(x => x.Time == maxLapTime)
                                 .Select(x => x.Index);
        Console.WriteLine($"{maxLapTime/60:D2}:{maxLapTime%60:D2} {string.Join(" ", maxLapIndices)}");

        // 5. Średni czas okrążenia
        var avgLapTime = (int)Math.Ceiling(seconds.Zip(seconds.Skip(1), (curr, next) => next - curr)
                                                .Take(lapCount)
                                                .Average());
        Console.WriteLine($"{avgLapTime/60:D2}:{avgLapTime%60:D2}");
    }
}