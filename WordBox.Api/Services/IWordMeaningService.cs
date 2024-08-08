
namespace WordBox.Api.Services;

public interface IWordMeaningService
{
    Task<Result<WordMeaningDto>> CreateWordMeaning(CreateWordMeaningDto createWordMeaningDto);
    Task<Result<WordMeaningDto>> UpdateWordMeaning(UpdateWordMeaningDto updateWordMeaningDto);
    Task<Result<WordMeaningDto>> DeleteWordMeaning(Guid id);
    Task<Result<WordMeaningDto>> GetWordMeaning(Guid id);
    Task<Result<List<WordMeaningDto>>> GetWordMeaningsByWordId(Guid wordId);
}