﻿using EnvDTE;

namespace VSP.Events
{
    public class PostSolutionCloseEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public PostSolutionCloseEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath { get; set; }

        public Solution Solution { get; set; }

    }
}