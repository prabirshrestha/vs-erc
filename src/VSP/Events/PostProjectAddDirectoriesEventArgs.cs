namespace VSP.Events
{
    public class PostProjectAddDirectoriesEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostProjectAddDirectoriesEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }


    }
}