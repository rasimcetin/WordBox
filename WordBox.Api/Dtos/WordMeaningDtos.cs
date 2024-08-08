
public record CreateWordMeaningDto(Guid wordId, string text);

public record UpdateWordMeaningDto(Guid id, string text);

public record WordMeaningDto(Guid id, Guid wordId, string text);