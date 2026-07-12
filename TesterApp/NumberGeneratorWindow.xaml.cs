using System.Windows;

namespace TesterApp;

public partial class NumberGeneratorWindow : Window
{
    public NumberGeneratorWindow()
    {
        InitializeComponent();
    }

    private void GenerateButton_Click(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(CountTextBox.Text, out var count) || count <= 0)
        {
            MessageBox.Show("Enter a whole number greater than zero.", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        var digits = Enumerable.Range(0, count)
            .Select(_ => Random.Shared.Next(0, 10).ToString());

        OutputTextBox.Text = string.Join(Environment.NewLine, digits);
    }
}
