using System.Windows;
using CrossCutting.Core.Logging;
using CrossCutting.MainModule.IOC;

namespace Soulstone.Player
{
    public partial class App : Application
    {
        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            IocUnityContainer.Instance.Resolve<ILogManager>().DefaultLogger.Error.Write("Unhandled Error", e.Exception);
            ErrorWindow errorWindow = new ErrorWindow(e.Exception);
            try
            {
                errorWindow.Owner = Application.Current.MainWindow;
            }
            catch
            {
                errorWindow.Owner = null;
            }

            errorWindow.ShowDialog();
            e.Handled = true;
        }
    }
}