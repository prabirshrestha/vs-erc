using System;
using VSP.Events.Vs;

namespace VSP.Events
{
    public class VsEvents
    {
        private readonly VsHelper vsHelper;
        private readonly DocumentListener documentListener;
        private readonly SolutionListener solutionListener;
        private readonly ProjectDocumensListener projectDocumentListener;
        private readonly ProjectRetargettingListener projectRetargettingListener;

        public VsHelper VsHelper
        {
            get { return this.vsHelper; }
        }

        public event EventHandler<PreSaveEventArgs> PreSave;
        public event EventHandler<PostSaveEventArgs> PostSave;
        public event EventHandler<PreDocumentWindowShowEventArgs> PreDocumentWindowShow;
        public event EventHandler<PostDocumentWindowHideEventArgs> PostDocumentWindowHide; 

        public event EventHandler<PostSolutionOpenEventArgs> PostSolutionOpen;
        public event EventHandler<QuerySolutionCloseEventArgs> QuerySolutionClose;
        public event EventHandler<PreSolutionCloseEventArgs> PreSolutionClose;
        public event EventHandler<PostSolutionCloseEventArgs> PostSolutionClose;

        public event EventHandler<PostProjectOpenEventArgs> PostProjectOpen;
        public event EventHandler<QueryProjectCloseEventArgs> QueryProjectClose;
        public event EventHandler<PreProjectCloseEventArgs> PreProjectClose;
        public event EventHandler<QueryProjectUnloadEventArgs> QueryProjectUnload;
        public event EventHandler<PreProjectUnloadEventArgs> PreProjectUnload;
        public event EventHandler<PostProjectLoadEventArgs> PostProjectLoad;

        public event EventHandler<QueryProjectAddFilesEventArgs> QueryProjectAddFiles; 
        public event EventHandler<PostProjectAddFilesEventArgs> PostProjectAddFiles; 
        public event EventHandler<PostProjectAddDirectoriesEventArgs> PostProjectAddDirectories;
        public event EventHandler<PostProjectRemoveFilesEventArgs> PostProjectRemoveFiles;
        public event EventHandler<PostProjectRemoveDirectoriesEventArgs> PostProjectRemoveDirectories; 

        public string[] Files { get; set; }

        public VsEvents(VsHelper vsHelper)
        {
            this.vsHelper = vsHelper;
            this.documentListener = new DocumentListener(this);
            this.solutionListener = new SolutionListener(this);
            this.projectDocumentListener = new ProjectDocumensListener(this);
            this.projectRetargettingListener = new ProjectRetargettingListener(this);
        }

        internal void TriggerPreSave(PreSaveEventArgs args)
        {
            if (PreSave != null)
            {
                PreSave(this, args);
            }
        }

        internal void TriggerPostSave(PostSaveEventArgs args)
        {
            if (PostSave != null)
            {
                PostSave(this, args);
            }
        }

        internal void TriggerPreDocumentWindowShow(PreDocumentWindowShowEventArgs args)
        {
            if (PreDocumentWindowShow != null)
            {
                PreDocumentWindowShow(this, args);
            }
        }

        internal void TriggerPostDocumentWindowHide(PostDocumentWindowHideEventArgs args)
        {
            if (PostDocumentWindowHide != null)
            {
                PostDocumentWindowHide(this, args);
            }
        }

        internal void TriggerPostSolutionOpen(PostSolutionOpenEventArgs args)
        {
            if (PostSolutionOpen != null)
            {
                PostSolutionOpen(this, args);
            }
        }

        internal void TriggerQuerySolutionClose(QuerySolutionCloseEventArgs args)
        {
            if (QuerySolutionClose != null)
            {
                QuerySolutionClose(this, args);
            }
        }

        internal void TriggerPreSolutionClose(PreSolutionCloseEventArgs args)
        {
            if (PreSolutionClose != null)
            {
                PreSolutionClose(this, args);
            }
        }

        internal void TriggerPostSolutionClose(PostSolutionCloseEventArgs args)
        {
            if (PostSolutionClose != null)
            {
                PostSolutionClose(this, args);
            }
        }

        internal void TriggerPostProjectOpen(PostProjectOpenEventArgs args)
        {
            if (PostProjectOpen != null)
            {
                PostProjectOpen(this, args);
            }
        }

        internal void TriggerQueryProjectClose(QueryProjectCloseEventArgs args)
        {
            if (QueryProjectClose != null)
            {
                QueryProjectClose(this, args);
            }
        }

        public void TriggerPreProjectClose(PreProjectCloseEventArgs args)
        {
            if (PreProjectClose != null)
            {
                PreProjectClose(this, args);
            }
        }

        public void TriggerQueryProjectUnload(QueryProjectUnloadEventArgs args)
        {
            if (QueryProjectUnload != null)
            {
                QueryProjectUnload(this, args);
            }
        }

        public void TriggerPreProjectUnload(PreProjectUnloadEventArgs args)
        {
            if (PreProjectUnload != null)
            {
                PreProjectUnload(this, args);
            }
        }

        public void TriggerPostProjectLoad(PostProjectLoadEventArgs args)
        {
            if (PostProjectLoad != null)
            {
                PostProjectLoad(this, args);
            }
        }

        public void TriggerQueryProjectAddFiles(QueryProjectAddFilesEventArgs args)
        {
            if (QueryProjectAddFiles != null)
            {
                QueryProjectAddFiles(this, args);
            }
        }

        public void TriggerPostProjectAddFiles(PostProjectAddFilesEventArgs args)
        {
            if (PostProjectAddFiles != null)
            {
                PostProjectAddFiles(this, args);
            }
        }

        public void TriggerPostProjectAddDirectories(PostProjectAddDirectoriesEventArgs args)
        {
            if (PostProjectAddDirectories != null)
            {
                PostProjectAddDirectories(this, args);
            }
        }

        public void TriggerPostProjectRemoveFiles(PostProjectRemoveFilesEventArgs args)
        {
            if (PostProjectRemoveFiles != null)
            {
                PostProjectRemoveFiles(this, args);
            }
        }

        public void TriggerPostProjectRemoveDirectories(PostProjectRemoveDirectoriesEventArgs args)
        {
            if (PostProjectRemoveDirectories != null)
            {
                PostProjectRemoveDirectories(this, args);
            }
        }
    }
}