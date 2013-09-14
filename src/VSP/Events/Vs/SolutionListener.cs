using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSP.Events.Vs
{
    public class SolutionListener : IVsSolutionEvents
    {
        private readonly VsEvents events;
        private uint pdwCookie;

        public SolutionListener(VsEvents events)
        {
            this.events = events;
            events.VsHelper.VsSolution.AdviseSolutionEvents(this, out pdwCookie);
        }

        public int OnAfterOpenProject(IVsHierarchy pHierarchy, int fAdded)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            var args = new PostSolutionOpenEventArgs(this.events);

            var solution = this.events.VsHelper.DTE.Solution;
            args.FilePath = solution.FullName;
            args.Solution = solution;
            this.events.TriggerPostSolutionOpen(args);

            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            var args = new QueryCloseSolutionEventArgs(this.events);

            var solution = this.events.VsHelper.DTE.Solution;
            args.CloseSolution = true;
            args.FilePath = solution.FullName;
            args.Solution = solution;
            this.events.TriggerQueryCloseSolution(args);

            if (!args.CloseSolution)
            {
                pfCancel = 1;
            }

            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            return VSConstants.S_OK;
        }
    }
}