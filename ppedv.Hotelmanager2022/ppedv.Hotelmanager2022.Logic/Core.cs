using ppedv.Hotelmanager2022.Model;

namespace ppedv.Hotelmanager2022.Logic
{
    public class Core
    {


        public decimal CalcBuchungsGesamtPreis(Buchung buchung)
        {
            if (buchung == null)
                throw new ArgumentNullException(nameof(buchung));

            if (buchung.Raum == null)
                throw new ArgumentException(nameof(buchung.Raum));

            if (buchung.Von >= buchung.Bis)
                throw new ArgumentException("Von - Bis Datum/Zeit darf nicht gleich oder Bis nicht vor Von sein");

            return buchung.Raum.PreisProTag * (buchung.Bis - buchung.Von).Days;

        }

        public Buchung GetTeuersteBuchung()
        {
            throw new NotImplementedException();
        }

    }
}