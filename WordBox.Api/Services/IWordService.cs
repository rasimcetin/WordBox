

namespace WordBox.Api.Services;

public interface IWordService
{
    Task<Result<WordDto>> CreateWord(CreateWordDto createWordDto);
    Task<Result<WordDto>> UpdateWord(UpdateWordDto updateWordDto);
    Task<Result<WordDto>> DeleteWord(Guid id);
    Task<Result<WordDto>> GetWord(Guid id);
    Task<Result<List<WordDto>>> GetWordsByLanguageId(Guid languageId);
    Task<Result<List<WordDto>>> GetAllWords();
}