namespace VSP.Events
{
    public class PostProjectRemoveDirectoriesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectRemoveDirectoriesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string[] Files { get; set; }

    }
}