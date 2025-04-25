using System.Collections.Generic;

namespace Ardonagh.Interfaces;

public interface ISaveLoadService<T>
{
    void Save(List<T> items);
    List<T> Load();
}