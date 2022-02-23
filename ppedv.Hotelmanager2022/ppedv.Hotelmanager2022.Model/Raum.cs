namespace ppedv.Hotelmanager2022.Model
{
    public class Raum : Entity
    {
        public string Nummer { get; set; } = string.Empty;
        public int AnzBetten { get; set; }
        public bool Raucher { get; set; }
        public decimal PreisProTag { get; set; }
        public virtual ICollection<Buchung> Buchungen { get; set; } = new HashSet<Buchung>();
    }
}