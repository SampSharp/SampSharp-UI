namespace SampSharp.UI.Utilities
{
    public interface IBatchedProperty<T>
    {
        void Apply();
        void SetContainer(T container);
        object Get();
        bool Set(object value);
    }
}