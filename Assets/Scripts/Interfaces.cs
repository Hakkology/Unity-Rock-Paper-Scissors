using System.Collections.Generic;

public enum EType
{
    Rock,
    Paper,
    Scissors
}

public enum EArenaSide
{
    Left,
    Right
}

public interface IEntity
{
    EArenaSide Side { get; set; }
    EType Type { get; }
    EType Prey { get; }
    void Init();
}
public interface IRock : IEntity { }
public interface IPaper : IEntity { }
public interface IScissor : IEntity { }