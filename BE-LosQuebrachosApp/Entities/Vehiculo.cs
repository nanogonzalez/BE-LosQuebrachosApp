namespace BE_LosQuebrachosApp.Entities
{
    public class Vehiculo
    {
        public int Id { get; set; }
        public string Chasis { get; set; }
        public string Acoplado { get; set; }
        public string Tipo { get; set; }
        public int CapacidadTN { get; set; }
        public Transporte Transporte { get; set; }
    
    }
}
