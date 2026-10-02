using System.Collections.ObjectModel;
using System.Windows;
using InvoiceLedger.Models;

namespace InvoiceLedger;

public partial class DashboardWindow : Window
{
    public ObservableCollection<Invoice> Invoices { get; } = new()
    {
        new() { Number = "СЧ-1024", Counterparty = "ООО «Альфа»", DueDate = DateTime.Today.AddDays(3), Amount = 85000, Status = "Ожидает оплаты" },
        new() { Number = "СЧ-1023", Counterparty = "ИП Смирнов А. В.", DueDate = DateTime.Today.AddDays(-2), Amount = 42800, Status = "Просрочен" },
        new() { Number = "СЧ-1022", Counterparty = "АО «Гамма»", DueDate = DateTime.Today.AddDays(-8), Amount = 71200, Status = "Оплачен" }
    };

    public DashboardWindow(string fullName)
    {
        InitializeComponent();
        WelcomeText.Text = $"Здравствуйте, {fullName}. Вот что происходит сегодня.";
        DataContext = this;
    }
}
