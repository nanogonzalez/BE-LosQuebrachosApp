using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Dtos
{
    public class DestinoDeCargaDto
    {
        public int Id { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public Cliente Cliente { get; set; }
    }
}
