using System.Text;

namespace Day06
{
    static class PartTwo
    {
        public static long GetResult(string[] input)
        {
            Race race = GetRace(input);

            return GetRaceWins(race);
        }

        static Race GetRace(string[] input)
        {
            long time = GetValue(input[0]);
            long distance = GetValue(input[1]);

            return new Race(time, distance);
        }

        static long GetValue(string input)
        {
            string[] values = input
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .ToArray();

            var sb = new StringBuilder();
            foreach (var val in values)
                sb.Append(val);

            return long.Parse(sb.ToString());
        }

        static long GetRaceWins(Race race)
        {
            long wins = 0;

            for (long milliseconds = 1; milliseconds <= race.Time; milliseconds++)
            {
                long distance = (race.Time - milliseconds) * milliseconds;
                if (race.Distance < distance)
                    wins++;
            }

            return wins;
        }
    }
}
