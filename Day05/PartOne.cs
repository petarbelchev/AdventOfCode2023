namespace Day05
{
    public static class PartOne
    {
        private static long[] _seeds;

        private static readonly string seedToSoil = "seed-to-soil map:";
        private static readonly string soilToFertilizer = "soil-to-fertilizer map:";
        private static readonly string fertilizerToWater = "fertilizer-to-water map:";
        private static readonly string waterToLight = "water-to-light map:";
        private static readonly string lightToTemperature = "light-to-temperature map:";
        private static readonly string temperatureToHumidity = "temperature-to-humidity map:";
        private static readonly string humidityToLocation = "humidity-to-location map:";
        
        private static readonly Dictionary<string, List<string>> _mapCollections = new()
        {
            { seedToSoil, new List<string>() },
            { soilToFertilizer, new List<string>() },
            { fertilizerToWater, new List<string>() },
            { waterToLight, new List<string>() },
            { lightToTemperature, new List<string>() },
            { temperatureToHumidity, new List<string>() },
            { humidityToLocation, new List<string>() }
        };

        public static long GetResult(string[] input)
        {
            LoadSeeds(input[0]);
            LoadMaps(input);

            long lowestLocation = long.MaxValue;
            foreach (var seed in _seeds)
            {
                long location = FindLocation(seed);

                if (location < lowestLocation)
                    lowestLocation = location;
            }

            return lowestLocation;
        }

        private static void LoadSeeds(string input)
            => _seeds = input.Split(": ")[1].Split().Select(long.Parse).ToArray();

        private static void LoadMaps(string[] input)
        {
            List<string> currCollection = default;

            for (int line = 2; line < input.Length; line++)
            {
                string lineValue = input[line];

                if (string.IsNullOrWhiteSpace(lineValue))
                    continue;

                if (_mapCollections.TryGetValue(lineValue, out List<string> value))
                {
                    currCollection = value;
                    continue;
                }

                currCollection.Add(lineValue);
            }
        }

        private static long FindLocation(long seed)
        {
            long soil = FindMap(seed, _mapCollections[seedToSoil]);
            if (soil == -1)
                soil = seed;

            long fertilizer = FindMap(soil, _mapCollections[soilToFertilizer]);
            if (fertilizer == -1)
                fertilizer = soil;

            long water = FindMap(fertilizer, _mapCollections[fertilizerToWater]);
            if (water == -1)
                water = fertilizer;

            long light = FindMap(water, _mapCollections[waterToLight]);
            if (light == -1)
                light = water;

            long temperature = FindMap(light, _mapCollections[lightToTemperature]);
            if (temperature == -1)
                temperature = light;

            long humidity = FindMap(temperature, _mapCollections[temperatureToHumidity]);
            if (humidity == -1)
                humidity = temperature;

            long location = FindMap(humidity, _mapCollections[humidityToLocation]);
            if (location == -1)
                location = humidity;

            return location;
        }

        private static long FindMap(long source, List<string> map)
        {
            for (int i = 0; i < map.Count; i++)
            {
                long[] values = map[i].Split().Select(long.Parse).ToArray();

                if (values[1] <= source && source <= values[1] + values[2])
                    return values[0] + source - values[1];
            }

            return -1;
        }
    }
}
