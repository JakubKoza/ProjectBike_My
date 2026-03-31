using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectBike.Abstractions;
using ProjectBike.DataAccess.Memory;
using ProjectBike.DataAccess.Memory.Repositories;
using ProjectBike.Desktop.Services;
using ProjectBike.Desktop.ViewModels.Bikes;
using ProjectBike.Desktop.ViewModels.Clients;
using ProjectBike.Desktop.ViewModels.Rentals;
using ProjectBike.Desktop.Windows;
using ProjectBike.ServiceAbstractions;
using ProjectBike.Services;
using System;
using System.Windows;

namespace ProjectBike.Desktop
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // 1. Baza Danych i UnitOfWork (Singleton, bo trzymamy dane w RAM)
                    services.AddSingleton<MemoryDbContext>();
                    services.AddSingleton<IUnitOfWork, UnitOfWorkMemory>();

                    // 2. Repozytoria
                    services.AddSingleton<IBikeRepository, BikeRepositoryMemory>();
                    services.AddSingleton<IClientRepository, ClientRepositoryMemory>();
                    services.AddSingleton<IRentalRepository, RentalRepositoryMemory>();
                    services.AddSingleton<IEmployeesRepository, EmployeesRepositoryMemory>();

                    // 3. Serwisy Logiki Biznesowej
                    services.AddSingleton<IBikeService, BikeService>();
                    services.AddSingleton<IClientService, ClientService>();
                    services.AddSingleton<IRentalService, RentalService>();
                    services.AddSingleton<IEmployeeService, EmployeeService>();
                    services.AddSingleton<IDataSeeder, DataSeeder>();

                    // 4. Serwisy UI (Dialogi i Powiadomienia)
                    services.AddSingleton<INotificationService, NotificationService>();
                    services.AddSingleton<IClientDialogService, ClientDialogService>();
                    services.AddSingleton<IBikeDialogService, BikeDialogService>();
                    services.AddSingleton<IBikesWindowService, BikesWindowService>();
                    services.AddSingleton<IManageRentalWindowService, ManageRentalWindowService>();

                    // 5. Fabryki Okien (rozwiązanie dla Twoich Func<Window>)
                    services.AddSingleton<Func<AddClientWindow>>(sp => () => sp.GetRequiredService<AddClientWindow>());
                    services.AddSingleton<Func<EditClientWindow>>(sp => () => sp.GetRequiredService<EditClientWindow>());
                    services.AddSingleton<Func<AddBikeWindow>>(sp => () => sp.GetRequiredService<AddBikeWindow>());
                    services.AddSingleton<Func<EditBikeWindow>>(sp => () => sp.GetRequiredService<EditBikeWindow>());
                    services.AddSingleton<Func<BikesWindow>>(sp => () => sp.GetRequiredService<BikesWindow>());
                    services.AddSingleton<Func<ManageRentalWindow>>(sp => () => sp.GetRequiredService<ManageRentalWindow>());

                    // 6. ViewModele
                    services.AddTransient<MainViewModel>();
                    services.AddTransient<AddClientViewModel>();
                    services.AddTransient<EditClientViewModel>();
                    services.AddTransient<AddBikeViewModel>();
                    services.AddTransient<EditBikeViewModel>();
                    services.AddTransient<BikesViewModel>();
                    services.AddTransient<ManageRentalViewModel>();

                    // 7. Okna widoków
                    services.AddTransient<FinalMainWindow>();
                    services.AddTransient<AddClientWindow>();
                    services.AddTransient<EditClientWindow>();
                    services.AddTransient<AddBikeWindow>();
                    services.AddTransient<EditBikeWindow>();
                    services.AddTransient<BikesWindow>();
                    services.AddTransient<ManageRentalWindow>();
                })
                .Build();
        }
        protected override async void OnStartup(StartupEventArgs e)
        {
            await _host.StartAsync();

            // Odpalenie Seedera, aby załadować startowe dane do pamięci RAM
            var seeder = _host.Services.GetRequiredService<IDataSeeder>();
            seeder.Seed();

            // Uruchomienie Głównego Okna
            var startmainWindow = _host.Services.GetRequiredService<FinalMainWindow>();
            startmainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await _host.StopAsync();
            _host.Dispose();
            base.OnExit(e);
        }
    }
}

