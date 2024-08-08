namespace WordBox.Api;

public class Word
{
     public Guid Id { get; set; } = Guid.NewGuid();

     public Guid LanguageId { get; set; }
    public string Text { get; set; } = string.Empty;

    public ICollection<WordMeaning> Meanings { get; } = new List<WordMeaning>();
   
}
