using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using VSP.Comands;
using VSP.Events;

namespace VSP
{
    public class VsHelper
    {
        private readonly Package package;
        private readonly VsEvents events;
        private readonly VsCommands commands;

        private static DTE2 dte;

        private static DTE2 sdte
        {
            get { return dte ?? (dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2); }
        }

        public VsHelper(Package package)
        {
            this.package = package;
            this.events = new VsEvents(this);
            this.commands = new VsCommands(this);
        }

        public Package Package
        {
            get { return this.package; }
        }

        public VsCommands Commands
        {
            get
            {
                return this.commands;
            }
        }

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public DTE2 DTE
        {
            get { return sdte; }
        }
    }
}