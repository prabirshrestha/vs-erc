using EnvDTE;

namespace VSP.Events
{
    public class QueryProjectUnloadEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QueryProjectUnloadEventArgs(VsEvents vsEvents)
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

        public bool UnloadProject { get; set; }

        public Project Project { get; set; }

    }
}