namespace WebApplication.Models
{
    public class Country
    {
        public long Id { get; set; }       // Идентификатор
        public string Name { get; set; }   // Название
        public string Code { get; set; }   // Код
        public long CityId { get; set; }   // Столица
        public virtual City City { get; set; }
        public double Area { get; set; }   // Площадь
        public int Population { get; set; }// Население
        public long RegionId { get; set; } // Регион
        public virtual Region Region { get; set; }
    }
}
