using AutoSalon.Desktop.Data.Models;
using System.Windows;

namespace AutoSalon.Desktop.Ui
{
    public partial class ManufacturerEditWindow : Window
    {
        public Manufacturer Result { get; private set; }

        public ManufacturerEditWindow()
        {
            InitializeComponent();
            Result = new Manufacturer();
        }

        public ManufacturerEditWindow(Manufacturer existing)
        {
            InitializeComponent();
            Result = existing;

            NameBox.Text = existing.Name;
            CountryBox.Text = existing.Country;
            WebsiteBox.Text = existing.Website;
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            var name = NameBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название обязательно.");
                return;
            }

            Result.Name = name;
            Result.Country = string.IsNullOrWhiteSpace(CountryBox.Text) ? null : CountryBox.Text.Trim();
            Result.Website = string.IsNullOrWhiteSpace(WebsiteBox.Text) ? null : WebsiteBox.Text.Trim();

            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
