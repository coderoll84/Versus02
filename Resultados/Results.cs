using BenchmarkDotNet.Attributes;

namespace Resultados
{
    public class Results
    {
        [Params(100)]
        public int IterationCount;

        [ParamsSource(nameof(ValuesForController))]
        public string Controller { get; set; }
        public IEnumerable<string> ValuesForController => new[] { "ef", "dpr", "rpo", "ado" };

        private readonly RestClient _restClient = new RestClient();


        [Benchmark]
        public async Task Get()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.Get(Controller, i);
            }
        }

        [Benchmark]
        public async Task GetSome()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.GetSome(Controller, i, i + 999);
            }
        }

        [Benchmark]
        public async Task GetDto()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.GetDto(Controller, i, i + 9);
            }
        }

        [Benchmark]
        public async Task GetSp()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.GetSp(Controller, i);
            }
        }

        [Benchmark]
        public async Task GetSomeSp()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.GetSomeSp(Controller, i, i + 999);
            }
        }

        [Benchmark]
        public async Task GetDtoSp()
        {
            for (int i = 1; i <= IterationCount; i++)
            {
                await _restClient.GetDtoSp(Controller, i, i + 9);
            }
        }
    }
}
