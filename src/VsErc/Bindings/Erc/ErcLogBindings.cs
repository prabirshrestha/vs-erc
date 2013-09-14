using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.Shell.Interop;

namespace PrabirShrestha.VsErc.Bindings.Erc
{
    public class ErcLogBinding : MethodBinding
    {
        private readonly object syncRoot = new object();
        private IVsOutputWindow outputWindow;
        private IVsOutputWindowPane pane;

        public override string Path
        {
            get { return "erc.log"; }
        }

        public override object[] Execute(params object[] parameters)
        {
            if (parameters == null)
            {
                Log();
            }
            else
            {
                Log(parameters);
            }

            return null;
        }

        private void Log()
        {
            if (EnsurePane())
            {
                Debug.WriteLine("--null--");
            }
        }

        public void Log(IEnumerable<object> parameters)
        {
            if (EnsurePane())
            {
                foreach (var parameter in parameters)
                {
                    string text;
                    if (parameter == null)
                    {
                        text = "nil";
                    }
                    else if (parameter is bool)
                    {
                        text = parameter.ToString().ToLowerInvariant();
                    }
                    else
                    {
                        text = parameter.ToString();
                    }

                    Debug.Write(text);
                    pane.OutputString(text);
                }

                Debug.WriteLine("");
                pane.OutputString(Environment.NewLine);
            }
        }

        public void Log(string message)
        {
            Log(new[] { message });
        }

        private bool EnsurePane()
        {
            if (pane == null)
            {
                lock (syncRoot)
                {
                    this.outputWindow = VsErcPackage.GetGlobalService<IVsOutputWindow>(typeof(SVsOutputWindow));
                    var customGuid = new Guid(GuidList.guidErcOutputPaneWindow);
                    this.outputWindow.CreatePane(ref customGuid, ".erc", 1, 1);
                    this.outputWindow.GetPane(ref customGuid, out this.pane);
                }
            }

            return pane != null;
        }
    }
}