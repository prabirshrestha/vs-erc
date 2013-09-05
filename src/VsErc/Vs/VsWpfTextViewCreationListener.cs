

using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace PrabirShrestha.VsErc.Vs
{
    [Export(typeof (IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public class JavaScriptSaveListener : IWpfTextViewCreationListener
    {
        public void TextViewCreated(IWpfTextView textView)
        {
	    Debug.WriteLine("loaded");
        }
    }
}