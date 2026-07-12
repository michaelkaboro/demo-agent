using System.Windows;
using System.Linq;

namespace TesterApp;

public partial class GoodbyeWindow : Window
{
    public GoodbyeWindow()
    {
        InitializeComponent();
    }

    private void BackToMainButton_Click(object sender, RoutedEventArgs e)
    {
        var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        if (mainWindow is null)
        {
            mainWindow = new MainWindow();
            mainWindow.Show();
        }
        else
        {
            mainWindow.Activate();
        }

        Close();
    }
}
