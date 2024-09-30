using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SkinHunt.Service.Controllers
{
    [ApiController]
    [Authorize]
    public class AppControllerBase : ControllerBase
    {
        public AppControllerBase()
        {   
        }

        protected Task Publish()
        {
            return Task.FromResult(0);
        }
    }
}
