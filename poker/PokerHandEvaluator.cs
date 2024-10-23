using System.Diagnostics;

namespace poker;

public static class PokerHandEvaluator
{
    public static HandRanking EvaluateHand(List<Card> hand)
    {
        if (hand.Count != 5)
            throw new ArgumentException("Hand must contain exactly 5 cards.");

        var sortedHand = hand.OrderBy(c => c.Rank).ToList();

        // Create frequency maps
        var rankGroups = hand.GroupBy(c => c.Rank).OrderByDescending(g => g.Count());
        var suitGroups = hand.GroupBy(c => c.Suit);

        var isFlush = suitGroups.Any(sg => sg.Count() >= 5);
        var isStraight = IsStraight(sortedHand);
        var isRoyal = IsRoyal(sortedHand);

        if (isFlush && isStraight)
        {
            return isRoyal ? HandRanking.RoyalFlush : HandRanking.StraightFlush;
        }

        switch (rankGroups.First().Count())
        {
            case 4:
                return HandRanking.FourOfAKind;
            case 3 when rankGroups.ElementAt(1).Count() >= 2:
                return HandRanking.FullHouse;
        }

        if (isFlush)
        {
            return HandRanking.Flush;
        }

        if (isStraight)
        {
            return HandRanking.Straight;
        }

        return rankGroups.First().Count() switch
        {
            3 => HandRanking.ThreeOfAKind,
            2 when rankGroups.ElementAt(1).Count() == 2 => HandRanking.TwoPair,
            2 => HandRanking.OnePair,
            _ => HandRanking.HighCard
        };
    }

    private static bool IsStraight(List<Card> sortedHand)
    {
        var ranks = sortedHand.Select(c => (int)c.Rank).Distinct().ToList();

        for (var i = 0; i <= ranks.Count - 5; i++)
        {
            if (ranks[i + 4] - ranks[i] == 4)
                return true;
        }

        return ranks.Contains((int)Rank.Ace) && ranks.Contains((int)Rank.Two)
                                             && ranks.Contains((int)Rank.Three) && ranks.Contains((int)Rank.Four)
                                             && ranks.Contains((int)Rank.Five);
    }

    private static bool IsRoyal(List<Card> sortedHand)
    {
        var suits = sortedHand.GroupBy(c => c.Suit).Where(sg => sg.Count() >= 5);

        return suits.Select(suitGroup => suitGroup.Select(c => c.Rank).Distinct().ToList()).Any(ranks =>
            ranks.Contains(Rank.Ten) && ranks.Contains(Rank.Jack) && ranks.Contains(Rank.Queen) &&
            ranks.Contains(Rank.King) && ranks.Contains(Rank.Ace));
    }


    public static List<Card> GetWinningHand(List<Card> hand1, List<Card> hand2)
    {
        return CompareHands(hand1, hand2) > 0 ? hand1 : hand2;
    }

    public static int CompareHands(List<Card> hand1, List<Card> hand2)
    {
        var rank1 = EvaluateHand(hand1);
        var rank2 = EvaluateHand(hand2);

        if (rank1 != rank2)
        {
            // Higher hand ranking wins
            return rank1 > rank2 ? 1 : -1;
        }

        // Hand rankings are equal, apply tie-breakers
        return CompareTieBreaker(rank1, hand1, hand2);
    }

    private static int CompareTieBreaker(HandRanking handRanking, List<Card> hand1, List<Card> hand2)
    {
        // Create frequency maps
        var hand1Groups = GetRankGroups(hand1);
        var hand2Groups = GetRankGroups(hand2);

        switch (handRanking)
        {
            case HandRanking.HighCard:
            case HandRanking.Flush:
                return CompareHighCards(hand1, hand2);

            case HandRanking.OnePair:
                return ComparePairs(hand1Groups, hand2Groups, 2);

            case HandRanking.TwoPair:
                return CompareTwoPairs(hand1Groups, hand2Groups);

            case HandRanking.ThreeOfAKind:
                return ComparePairs(hand1Groups, hand2Groups, 3);

            case HandRanking.Straight:
            case HandRanking.StraightFlush:
                return CompareStraights(hand1, hand2);

            case HandRanking.FullHouse:
                return CompareFullHouses(hand1Groups, hand2Groups);

            case HandRanking.FourOfAKind:
                return ComparePairs(hand1Groups, hand2Groups, 4);

            case HandRanking.RoyalFlush:
                // Royal Flush tie
                return 0;

            default:
                return 0;
        }
    }

    private static List<IGrouping<Rank, Card>> GetRankGroups(List<Card> hand)
    {
        return hand.GroupBy(c => c.Rank)
            .OrderByDescending(g => g.Count())
            .ThenByDescending(g => g.Key)
            .ToList();
    }

