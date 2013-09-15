using System;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs.Events
{
    public class DocumentListener : IBinding, IVsRunningDocTableEvents3
    {
        uint pdwCookie = 0;
        IVsRunningDocumentTable vsRunningDocumentTable;

        public void Bind(ErcBindings ercBindings)
        {
            ErcBindings = ercBindings;
            vsRunningDocumentTable = VsErcPackage.GetGlobalService<IVsRunningDocumentTable>(typeof(SVsRunningDocumentTable));
            vsRunningDocumentTable.AdviseRunningDocTableEvents(this, out pdwCookie);
        }

        public ErcBindings ErcBindings { get; private set; }
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
            vsRunningDocumentTable.GetDocumentInfo(docCookie, out flags, out readlocks, out editlocks,
                out name, out hier, out itemid, out docData);

            return VSConstants.S_OK;
        }

        public int OnAfterSave(uint docCookie)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeDocumentWindowShow(uint docCookie, int fFirstShow, IVsWindowFrame pFrame)
        {
            return VSConstants.S_OK;
        }

        public int OnBeforeLastDocumentUnlock(uint docCookie, uint dwRDTLockType, uint dwReadLocksRemaining, uint dwEditLocksRemaining)
        {
            return VSConstants.S_OK;
        }
    }
}