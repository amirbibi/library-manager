using System.Windows;

namespace UI
{
    /// <summary>
    /// Interaction logic for ReturnItemDialog.xaml
    /// </summary>
    public partial class ReturnItemDialog : Window
    {
        public string? ItemNameToRent { get; private set; }

        public ReturnItemDialog()
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
