using System;
using System.Linq;
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

        public IVsHierarchy GetHierarchyFromCookie(uint docCookie)
        {
            uint flags, readlocks, editlocks;
            string name; IVsHierarchy hier;
            uint itemid; IntPtr docData;

            RunningDocumentTable.GetDocumentInfo(
                docCookie, out flags, out readlocks, out editlocks, out name, out hier, out itemid, out docData);

            return hier;
        }

        // Copied from http://social.msdn.microsoft.com/Forums/is/vsx/thread/2b5fcbd9-ddc9-42c9-b04e-67bd2aa4beb7
        private string GetDocumentMoniker(uint docCookie)
        {
            uint rdtFlags, readLocks, editLocks, itemId;
            IVsHierarchy owningHierarchy;
            string documentMoniker;
            IntPtr punkDocData;
            if (ErrorHandler.Succeeded(RunningDocumentTable.GetDocumentInfo(docCookie,
                                                        out rdtFlags,
                                                        out readLocks,
                                                        out editLocks,
                                                        out documentMoniker,
                                                        out owningHierarchy,
                                                        out itemId,
                                                        out punkDocData)))
            {
                try
                {
                    return documentMoniker;
                }
                finally
                {
                    if (punkDocData != IntPtr.Zero)
                    {
                        //It is important to release this, it is an IntPtr that represents a COM object, pursuant to the rules of COM
                        //(since this is an out parameter) it has had its ref-count increased by 1, which means if we don't call Release
                        //on it we will cause it to leak (and anything it holds on to).
                        Marshal.Release(punkDocData);
                    }
                }
            }

            return null;
        }

        public Document GetDocumentFromCookie(uint docCookie)
        {
            //http://pec.codeplex.com/
            // Retrieve document information from the cookie to get the full document name.
            string documentName = GetDocumentMoniker(docCookie);

            // Search against the IDE documents to find the object that matches the full document name.
            return dte
                    .Documents
                    .OfType<Document>()
                    .FirstOrDefault(x => x != null && x.FullName == documentName);
        }

        private IVsTrackProjectDocuments2 projectDocumentTracker2;
        public IVsTrackProjectDocuments2 ProjectDocumentTracker2
        {
            get
            {
                if (projectDocumentTracker2 == null)
                {
                    projectDocumentTracker2 = GetGlobalService<IVsTrackProjectDocuments2>(typeof(SVsTrackProjectDocuments));
                }

                return projectDocumentTracker2;
            }
        }

        private IVsTrackProjectRetargeting vsTrackProjectRetargeting;
        public IVsTrackProjectRetargeting VsTrackProjectRetargeting
        {
            get
            {
                if (vsTrackProjectRetargeting == null)
                {
                    vsTrackProjectRetargeting = GetGlobalService<IVsTrackProjectRetargeting>(typeof(SVsTrackProjectRetargeting));
                }

                return vsTrackProjectRetargeting;
            }
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