using NLua;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs
{
    public class ErcEditorVsAssemblies : ValueBinding<LuaTable>
    {
        public override string Path
        {
            get { return "erc.editor.vs.assemblies"; }
        }

        public override LuaTable Value
        {
            get
            {
                ErcBindings.Lua.NewTable("erc.editor.vs.assemblies");
                var assemblies = ErcBindings.Lua.GetTable("erc.editor.vs.assemblies");

                assemblies["winforms"] = typeof (System.Windows.Forms.MessageBox).Assembly.FullName;
                assemblies["vsshell"] = typeof(Microsoft.VisualStudio.VSConstants).Assembly.FullName;
                assemblies["vsinterop"] = typeof(Microsoft.VisualStudio.Shell.Interop.IVsCommandWindow).Assembly.FullName;

                return assemblies;
            }
        }
    }
}
