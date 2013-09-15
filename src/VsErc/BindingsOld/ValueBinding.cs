
namespace PrabirShrestha.VsErc.Bindings
{
    public abstract class ValueBinding<T> : IBinding
    {
        public virtual void Bind(ErcBindings ercBindings)
        {
            ErcBindings = ercBindings;
            ercBindings.Lua[Path] = Value;
        }

        public ErcBindings ErcBindings { get; private set; }

        public abstract string Path { get; }

        public abstract T Value { get; }
    }
}