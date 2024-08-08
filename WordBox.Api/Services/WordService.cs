
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using WordBox.Api;

namespace WordBox.Api.Services;

public class WordService : IWordService
{
    private readonly WordBoxDbContext _context;
    public WordService(WordBoxDbContext context) => _context = context;

    public async Task<Result<WordDto>> CreateWord(CreateWordDto createWordDto)
    {
        if (await _context.Words.AnyAsync(w => w.Text == createWordDto.text && w.LanguageId == createWordDto.languageId)) 
        {
            return Result<WordDto>.Failure("Word already exists");
        }

        var word = new Word
        {
            Id = Guid.NewGuid(),
            LanguageId = createWordDto.languageId,
            Text = createWordDto.text
        };
        _context.Words.Add(word);
        await _context.SaveChangesAsync();


        return Result<WordDto>.Success(new WordDto(word.Id, word.LanguageId, word.Text));
    }

    public Task<Result<WordDto>> DeleteWord(Guid id)
    {
        var word = _context.Words.Find(id);
        if (word == null) return Task.FromResult(Result<WordDto>.Failure("Word not found"));
        _context.Words.Remove(word);
        _context.SaveChanges();
        return Task.FromResult(Result<WordDto>.Success(new WordDto(word.Id, word.LanguageId, word.Text)));
    }

    public Task<Result<List<WordDto>>> GetAllWords()
    {
        var words = _context.Words
            .Select(w => new WordDto(w.Id, w.LanguageId, w.Text))
            .ToList();
        return Task.FromResult(Result<List<WordDto>>.Success(words));
    }

    public Task<Result<WordDto>> GetWord(Guid id)
    {
        var wordDto = _context.Words
            .Select(w => new WordDto(w.Id, w.LanguageId, w.Text))
            .FirstOrDefault(w => w.id == id);
        if (wordDto == null)
        {
            return Task.FromResult(Result<WordDto>.Failure("Word not found"));
        } 
        return Task.FromResult(Result<WordDto>.Success(wordDto));
    }

    public Task<Result<List<WordDto>>> GetWordsByLanguageId(Guid languageId)
    {
        var language = _context.Languages.Find(languageId);
        if (language == null)
        {
            return Task.FromResult(Result<List<WordDto>>.Failure("Language not found"));
        }

        var words = _context.Words
            .Where(w => w.LanguageId == languageId)
            .Select(w => new WordDto(w.Id, w.LanguageId, w.Text))
            .ToList();
        return Task.FromResult(Result<List<WordDto>>.Success(words));
    }

    public Task<Result<WordDto>> UpdateWord(UpdateWordDto updateWordDto)
    {
        var word = _context.Words.Find(updateWordDto.id);
        if (word == null)
        {
            return Task.FromResult(Result<WordDto>.Failure("Word not found"));
        }         
        word.Text = updateWordDto.text;
        _context.SaveChanges();
        return Task.FromResult(Result<WordDto>.Success(new WordDto(word.Id, word.LanguageId, word.Text)));
    }
}