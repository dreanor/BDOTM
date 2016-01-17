using System.ComponentModel;

namespace BDOTM
{
    public interface ITemplateItem : INotifyPropertyChanged
    {
        string Name { get; set; }

        string Description { get; set; }

        string Path { get; set; }

        bool IsActive { get; set; }
    }
}