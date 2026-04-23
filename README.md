
![Logo](https://cdn.discordapp.com/attachments/926219187037302835/1496838360059351120/logoprojekt-Photoroom.png?ex=69eb56da&is=69ea055a&hm=13cd742a31d55bb40d20f058dd19170abd3feb846616bfb5cd7a7fbee315ad7f)

##
> 🚧 **UWAGA:** Projekt jest obecnie w fazie aktywnego rozwoju. 
> Część funkcjonalności (np. moduł wypożyczeń) może być niestabilna.
>**Interfejs w trakcie tworzenia, aktualnie działa tylko opcja konsoli.** Do zaimplementowania:
>* skończenie WPF
>* dodanie testów (xUnit)
>* zaimplementowanie Bazy Danych
>* Dodanie systemu Logów

## 
**Aplikacja desktopowa** do zarządzania bazą klienów i wypożyczeniami stworzona obiektowo w języku **C#** i frameworku **.NET**. Aplikacja posiada dwa niezależne interfejsy użytkownika (konsolowy oraz WPF), które korzystają z tej samej, współdzielonej logiki biznesowej

Tworzony jest zgodnie z zasadami **Clean Architecture** oraz wykorzystuje wzorce projektowe

**Główna Funcjonalność**
* **Zarządzanie Klientami:** Rejestracja, edycja i usuwanie klientów
* **Baza Rowerów:** Śledzenie dostępności rowerów, ich typów
* **Moduł Wypożyczeń:** Proces wynajmu sprzętu, przypisywania rowerów do klientów
* **Zarządzanie Pracownikami:** Ewidencja pracowników wypożyczalni

**Technologie i Wzorce**
* **Język** C# (.Net)
* **Interfejs** WPF (Material Desing in XAML) + Aplikacja Konsolowa
* **Architektura**  MVVM - w WPF
* **Wzorce Projektowe**
    * Repository Pattern & Unit of Work 
    * Dependency Injection (DI) wstrzykiwanie zależności za pomocą `Microsoft.Extensions.Hosting` (staram się dobrze opanować)
## Widok Interfejsu

![Główny widok](https://media.discordapp.net/attachments/926219187037302835/1496840953909088276/main.png?ex=69eb5945&is=69ea07c5&hm=e235bb43101550ab2b8e5bd9cd72c579e6a2a9f1a3eff1ee6f9f90fc94727648&=&format=webp&quality=lossless)
![Dodaj Klienta](https://media.discordapp.net/attachments/926219187037302835/1496840954420662484/main2.png?ex=69eb5945&is=69ea07c5&hm=4f6b31ec291ad9badfcef5c63d15a03bef10df4461d5b01172b99ea7bda0530f&=&format=webp&quality=lossless)

## Struktura plików (N-Tier)
```
├── 📁 ProjectBike.Abstractions       # Interfejsy dla Repozytoriów i Unit of Work   
├── 📁 ProjectBike.Console            # Aplikacja konsolowa  
├── 📁 ProjectBike.DataAccess.Memory  # Implementacja bazy danych w pamięci (DbContext)
├── 📁 ProjectBike.DataModel          # Definicje encji (Client, Bike, Rental, Employee)
├── 📁 ProjectBike.Desktop            # Główna aplikacja okienkowa (WPF / MVVM)
│   ├── 📁 View (OLD)                 # Wykluczony z projektu
│   ├── 📁 ViewModels                 # Logika sterująca widokami
│   ├── 📁 Views                      # Kontrolki interfejsu (UserControls)
│   └── 📁 Windows                    # Główne okna aplikacji
├── 📁 ProjectBike.ServiceAbstractions# Interfejsy dla serwisów logiki biznesowej
├── 📁 ProjectBike.Services.Memory    # Serwisy (np. ClientService, RentalService) i DataSeeder
└── 📁 ProjectBike.Test               # Testy jednostkowe w frameworku xUnit
```
## Licencja

Szczegóły oraz informacje o prawach autorskich znajdziesz w pliku [LICENSE](LICENSE).

 
