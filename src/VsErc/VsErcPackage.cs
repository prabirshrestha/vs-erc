using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
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
        private readonly Lua lua;

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

            this.lua = new Lua();
            this.lua.LoadCLRPackage();
            this.lua.NewTable("erc");
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overridden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Debug.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this.ToString()));
            base.Initialize();

            var ercPath = GetErcFilePath();
            this.lua["erc.MYERC"] = ercPath;

            this.RegisterErcLogWindow();
            this.LoadScriptFiles();
        }

        private void RegisterErcLogWindow()
        {
            Guid customGuid = new Guid(GuidList.guidErcOutputPaneWindow);
            string customTitle = ".erc";
            this.outWindow.CreatePane(ref customGuid, customTitle, 1, 1);
            this.outWindow.GetPane(ref customGuid, out ercLogWindowPane);

            var methodInfo = GetType().GetMethod("Log");
            this.lua.RegisterFunction("erc.log", this, methodInfo);

            methodInfo = GetType().GetMethod("ActivateLogWindowPane");
            this.lua.RegisterFunction("erc.activateLogView", this, methodInfo);
        }

        public void Log(object obj)
        {
            if (obj == null)
            {
                this.ercLogWindowPane.OutputString("--null--");
            }
            else
            {
                this.ercLogWindowPane.OutputString(obj.ToString());
            }
        }

        public void ActivateLogWindowPane()
        {
            this.ercLogWindowPane.Activate();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.lua != null)
            {
                this.lua.Dispose();
            }

            base.Dispose(disposing);
        }

        #endregion

        public static string GetErcFilePath()
        {
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".erc");
        }

        private void LoadScriptFiles()
        {
            var initScript = Path.Combine(AssemblyDirectory, "scripts", "init.lua");
            try
            {
                this.lua.DoFile(initScript);
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
