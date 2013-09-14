namespace PrabirShrestha.VsErc.Bindings.Erc.Editor
{
    public class ErcEditorNameBindings : GetterBinding<string>
    {
        public override string Path
        {
            get { return "erc.editor.name"; }
        }

        public override string Value
        {
            get
            {
                return "vs";
            }
        }
    }
}