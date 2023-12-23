namespace Day07;

public static class PartOne
{
    private class Hand : IComparable<Hand>
    {
        private static Dictionary<char, int> _cardsStrength = new()
        {
            { '2', 1 },
            { '3', 2 },
            { '4', 3 },
            { '5', 4 },
            { '6', 5 },
            { '7', 6 },
            { '8', 7 },
            { '9', 8 },
            { 'T', 10 },
            { 'J', 11 },
            { 'Q', 12 },
            { 'K', 13 },
            { 'A', 14 }
        };

        public char[] Cards { get; set; }

        public HandType Type { get; set; }

        public int Bid { get; set; }

        public int CompareTo(Hand other)
        {
            int result = default;
         
            if (Type == other.Type)
            {
                for (int i = 0; i < Cards.Length; i++)
                {
                    char card = Cards[i];
                    char otherCard = other.Cards[i];
                    result = _cardsStrength[card] - _cardsStrength[otherCard];
            
                    if (result == 0)
                        continue;
                    
                    break;
                }
            }
            else
                result = Type - other.Type;

            return result;
        }
    }

    private static List<Hand> _hands;

    public static int GetResult(string[] input)
    {
        _hands = new();

        foreach (string line in input)
            ReadHand(line);
        
        int winnings = CalculateWinnings();

        return winnings;
    }

    private static void ReadHand(string input)
    {
        string[] data = input.Split();
        var cards = new Dictionary<char, int>();
        
        foreach (var card in data[0])
        {
            if (!cards.ContainsKey(card))
                cards[card] = 0;
            
            cards[card]++;
        }
        
        int[] values = cards.Values.ToArray();
        HandType handType = values.Contains(5) ? HandType.FiveOfAKind
            : values.Contains(4) ? HandType.FourOfAKind
            : values.Contains(3) && values.Contains(2) ? HandType.FullHouse
            : values.Contains(3) ? HandType.ThreeOfAKind
            : values.Where(v => v == 2).Count() == 2 ? HandType.TwoPair
            : values.Contains(2) ? HandType.OnePair
            : HandType.HighCard;
        
        _hands.Add(new Hand
        {
            Cards = data[0].ToCharArray(),
            Type = handType,
            Bid = int.Parse(data[1])
        });
    }

    private static int CalculateWinnings()
    {
        int rank = 1;
        int winnings = 0;
        _hands.Sort();

        foreach (Hand hand in _hands)
            winnings += hand.Bid * rank++;

        return winnings;
    }
}
