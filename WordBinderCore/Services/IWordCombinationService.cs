namespace WordBinderCore.Services
{
    public interface IWordCombinationService
    {
        void Initialize(int lengthOfTheCombinedWord);
        FindCombinationsResult FindCombinations();
    }
}
