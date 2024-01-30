[TestClass]
public class WordCombinationServiceTests
{
    private IWordCombinationService? _wordCombinationService;

    [TestInitialize]
    public void TestInitialize()
    {
        var wordRepository = new WordRepository("inputTest.txt");
        _wordCombinationService = new WordCombinationService(wordRepository);
    }

    [TestMethod]
    public void TestFindCombinations_Length6()
    {
        // Arrange
        var lengthOfTheCombinedWord = 6;
        var expectedCombinations = new List<List<string>>
        {
            new List<string> { "ban", "ana" },
            new List<string> { "po", "ti", "kl" },
            new List<string> { "pot", "ikl" }             
        };

        // Act
        _wordCombinationService?.Initialize(lengthOfTheCombinedWord);
        var result = _wordCombinationService?.FindCombinations();

        var actualCombinations = result?.Combinations?
            .Select(combination => combination.Select(word => word.Value).ToList())
            .ToList();

        // Assert
        Assert.IsTrue(AreEquivalent(expectedCombinations, actualCombinations));
    }

    [TestMethod]
    public void TestFindCombinations_Length9()
    {
        // Arrange
        var lengthOfTheCombinedWord = 9;
        var expectedCombinations = new List<List<string>>
        {
            new List<string> { "app", "le", "pies" }, 
            new List<string> { "apple", "pies" },  
            new List<string> { "applepi", "e", "s" },  
        };

        // Act
        _wordCombinationService?.Initialize(lengthOfTheCombinedWord);
        var result = _wordCombinationService?.FindCombinations();

        var actualCombinations = result?.Combinations?
            .Select(combination => combination.Select(word => word.Value).ToList())
            .ToList();

        // Assert
        Assert.IsTrue(AreEquivalent(expectedCombinations, actualCombinations));
    }

    [TestMethod]
    public void TestFindCombinations_WithInvalidLength()
    {
        // Arrange
        var lengthOfTheCombinedWord = 0;

        // Act and Assert
        Assert.ThrowsException<ArgumentException>(() => _wordCombinationService?.Initialize(lengthOfTheCombinedWord));
    }

    [TestMethod]
    public void TestFindCombinations_WithoutInitialization()
    {
        // Act and Assert
        Assert.ThrowsException<ArgumentNullException>(() => _wordCombinationService?.FindCombinations());
    }

    private bool AreEquivalent(List<List<string>> list1, List<List<string>> list2)
    {
        if (list1.Count != list2.Count)
            return false;

        var sortedList1 = list1.OrderBy(x => string.Join("", x)).ToList();
        var sortedList2 = list2.OrderBy(x => string.Join("", x)).ToList();

        for (int i = 0; i < sortedList1.Count; i++)
        {
            if (!sortedList1[i].SequenceEqual(sortedList2[i]))
                return false;
        }

        return true;
    }

}
