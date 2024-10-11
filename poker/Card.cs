namespace poker;

public struct Card(Rank rank, Suit suit)
{
    public Rank Rank { get; set; } = rank;
    public Suit Suit { get; set; } = suit;
}