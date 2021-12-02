using Aplicacion.Data;

namespace Aplicacion.Services
{
    public interface IService
    {
        // Where Id = X
        public Task<Tabla> Get(int id);

        // Where Id > X and Id < Y
        public Task<List<Tabla>> GetSome(int from, int to);

        //Inner join
        public Task<TablaDto[]> GetDto(int from, int to);

        public Task<Tabla> GetSp(int id);

        public Task<List<Tabla>> GetSomeSp(int from, int to);

        public Task<List<TablaDto>> GetDtoSp(int from, int to);
    }
}