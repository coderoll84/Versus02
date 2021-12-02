namespace Resultados
{
    public class BottleneckProcess
    {
        public string GetLastItem(string csvString)
        {
            var items = csvString.Split(",");
            var lastItem = items.LastOrDefault();
            return lastItem ?? string.Empty;
        }
    }
}
