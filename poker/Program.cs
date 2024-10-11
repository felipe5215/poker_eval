using poker;

var allKinds = Enum.GetValues(typeof(Suit));
var allValues = Enum.GetValues(typeof(Rank));

// var results = new List<int>();

var allGames = 0;
long player1Wins = 0;
long player2Wins = 0;

var deck = (from Suit suit in allKinds from Rank rank in allValues select new Card(rank, suit)).ToList();

const long numberOfRuns = 999999;

for (var i = 0; i <= numberOfRuns; i++)
{
    var completed = (double)i / numberOfRuns * 100;
    if (i % 1000 == 0)
    {
        Console.WriteLine($"{completed:F2}%");
    }

    var rnd = new Random();

    deck = deck.OrderBy(x => rnd.Next()).ToList();

    var player1Hand = deck.Take(5).ToList();
    var player2Hand = deck.Skip(5).Take(5).ToList();

    // results.Add(PokerHandEvaluator.CompareHands(player1Hand, player2Hand));

    var player1Ranking = PokerHandEvaluator.CompareHands(player1Hand, player2Hand);

    switch (player1Ranking)
    {
        case 1:
            player1Wins++;
            break;
        case -1:
            player2Wins++;
            break;
    }

    allGames++;
}


// calculate percentage of wins

var player1WinPercentage = (double)player1Wins / allGames * 100;
var player2WinPercentage = (double)player2Wins / allGames * 100;

Console.WriteLine($"Player 1 wins: {player1Wins} ({player1WinPercentage}%)");
Console.WriteLine($"Player 2 wins: {player2Wins} ({player2WinPercentage}%)");


internal struct PlayerHand(List<Card> hand, HandRanking ranking)
{
    public List<Card> Hand { get; set; } = hand;
    public HandRanking Ranking { get; set; } = ranking;
}