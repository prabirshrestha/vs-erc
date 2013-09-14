using System;
using VSP.Events.Vs;

namespace VSP.Events
{
    public class VsEvents
    {
        private readonly VsHelper vsHelper;
        private readonly DocumentListener documentListener;
        private readonly SolutionListener solutionListener;

        public VsHelper VsHelper
        {
            get { return this.vsHelper; }
        }

        public event EventHandler<PreSaveEventArgs> PreSave;
        public event EventHandler<PostSaveEventArgs> PostSave;

        public event EventHandler<PostSolutionOpenEventArgs> PostSolutionOpen; 
        public event EventHandler<QueryCloseSolutionEventArgs> QueryCloseSolution; 

        public VsEvents(VsHelper vsHelper)
        {
            this.vsHelper = vsHelper;
            this.documentListener = new DocumentListener(this);
            this.solutionListener = new SolutionListener(this);
        }

        internal void TriggerPreSave(PreSaveEventArgs args)
        {
            if (PreSave != null)
            {
                PreSave(this, args);
            }
        }

        internal void TriggerPostSave(PostSaveEventArgs args)
        {
            if (PostSave != null)
            {
                PostSave(this, args);
            }
        }

        internal void TriggerPostSolutionOpen(PostSolutionOpenEventArgs args)
        {
            if (PostSolutionOpen != null)
            {
                PostSolutionOpen(this, args);
            }
        }

        internal void TriggerQueryCloseSolution(QueryCloseSolutionEventArgs args)
        {
            if (QueryCloseSolution != null)
            {
                QueryCloseSolution(this, args);
            }
        }
    }
}