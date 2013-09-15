using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;

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

        private IWpfTextView wpfTextView;
        public IWpfTextView WpfTextView
        {
            get {
                return wpfTextView ??
                       (wpfTextView = this.events.VsHelper.GetWpfTextViewFromVsWindowFrame(VsWindowFrame));
            }
        }
    }
}