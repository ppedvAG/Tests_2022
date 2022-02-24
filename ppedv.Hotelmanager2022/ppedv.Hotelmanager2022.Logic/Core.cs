using ppedv.Hotelmanager2022.Model;
using ppedv.Hotelmanager2022.Model.Contracts;

namespace ppedv.Hotelmanager2022.Logic
{
    public class Core
    {

        public IRepository Repository { get; init; }

        public Core(IRepository repository)
        {
            Repository = repository;
        }

        public Buchung GetTeuersteBuchung()
        {
            return Repository.Query<Buchung>().OrderByDescending(x => CalcBuchungsGesamtPreis(x)).ThenByDescending(x => x.Von).FirstOrDefault();
        }

        public virtual decimal CalcBuchungsGesamtPreis(Buchung buchung)
        {
            if (buchung == null)
                throw new ArgumentNullException(nameof(buchung));

            if (buchung.Raum == null)
                throw new ArgumentException(nameof(buchung.Raum));

            if (buchung.Von >= buchung.Bis)
                throw new ArgumentException("Von - Bis Datum/Zeit darf nicht gleich oder Bis nicht vor Von sein");

            return buchung.Raum.PreisProTag * (buchung.Bis - buchung.Von).Days;

        }



    }
}