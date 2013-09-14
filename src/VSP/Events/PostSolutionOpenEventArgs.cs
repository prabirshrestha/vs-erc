using EnvDTE;

namespace VSP.Events
{
    public class PostSolutionOpenEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostSolutionOpenEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath
        {
            get
            {
                return Solution.FullName;
            }
        }

        public Solution Solution { get; set; }

    }
}