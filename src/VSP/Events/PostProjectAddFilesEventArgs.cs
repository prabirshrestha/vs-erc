namespace VSP.Events
{
    public class PostProjectAddFilesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectAddFilesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string[] Files { get; set; }

    }
}