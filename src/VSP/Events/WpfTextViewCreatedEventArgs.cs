using System;
using Microsoft.VisualStudio.Text.Editor;

namespace VSP.Events
{
    public class WpfTextViewCreatedEventArgs : EventArgs
    {
        private readonly VsEvents events;
        private readonly IWpfTextView wpfTextView;

        public WpfTextViewCreatedEventArgs(VsEvents events, IWpfTextView wpfTextView)
        {
            this.events = events;
            this.wpfTextView = wpfTextView;
        }

        public IWpfTextView WpfTextView
        {
            get
            {
                return this.wpfTextView;
            }
        }
    }
}