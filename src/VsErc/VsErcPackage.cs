using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EnvDTE;
using EnvDTE80;
using Microsoft.Win32;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using NLua;
using NLua.Exceptions;

namespace PrabirShrestha.VsErc
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [Guid(GuidList.guidVsErcPkgString)]
    [ProvideAutoLoad(UIContextGuids80.SolutionExists)]
    [ProvideAutoLoad(UIContextGuids80.NoSolution)]
    public sealed class VsErcPackage : Package
    {
        private static readonly Lua lua = new Lua();
        private static DTE2 _dte;

        public static Lua Lua { get { return lua; } }

        public static DTE2 DTE
        {
            get
            {
                if (_dte == null)
                    _dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2;

                return _dte;
            }
        }

        static VsErcPackage()
        {
            lua = new Lua();
            lua.LoadCLRPackage();
            lua.NewTable("erc");
            lua.NewTable("erc.editor");
            lua.NewTable("erc.editor.vs");
            lua.NewTable("erc._editor");
            lua.NewTable("erc._editor.vs");
        }

        private readonly IVsOutputWindow outWindow;
        private IVsOutputWindowPane ercLogWindowPane;

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public VsErcPackage()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this.ToString()));
            outWindow = GetGlobalService(typeof(SVsOutputWindow)) as IVsOutputWindow;
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            var ercPath = GetErcFilePath();
            if (File.Exists(ercPath))
            {
                Lua["erc.MYERC"] = ercPath;
            }

            this.RegisterErcLogWindow();
            this.RegisterDte();
            this.RegisterCast();
            this.RegisterServiceProvider();

            this.LoadScriptFiles();
        }

        private void RegisterServiceProvider()
        {
            Lua.NewTable("erc._editor.vs.serviceprovider");

            var methodInfo = GetType().GetMethod("GetServiceByGuid");
            Lua.RegisterFunction("erc._editor.vs.serviceprovider.getbyguid", methodInfo);

            methodInfo = GetType().GetMethod("GetServiceByType");
            Lua.RegisterFunction("erc._editor.vs.serviceprovider.getbytype", methodInfo);
        }

        public static object GetServiceByGuid(string guid)
        {
            return ServiceProvider.GlobalProvider.GetService(new Guid(guid));
        }

        public static object GetServiceByType(Type serviceType)
        {
            return ServiceProvider.GlobalProvider.GetService(serviceType);
        }

        private void RegisterErcLogWindow()
        {
            Guid customGuid = new Guid(GuidList.guidErcOutputPaneWindow);
            string customTitle = ".erc";
            this.outWindow.CreatePane(ref customGuid, customTitle, 1, 1);
            this.outWindow.GetPane(ref customGuid, out ercLogWindowPane);

            var methodInfo = GetType().GetMethod("Log");
            Lua.RegisterFunction("erc.log", this, methodInfo);

            methodInfo = GetType().GetMethod("ActivateLogWindowPane");
            Lua.RegisterFunction("erc.activatelogview", this, methodInfo);
        }

        public void Log(object obj)
        {
            if (obj == null)
            {
                this.ercLogWindowPane.OutputString("--null--" + Environment.NewLine);
            }
            else
            {
                this.ercLogWindowPane.OutputString(string.Format("{0}{1}", obj, Environment.NewLine));
            }
        }

        private void RegisterDte()
        {
            var methodInfo = GetType().GetProperty("DTE").GetMethod;
            Lua.RegisterFunction("erc._editor.vs.dte", this, methodInfo);
        }

        public void ActivateLogWindowPane()
        {
            this.ercLogWindowPane.Activate();
        }

        private void RegisterCast()
        {
            // http://stackoverflow.com/a/4925762/157260
            var methodInfo = GetType().GetMethod("DynamicCastTo");
            Lua.RegisterFunction("erc._editor.vs.cast", methodInfo);
        }

        public static T CastTo<T>(object obj, bool safeCast) where T : class
        {
            try
            {
                return (T)obj;
            }
            catch
            {
                if (safeCast) return null;
                else throw;
            }
        }

        public static dynamic DynamicCastTo(object obj, Type castTo, bool safeCast)
        {
            MethodInfo castMethod = typeof(VsErcPackage).GetMethod("CastTo").MakeGenericMethod(castTo);
            return castMethod.Invoke(null, new object[] { obj, safeCast });
        }

        protected override void Dispose(bool disposing)
        {
            if (Lua != null)
            {
                Lua.Dispose();
            }

            base.Dispose(disposing);
        }

        public static string GetErcFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".erc");
        }

        private void LoadScriptFiles()
        {
            var initScript = Path.Combine(AssemblyDirectory, "scripts", "init.lua");
            try
            {
                Lua.DoFile(initScript);
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                Log(ex.StackTrace);
                MessageBox.Show(ex.Message, "VsErc Init Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        static public string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

    }
}
