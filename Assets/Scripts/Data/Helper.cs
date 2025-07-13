public static class TypeHelper
{
    public static EType GetPrey(EType type)
    {
        return type switch
        {
            EType.Rock => EType.Scissors,
            EType.Paper => EType.Rock,
            EType.Scissors => EType.Paper,
            _ => throw new System.ArgumentOutOfRangeException()
        };
    }
}