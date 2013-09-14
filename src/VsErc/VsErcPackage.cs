using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Shell;
using NLua;
using PrabirShrestha.VsErc.Bindings;
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
        private static ErcBindings ercBindings;

        private readonly VsHelper vsHelper;

        public VsHelper VsHelper
        {
            get { return vsHelper; }
        }

        public VsErcPackage()
        {
            vsHelper = new VsHelper(this);
        }

        public static ErcBindings ErcBindings
        {
            get { return ercBindings; }
        }

        public static VsErcPackage Instance { get; private set; }

        protected override void Initialize()
        {
            base.Initialize();
            Instance = this;

            VsHelper.Events.PreSave += (sender, args) => {
                Debug.WriteLine("pre save " + args.FilePath);
            };

            VsHelper.Events.PostSave += (sender, args) => {
                Debug.WriteLine("post save " + args.FilePath);
            };

            VsHelper.Events.PostSolutionOpen += (sender, args) => {
                Debug.WriteLine("post solution open " + args.FilePath);
            };

            VsHelper.Events.QueryCloseSolution += (sender, args) => {
                Debug.WriteLine("query colse solution " + args.FilePath);
            };

            VsHelper.Events.PreSolutionClose += (sender, args) =>
                Debug.WriteLine("pre solution close " + args.FilePath);

            VsHelper.Events.PostSolutionClose += (sender, args) =>
                Debug.WriteLine("post solution close " + args.FilePath);

            //var lua = new Lua();
            //lua.LoadCLRPackage();
            //ercBindings = new ErcBindings(lua, ErcBindings.DefaultErcFilePath);
            //ErcBindings.Initialize();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            ercBindings.Dispose();
        }

        public static T GetGlobalService<T>(Type type = null) where T : class
        {
            return GetGlobalService(type ?? typeof(T)) as T;
        }
    }
}
