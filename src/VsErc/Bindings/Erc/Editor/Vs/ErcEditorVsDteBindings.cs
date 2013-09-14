using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs
{
    public class ErcEditorVsDteBindings : GetterBinding<DTE2>
    {
        private static DTE2 _dte;

        public override string Path
        {
            get { return "erc.editor.vs.dte"; }
        }

        public override DTE2 Value
        {
            get { return DTE; }
        }

        internal static DTE2 DTE
        {
            get
            {
                if (_dte == null)
                    _dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

                return _dte;
            }
        }
    }
}