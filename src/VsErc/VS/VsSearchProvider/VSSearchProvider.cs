using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using NLua;

namespace PrabirShrestha.VsErc.VS.VsSearchProvider
{
    [Guid(GuidList.guidVsErcSearchProvider)]
    public class VSSearchProvider : IVsSearchProvider
    {
        // Defines all string variables like Description(Hover over Search Heading), Search Heading text, Category Shortcut
        private const string DescriptionString = "search through the vserc plugins";
        private const string DisplayTextString = "VsErc";
        private const string CategoryShortcutString = "erc";

        private IList<VSSearchableItem> SearchableItems
        {
            get
            {
                IList<VSSearchableItem> searchableItems = new List<VSSearchableItem>();
                var commands = (LuaTable)VsErcPackage.Instance.Lua.GetFunction("erc.commands.getall").Call()[0];
                foreach (LuaTable command in commands.Values)
                {
                    var item = new VSSearchableItem((string)command["name"], (string)command["description"], null);
                    searchableItems.Add(item);
                }
                return searchableItems;
            }
        }

        public Guid Category
        {
            get { return GetType().GUID; }
        }

        //Main Search method that calls MSDNSearchTask to create and execute search query
        public IVsSearchTask CreateSearch(uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchProviderCallback pSearchCallback)
        {
            return new VSSearchTask(this.SearchableItems, this, dwCookie, pSearchQuery, pSearchCallback);

        }

        //Verifies persistent data to populate MRU list with previously selected result
        public IVsSearchItemResult CreateItemResult(string lpszPersistenceData)
        {
            return VSSearchResult.CreateItemResult(lpszPersistenceData, this.SearchableItems, this);
        }

        //MSDN Search Category Heading 
        public string DisplayText
        {
            get { return DisplayTextString; }
        }

        //MSDN Search Description - shows as tooltip on hover over Search Category Heading
        public string Description
        {
            get
            {
                return DescriptionString;
            }
        }

        //
        public void ProvideSearchSettings(IVsUIDataSource pSearchOptions)
        {
        }

        //MSDN Category shortcut to scope results to to show only from MSDN Library
        public string Shortcut
        {
            get
            {
                return CategoryShortcutString;
            }
        }

        public string Tooltip
        {
            get { return null; } //no additional tooltip
        }

        public IVsUIObject Icon
        {
            get
            {
                return null;
                //var image = BitmapFrame.Create(new Uri("pack://application:,,,/WebEssentials2012;component/Resources/vsgallery.png", UriKind.RelativeOrAbsolute));
                //return WpfPropertyValue.CreateIconObject(image);
            }
        }
    }
}