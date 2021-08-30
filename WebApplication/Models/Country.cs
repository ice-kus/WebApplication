namespace WebApplication.Models
{
    public class Country
    {
        public long id { get; set; }       // Идентификатор
        public string name { get; set; }   // Название
        public string code { get; set; }   // Код
        public long cityid { get; set; }   // Столица
        public virtual City city { get; set; }
        public double area { get; set; }   // Площадь
        public int population { get; set; }// Население
        public long regionid { get; set; } // Регион
        public virtual Region region { get; set; }
    }
}
