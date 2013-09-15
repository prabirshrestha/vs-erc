namespace VSP.Events
{
    public class PostProjectRemoveFilesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectRemoveFilesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string[] Files { get; set; }

    }
}