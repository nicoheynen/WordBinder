namespace WordBinderCore.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly string _filePath;

        public WordRepository(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Word> GetWords()
        {
            var lines = File.ReadAllLines(_filePath);
            foreach (var line in lines)
            {
                yield return new Word(line);
            }
        }
    }
}
