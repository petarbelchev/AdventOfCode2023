namespace Day07;

static class PartTwo
{
    private class Hand : IComparable<Hand>
    {
        private static Dictionary<char, int> _cardsStrength = new()
        {
            { 'J', 1 },
            { '2', 2 },
            { '3', 3 },
            { '4', 4 },
            { '5', 5 },
            { '6', 6 },
            { '7', 7 },
            { '8', 8 },
            { '9', 9 },
            { 'T', 10 },
            { 'Q', 11 },
            { 'K', 12 },
            { 'A', 13 }
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

        char joker = 'J';
        if (cards.ContainsKey(joker))
        {
            KeyValuePair<char, int> mostMetCard = cards
                .OrderByDescending(c => c.Value)
                .FirstOrDefault(c => c.Key != joker);
        
            if (mostMetCard.Key != default)
            {
                cards[mostMetCard.Key] += cards[joker];
                cards[joker] = 0;
            }
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
