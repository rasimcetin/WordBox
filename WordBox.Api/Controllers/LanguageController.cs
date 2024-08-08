
using Microsoft.AspNetCore.Mvc;
using WordBox.Api.Services;

[ApiController]
[Route("api/languages")]
public class LanguageController: ControllerBase
{
    private readonly ILanguageService _languageService;
    public LanguageController(ILanguageService languageService) => _languageService = languageService;

    [HttpGet]
    public async Task<ActionResult<Result<List<LanguageDto>>>> GetAllLanguages()
    {
        var result = await _languageService.GetAllLanguages();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Result<LanguageDto>>> GetLanguageById(Guid id)
    {
        var result = await _languageService.GetLanguage(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result<LanguageDto>>> CreateLanguage(CreateLanguageDto createLanguageDto)
    {
        var result = await _languageService.CreateLanguage(createLanguageDto);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Result<LanguageDto>>> DeleteLanguage(Guid id)
    {
        var result = await _languageService.DeleteLanguage(id);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

    [HttpPut]
    public async Task<ActionResult<Result<LanguageDto>>> UpdateLanguage(UpdateLanguageDto updateLanguageDto)
    {
        var result = await _languageService.UpdateLanguage(updateLanguageDto);
        if (!result.IsSuccess)
        {
            return NotFound(result);
        }
        return Ok(result);
    }

}