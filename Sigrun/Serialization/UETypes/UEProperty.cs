namespace Sigrun.Serialization.UETypes
{
    public class UEProperty
    {
        public string Name { get; internal set; }
        public string Type { get; internal set; }
    }

    public abstract class UEProperty<T> : UEProperty
    {
        public T Value { get; set; }
    }

    public class UENoneProperty : UEProperty
    {
        public UENoneProperty() { Name = "None"; }
    }

    public class UEIntProperty : UEProperty<int> { }
    public class UEArrayProperty<T> : UEProperty<T[]> { }
}
