﻿using EnvDTE;

namespace VSP.Events
{
    public class QuerySolutionCloseEventArgs
    {
        private readonly VsEvents events;

        public VsEvents Events
        {
            get
            {
                return this.events;
            }
        }

        public QuerySolutionCloseEventArgs(VsEvents vsEvents)
        {
            this.events = vsEvents;
        }

        public string FilePath
        {
            get
            {
                return Solution.FullName;
            }
        }

        public bool CloseSolution { get; set; }

        public Solution Solution { get; set; }

    }
}