namespace ppedv.Hotelmanager2022.Model
{
    public class Buchung : Entity
    {
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        public virtual Gast? Gast { get; set; }
        public virtual Raum? Raum { get; set; }
    }
}