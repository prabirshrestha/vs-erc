using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs.Events
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public class ErcEditorVsEventsWpfTextViewCreationListener : IWpfTextViewCreationListener
    {
        public void TextViewCreated(IWpfTextView textView)
        {
            //VsErcPackage.ErcBindings.EmitFunction.Call("_vswpftextviewcreated", textView);
        }
    }
}