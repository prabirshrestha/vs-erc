using Microsoft.VisualStudio.Shell;

namespace VSP
{
    public class VSHelper
    {
        private readonly Package package;

        public VSHelper(Package package)
        {
            this.package = package;
        }

        public Package Package
        {
            get { return this.package; }
        }
    }
}