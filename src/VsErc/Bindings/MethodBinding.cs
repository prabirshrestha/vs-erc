namespace PrabirShrestha.VsErc.Bindings
{
    public abstract class MethodBinding : IBinding
    {
        public virtual void Bind(ErcBindings ercBindings)
        {
            ErcBindings = ercBindings;
            ercBindings.Lua.RegisterFunction(Path, this, typeof(MethodBinding).GetMethod("Execute"));
        }

        public ErcBindings ErcBindings { get; private set; }

        public abstract string Path { get; }

        public abstract object[] Execute(params object[] parameters);
    }
}