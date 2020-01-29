using System.Collections.Generic;

public interface IContainer
{
    List<ILookable> GetContents();
}
