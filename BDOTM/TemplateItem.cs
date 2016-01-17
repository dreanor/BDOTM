using helper.mvvm.baseclasses;

namespace BDOTM
{
    public class TemplateItem : ViewModelBase<ITemplateItem>, ITemplateItem
    {
        public string Name
        {
            get { return this.Get(x => x.Name); }
            set { this.Set(x => x.Name, value); }
        }
        public string Description
        {
            get { return this.Get(x => x.Description); }
            set { this.Set(x => x.Description, value); }
        }
        public string Path
        {
            get { return this.Get(x => x.Path); }
            set { this.Set(x => x.Path, value); }
        }
        public bool IsActive
        {
            get { return this.Get(x => x.IsActive); }
            set { this.Set(x => x.IsActive, value); }
        }
        public TemplateItem(string name, string description, string path)
        {
            Name = name;
            Description = description;
            Path = path;
        }
    }
}
