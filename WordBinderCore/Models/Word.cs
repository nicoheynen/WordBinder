public class Word
{
    public string Value { get; }
    public int Length => Value.Length;

    public Word(string value)
    {
        Value = value;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Word other)
        {
            return Value == other.Value;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}