using WordBinderCore.Services;

public class AppRunner
{
    private readonly IWordCombinationService _wordCombinationService;

    public AppRunner(IWordCombinationService wordCombinationService)
    {
        _wordCombinationService = wordCombinationService;
    }

    public void Run()
    {
        _wordCombinationService.Initialize(6);
        var result = _wordCombinationService.FindCombinations();

        if (result != null && result.Combinations != null && result.Combinations.Any())
        {
            foreach (var combination in result.Combinations)
            {
                var wordValues = combination.Select(word => word.Value).ToList();
                Console.WriteLine(string.Join("+", wordValues) + "=" + string.Concat(wordValues));
            }
        }
        else
        {
            Console.WriteLine("No combinations were found.");
        }
    }
}
