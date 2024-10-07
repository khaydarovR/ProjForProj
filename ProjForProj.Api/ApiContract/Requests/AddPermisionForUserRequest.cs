using ProjForProj.Api.Domain.Entity;

namespace ProjForProj.Api.ApiContract.Requests
{
    public class AddPermisionForUserRequest
    {
        public Guid UserId { get; set; }
        public ICollection<string> Permissions { get; set; }
    }

}