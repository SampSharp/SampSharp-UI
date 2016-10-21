using SampSharp.GameMode.Display;

namespace SampSharp.UI
{
    public interface IBatchedProperty<T>
    {
        void Apply();
        void SetContainer(T container);
        object Get();
        bool Set(object value);
    }
}