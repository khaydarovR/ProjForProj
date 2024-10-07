using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.ApiContract.Requests
{
    public class AddPermisionForUserRequest
    {
        public Guid UserId { get; set; }
        public ICollection<string> Permissions { get; set; }
    }

    public class CreateProjectRequest
    {
        /// <summary>
        /// Код проекта
        /// </summary>
        /// <example>ADS534</example>
        public string Code { get; set; }
        /// <summary>
        /// Название проекта
        /// </summary>
        /// <example>Get from 1С</example>
        public string Name { get; set; }
    }

    public class UpdateProjectRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

}