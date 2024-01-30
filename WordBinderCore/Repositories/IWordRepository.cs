namespace WordBinderCore.Repositories
{
    public interface IWordRepository
    {
        IEnumerable<Word> GetWords();
    }
}
