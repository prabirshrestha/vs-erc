using System.Drawing;

namespace PrabirShrestha.VsErc.VS.VsSearchProvider
{
    public class VSSearchableItem
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public Icon Icon { get; private set; }

        public VSSearchableItem(string name, string description, Icon icon)
        {
            Name = name;
            Description = description;
            Icon = icon;
        } 
    }
}