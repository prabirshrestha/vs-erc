using System.Reflection;

namespace PrabirShrestha.VsErc.Bindings.Erc
{
    public class ErcNameBindings : GetterBinding<string>
    {
        public override string Path
        {
            get { return "erc.name"; }
        }

        public override string Value
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
            }
        }
    }
}