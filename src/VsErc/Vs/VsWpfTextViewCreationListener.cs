

using System.ComponentModel.Composition;
using System.Diagnostics;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using NLua;

namespace PrabirShrestha.VsErc.Vs
{
    [Export(typeof (IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    public class VsErcWpfTextViewCreationListener : IWpfTextViewCreationListener
    {
        private static LuaFunction textcreatedFunction;

        private static LuaFunction TextcreatedFunction
        {
            get
            {
                if (textcreatedFunction == null)
                {
                    textcreatedFunction =
                        VsErcPackage.Lua.GetFunction("erc._editor.vs.events.onload_VsErcWpfTextViewCreationListener_TextCreated");
                }
                return textcreatedFunction;
            }
        }

        public void TextViewCreated(IWpfTextView textView)
        {
            TextcreatedFunction.Call(textView);
            textView.Closed += textView_Closed;
        }

        void textView_Closed(object sender, System.EventArgs e)
        {
        }
    }
}