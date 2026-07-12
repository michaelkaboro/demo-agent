using System.Windows;

namespace TesterApp;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OpenHomeScreen_Click(object sender, RoutedEventArgs e)
    {
        new HomeWindow().Show();
    }

    private void OpenNumberGeneratorScreen_Click(object sender, RoutedEventArgs e)
    {
        new NumberGeneratorWindow().Show();
    }

    private void OpenGoodbyeScreen_Click(object sender, RoutedEventArgs e)
    {
        new GoodbyeWindow().Show();
    }
}