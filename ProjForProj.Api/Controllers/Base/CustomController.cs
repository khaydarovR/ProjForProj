using Microsoft.AspNetCore.Mvc;
using ProjForProj.Api.Domain;
using System.Security.Claims;

namespace GD.Api.Controllers.Base
{
    public class CustomController: ControllerBase
    {
        internal Guid ContextUserId
        {
            get
            {
                return Guid.Parse(User.FindFirst(PFPUserClaimTypes.Id)!.Value);
            }
        }
        internal string? CurrentIp => HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();
    }
}
