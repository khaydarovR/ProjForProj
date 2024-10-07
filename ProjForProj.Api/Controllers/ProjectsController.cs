using GD.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.Common;
using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProjectsController : CustomController
{
    private readonly IProjectService _projectService;

    public ProjectsController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Получить все проекты
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var res = await _projectService.GetAllAsync();
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);

        //TODO добавить мидлвар для отлова ошибок (+логи)
    }

    /// <summary>
    /// Получить проект по Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var res = await _projectService.GetByIdAsync(id);
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
    }


    /// <summary>
    /// Создать проект
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateProjectRequest dto)
    {
        //TODO в сервис передавать отдельный dto, т.к. контроллеры не должны знать про уровень domain
        // Cервисы тоже должны работать независимо от dto в http запросах
        // напрмер эти сервисы могут исползоваться не только в web api а напрмер на MVC сайте
        // (ApiController -> Services(App) -> DAL -> Domain)
        // Т.о Для схемы http запросов свои DTO, для сервисов свои  (для маппинга можно юзать mapster)

        var proj = new Project() { Name = dto.Name, Code = dto.Code };
        var res = await _projectService.CreateAsync(proj);
        return res.IsSuccess?Ok(res.Data) : BadRequest(res.ErrorList);
    }

    /// <summary>
    /// Изменить данные проекта
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, UpdateProjectRequest dto)
    {
        var proj = new Project() {Id = id, Name = dto.Name, Code = dto.Code };

        var res = await _projectService.UpdateAsync(proj);
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
    }


    /// <summary>
    /// Мягкое удаление проекта
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var res = await _projectService.DeleteAsync(id);
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);

    }

    /// <summary>
    /// Добавить разрешения для пользователя в указанном проекте
    /// </summary>
    /// <param name="id"></param>
    /// <param name="accessRequest"></param>
    /// <returns></returns>
    [HttpPost("{id}:AddUserAccess")]
    public async Task<IActionResult> AddAccess(Guid id, [FromBody] AddPermisionForUserRequest accessRequest)
    {
        var res = await _projectService.AddUserAsync(id, accessRequest);
        return res.IsSuccess ? Ok(res.Data) : BadRequest(res.ErrorList);
    }
}

