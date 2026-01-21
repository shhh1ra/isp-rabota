using AutoSalon.Desktop.Data;
using AutoSalon.Desktop.Data.Models;
using AutoSalon.Desktop.Ui;
using System.Windows;

namespace AutoSalon.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly string _role;

        private List<Manufacturer> _manufacturers = new();

        public MainWindow(string role)
        {
            InitializeComponent();
            _role = role;

            ApplyRolePermissions();
            LoadManufacturers();
        }

        private void ApplyRolePermissions()
        {
            var canEdit = _role == "Admin";

            BtnAddMan.IsEnabled = canEdit;
            BtnEditMan.IsEnabled = canEdit;
            BtnDelMan.IsEnabled = canEdit;
        }

        private void LoadManufacturers()
        {
            using var db = new AutoSalonContext();
            _manufacturers = db.Manufacturers
                .OrderBy(m => m.Name)
                .ToList();

            ApplyManufacturerFilter();
        }

        private void ApplyManufacturerFilter()
        {
            var text = ManSearchBox.Text.Trim().ToLower();

            var filtered = string.IsNullOrWhiteSpace(text)
                ? _manufacturers
                : _manufacturers.Where(m =>
                        (m.Name ?? "").ToLower().Contains(text) ||
                        (m.Country ?? "").ToLower().Contains(text))
                    .ToList();

            ManGrid.ItemsSource = filtered;

            ManCountText.Text = $"Всего: {_manufacturers.Count} | Найдено: {filtered.Count}";
            ManEmptyText.Text = filtered.Count == 0 ? "Нет результатов поиска" : "";
        }

        private Manufacturer? SelectedManufacturer()
            => ManGrid.SelectedItem as Manufacturer;

        private void ManufacturerSearch_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
            => ApplyManufacturerFilter();

        private void AddManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var wnd = new ManufacturerEditWindow();
            if (wnd.ShowDialog() != true)
                return;

            using var db = new AutoSalonContext();

            // 1️⃣ Проверка ДО сохранения
            bool exists = db.Manufacturers
                .Any(m => m.Name.ToLower() == wnd.Result.Name.ToLower());

            if (exists)
            {
                MessageBox.Show(
                    "Производитель с таким названием уже существует.",
                    "Ошибка добавления",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                // 2️⃣ Попытка сохранить
                db.Manufacturers.Add(wnd.Result);
                db.SaveChanges();

                LoadManufacturers();
            }
            catch (Microsoft.Data.SqlClient.SqlException ex)
            {
                // 3️⃣ Обработка SQL-ошибок (на всякий случай)
                MessageBox.Show(
                    "Ошибка при сохранении данных.\n" +
                    "Возможно, производитель с таким названием уже существует.",
                    "Ошибка базы данных",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // 4️⃣ Общая защита
                MessageBox.Show(
                    "Неизвестная ошибка:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void EditManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var sel = SelectedManufacturer();   // ← ВОТ ЭТОГО не хватало

            if (sel is null)
            {
                MessageBox.Show("Выберите производителя.");
                return;
            }

            var wnd = new ManufacturerEditWindow(new Manufacturer
            {
                ManufacturerId = sel.ManufacturerId,
                Name = sel.Name,
                Country = sel.Country,
                Website = sel.Website
            });

            if (wnd.ShowDialog() != true)
                return;

            using var db = new AutoSalonContext();

            // проверка на дубликат, КРОМЕ самого себя
            bool exists = db.Manufacturers.Any(m =>
                m.ManufacturerId != sel.ManufacturerId &&
                m.Name.ToLower() == wnd.Result.Name.ToLower());

            if (exists)
            {
                MessageBox.Show(
                    "Производитель с таким названием уже существует.",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                var entity = db.Manufacturers.First(m => m.ManufacturerId == sel.ManufacturerId);
                entity.Name = wnd.Result.Name;
                entity.Country = wnd.Result.Country;
                entity.Website = wnd.Result.Website;

                db.SaveChanges();
                LoadManufacturers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ошибка:\n" + ex.Message,
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }


        private void DeleteManufacturer_Click(object sender, RoutedEventArgs e)
        {
            var sel = SelectedManufacturer();
            if (sel == null)
            {
                MessageBox.Show("Выберите производителя.");
                return;
            }

            using var db = new AutoSalonContext();

            // проверка ссылок (по заданию)
            bool hasBrands = db.Brands.Any(b => b.ManufacturerId == sel.ManufacturerId);
            if (hasBrands)
            {
                MessageBox.Show("Удаление невозможно: у производителя есть связанные марки.");
                return;
            }

            var entity = db.Manufacturers.First(m => m.ManufacturerId == sel.ManufacturerId);
            db.Manufacturers.Remove(entity);
            db.SaveChanges();

            LoadManufacturers();
        }
    }
}
