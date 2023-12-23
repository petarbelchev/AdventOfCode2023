namespace Day06
{
    static class PartOne
    {
        public static int GetResult(string[] input)
        {
            List<Race> races = GetRaces(input);
            var wins = new List<int>();
            foreach (Race race in races)
                wins.Add(GetRaceWins(race));

            return CalculateProduct(wins);
        }

        static List<Race> GetRaces(string[] input)
        {
            int[] times = GetValues(input[0]);
            int[] distances = GetValues(input[1]);
            var output = new List<Race>();

            for (int i = 0; i < times.Length; i++)
                output.Add(new Race(times[i], distances[i]));

            return output;
        }

        static int[] GetValues(string input)
        {
            return input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
                .ToArray();
        }

        static int GetRaceWins(Race race)
        {
            int wins = 0;

            for (int milliseconds = 1; milliseconds <= race.Time; milliseconds++)
            {
                long distance = (race.Time - milliseconds) * milliseconds;
                if (race.Distance < distance)
                    wins++;
            }

            return wins;
        }

        static int CalculateProduct(List<int> values)
        {
            int product = 1;
            foreach (var val in values)
                product *= val;

            return product;
        }
    }
}
