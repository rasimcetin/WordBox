
namespace WordBox.Api.Services;
public interface ILanguageService
{
    Task<Result<LanguageDto>> CreateLanguage(CreateLanguageDto createLanguageDto);
    Task<Result<LanguageDto>> UpdateLanguage(UpdateLanguageDto updateLanguageDto);
    Task<Result<LanguageDto>> DeleteLanguage(Guid id);
    Task<Result<LanguageDto>> GetLanguage(Guid id);
    Task<Result<List<LanguageDto>>> GetAllLanguages();
}