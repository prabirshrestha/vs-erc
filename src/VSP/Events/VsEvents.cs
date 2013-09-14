using System;
using VSP.Events.Vs;

namespace VSP.Events
{
    public class VsEvents
    {
        private readonly VsHelper vsHelper;
        private readonly DocumentListener documentListener;

        public VsHelper VsHelper
        {
            get { return this.vsHelper; }
        }

        public event EventHandler<PreSaveEventArgs> PreSave;
        public event EventHandler<PostSaveEventArgs> PostSave;

        public VsEvents(VsHelper vsHelper)
        {
            this.vsHelper = vsHelper;
            this.documentListener = new DocumentListener(this);
        }

        internal void TriggerPreSave(PreSaveEventArgs args)
        {
            if (PreSave != null)
            {
                PreSave(this, args);
            }
        }

        public void TriggerPostSave(PostSaveEventArgs args)
        {
            if (PostSave != null)
            {
                PostSave(this, args);
            }
        }
    }
}