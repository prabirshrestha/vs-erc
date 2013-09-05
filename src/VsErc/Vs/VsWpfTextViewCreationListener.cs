using System;
using System.ComponentModel.Composition;
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
        private static Lazy<LuaFunction> TextCreatedFunction = new Lazy<LuaFunction>(() =>
            VsErcPackage.Lua.GetFunction("erc._editor.vs.events.onload_VsErcWpfTextViewCreationListener_TextCreated"));
        private static Lazy<LuaFunction> TextClosedFunction = new Lazy<LuaFunction>(() =>
            VsErcPackage.Lua.GetFunction("erc._editor.vs.events.onclose_VsErcWpfTextViewCreationListener_TextClosed"));

        public void TextViewCreated(IWpfTextView textView)
        {
            TextCreatedFunction.Value.Call(textView);
            textView.Closed += textView_Closed;
        }

        void textView_Closed(object sender, EventArgs e)
        {
            TextClosedFunction.Value.Call(sender);
        }
    }
}