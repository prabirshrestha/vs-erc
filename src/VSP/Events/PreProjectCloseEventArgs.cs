using EnvDTE;

namespace VSP.Events
{
    public class PreProjectCloseEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PreProjectCloseEventArgs(VsEvents vsEvents)
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
        public bool Removed { get; set; }
    }
}