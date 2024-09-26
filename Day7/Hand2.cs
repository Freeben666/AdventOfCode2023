using System;
using System.ComponentModel.DataAnnotations;

namespace Day7;

public class Hand2 : IComparable<Hand2>
    {
        public string Cards;
        public int Bid;
        public HandType Type;

    public Hand2(string cards, int bid)
    {
        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
        Bid = bid;

        this.SetType();
    }

    public void SetType()
        {
            var cardCounts = new Dictionary<char, int>();
            var jokers = 0;

            foreach (var card in Cards)
            {
                if (card == 'J') {
                    jokers++;
                    continue;
                }
                if (!cardCounts.ContainsKey(card)) cardCounts[card] = 0;
                cardCounts[card]++;
            }

            var pairs = cardCounts.Values.Where(x => x == 2).Count();
            var threes = cardCounts.Values.Where(x => x == 3).Count();
            var fours = cardCounts.Values.Where(x => x == 4).Count();
            var fives = cardCounts.Values.Where(x => x == 5).Count();

            if (
                fives > 0 ||
                (fours > 0 && jokers > 0) ||
                (threes > 0 && jokers > 1) ||
                (pairs > 0 && jokers > 2) ||
                jokers > 3
            ) Type = HandType.FiveOfAKind;
            else if (
                fours > 0 ||
                (threes > 0 && jokers > 0) ||
                (pairs > 0 && jokers > 1) ||
                jokers > 2
            ) Type = HandType.FourOfAKind;
            else if (
                (threes == 1 && pairs > 0) ||
                threes > 1
            ) Type = HandType.FullHouse;
            else if (
                threes > 0 ||
                (pairs > 0 && jokers > 0) ||
                jokers > 1
            ) Type  = HandType.ThreeOfAKind;
            else if (pairs > 1) Type = HandType.TwoPair;
            else if (
                pairs > 0 ||
                jokers >0
            ) Type = HandType.OnePair;
            else Type = HandType.HighCard;
        }

        public int CompareTo(Hand2? other)
        {
            if (other == null) return -1; // If the other hand is null, this hand is greater.
            
            if ((int)this.Type < (int)other.Type) return -1;
            if ((int)this.Type > (int)other.Type) return 1;

            var thisCards = Cards.ToList();
            var otherCards = other.Cards.ToList();

            for (int i = 0; i < thisCards.Count; i++)
            {
                if (CardValue(thisCards[i]) < CardValue(otherCards[i]))
                {
                    //Console.WriteLine("{0} < {1}",this.Cards, other.Cards);
                    return -1;
                }
                if (CardValue(thisCards[i]) > CardValue(otherCards[i]))
                {
                    //Console.WriteLine("{0} > {1}",this.Cards, other.Cards);
                    return 1;
                }
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
                case 'J': return 1;
                case 'T': return 10;
                default: return card - '0';
            }
        }
    }