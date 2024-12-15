using System.Windows;

namespace SegmentFilter;


public class App(MainWindow mainWindow) : Application
{
    private readonly MainWindow mainWindow = mainWindow;

    protected override void OnStartup(StartupEventArgs e)
    {
        mainWindow.Show();

        base.OnStartup(e);
    }
}
