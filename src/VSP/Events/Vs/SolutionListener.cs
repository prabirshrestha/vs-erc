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
            var args = new PostProjectOpenEventArgs(this.events)
            {
                Project = this.events.VsHelper.GetProject(pHierarchy),
                Added = Convert.ToBoolean(fAdded)
            };

            this.events.TriggerPostProjectOpen(args);

            return VSConstants.S_OK;
        }

        public int OnQueryCloseProject(IVsHierarchy pHierarchy, int fRemoving, ref int pfCancel)
        {
            var args = new QueryCloseProjectEventArgs(this.events)
            {
                CloseProject = true,
                Project = this.events.VsHelper.GetProject(pHierarchy),
                Removing = Convert.ToBoolean(fRemoving)
            };

            this.events.TriggerQueryCloseProject(args);

            if (!args.CloseProject)
            {
                pfCancel = 1;
            }

            return VSConstants.S_OK;
        }

        public int OnBeforeCloseProject(IVsHierarchy pHierarchy, int fRemoved)
        {
            var args = new PreProjectCloseEventArgs(this.events)
            {
                Project = this.events.VsHelper.GetProject(pHierarchy),
                Removed = Convert.ToBoolean(fRemoved)
            };

            this.events.TriggerPreProjectClose(args);

            return VSConstants.S_OK;
        }

        public int OnAfterLoadProject(IVsHierarchy pStubHierarchy, IVsHierarchy pRealHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnQueryUnloadProject(IVsHierarchy pRealHierarchy, ref int pfCancel)
        {
            var args = new QueryUnloadProjectEventArgs(this.events)
            {
                Project = this.events.VsHelper.GetProject(pRealHierarchy),
                UnloadProject = true
            };

            this.events.TriggerQueryUnloadProject(args);

            if (!args.UnloadProject)
            {
                pfCancel = 1;
            }

            return VSConstants.S_OK;
        }

        public int OnBeforeUnloadProject(IVsHierarchy pRealHierarchy, IVsHierarchy pStubHierarchy)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterOpenSolution(object pUnkReserved, int fNewSolution)
        {
            var args = new PostSolutionOpenEventArgs(this.events)
            {
                Solution = this.events.VsHelper.DTE.Solution
            };

            this.events.TriggerPostSolutionOpen(args);

            return VSConstants.S_OK;
        }

        public int OnQueryCloseSolution(object pUnkReserved, ref int pfCancel)
        {
            var args = new QueryCloseSolutionEventArgs(this.events)
            {
                CloseSolution = true,
                Solution = this.events.VsHelper.DTE.Solution
            };

            this.events.TriggerQueryCloseSolution(args);

            if (!args.CloseSolution)
            {
                pfCancel = 1;
            }

            return VSConstants.S_OK;
        }

        public int OnBeforeCloseSolution(object pUnkReserved)
        {
            var args = new PreSolutionCloseEventArgs(this.events)
            {
                Solution = this.events.VsHelper.DTE.Solution
            };

            this.events.TriggerPreSolutionClose(args);

            return VSConstants.S_OK;
        }

        public int OnAfterCloseSolution(object pUnkReserved)
        {
            var args = new PostSolutionCloseEventArgs(this.events)
            {
                Solution = this.events.VsHelper.DTE.Solution
            };

            this.events.TriggerPostSolutionClose(args);

            return VSConstants.S_OK;
        }
    }
}