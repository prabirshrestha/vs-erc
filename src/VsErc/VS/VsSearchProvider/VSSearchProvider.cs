﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;

namespace PrabirShrestha.VsErc.VS.VsSearchProvider
{
    [Guid(GuidList.guidVsErcSearchProvider)]
    public class VSSearchProvider : IVsSearchProvider
    {
        // Defines all string variables like Description(Hover over Search Heading), Search Heading text, Category Shortcut
        private const string DescriptionString = "search through the vserc plugins";
        private const string DisplayTextString = "VsErc";
        private const string CategoryShortcutString = "erc";

        private IList<VSSearchableItem> searchableItems;

        public VSSearchProvider()
        {
            searchableItems = new List<VSSearchableItem>();
            searchableItems.Add(new VSSearchableItem("JSON Encode", "JSON Encode string", null));
            searchableItems.Add(new VSSearchableItem("JSON Decode", "JSON Decode string", null));
            searchableItems.Add(new VSSearchableItem("Url Encode", "Url Encode string", null));
            searchableItems.Add(new VSSearchableItem("Url Decode", "Url Decode string", null));
            searchableItems.Add(new VSSearchableItem("Html Encode", "Html Encode string", null));
            searchableItems.Add(new VSSearchableItem("Html Decode", "Html Decode string", null));
        }

        public Guid Category
        {
            get { return GetType().GUID; }
        }

        //Main Search method that calls MSDNSearchTask to create and execute search query
        public IVsSearchTask CreateSearch(uint dwCookie, IVsSearchQuery pSearchQuery, IVsSearchProviderCallback pSearchCallback)
        {
            return new VSSearchTask(this.searchableItems, this, dwCookie, pSearchQuery, pSearchCallback);
   
        }

        //Verifies persistent data to populate MRU list with previously selected result
        public IVsSearchItemResult CreateItemResult(string lpszPersistenceData)
        {
            return VSSearchResult.CreateItemResult(lpszPersistenceData, this.searchableItems, this);
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