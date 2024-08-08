namespace WordBox.Api;

public class WordMeaning
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid WordId { get; set; }

    public Word Word { get; set; } = null!;
    public string Text { get; set; } = string.Empty;
}
