using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.Common;
using ProjForProj.Api.Domain.Entity;

public interface IProjectService
{
    Task<Res<IEnumerable<Project>>> GetAllAsync();
    Task<Res<ProjectResponse>> GetByIdAsync(Guid id);
    Task<Res<bool>> CreateAsync(Project project);
    Task<Res<bool>> UpdateAsync(Project project);
    Task<Res<bool>> DeleteAsync(Guid id);
    Task<Res<bool>> AddUserAsync(Guid id, AddPermisionForUserRequest accessRequest);
}
