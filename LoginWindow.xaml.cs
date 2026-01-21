using AutoSalon.Desktop.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;
using WpfApp1;

namespace AutoSalon.Desktop
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            // на всякий: создать БД, если её нет
            using var db = new AutoSalonContext();
            db.Database.Migrate();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            var login = LoginBox.Text.Trim();
            var pass = PassBox.Password;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Введите логин и пароль.");
                return;
            }

            using var db = new AutoSalonContext();
            var user = db.Users.Include(u => u.Role)
                .FirstOrDefault(u => u.Login == login && u.IsActive);

            if (user == null)
            {
                MessageBox.Show("Пользователь не найден или отключён.");
                return;
            }

            var hash = PasswordUtil.Hash(pass);
            if (user.PasswordHash != hash)
            {
                MessageBox.Show("Неверный пароль.");
                return;
            }

            var main = new MainWindow(user.Role.Name);
            main.Show();
            Close();
        }
    }
}
