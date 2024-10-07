using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.ApiContract.Responses;
using ProjForProj.Api.Common;
using ProjForProj.Api.Domain.Entity;

public interface IDesigneObjService
{
    Task<Res<bool>> CreateAsync(CreateDesigneObjRequest designObject);
    Task<Res<DesigneObjResponse>> GetByIdAsync(Guid designeObjId);
}
