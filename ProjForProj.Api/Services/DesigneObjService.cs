using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.ApiContract.Responses;
using ProjForProj.Api.Common;
using ProjForProj.Api.DAL;
using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.Services
{
    public class DesigneObjService(AppDbContext dbContext, UserManager<AppUser> userManager) : IDesigneObjService
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<Res<bool>> CreateAsync(CreateDesigneObjRequest designObject)
        {
            var project = await _dbContext.Projects
                .Include(p => p.DesignObjects)
                .FirstOrDefaultAsync(p => p.Id == designObject.ProjectId);

            if (project == null)
                return new Res<bool>($"Project with ID {designObject.ProjectId} not found");

            //валидация, что бы нельзя было указывать в качестве родителя объект из другого проекта
            if (designObject.ParentObjectId.HasValue)
            {
                var parentObject = await _dbContext.DesignObjects
                    .Where(d => d.ProjectId == designObject.ProjectId)
                    .FirstOrDefaultAsync(d => d.Id == designObject.ParentObjectId.Value);

                if (parentObject == null)
                    return new Res<bool>($"Parent design object with ID {designObject.ParentObjectId.Value} not found or not in the same project");
            }

            var newDesignObject = new DesignObject
            {
                Code = designObject.Code,
                Name = designObject.Name,
                ParentObjectId = designObject.ParentObjectId??null
            };

            project.DesignObjects.Add(newDesignObject);
            await _dbContext.SaveChangesAsync();

            return new Res<bool>(true);
        }


        public async Task<Res<DesigneObjResponse>> GetByIdAsync(Guid designeObjId)
        {
            var designObject = await _dbContext.DesignObjects
                .Include(d => d.Project)
                .Include(d => d.ParentObject)
                .Include(d => d.ChildObjects)
                .Include(d => d.DocumentationSets)
                .Select(d => new DesigneObjResponse
                {
                    ChildObjectsIds = d.ChildObjects.Select(d => d.Id).ToList(),
                    DocumentationSetsIds = d.DocumentationSets.Select(d => d.Id).ToList(),
                    ProjectName = d.Project.Name,
                    FullCode = GetFullCode(d),
                    Id = d.Id,
                    Name = d.Name,
                    ParentObjectId = d.ParentObjectId,
                })
                .FirstOrDefaultAsync(d => d.Id == designeObjId);

            if (designObject == null)
                return new Res<DesigneObjResponse>($"Design object with ID {designeObjId} not found.");

            return new Res<DesigneObjResponse>(designObject);
        }


        private static string GetFullCode(DesignObject designObject)
        {
            if (designObject.ParentObject == null)
                return designObject.Code;
            else
                return $"{GetFullCode(designObject.ParentObject)}.{designObject.Code}";
        }
    }

}

