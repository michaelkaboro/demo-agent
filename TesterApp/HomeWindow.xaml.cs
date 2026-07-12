using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Linq;

namespace TesterApp;

public partial class HomeWindow : Window
{
    private static readonly HttpClient HttpClient = new();

    public HomeWindow()
    {
        InitializeComponent();
    }

    private async void HomeWindow_Loaded(object sender, RoutedEventArgs e)
    {
        await LoadKenyaInfoAsync();
    }

    private async Task LoadKenyaInfoAsync()
    {
        try
        {
            const string url = "https://en.wikipedia.org/w/api.php?action=query&format=json&prop=extracts&explaintext=1&titles=Kenya";
            using var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            await using var stream = await response.Content.ReadAsStreamAsync();
            using var document = await JsonDocument.ParseAsync(stream);
            var extract = document.RootElement
                .GetProperty("query")
                .GetProperty("pages")
                .EnumerateObject()
                .Select(page => page.Value)
                .FirstOrDefault(page => page.TryGetProperty("extract", out _))
                .GetProperty("extract")
                .GetString();

            KenyaInfoTextBlock.Text = string.IsNullOrWhiteSpace(extract)
                ? "No Kenya information was returned from Wikipedia."
                : extract;
            StatusTextBlock.Text = "Showing Kenya information from Wikipedia.";
        }
        catch
        {
            KenyaInfoTextBlock.Text =
                "Unable to load Kenya information from Wikipedia right now.\n\n" +
                "Kenya is an East African country known for its diverse landscapes, Indian Ocean coastline, national parks, and rich cultural heritage. " +
                "Its capital city is Nairobi, while Swahili and English are the official languages. " +
                "Kenya is home to famous wildlife reserves such as the Maasai Mara, Mount Kenya, the Great Rift Valley, and thriving agricultural, tourism, and technology sectors.\n\n" +
                "Please try again later when an internet connection is available.";
            StatusTextBlock.Text = "Wikipedia could not be reached, so a local summary is being shown.";
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
}