    private static int CompareHighCards(List<Card> hand1, List<Card> hand2)
    {
        var hand1Ranks = hand1.Select(c => (int)c.Rank).OrderByDescending(r => r).ToList();
        var hand2Ranks = hand2.Select(c => (int)c.Rank).OrderByDescending(r => r).ToList();

        for (int i = 0; i < Math.Min(hand1Ranks.Count, hand2Ranks.Count); i++)
        {
            if (hand1Ranks[i] != hand2Ranks[i])
                return hand1Ranks[i] > hand2Ranks[i] ? 1 : -1;
        }

        return 0; // Tie
    }

    private static int ComparePairs(List<IGrouping<Rank, Card>> hand1Groups, List<IGrouping<Rank, Card>> hand2Groups,
        int groupSize)
    {
        // Compare the group of specified size
        var hand1Group = hand1Groups.FirstOrDefault(g => g.Count() == groupSize);
        var hand2Group = hand2Groups.FirstOrDefault(g => g.Count() == groupSize);

        if (hand1Group.Key != hand2Group.Key)
            return hand1Group.Key > hand2Group.Key ? 1 : -1;

        // Compare kickers
        var hand1Kickers = hand1Groups.Where(g => g.Count() != groupSize)
            .Select(g => (int)g.Key)
            .OrderByDescending(r => r)
            .ToList();

        var hand2Kickers = hand2Groups.Where(g => g.Count() != groupSize)
            .Select(g => (int)g.Key)
            .OrderByDescending(r => r)
            .ToList();

        for (int i = 0; i < Math.Min(hand1Kickers.Count, hand2Kickers.Count); i++)
        {
            if (hand1Kickers[i] != hand2Kickers[i])
                return hand1Kickers[i] > hand2Kickers[i] ? 1 : -1;
        }

        return 0; // Tie
    }

    private static int CompareTwoPairs(List<IGrouping<Rank, Card>> hand1Groups, List<IGrouping<Rank, Card>> hand2Groups)
    {
        var hand1Pairs = hand1Groups.Where(g => g.Count() == 2)
            .Select(g => g.Key)
            .OrderByDescending(r => r)
            .ToList();

        var hand2Pairs = hand2Groups.Where(g => g.Count() == 2)
            .Select(g => g.Key)
            .OrderByDescending(r => r)
            .ToList();

        // Compare highest pair
        if (hand1Pairs[0] != hand2Pairs[0])
            return hand1Pairs[0] > hand2Pairs[0] ? 1 : -1;

        // Compare second pair
        if (hand1Pairs[1] != hand2Pairs[1])
            return hand1Pairs[1] > hand2Pairs[1] ? 1 : -1;

        // Compare kickers
        var hand1Kicker = hand1Groups.Where(g => g.Count() == 1).Select(g => g.Key).First();
        var hand2Kicker = hand2Groups.Where(g => g.Count() == 1).Select(g => g.Key).First();

        if (hand1Kicker != hand2Kicker)
            return hand1Kicker > hand2Kicker ? 1 : -1;

        return 0; // Tie
    }

    private static int CompareStraights(List<Card> hand1, List<Card> hand2)
    {
        int hand1HighCard = GetHighCardInStraight(hand1);
        int hand2HighCard = GetHighCardInStraight(hand2);

        if (hand1HighCard != hand2HighCard)
            return hand1HighCard > hand2HighCard ? 1 : -1;

        return 0; // Tie
    }

    private static int GetHighCardInStraight(List<Card> hand)
    {
        var ranks = hand.Select(c => (int)c.Rank).Distinct().OrderBy(r => r).ToList();

        // Handle Ace-low straight (A-2-3-4-5)
        if (!ranks.Contains((int)Rank.Ace) || !ranks.Contains(2)) return ranks.Last();
        ranks.Remove((int)Rank.Ace);
        ranks.Insert(0, 1); // Treat Ace as 1

        return ranks.Last();
    }

    private static int CompareFullHouses(List<IGrouping<Rank, Card>> hand1Groups,
        List<IGrouping<Rank, Card>> hand2Groups)
    {
        // Compare three of a kind
        var hand1Three = hand1Groups.First(g => g.Count() == 3).Key;
        var hand2Three = hand2Groups.First(g => g.Count() == 3).Key;

        if (hand1Three != hand2Three)
            return hand1Three > hand2Three ? 1 : -1;

        // Compare pair
        var hand1Pair = hand1Groups.First(g => g.Count() == 2).Key;
        var hand2Pair = hand2Groups.First(g => g.Count() == 2).Key;

        if (hand1Pair != hand2Pair)
            return hand1Pair > hand2Pair ? 1 : -1;

        return 0; // Tie
    }
}