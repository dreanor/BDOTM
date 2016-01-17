using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BDOTM
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<TemplateItem> Templates { get; set; }
    }
}