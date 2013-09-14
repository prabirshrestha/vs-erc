using EnvDTE;

namespace VSP.Events
{
    public class PreProjectUnloadEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PreProjectUnloadEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath
        {
            get
            {
                return Project.FullName;
            }
        }

        public Project Project { get; set; }
    }
}