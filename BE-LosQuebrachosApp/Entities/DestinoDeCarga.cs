namespace BE_LosQuebrachosApp.Entities
{
    public class DestinoDeCarga
    {
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public Cliente Cliente { get; set; }
    }
}
