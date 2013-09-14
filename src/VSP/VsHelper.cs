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
    }
}