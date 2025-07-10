public class Scissors : Entity, IScissor
{
    public override EType Type => EType.Scissors;
    public override EType Prey => EType.Paper;
}