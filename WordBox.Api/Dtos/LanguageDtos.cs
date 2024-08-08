
public record CreateLanguageDto(string Name, string Code);

public record UpdateLanguageDto(Guid Id, string Name, string Code);

public record LanguageDto(Guid Id, string Name, string Code);