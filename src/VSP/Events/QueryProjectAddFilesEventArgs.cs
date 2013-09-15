using EnvDTE;

namespace VSP.Events
{
    public class QueryProjectAddFilesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QueryProjectAddFilesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string[] Files { get; set; }
    }
}