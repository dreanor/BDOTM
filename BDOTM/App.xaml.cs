using System.Reflection;
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
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            view.Title = string.Format("Black Desert Online Template Manager v{0}.{1}.{2}", version.Major, version.Minor, version.Revision);
            view.Show();
        }
    }
}
