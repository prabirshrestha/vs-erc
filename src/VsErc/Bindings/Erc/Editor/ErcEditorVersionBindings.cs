using PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor
{
    public class ErcEditorVersionBindings : GetterBinding<string>
    {
        public override string Path
        {
            get { return "erc.editor.version"; }
        }

        public override string Value
        {
            get
            {
                return ErcEditorVsDteBindings.DTE.Version;
            }
        }
    }
}