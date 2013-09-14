using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NLua;

namespace PrabirShrestha.VsErc.Bindings
{
    public class ErcBindings : IDisposable
    {
        private readonly Lua lua;
        private readonly string ercPath;
        private Erc.ErcLogBinding logBinding;
        private readonly Lazy<LuaFunction> emitFunction;

        public Lua Lua
        {
            get { return this.lua; }
        }

        public LuaFunction EmitFunction
        {
            get { return this.emitFunction.Value; }
        }

        public ErcBindings(Lua lua, string ercPath)
        {
            this.lua = lua;
            this.ercPath = ercPath;
            this.emitFunction = new Lazy<LuaFunction>(() => this.lua.GetFunction("erc.emit"));
        }

        public void Initialize()
        {
            RegisterBindings();
            LoadScriptFiles();
        }

        private void RegisterBindings()
        {
            // all bindings goes here

            Lua.NewTable("erc");

            this.logBinding = new Erc.ErcLogBinding();
            this.logBinding.Bind(this);

            new Erc.ErcMyErcBindings(this.ercPath).Bind(this);
            new Erc.ErcNameBindings().Bind(this);
            new Erc.ErcVersionBindings().Bind(this);

            Lua.NewTable("erc.editor");
            new Erc.Editor.ErcEditorNameBindings().Bind(this);
            new Erc.Editor.ErcEditorVersionBindings().Bind(this);

            Lua.NewTable("erc.editor.vs");

            new Erc.Editor.Vs.ErcEditorVsDteBindings().Bind(this);
            new Erc.Editor.Vs.ErcEditorVsAssemblies().Bind(this);

            Lua.NewTable("erc.editor.vs._events");
        }

        private void LoadScriptFiles()
        {
            // init.lua is responsible for load erc.MYERC
            var initScript = Path.Combine(AssemblyDirectory, "scripts", "init.lua");
            try
            {
                Lua.DoFile(initScript);
            }
            catch (Exception ex)
            {
                this.logBinding.Log(ex.Message);
                this.logBinding.Log(ex.StackTrace);
                MessageBox.Show(ex.Message, "VsErc Init Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static string DefaultErcFilePath
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".erc");
            }
        }

        static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public void Dispose()
        {
            if (Lua != null)
            {
                Lua.Dispose();
            }
        }
    }
}
