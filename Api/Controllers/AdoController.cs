using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdoController : BaseController
    {
        public AdoController(IServiceAdo service):base(service)
        {
        }
    }
}
