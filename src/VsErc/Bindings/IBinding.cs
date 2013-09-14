namespace PrabirShrestha.VsErc.Bindings
{
    public interface IBinding
    {
        void Bind(ErcBindings ercBindings);

        ErcBindings ErcBindings { get; }
    }
}