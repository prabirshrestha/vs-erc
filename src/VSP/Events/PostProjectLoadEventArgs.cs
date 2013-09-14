using EnvDTE;

namespace VSP.Events
{
    public class PostProjectLoadEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectLoadEventArgs(VsEvents vsEvents)
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