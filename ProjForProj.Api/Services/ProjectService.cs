using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjForProj.Api.ApiContract.Requests;
using ProjForProj.Api.Common;
using ProjForProj.Api.DAL;
using ProjForProj.Api.Domain;
using ProjForProj.Api.Domain.Entity;
using System.Security.Claims;

namespace ProjForProj.Api.Services
{
    public class ProjectService(AppDbContext dbContext, UserManager<AppUser> userManager) : IProjectService
    {
        private readonly AppDbContext _dbContext = dbContext;
        private readonly UserManager<AppUser> userManager = userManager;

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Res<Project>> GetByIdAsync(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
                return new Res<Project>($"Project with ID {id} not found.");
            return new Res<Project>(project);
        }

        public async Task<Res<bool>> CreateAsync(Project project)
        {
            try
            {
                _dbContext.Projects.Add(project);
                await _dbContext.SaveChangesAsync();
                return new Res<bool>(true);
            }
            catch (Exception ex)
            {
                return new Res<bool>($"Error creating project: {ex.Message}");
            }
        }

        public async Task<Res<bool>> UpdateAsync(Project project)
        {
            var existingProject = await _dbContext.Projects.FindAsync(project.Id);
            if (existingProject == null)
                return new Res<bool>($"Project with ID {project.Id} not found.");

            try
            {
                _dbContext.Entry(existingProject).CurrentValues.SetValues(project);
                await _dbContext.SaveChangesAsync();
                return new Res<bool>(true);
            }
            catch (Exception ex)
            {
                return new Res<bool>($"Error updating project: {ex.Message}");
            }
        }

        public async Task<Res<bool>> DeleteAsync(Guid id)
        {
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
                return new Res<bool>($"Project with ID {id} not found.");

            try
            {
                project.IsDeleted = true;
                await _dbContext.SaveChangesAsync();
                return new Res<bool>(true);
            }
            catch (Exception ex)
            {
                return new Res<bool>($"Error deleting project: {ex.Message}");
            }
        }

        public async Task<Res<bool>> AddUserAsync(Guid id, AddPermisionForUserRequest accessRequest)
        {
            
            var project = await _dbContext.Projects.FindAsync(id);
            if (project == null)
                return new Res<bool>($"Project with ID {id} not found.");

            var user = await userManager.FindByIdAsync(accessRequest.UserId.ToString());

            var claims = await userManager.GetClaimsAsync(user);

            //TODO вынести управление разрешениями в отдельную таблицу. Пока использовал просто claim с префиксом id проекта
            var oldPermisions = claims.FirstOrDefault(c => c.Type == id + PFPUserClaimTypes.Permisions)!.Value;
            var newPermisons = oldPermisions + "," + accessRequest.Permissions;
            
            var newClaim = new Claim(id + PFPUserClaimTypes.Permisions, newPermisons);
            var res = await userManager.AddClaimAsync(user, newClaim);

            try
            {
                await _dbContext.SaveChangesAsync();
                return new Res<bool>(true);
            }
            catch (Exception ex)
            {
                return new Res<bool>($"Error adding user access: {ex.Message}");
            }
        }
    }

}

