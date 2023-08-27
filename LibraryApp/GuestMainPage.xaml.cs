using LibraryApp;
using Logic;
using System.Windows;
using System.Windows.Controls;

namespace UI
{
    /// <summary>
    /// Interaction logic for GuestMainPage.xaml
    /// </summary>
    public partial class GuestMainPage : Page
    {
        public GuestMainPage(LibraryManager libraryManager)
        {
            InitializeComponent();
            basicMainPage.DataContext = libraryManager;
        }

        private void BackToLoginButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            mainWindow.NavigateToLoginPage();
        }
    }
}
