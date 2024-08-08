
public record CreateWordDto (Guid languageId, string text);
public record UpdateWordDto (Guid id, string text);

public record WordDto(Guid id, Guid languageId, string text);