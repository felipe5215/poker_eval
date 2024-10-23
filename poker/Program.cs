using poker;

var allKinds = Enum.GetValues(typeof(Suit));
var allValues = Enum.GetValues(typeof(Rank));


var deck = (from Suit suit in allKinds from Rank rank in allValues select new Card(rank, suit)).ToList();

const long numberOfRuns = 20;
var absoluteBestHand = new List<Card>();

for (var i = 0; i <= numberOfRuns; i++)
{
    var rnd = new Random();

    deck = deck.OrderBy(x => rnd.Next()).ToList();

    var player1Hand = deck.Take(7).ToList();
    var player2Hand = deck.Skip(7).Take(7).ToList();

// var player1Ranking = PokerHandEvaluator.CompareHands(player1Hand, player2Hand);
    var player1Combinations = GetCombinations(player1Hand, 5);
    var p1BestHand = DetermineBestHand(player1Combinations);

    var player2Combinations = GetCombinations(player2Hand, 5);
    var p2BestHand = DetermineBestHand(player2Combinations);

    if (i == 0)
    {
        absoluteBestHand = PokerHandEvaluator.GetWinningHand(p1BestHand, p2BestHand);
    }
    else
    {
        absoluteBestHand = PokerHandEvaluator.GetWinningHand(absoluteBestHand,
            PokerHandEvaluator.GetWinningHand(p1BestHand, p2BestHand));
    }
}

var ranking = PokerHandEvaluator.EvaluateHand(absoluteBestHand);
Console.WriteLine(ranking);


var winningHand = new List<string>();
foreach (var card in absoluteBestHand)
{
    var cardString = $"{card.Rank} of {card.Suit}";
    winningHand.Add(cardString);
}

Console.WriteLine(string.Join(", ", winningHand));


static List<List<T>> GetCombinations<T>(List<T> list, int k)
{
    var result = new List<List<T>>();

    if (k == 0)
    {
        result.Add(new List<T>());
        return result;
    }

    if (list.Count == 0)
    {
        return result; // Empty list, no combinations can be formed
    }

    T head = list[0];
    var rest = list.GetRange(1, list.Count - 1);

    // Combinations that include the head
    foreach (var combination in GetCombinations(rest, k - 1))
    {
        var combWithHead = new List<T> { head };
        combWithHead.AddRange(combination);
        result.Add(combWithHead);
    }

    // Combinations that exclude the head
    result.AddRange(GetCombinations(rest, k));

    return result;
}

static List<Card> DetermineBestHand(List<List<Card>> hands)
{
    var currentBestHand = hands[0];

    foreach (var hand in hands.GetRange(1, hands.Count - 1))
    {
        var result = PokerHandEvaluator.GetWinningHand(hand, currentBestHand);
        if (result == hand)
        {
            currentBestHand = hand;
        }
    }

    return currentBestHand;
}


internal struct PlayerHand(List<Card> hand, HandRanking ranking)
{
    public List<Card> Hand { get; set; } = hand;
    public Card[] cards;
}