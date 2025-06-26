using System.Collections.Generic;

public enum EType
{
    Rock,
    Paper,
    Scissors
}
public interface IEntity
{
    EType Type { get; }
    EType Prey { get; }
    void Init();
}
public interface IRock : IEntity { }
public interface IPaper : IEntity { }
public interface IScissor : IEntity { }