namespace PrabirShrestha.VsErc.Bindings
{
    public abstract class GetterBinding<T> : IBinding
    {
        public virtual void Bind(ErcBindings ercBindings)
        {
            ErcBindings = ercBindings;
            ercBindings.Lua.RegisterFunction(Path, this, Reflect.GetProperty(() => Value).GetMethod);
        }

        public ErcBindings ErcBindings { get; private set; }

        public abstract string Path { get; }

        public abstract T Value { get; }

    }
}
