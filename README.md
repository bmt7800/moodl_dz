# FinFlow — учет счетов и платежей

Это приложение написано на **C#**: WPF-проект для Visual Studio 2022 на .NET 8. Оно включает экран авторизации, главную панель со счетами и подключение к Microsoft SQL Server через Entity Framework Core.

## Запуск

1. Откройте решение `InvoiceLedger.sln` в Visual Studio 2022. В решении находится C#-проект `InvoiceLedger.csproj`.
2. Установите .NET 8 SDK и SQL Server LocalDB (или измените `ConnectionStrings:InvoiceLedger` в `appsettings.json` на адрес вашего SQL Server).
3. Откройте SQL Server Management Studio, подключитесь к своему серверу и выполните скрипт `Database/InvoiceLedger.sql`. Он создаст базу `InvoiceLedgerDb`, таблицы `Users` и `Invoices`, а также демо-пользователя.
4. Нажмите **F5**. Если скрипт не запускать, приложение самостоятельно создаст БД при первой попытке входа через настроенное подключение.

**Демо-учетная запись:** `admin@finflow.local` / `Admin123!`.

> Для production вместо `EnsureCreated` используйте миграции EF Core, а пароли храните через ASP.NET Core Identity или PBKDF2/Argon2 с уникальной солью.
