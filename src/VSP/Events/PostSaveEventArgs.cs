namespace VSP.Events
{
    public class PostSaveEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostSaveEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath { get; set; }
        public uint DocCookie { get; set; }

    }
}