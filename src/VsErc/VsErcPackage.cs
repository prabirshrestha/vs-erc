using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using NLua;
using PrabirShrestha.VsErc.LuaBindings;
using VSP;

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
        private static VsErcPackage instance;
        private readonly VsHelper vsHelper;
        private readonly Lua lua;
        private readonly VsErcBindings vsErcBindings;
        private readonly VsErcLogger vsErcLogger;

        public VsHelper VsHelper
        {
            get
            {
                return this.vsHelper;
            }
        }

        public Lua Lua
        {
            get
            {
                return this.lua;
            }
        }

        public VsErcLogger Logger
        {
            get { return this.vsErcLogger; }
        }

        public static VsErcPackage Instance
        {
            get { return instance; }
        }

        public VsErcPackage()
        {
            instance = this;
            this.vsHelper = new VsHelper(this);
            this.vsErcLogger = new VsErcLogger(this);
            this.lua = new Lua();
            this.vsErcBindings = new VsErcBindings(this, VsErcBindings.DefaultErcFilePath);
        }

        protected override void Initialize()
        {
            base.Initialize();
            this.vsErcLogger.Initialize();
            lua.LoadCLRPackage();
            this.vsErcBindings.Initialize();
            this.vsErcBindings.LoadScripts();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            this.vsErcBindings.Dispose();
            this.lua.Dispose();
        }
    }
}
