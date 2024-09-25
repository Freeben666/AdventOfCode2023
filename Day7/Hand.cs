using System;

namespace Day7;

public enum HandType
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
public class Hand : IComparable<Hand>
    {
        public string Cards;
        public int Bid;
        public HandType Type;

    public Hand(string cards, int bid)
    {
        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
        Bid = bid;

        this.SetType();
    }

    public void SetType()
        {
            var cardCounts = new Dictionary<char, int>();
            foreach (var card in Cards)
            {
                if (!cardCounts.ContainsKey(card)) cardCounts[card] = 0;
                cardCounts[card]++;
            }

            var pairs = cardCounts.Values.Where(x => x == 2).Count();
            var threes = cardCounts.Values.Where(x => x == 3).Count();
            var fours = cardCounts.Values.Where(x => x == 4).Count();
            var fives = cardCounts.Values.Where(x => x == 5).Count();

            if (fives > 0) Type = HandType.FiveOfAKind;
            else if (fours > 0) Type = HandType.FourOfAKind;
            else if (threes > 0 && pairs > 0) Type = HandType.FullHouse;
            else if (threes > 0) Type  = HandType.ThreeOfAKind;
            else if (pairs == 2) Type = HandType.TwoPair;
            else if (pairs == 1) Type = HandType.OnePair;
            else Type = HandType.HighCard;
        }

        public int CompareTo(Hand? other)
        {
            if (other == null) return -1; // If the other hand is null, this hand is greater.
            
            if ((int)this.Type < (int)other.Type) return -1;
            if ((int)this.Type > (int)other.Type) return 1;

            var thisCards = Cards.ToList();
            var otherCards = other.Cards.ToList();

            for (int i = 0; i < thisCards.Count; i++)
            {
                if (CardValue(thisCards[i]) < CardValue(otherCards[i])) return -1;
                if (CardValue(thisCards[i]) > CardValue(otherCards[i])) return 1;
            }

            return 0;
        }

        private int CardValue(char card)
        {
            switch (card)
            {
                case 'A': return 14;
                case 'K': return 13;
                case 'Q': return 12;
                case 'J': return 11;
                case 'T': return 10;
                default: return card - '0';
            }
        }
    }