using System;
using System.ComponentModel.DataAnnotations;

namespace Day7;

public class Hand2 : IComparable<Hand2>
    {
        public string Cards;
        public int Bid;
        public HandType Type;

        private int _jokers;
        private int _pairs;
        private int _trips;
        private int _quads;
        private int _quints;
        private Dictionary<char, int> _cardCounts = new Dictionary<char, int>();

    public Hand2(string cards, int bid)
    {
        Cards = cards ?? throw new ArgumentNullException(nameof(cards));
        Bid = bid;

        // Count the number of each kind of cards.
        foreach (var card in Cards)
        {
            if (card == 'J') {
                _jokers++;
                continue;
            }
            if (!_cardCounts.ContainsKey(card)) _cardCounts[card] = 0;
            _cardCounts[card]++;
        }

        _pairs = _cardCounts.Values.Where(x => x == 2).Count();
        _trips = _cardCounts.Values.Where(x => x == 3).Count();
        _quads = _cardCounts.Values.Where(x => x == 4).Count();
        _quints = _cardCounts.Values.Where(x => x == 5).Count();

        // Determine the type of hand.
        if (_cardCounts.Count() == 0){
            if (_jokers == 5) Type = HandType.FiveOfAKind;
            else throw new Exception("Impossible hand !");
        }
        else if (_cardCounts.Values.Max() + _jokers >= 5) Type = HandType.FiveOfAKind;
        else if (_cardCounts.Values.Max() + _jokers == 4) Type = HandType.FourOfAKind;
        else if (
            (_trips >0 && _pairs >0) ||
            (_trips > 0 && _jokers > 0) || //Should already be Quads, but whatever
            (_pairs == 2 && _jokers == 1)
        ) Type = HandType.FullHouse;
        else if (_cardCounts.Values.Max() + _jokers == 3) Type = HandType.ThreeOfAKind;
        else if (_pairs == 2) Type = HandType.TwoPair;
        else if (_cardCounts.Values.Max() + _jokers == 2) Type = HandType.OnePair;
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