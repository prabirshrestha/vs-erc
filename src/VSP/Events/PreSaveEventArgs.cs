using EnvDTE;

namespace VSP.Events
{
    public class PreSaveEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PreSaveEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath { get; set; }
        public uint DocCookie { get; set; }
    }
}