using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DprController : BaseController
    {
        public DprController(IServiceDpr service) : base(service)
        {
        }
    }
}