using GD.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.Common;
using ProjForProj.Api.Domain.Entity;
using ProjForProj.Api.Services;
using System.Diagnostics;

namespace ProjForProj.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DesignObjsController(IDesigneObjService doService) : CustomController
{
    private readonly IDesigneObjService _doService = doService;


    /// <summary>
    /// Получить объект проектирования
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var sw = new Stopwatch();
        sw.Start();
        var res = await _doService.GetByIdAsync(id);
        sw.Stop();
        Console.WriteLine(sw.ElapsedMilliseconds);
        Console.WriteLine("==================================================");
        return res.IsSuccess? Ok(res.Data) : BadRequest(res.ErrorList);
    }

    /// <summary>
    /// Создать DO
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateDO([FromBody] CreateDesigneObjRequest dto)
    {
        var res = await _doService.CreateAsync(dto);
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
    }
}

