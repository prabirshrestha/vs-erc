using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

namespace VSP.Events.Vs
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("Any")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public class WpfTextViewCreationListener : IWpfTextViewCreationListener
    {
        public void TextViewCreated(IWpfTextView wpfTextView)
        {
            var vsHelper = VsHelper.Instance;
            if (vsHelper != null)
            {
                var args = new WpfTextViewCreatedEventArgs(vsHelper.Events, wpfTextView);
                vsHelper.Events.TriggerWpfTextViewCreated(args);
            }
        }
    }
}