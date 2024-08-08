
using Microsoft.AspNetCore.Mvc;
using WordBox.Api.Services;

namespace WordBox.Api.Controllers;

[ApiController]
[Route("api/wordmeanings")]
public class WordMeaningController : ControllerBase
{
    private readonly IWordMeaningService _wordMeaningService;
    public WordMeaningController(IWordMeaningService wordMeaningService) => _wordMeaningService = wordMeaningService;

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<WordMeaningDto>>> GetWordMeaningById(Guid id)
    {
        var result = await _wordMeaningService.GetWordMeaning(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("word/{wordId}")]
    public async Task<ActionResult<Result<List<WordMeaningDto>>>> GetWordMeaningsByWordId(Guid wordId)
    {
        var result = await _wordMeaningService.GetWordMeaningsByWordId(wordId);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<WordMeaningDto>>> CreateWordMeaning(CreateWordMeaningDto createWordMeaningDto)
    {
        var result = await _wordMeaningService.CreateWordMeaning(createWordMeaningDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<WordMeaningDto>>> DeleteWordMeaning(Guid id)
    {
        var result = await _wordMeaningService.DeleteWordMeaning(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Result<WordMeaningDto>>> UpdateWordMeaning(UpdateWordMeaningDto updateWordMeaningDto)
    {
        var result = await _wordMeaningService.UpdateWordMeaning(updateWordMeaningDto);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }
}