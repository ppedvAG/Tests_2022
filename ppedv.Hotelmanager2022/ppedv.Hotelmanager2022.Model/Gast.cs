namespace ppedv.Hotelmanager2022.Model
{
    public class Gast : Entity
    {
        public string Name { get; set; } = string.Empty;
        public DateTime GebDatum { get; set; }
        public virtual ICollection<Buchung> Buchungen { get; set; } = new HashSet<Buchung>();

    }
}