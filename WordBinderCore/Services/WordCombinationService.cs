namespace WordBinderCore.Services
{
    public class WordCombinationService : IWordCombinationService
    {
        private readonly IWordRepository _wordRepository;
        private HashSet<Word>? _wordDict;
        private List<Word>? _targetLengthWords;

        public WordCombinationService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public void Initialize(int lengthOfTheCombinedWord)
        {
            if (lengthOfTheCombinedWord <= 0)
            {
                throw new ArgumentException("Length of the combined word must be greater than 0.", nameof(lengthOfTheCombinedWord));
            }

            var words = _wordRepository.GetWords();
            _targetLengthWords = words.Where(word => word.Length == lengthOfTheCombinedWord).ToList();
            _wordDict = new HashSet<Word>(words.Except(_targetLengthWords));
        }

        public FindCombinationsResult FindCombinations()
        {
            if (_wordDict == null || _targetLengthWords == null)
            {
                throw new ArgumentNullException("The word dictionary or the target length words list is not initialized.");
            }

            var combinations = new List<List<Word>>();

            foreach (var word in _targetLengthWords)
            {
                combinations.AddRange(FindWordCombinations(word, _wordDict));
            }

            return new FindCombinationsResult
            {
                Combinations = combinations
            };
        }

        private List<List<Word>> FindWordCombinations(Word word, HashSet<Word> wordDict)
        {
            var combinations = new List<List<Word>>();

            if (wordDict.Contains(word))
            {
                combinations.Add(new List<Word> { word });
            }

            for (int i = 1; i < word.Length; i++)
            {
                var firstWordValue = word.Value.Substring(0, i);
                var firstWord = new Word(firstWordValue);

                if (wordDict.Contains(firstWord))
                {
                    var remainingWordValue = word.Value.Substring(i);
                    var remainingWord = new Word(remainingWordValue);
                    var subCombinations = FindWordCombinations(remainingWord, wordDict);

                    foreach (var subCombination in subCombinations)
                    {
                        var combination = new List<Word> { firstWord };
                        combination.AddRange(subCombination);
                        combinations.Add(combination);
                    }
                }
            }

            return combinations;
        }
    }
}
