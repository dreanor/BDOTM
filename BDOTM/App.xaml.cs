using System.Windows;

namespace BDOTM
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            IViewModel viewModel = new ViewModel();
            View view = new View(viewModel);
            view.Show();
        }
    }
}
