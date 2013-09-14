using System;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;

namespace PrabirShrestha.VsErc.Bindings.Erc.Editor.Vs
{
    public class ErcEditorVsDteBindings : IBinding
    {
        private static DTE2 _dte;

        internal static DTE2 DTE
        {
            get
            {
                if (_dte == null)
                    _dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

                return _dte;
            }
        }

        public void Bind(ErcBindings ercBindings)
        {
            ErcBindings = ercBindings;
            ercBindings.Lua.NewTable("erc.editor.vs.dte");
            ercBindings.Lua.RegisterFunction("erc.editor.vs.dte.getdte", Reflect.GetProperty(() => DTE).GetMethod);
            ercBindings.Lua.RegisterFunction("erc.editor.vs.dte.executecommand", Reflect.GetMethod(()=> ExecuteCommand(default(string), default(string))));
        }

        public ErcBindings ErcBindings { get; private set; }

        public static void ExecuteCommand(string commandName, string commandArgs = "")
        {
            DTE.ExecuteCommand(commandName, commandArgs);
        }
    }
}