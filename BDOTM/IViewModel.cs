using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace BDOTM
{
    public interface IViewModel : INotifyPropertyChanged
    {
        ObservableCollection<TemplateItem> Templates { get; set; }

        ICommand OpenFolderCmd { get; }
    }
}