using Microsoft.EntityFrameworkCore;

namespace WordBox.Api.Services;

public class WordMeaningService : IWordMeaningService
{
    private readonly WordBoxDbContext _context;
    public WordMeaningService(WordBoxDbContext context) => _context = context;

    public async Task<Result<WordMeaningDto>> CreateWordMeaning(CreateWordMeaningDto createWordMeaningDto)
    {
        var wordMeaning = new WordMeaning
        {
            Id = Guid.NewGuid(),
            WordId = createWordMeaningDto.wordId,
            Text = createWordMeaningDto.text
        };
        _context.WordMeanings.Add(wordMeaning);
        await _context.SaveChangesAsync();
        return Result<WordMeaningDto>.Success(new WordMeaningDto(wordMeaning.Id, wordMeaning.WordId, wordMeaning.Text));
    }

    public async Task<Result<WordMeaningDto>> UpdateWordMeaning(UpdateWordMeaningDto updateWordMeaningDto)
    {
        var wordMeaning = _context.WordMeanings.Find(updateWordMeaningDto.id);
        if (wordMeaning == null)
        {
            return Result<WordMeaningDto>.Failure("Word Meaning not found");
        } 
        wordMeaning.Text = updateWordMeaningDto.text;
        await _context.SaveChangesAsync();
        return Result<WordMeaningDto>.Success(new WordMeaningDto(wordMeaning.Id, wordMeaning.WordId, wordMeaning.Text));
    }

    public async Task<Result<WordMeaningDto>> DeleteWordMeaning(Guid id)
    {
        var wordMeaning = _context.WordMeanings.Find(id);
        if (wordMeaning == null)
        {
            return Result<WordMeaningDto>.Failure("Word Meaning not found");
        }
        _context.WordMeanings.Remove(wordMeaning);
        await _context.SaveChangesAsync();
        return Result<WordMeaningDto>.Success(new WordMeaningDto(wordMeaning.Id, wordMeaning.WordId, wordMeaning.Text));
    }

    public async Task<Result<WordMeaningDto>> GetWordMeaning(Guid id)
    {
        var wordMeaning = await _context.WordMeanings.FirstOrDefaultAsync(wm => wm.Id == id);
        if (wordMeaning == null)
        {
            return Result<WordMeaningDto>.Failure("Word Meaning not found");
        }
        return Result<WordMeaningDto>.Success(new WordMeaningDto(wordMeaning.Id, wordMeaning.WordId, wordMeaning.Text));
    }

    public async Task<Result<List<WordMeaningDto>>> GetWordMeaningsByWordId(Guid wordId)
    {
        var wordMeanings = await _context.WordMeanings
            .Where(wm => wm.WordId == wordId)
            .Select(wm => new WordMeaningDto(wm.Id, wm.WordId, wm.Text))
            .ToListAsync();
        return Result<List<WordMeaningDto>>.Success(wordMeanings);
    }
}