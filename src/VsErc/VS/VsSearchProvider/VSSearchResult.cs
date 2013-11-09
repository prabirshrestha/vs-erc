using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using Microsoft.Internal.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell.Interop;
using NLua;

namespace PrabirShrestha.VsErc.VS.VsSearchProvider
{
    public class VSSearchResult : IVsSearchItemResult
    {
        VSSearchableItem Item;
        IVsSearchProvider Provider;

        private LuaFunction executeCommandLuaFunction;

        private LuaFunction ExecuteCommandLuaFunction
        {
            get
            {
                if (executeCommandLuaFunction == null)
                {
                    executeCommandLuaFunction = VsErcPackage.Instance.Lua.GetFunction("erc.commands.execute");
                }
                return executeCommandLuaFunction;
            }
        }

        public VSSearchResult(VSSearchableItem item, IVsSearchProvider provider)
        {
            this.Item = item;
            this.Provider = provider;
        }

        public IDataObject DataObject
        {
            get { return null; }
        }

        public string Description
        {
            get { return this.Item.Description; }
        }

        public string DisplayText
        {
            get { return this.Item.Name; }
        }

        public IVsUIObject Icon
        {
            get
            {
                if (this.Item.Icon == null)
                    return null;

                // If all items returned from the search provider use the same icon, consider using a static member variable 
                // (e.g. on the search provider class) to initialize and return the IVsUIObject - it will save time and memory 
                // creating these objects.

                // Helper classses in Microsoft.Internal.VisualStudio.PlatformUI can be used to construct IVsUIObject of VsUIType.Icon
                // Use Win32IconUIObject if you have a HICON, use WinFormsIconUIObject if you have a System.Drawing.Icon, or
                // use WpfPropertyValue.CreateIconObject() if you have a WPF ImageSource.
                return new WinFormsIconUIObject(this.Item.Icon);
            }
        }


        public void InvokeAction()
        {
            // This function is called when the user selects the item result from the Quick Launch popup
            try
            {
                ExecuteCommandLuaFunction.Call(this.Item.Name);
            }
            catch (Exception ex)
            {
                VsErcPackage.Instance.Logger.Log(ex.Message);
                VsErcPackage.Instance.Logger.Log(ex.StackTrace);
            }
        }

        public string PersistenceData
        {
            get
            {
                // For the fruits and vegetables search providers it suffice to store the item name to reconstruct the item
                // because when IVsSearchProvider.CreateItemResult is called to re-create the item we can look it up in the 
                // fruits/vegetables lists and find it by name. Other real-life search providers will probably need to store 
                // enough relevant data so the item can be recreated.
                return this.Item.Name;
            }
        }

        public IVsSearchProvider SearchProvider
        {
            get { return this.Provider; }
        }

        public string Tooltip
        {
            get { return null; }
        }

        public static IVsSearchItemResult CreateItemResult(string lpszPersistenceData, IList<VSSearchableItem> items, IVsSearchProvider provider)
        {
            foreach (var item in items)
            {
                // Try to match the name, that we reported as persistence string
                if (item.Name.Equals(lpszPersistenceData, StringComparison.Ordinal))
                {
                    // Create a new item. The item creation must be a fast operation (e.g. should not make network requests)
                    return new VSSearchResult(item, provider);
                }
            }

            // We got called with an item that we cannot recreate, return null
            return null;
        }
    }
}