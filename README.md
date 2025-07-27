# Project Title

Fishing Pond Management System

## Description

A desktop WPF (.NET 8 / MVVM) application that helps fishing-pond owners manage ponds, fish species, bookings, customers and daily KPIs — replacing paper notebooks with a single, intuitive tool.

## Features

- **Secure owner login / register**.
- **Pond management** – add, edit, hide / unhide (soft-delete), per-pond fish species.
- **Booking workflow** – capacity check, auto-create customer, **VIP rule** (≥ 5 visits → 20 % discount), booking history.
- **Customer module** – list, search by phone/name, VIP badge.
- **Dashboard** – today’s revenue, active customers, total bookings + weekly trends chart.
- **SQL Server** persistence (EF Core code-first).

## Getting Started

Instructions on how to set up and run your project locally for development or testing.

### Prerequisites
| Software | Version | Notes |
|----------|---------|-------|
| **.NET SDK** | 8.0.x | `dotnet --version` |
| **Visual Studio** | 2022 | Workload **“.NET Desktop Development”** |
| **SQL Server** | 2022 LocalDB (or Express) | `SqlLocalDB info` |
| **Git** | Latest | Clone / commit |

### Installation

# 1. Clone source
git clone https://github.com/viehai/FishingLake.git
cd FishingLake

# 2. Restore NuGet packages
dotnet restore

# 3. Create / migrate database
dotnet ef database update --project FishingLake.DAL

# 4. Build & run
dotnet run --project Fishing_Lake

### Usage
After you’ve run `dotnet run --project Fishing_Lake`, use the app as follows:

| Action | Steps |
| ------ | ----- |
| **Add Pond** | Dashboard ➜ *Add Pond* → fill name, location, capacity → **Save Pond** |
| **Hide / Show Pond** | Dashboard list → click **Hide** (soft-delete) or **Show** to unhide |
| **Create Booking** | Row ➜ **Book** → enter customer info → **Confirm**<br/>• Capacity auto-validated<br/>• VIP discount auto-applied when visits ≥ 5 |
| **View Customers** | Navigation ➜ *Customers* → search by phone/name |
| **Booking History** | Navigation ➜ *Booking History* → filter by date or phone |
| **Dashboard KPIs** | Cards refresh automatically after each booking |



## Authors

Chu Việt Hải

## Acknowledgments

- FPT University – guidance & course rubric  
- Entity Framework Core – ORM layer  
- xUnit & Coverlet – unit-testing & coverage  
- PlantUML – diagram generation for Reports 2-4
- ChatGPT – drafting documentation and code snippets
