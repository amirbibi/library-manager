using Logic;
using Logic.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UI
{
    /// <summary>
    /// Interaction logic for BasicMainPage.xaml
    /// </summary>
    public partial class BasicMainPage : UserControl
    {
        private LibraryManager? _libraryManager;

        public BasicMainPage()
        {
            InitializeComponent();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == DataContextProperty)
            {
                _libraryManager = (LibraryManager)DataContext;

                if (_libraryManager != null)
                {
                    itemDataGrid.ItemsSource = _libraryManager.GetAllItems();
                    libraryCount.DataContext = _libraryManager;
                    rentedItemsCount.DataContext = _libraryManager;
                }
            }
        }


        public ObservableCollection<Item>? FilterItems()
        {
            if (_libraryManager != null)
                return new ObservableCollection<Item>(_libraryManager.FilterItems(type.Text, name.Text, genre.Text, author.Text, publisher.Text));

            return _libraryManager != null ? new ObservableCollection<Item>(_libraryManager.GetAllItems()) : null;
        }


        private void Type_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemDataGrid != null && _libraryManager != null)
                itemDataGrid.ItemsSource = FilterItems();
        }

        private void Type_GotFocus(object sender, RoutedEventArgs e)
        {
            if (type.Text == "Type")
                type.Text = "";
        }

        private void Type_LostFocus(object sender, RoutedEventArgs e)
        {
            if (type.Text == "")
                type.Text = "Type";
        }

        private void Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemDataGrid != null)
                itemDataGrid.ItemsSource = FilterItems();
        }

        private void Name_GotFocus(object sender, RoutedEventArgs e)
        {
            if (name.Text == "Name")
                name.Text = "";
        }

        private void Name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (name.Text == "")
                name.Text = "Name";
        }

        private void Genre_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemDataGrid != null)
                itemDataGrid.ItemsSource = FilterItems();
        }

        private void Genre_GotFocus(object sender, RoutedEventArgs e)
        {
            if (genre.Text == "Genre")
                genre.Text = "";
        }

        private void Genre_LostFocus(object sender, RoutedEventArgs e)
        {
            if (genre.Text == "")
                genre.Text = "Genre";
        }

        private void Author_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemDataGrid != null)
                itemDataGrid.ItemsSource = FilterItems();
        }

        private void Author_GotFocus(object sender, RoutedEventArgs e)
        {
            if (author.Text == "Author")
                author.Text = "";
        }

        private void Author_LostFocus(object sender, RoutedEventArgs e)
        {
            if (author.Text == "")
                author.Text = "Author";
        }

        private void Publisher_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (itemDataGrid != null)
                itemDataGrid.ItemsSource = FilterItems();
        }

        private void Publisher_GotFocus(object sender, RoutedEventArgs e)
        {
            if (publisher.Text == "Publisher")
                publisher.Text = "";
        }

        private void Publisher_LostFocus(object sender, RoutedEventArgs e)
        {
            if (publisher.Text == "")
                publisher.Text = "Publisher";
        }

        private void ItemDataGrid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (_libraryManager != null)
                libraryCount.Text = _libraryManager.Count.ToString();
        }
        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            itemDataGrid.UnselectAll();
        }
    }
}
