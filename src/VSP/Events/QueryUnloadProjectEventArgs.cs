using EnvDTE;

namespace VSP.Events
{
    public class QueryUnloadProjectEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QueryUnloadProjectEventArgs(VsEvents vsEvents)
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