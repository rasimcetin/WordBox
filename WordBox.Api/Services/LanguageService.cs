
using Microsoft.EntityFrameworkCore;
using WordBox.Api;

namespace WordBox.Api.Services;

public class LanguageService : ILanguageService
{
    private readonly WordBoxDbContext _context;
    public LanguageService(WordBoxDbContext context) => _context = context;

    public async Task<Result<LanguageDto>> CreateLanguage(CreateLanguageDto createLanguageDto)
    {
        var language = new Language
        {
            Id = Guid.NewGuid(),
            Name = createLanguageDto.Name,
            Code = createLanguageDto.Code
        };
        _context.Languages.Add(language);
        await _context.SaveChangesAsync();
        return Result<LanguageDto>.Success(new LanguageDto(language.Id, language.Name, language.Code));
    }

    public async Task<Result<LanguageDto>> UpdateLanguage(UpdateLanguageDto updateLanguageDto)
    {
        var language = _context.Languages.Find(updateLanguageDto.Id);
        if (language == null) return Result<LanguageDto>.Failure("Language not found");
        language.Name = updateLanguageDto.Name;
        language.Code = updateLanguageDto.Code;
        await _context.SaveChangesAsync();
        return Result<LanguageDto>.Success(new LanguageDto(language.Id, language.Name, language.Code));
    }

    public async Task<Result<LanguageDto>> DeleteLanguage(Guid id)
    {
        var language = await _context.Languages.FindAsync(id);
        if (language == null) return Result<LanguageDto>.Failure("Language not found");
        _context.Languages.Remove(language);
        await _context.SaveChangesAsync();
        return Result<LanguageDto>.Success(new LanguageDto(language.Id, language.Name, language.Code));
    }

    public async Task<Result<LanguageDto>> GetLanguage(Guid id)
    {
        var language = await _context.Languages.FindAsync(id);
        if (language == null) return Result<LanguageDto>.Failure("Language not found");
        return Result<LanguageDto>.Success(new LanguageDto(language.Id, language.Name, language.Code));
    }

    public async Task<Result<List<LanguageDto>>> GetAllLanguages()
    {
        var languages = await _context.Languages
            .Select(l => new LanguageDto(l.Id, l.Name, l.Code))
            .ToListAsync();
        return Result<List<LanguageDto>>.Success(languages);
    }
}