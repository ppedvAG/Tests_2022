// See https://aka.ms/new-console-template for more information
using ppedv.Hotelmanager2022.Data.EFCore;
using ppedv.Hotelmanager2022.Logic;
using ppedv.Hotelmanager2022.Model;

Console.WriteLine("Hello, World!");

var core = new Core(new EfRepository());

var query = core.Repository.Query<Buchung>().OrderBy(x => x.Von);

foreach (var b in query.ToList())
{
    Console.WriteLine($"{b.Von:d} - {b.Bis:d} Raum: {b.Raum?.Nummer} Gast: {b.Gast?.Name}");
}

