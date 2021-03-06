﻿using System;
using System.IO;
using System.Reflection;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;

namespace PrabirShrestha.VsErc.LuaBindings
{
    public class VsErcBindings : IDisposable
    {
        private readonly VsErcPackage package;
        private readonly string ercFilePath;

        public VsErcBindings(VsErcPackage package, string ercFilePath)
        {
            this.package = package;
            this.ercFilePath = ercFilePath;
        }

        public static string DefaultErcFilePath
        {
            get
            {
                // todo: check in ENV variable
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

        }

        public void Initialize()
        {
            var lua = this.package.Lua;

            lua.NewTable("erc");
            lua.RegisterFunction("erc._log", this.package.Logger, this.package.Logger.GetType().GetMethod("Log"));
            
            lua["erc.MYERC"] = ercFilePath;
            lua.NewTable("erc.editor");
            lua.NewTable("erc.editor.vs");
            this.RegisterDotNetAssemblyNames();

            lua.RegisterFunction("erc.name", this, Reflect.GetProperty(() => Name).GetMethod);
            lua.RegisterFunction("erc.version", this, Reflect.GetProperty(() => Version).GetMethod);
            lua.RegisterFunction("erc.platform", this, Reflect.GetProperty(() => Platform).GetMethod);
            lua.RegisterFunction("erc.arch", this, Reflect.GetProperty(() => Arch).GetMethod);

            lua.RegisterFunction("erc.editor.vs.dte", Reflect.GetProperty(() => DTE).GetMethod);

            lua["erc.editor.vs._vshelper"] = this.package.VsHelper;
        }

        public void LoadScripts()
        {
            // init.lua is responsible for load erc.MYERC
            var initScript = Path.Combine(AssemblyDirectory, "scripts", "init.lua");
            try
            {
                this.package.Lua.DoFile(initScript);
            }
            catch (Exception ex)
            {
                this.package.Logger.Log(string.Format("{0}\n{1}", ex.Message, ex.StackTrace));
            }
        }

        public string Name
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyTitleAttribute>().Title;
            }
        }

        public string Version
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            }
        }

        public string Platform
        {
            get
            {
                switch (Environment.OSVersion.Platform)
                {
                    case PlatformID.Win32S:
                    case PlatformID.Win32Windows:
                    case PlatformID.Win32NT:
                    case PlatformID.WinCE:
                        return "win";
                    case PlatformID.Unix:
                        return "unix";
                    case PlatformID.MacOSX:
                        return "osx";
                    case PlatformID.Xbox:
                        return "xbox";
                    default:
                        return null;
                }
            }
        }

        public string Arch
        {
            get
            {
                return "x32";
            }
        }

        private static DTE2 dte;

        private static DTE2 DTE
        {
            get { return dte ?? (dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2); }
        }

        private void RegisterDotNetAssemblyNames()
        {
            //IWpfTextView t = null;
            //ITextEdit textEdit = t.TextBuffer.CreateEdit();
            //var selection = t.Selection.g.StreamSelectionSpan.SnapshotSpan;
            //var x = selection.Start.Position - selection.Start.GetContainingLine().Start.Position + 1;

           
            ////local selection = wpftextview.Selection.StreamSelectionSpan.SnapshotSpan;
        ////local edit = wpftextview.TextBuffer:CreateEdit()
            var lua = this.package.Lua;
            lua.NewTable("erc.editor.vs.assemblies");
            var assemblies = lua.GetTable("erc.editor.vs.assemblies");

            assemblies["winforms"] = typeof(System.Windows.Forms.MessageBox).Assembly.FullName;
            assemblies["vsshell"] = typeof(Microsoft.VisualStudio.VSConstants).Assembly.FullName;
            assemblies["vsinterop"] = typeof(Microsoft.VisualStudio.Shell.Interop.IVsCommandWindow).Assembly.FullName;
        }
    }
}