using System.Windows;
using InvoiceLedger.Data;

namespace InvoiceLedger;

public partial class MainWindow : Window
{
    private readonly AuthenticationService _authentication;

    public MainWindow()
    {
        InitializeComponent();
        _authentication = new AuthenticationService();
    }

    private async void Login_Click(object sender, RoutedEventArgs e)
    {
        MessageBorder.Visibility = Visibility.Collapsed;
        if (string.IsNullOrWhiteSpace(EmailTextBox.Text) || string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            ShowError("Укажите электронную почту и пароль.");
            return;
        }

        try
        {
            var user = await _authentication.SignInAsync(EmailTextBox.Text.Trim(), PasswordBox.Password);
            if (user is null)
            {
                ShowError("Неверная почта или пароль.");
                return;
            }

            new DashboardWindow(user.FullName).Show();
            Close();
        }
        catch (Exception)
        {
            ShowError("Не удалось подключиться к SQL Server. Проверьте строку подключения в appsettings.json.");
        }
    }

    private void ForgotPassword_Click(object sender, RoutedEventArgs e) =>
        MessageBox.Show("Обратитесь к администратору для восстановления доступа.", "Восстановление пароля");

    private void ShowError(string text)
    {
        MessageText.Text = text;
        MessageBorder.Visibility = Visibility.Visible;
    }
}
