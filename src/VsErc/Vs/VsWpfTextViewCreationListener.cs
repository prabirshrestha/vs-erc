using System;
using System.ComponentModel.Composition;
using System.Windows;
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
        private static Lazy<LuaFunction> ActivatedFocusFunction = new Lazy<LuaFunction>(() =>
           VsErcPackage.Lua.GetFunction("erc._editor.vs.events.onactivated_VsErcWpfTextViewCreationListener_VisualElementOnGotFocus"));
        private static Lazy<LuaFunction> DeactivatedFocusFunction = new Lazy<LuaFunction>(() =>
            VsErcPackage.Lua.GetFunction("erc._editor.vs.events.ondeactivated_VsErcWpfTextViewCreationListener_VisualElementOnLostFocus"));

        private IWpfTextView textView;

        public void TextViewCreated(IWpfTextView textView)
        {
            this.textView = textView;
            TextCreatedFunction.Value.Call(this.textView);
            textView.Closed += textView_Closed;
            textView.VisualElement.GotFocus +=VisualElement_GotFocus;
            textView.VisualElement.LostFocus += VisualElementOnLostFocus;
        }       

        void textView_Closed(object sender, EventArgs e)
        {
            TextClosedFunction.Value.Call(this.textView);
        }

        private void VisualElementOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            DeactivatedFocusFunction.Value.Call(this.textView);
        }

        private void VisualElement_GotFocus(object sender, RoutedEventArgs e)
        {
            ActivatedFocusFunction.Value.Call(this.textView);
        }
    }
}