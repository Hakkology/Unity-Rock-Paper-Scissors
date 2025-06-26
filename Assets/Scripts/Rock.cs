using System;

public class Rock : Entity
{
    public override EType Type => EType.Rock;
    public override EType Prey => EType.Scissors;
}