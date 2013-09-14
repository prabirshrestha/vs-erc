using EnvDTE;

namespace VSP.Events
{
    public class QueryProjectCloseEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QueryProjectCloseEventArgs(VsEvents vsEvents)
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

        public bool Removing { get; set; }

        public bool CloseProject { get; set; }

        public Project Project { get; set; }

    }
}