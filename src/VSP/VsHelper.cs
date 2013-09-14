using System;
using System.Runtime.InteropServices;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSP.Comands;
using VSP.Events;

namespace VSP
{
    public class VsHelper
    {
        private readonly Package package;
        private readonly VsEvents events;
        private readonly VsCommands commands;

        private static DTE2 dte;

        private static DTE2 sdte
        {
            get { return dte ?? (dte = ServiceProvider.GlobalProvider.GetService(typeof(DTE)) as DTE2); }
        }

        public VsHelper(Package package)
        {
            this.package = package;
            this.events = new VsEvents(this);
            this.commands = new VsCommands(this);
        }

        public Package Package
        {
            get { return this.package; }
        }

        public VsCommands Commands
        {
            get
            {
                return this.commands;
            }
        }

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public DTE2 DTE
        {
            get { return sdte; }
        }

        private IVsRunningDocumentTable runningDocumentTable;
        public IVsRunningDocumentTable RunningDocumentTable
        {
            get
            {
                if (runningDocumentTable == null)
                {
                    runningDocumentTable = GetGlobalService<IVsRunningDocumentTable>(typeof(SVsRunningDocumentTable));
                }

                return runningDocumentTable;
            }
        }

        private IVsSolution vsSolution;
        public IVsSolution VsSolution
        {
            get
            {
                if (vsSolution == null)
                {
                    vsSolution = GetGlobalService<IVsSolution>(typeof(SVsSolution));
                }

                return vsSolution;
            }
        }

        /// <remarks>http://blogs.clariusconsulting.net/pga/how-do-i-get-a-project-from-a-ivshierarchy-and-viceversa/</remarks>
        public Project GetProject(IVsHierarchy hierarchy)
        {
            object project;

            ErrorHandler.ThrowOnFailure
                (hierarchy.GetProperty(
                    VSConstants.VSITEMID_ROOT,
                    (int)__VSHPROPID.VSHPROPID_ExtObject,
                    out project));

            return (project as Project);
        }

        /// <remarks>http://blogs.clariusconsulting.net/pga/how-do-i-get-a-project-from-a-ivshierarchy-and-viceversa/</remarks>
        public IVsHierarchy GetHierarchy(Project project, IServiceProvider serviceProvider = null)
        {
            var solution = serviceProvider == null ? VsSolution :
                serviceProvider.GetService(typeof(SVsSolution)) as IVsSolution;

            IVsHierarchy hierarchy;

            solution.GetProjectOfUniqueName(project.FullName, out hierarchy);

            return hierarchy;
        }

        public object GetGlobalService(Type type)
        {
            return GetService(GlobalServiceProvider, type.GUID, false);
        }

        public T GetGlobalService<T>(Type type = null) where T : class
        {
            return GetGlobalService(type ?? typeof(T)) as T;
        }

        private static Microsoft.VisualStudio.OLE.Interop.IServiceProvider globalServiceProvider;
        private static Microsoft.VisualStudio.OLE.Interop.IServiceProvider GlobalServiceProvider
        {
            get
            {
                if (globalServiceProvider == null)
                {
                    globalServiceProvider = (Microsoft.VisualStudio.OLE.Interop.IServiceProvider)Package.GetGlobalService(
                        typeof(Microsoft.VisualStudio.OLE.Interop.IServiceProvider));
                }

                return globalServiceProvider;
            }
        }

        private static object GetService(Microsoft.VisualStudio.OLE.Interop.IServiceProvider serviceProvider, Guid guidService, bool unique)
        {
            var guidInterface = VSConstants.IID_IUnknown;
            var ptr = IntPtr.Zero;
            object service = null;

            if (serviceProvider.QueryService(ref guidService, ref guidInterface, out ptr) == 0 &&
                ptr != IntPtr.Zero)
            {
                try
                {
                    if (unique)
                    {
                        service = Marshal.GetUniqueObjectForIUnknown(ptr);
                    }
                    else
                    {
                        service = Marshal.GetObjectForIUnknown(ptr);
                    }
                }
                finally
                {
                    Marshal.Release(ptr);
                }
            }

            return service;
        }
    }
}