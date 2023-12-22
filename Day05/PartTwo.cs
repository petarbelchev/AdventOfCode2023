namespace Day05
{
    static class PartTwo
    {
        record Range(long Start, long End);

        class Map
        {
            public Range Destination { get; set; }
            public Range Source { get; set; }
        }

        private static readonly string seedToSoil = "seed-to-soil map:";
        private static readonly string soilToFertilizer = "soil-to-fertilizer map:";
        private static readonly string fertilizerToWater = "fertilizer-to-water map:";
        private static readonly string waterToLight = "water-to-light map:";
        private static readonly string lightToTemperature = "light-to-temperature map:";
        private static readonly string temperatureToHumidity = "temperature-to-humidity map:";
        private static readonly string humidityToLocation = "humidity-to-location map:";

        private static Dictionary<string, List<Map>> _mapsCollections;

        public static long GetResult(string[] input)
        {
            List<Range> startRanges = GetStartRanges(input[0]);
            LoadMaps(input);

            foreach (var maps in _mapsCollections)
            {
                var nextMapRanges = new List<Range>(startRanges);
                startRanges.Clear();

                var queue = new Queue<Range>(nextMapRanges);
                nextMapRanges.Clear();

                while (queue.Any())
                {
                    Range range = queue.Dequeue();
                    var map = maps.Value.FirstOrDefault(map => IsMatch(range, map.Source));
                    if (map is null)
                    {
                        nextMapRanges.Add(range);
                    }
                    else if (map.Source.Start <= range.Start && range.End <= map.Source.End)
                    {
                        long shift = map.Destination.Start - map.Source.Start;
                        startRanges.Add(new Range(range.Start + shift, range.End + shift));
                    }
                    else if (range.Start < map.Source.Start)
                    {
                        queue.Enqueue(new Range(range.Start, map.Source.Start - 1));
                        queue.Enqueue(new Range(map.Source.Start, range.End));
                    }
                    else
                    {
                        queue.Enqueue(new Range(range.Start, map.Source.End));
                        queue.Enqueue(new Range(map.Source.End + 1, range.End));
                    }
                }

                startRanges.AddRange(nextMapRanges);
            }

            return startRanges.Min(r => r.Start);
        }

        static List<Range> GetStartRanges(string input)
        {
            long[] values = input.Split(": ")[1].Split().Select(long.Parse).ToArray();
            List<Range> output = new();

            foreach (long[] seed in values.Chunk(2))
                output.Add(new Range(seed[0], seed[0] + seed[1] - 1));

            return output;
        }

        static void LoadMaps(string[] input)
        {
            _mapsCollections = new()
            {
                { seedToSoil, new List<Map>() },
                { soilToFertilizer, new List<Map>() },
                { fertilizerToWater, new List<Map>() },
                { waterToLight, new List<Map>() },
                { lightToTemperature, new List<Map>() },
                { temperatureToHumidity, new List<Map>() },
                { humidityToLocation, new List<Map>() }
            };

            List<Map> currCollection = default;

            for (int line = 2; line < input.Length; line++)
            {
                string lineValue = input[line];

                if (string.IsNullOrWhiteSpace(lineValue))
                    continue;

                if (_mapsCollections.TryGetValue(lineValue, out List<Map> value))
                {
                    currCollection = value;
                    continue;
                }

                long[] lineData = lineValue.Split().Select(long.Parse).ToArray();
                currCollection.Add(new Map
                {
                    Destination = new Range(lineData[0], lineData[0] + lineData[2] - 1),
                    Source = new Range(lineData[1], lineData[1] + lineData[2] - 1)
                });
            }
        }

        static bool IsMatch(Range range, Range mapSource)
            => mapSource.Start <= range.End && range.Start <= mapSource.End;
    }
}
