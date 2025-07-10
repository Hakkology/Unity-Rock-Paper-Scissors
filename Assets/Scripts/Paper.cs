using System.Net;

public class Paper : Entity, IPaper
{
    public override EType Type => EType.Paper;
    public override EType Prey => EType.Rock;
}