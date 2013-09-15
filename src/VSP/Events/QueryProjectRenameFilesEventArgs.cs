namespace VSP.Events
{
    public class QueryProjectRenameFilesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QueryProjectRenameFilesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string[] OldFiles { get; set; }
        public string[] NewFiles { get; set; }
    }
}