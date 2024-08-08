using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WordBox.Api;
using WordBox.Api.Services;

namespace WordBox.Api.Controllers;

[ApiController]
[Route("api/words")]
public class WordController : ControllerBase
{
    private IWordService _wordService;
    public WordController(IWordService wordService) => _wordService = wordService;

    [HttpGet]
    public async Task<ActionResult<Result<List<WordDto>>>> GetAllWords()
    {
        var result = await _wordService.GetAllWords();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<WordDto>>> GetWordById(Guid id)
    {
        var result = await _wordService.GetWord(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("languagewords/{languageId}")]
    public async Task<ActionResult<Result<List<WordDto>>>> GetWordsByLanguageId(Guid languageId)
    {
        var result = await _wordService.GetWordsByLanguageId(languageId);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<WordDto>>> CreateWord(CreateWordDto createWordDto)
    {
        var result = await _wordService.CreateWord(createWordDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<WordDto>>> DeleteWord(Guid id)
    {
        var result = await _wordService.DeleteWord(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Result<WordDto>>> UpdateWord(UpdateWordDto updateWordDto)
    {
        var result = await _wordService.UpdateWord(updateWordDto);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}