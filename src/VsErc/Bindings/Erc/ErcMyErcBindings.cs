using System.IO;

namespace PrabirShrestha.VsErc.Bindings.Erc
{
    public class ErcMyErcBindings : ValueBinding<string>
    {
        private readonly string ercPath;

        public ErcMyErcBindings(string ercPath)
        {
            this.ercPath = ercPath;
        }

        public override string Path
        {
            get { return "erc.MYERC"; }
        }

        public override string Value
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.ercPath) && File.Exists(this.ercPath)
                    ? this.ercPath 
                    : null;
            }
        }
    }
}