using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RpoController : BaseController
    {
        public RpoController(IServiceRpo service):base(service)
        {
        }
    }
}
