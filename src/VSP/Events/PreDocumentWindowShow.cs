using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSP.Events
{
    public class PreDocumentWindowShowEventArgs : EventArgs
    {
        private readonly VsEvents events;

        public PreDocumentWindowShowEventArgs(VsEvents events)
        {
            this.events = events;
        }

        public uint DocCookie { get; set; }
        public bool FirstShow { get; set; }
        public IVsWindowFrame VsWindowFrame { get; set; }
        public string FilePath { get; set; }
    }
}