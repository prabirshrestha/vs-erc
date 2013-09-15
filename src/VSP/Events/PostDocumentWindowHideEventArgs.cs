using System;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;

namespace VSP.Events
{
    public class PostDocumentWindowHideEventArgs : EventArgs
    {
        private readonly VsEvents events;

        public PostDocumentWindowHideEventArgs(VsEvents events)
        {
            this.events = events;
        }

        public uint DocCookie { get; set; }
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