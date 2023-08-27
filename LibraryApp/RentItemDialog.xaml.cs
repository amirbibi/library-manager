using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for RentItemDialog.xaml
    /// </summary>
    public partial class RentItemDialog : Window
    {
        public string? ItemNameToRent { get; private set; }

        public RentItemDialog()
        {
            InitializeComponent();
            okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ItemNameToRent = inputTextBox.Text;
            DialogResult = true;
        }
    }

}
