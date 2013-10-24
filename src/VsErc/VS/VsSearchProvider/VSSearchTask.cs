using System;
using System.Collections.Generic;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;

namespace PrabirShrestha.VsErc.VS.VsSearchProvider
{
    public class VSSearchTask : VsSearchTask
    {
        private readonly VSSearchProvider provider;
        private readonly IList<VSSearchableItem> searchableItems; 

        public VSSearchTask(IList<VSSearchableItem> searchableItems, VSSearchProvider provider, uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchProviderCallback pSearchCallback)
            : base(dwCookie, pSearchQuery, pSearchCallback)
        {
            this.provider = provider;
            this.searchableItems = searchableItems;
        }

        protected override void OnStartSearch()
        {
            // Get the tokens count in the query
            uint tokenCount = this.SearchQuery.GetTokens(0, null);

            // Get the tokens
            IVsSearchToken[] tokens = new IVsSearchToken[tokenCount];
            this.SearchQuery.GetTokens(tokenCount, tokens);

            for (int itemIndex = 0; itemIndex < this.searchableItems.Count; itemIndex++)
            {
                var item = this.searchableItems[itemIndex];

                // Check if the search was canceled
                if (this.TaskStatus == VSConstants.VsSearchTaskStatus.Stopped)
                {
                    // The completion was already notified by the base.OnStopSearch, there is nothing else to do
                    return;
                }

                // Check if the item matches the current query
                if (Matches(item, tokens))
                {
                    // Create and report new result
                    IVsSearchProviderCallback providerCallback = (IVsSearchProviderCallback)this.SearchCallback;
                    providerCallback.ReportResult(this, new VSSearchResult(item, this.provider));

                    // Keep track of how many results we have found, and the base class will use this number when calling the callback to report completion
                    this.SearchResults++;
                }

                // Since we know how many items we have, we can report progress
                this.SearchCallback.ReportProgress(this, (uint)(itemIndex + 1), (uint)this.searchableItems.Count);
            }

            // Now call the base class - it will set the task status to complete and will callback to report search complete
            base.OnStartSearch();
        }

        protected new IVsSearchProviderCallback SearchCallback
        {
            get
            {
                return (IVsSearchProviderCallback)base.SearchCallback;
            }
        }

        /// <summary>
        /// Checks whether an item matches the search query
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Matches(VSSearchableItem item, IVsSearchToken[] tokens)
        {
            foreach (IVsSearchToken token in tokens)
            {
                bool tokenMatches = false;

                // We'll search description and name
                if (item.Name.IndexOf(token.ParsedTokenText, StringComparison.CurrentCultureIgnoreCase) != -1)
                    tokenMatches = true;

                if (item.Description != null && item.Description.IndexOf(token.ParsedTokenText, StringComparison.CurrentCultureIgnoreCase) != -1)
                    tokenMatches = true;

                if (!tokenMatches)
                    return false;
            }

            return true;
        }
    }
}