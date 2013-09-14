using System.Reflection;

namespace PrabirShrestha.VsErc.Bindings.Erc
{
    public class ErcVersionBindings : GetterBinding<string>
    {
        public override string Path
        {
            get { return "erc.version"; }
        }

        public override string Value
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            }
        }
    }
}