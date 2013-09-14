using EnvDTE;

namespace VSP.Events
{
    public class PostProjectOpenEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectOpenEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath { get; set; }

        public Project Project { get; set; }
        public bool Added { get; set; }
    }
}