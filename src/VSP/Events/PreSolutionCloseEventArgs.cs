using EnvDTE;

namespace VSP.Events
{
    public class PreSolutionCloseEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PreSolutionCloseEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath { get; set; }

        public Solution Solution { get; set; }

    }
}