using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EfController : BaseController
    {
        public EfController(IServiceEf service):base(service)
        {
        }
    }
}
