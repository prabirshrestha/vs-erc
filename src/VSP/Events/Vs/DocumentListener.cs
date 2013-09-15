using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace VSP.Events.Vs
{
    internal class DocumentListener : IVsRunningDocTableEvents3
    {
        private readonly VsEvents events;
        private uint pdwCookie;

        public DocumentListener(VsEvents events)
        {
            this.events = events;
            events.VsHelper.RunningDocumentTable.AdviseRunningDocTableEvents(this, out pdwCookie);
        }

        public int OnAfterAttributeChange(uint docCookie, uint grfAttribs)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterAttributeChangeEx(uint docCookie, uint grfAttribs, IVsHierarchy pHierOld, uint itemidOld, string pszMkDocumentOld, IVsHierarchy pHierNew, uint itemidNew, string pszMkDocumentNew)
        {
            return VSConstants.S_OK;
        }

        public int OnAfterDocumentWindowHide(uint docCookie, IVsWindowFrame pFrame)
        {
            uint flags, readlocks, editlocks;
            string name; IVsHierarchy hier;
            uint itemid; IntPtr docData;

            this.events.VsHelper.RunningDocumentTable
                .GetDocumentInfo(docCookie, out flags, out readlocks, out editlocks, out name,
                    out hier, out itemid, out docData);

            var args = new PostDocumentWindowHideEventArgs(this.events)
            {
                DocCookie = docCookie,
                VsWindowFrame = pFrame,
                FilePath = name
            };

            this.events.TriggerPostDocumentWindowHide(args);

            return VSConstants.S_OK;
        }

        public int OnAfterFirstDocumentLock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeSave(uint docCookie)
        {
            uint flags, readlocks, editlocks;
            string name; IVsHierarchy hier;
            uint itemid; IntPtr docData;

            this.events.VsHelper.RunningDocumentTable
                .GetDocumentInfo(docCookie, out flags, out readlocks, out editlocks, out name,
                    out hier, out itemid, out docData);

            var args = new PreSaveEventArgs(this.events)
            {
                FilePath = name,
                DocCookie = docCookie
            };

            this.events.TriggerPreSave(args);

            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            uint flags, readlocks, editlocks;
            string name; IVsHierarchy hier;
            uint itemid; IntPtr docData;

            this.events.VsHelper.RunningDocumentTable
                .GetDocumentInfo(docCookie, out flags, out readlocks, out editlocks, out name,
                    out hier, out itemid, out docData);

            var args = new PostSaveEventArgs(this.events)
            {
                FilePath = name,
                DocCookie = docCookie
            };

            this.events.TriggerPostSave(args);

            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            uint flags, readlocks, editlocks;
            string name; IVsHierarchy hier;
            uint itemid; IntPtr docData;

            this.events.VsHelper.RunningDocumentTable
                .GetDocumentInfo(docCookie, out flags, out readlocks, out editlocks, out name,
                    out hier, out itemid, out docData);

            var args = new PreDocumentWindowShowEventArgs(this.events)
            {
                DocCookie = docCookie,
                FirstShow = Convert.ToBoolean(fFirstShow),
                VsWindowFrame = pFrame,
                FilePath = name
            };

            this.events.TriggerPreDocumentWindowShow(args);

            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }
    }
}