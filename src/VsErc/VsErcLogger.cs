using System;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell.Interop;

namespace PrabirShrestha.VsErc
{
    public class VsErcLogger
    {
        private readonly VsErcPackage package;
        private readonly object syncRoot = new object();
        private IVsOutputWindow outputWindow;
        private IVsOutputWindowPane pane;

        public VsErcLogger(VsErcPackage package)
        {
            this.package = package;
        }

        public void Initialize()
        {
            this.EnsurePane();
        }

        public void Log(string value)
        {
            if (EnsurePane())
            {
                Debug.Write(value);
                pane.OutputString(value);
            }
        }

        private bool EnsurePane()
        {
            if (pane == null)
            {
                lock (syncRoot)
                {
                    this.outputWindow = this.package.VsHelper.GetGlobalService<IVsOutputWindow>(typeof(SVsOutputWindow));
                    var customGuid = new Guid(GuidList.guidErcOutputPaneWindow);
                    this.outputWindow.CreatePane(ref customGuid, ".erc", 1, 0);
                    this.outputWindow.GetPane(ref customGuid, out this.pane);
                }
            }

            return pane != null;
        }
    }
}