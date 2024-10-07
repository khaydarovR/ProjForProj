using Microsoft.AspNetCore.Identity;

namespace ProjForProj.Api.Common
{
    public class AppUser: IdentityUser<Guid>
    {
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}