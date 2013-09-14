using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSP.Events.Vs
{
    public class ProjectRetargettingListener : IVsTrackProjectRetargetingEvents
    {
        private readonly VsEvents events;
        private uint pdwCookie;

        public ProjectRetargettingListener(VsEvents events)
        {
            this.events = events;
            events.VsHelper.VsTrackProjectRetargeting.AdviseTrackProjectRetargetingEvents(this, out pdwCookie);
        }

        public int OnRetargetingBeforeChange(string projRef, IVsHierarchy pBeforeChangeHier, string currentTargetFramework,
            string newTargetFramework, out bool pCanceled, out string ppReasonMsg)
        {
            pCanceled = false;
            ppReasonMsg = "";
            return VSConstants.S_OK;
        }

        public int OnRetargetingCanceledChange(string projRef, IVsHierarchy pBeforeChangeHier, string currentTargetFramework,
            string newTargetFramework)
        {
            return VSConstants.S_OK;
        }

        public int OnRetargetingBeforeProjectSave(string projRef, IVsHierarchy pBeforeChangeHier, string currentTargetFramework,
            string newTargetFramework)
        {
            return VSConstants.S_OK;
        }

        public int OnRetargetingAfterChange(string projRef, IVsHierarchy pAfterChangeHier, string fromTargetFramework,
            string toTargetFramework)
        {
            return VSConstants.S_OK;
        }

        public int OnRetargetingFailure(string projRef, IVsHierarchy pHier, string fromTargetFramework, string toTargetFramework)
        {
            return VSConstants.S_OK;
        }
    }
}