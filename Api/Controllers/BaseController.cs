using Aplicacion.Data;
using Aplicacion.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private readonly IService service;

        public BaseController(IService service)
        {
            this.service = service;
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Tabla>> Get(int id)
        {
            return await service.Get(id);
        }

        [HttpGet("[action]/{from}/{to}")]
        public async Task<ActionResult<List<Tabla>>> GetSome(int from, int to)
        {
            return await service.GetSome(from,to);
        }

        [HttpGet("[action]/{from}/{to}")]
        public async Task<ActionResult<TablaDto[]>> GetDto(int from, int to)
        {
            return await service.GetDto(from, to);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Tabla>> GetSp(int id)
        {
            return await service.GetSp(id);
        }

        [HttpGet("[action]/{from}/{to}")]
        public async Task<ActionResult<List<Tabla>>> GetSomeSp(int from, int to)
        {
            return await service.GetSomeSp(from,to);
        }

        [HttpGet("[action]/{from}/{to}")]
        public async Task<ActionResult<List<TablaDto>>> GetDtoSp(int from, int to)
        {
            return await service.GetDtoSp(from, to);
        }
    }
}
