using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Dtos
{
    public class OrdenDeCargaDto
    {
        public int Id { get; set; }
        public string? NumeroOrden { get; set; }
        public DestinoDeCarga DestinoDeCarga { get; set; }
        public DestinoDeDescarga DestinoDeDescarga { get; set; }
        public int DistanciaViaje { get; set; }
        public DateTime DiaHoraCarga { get; set; }
        public string TipoMercaderia { get; set; }
        public Cliente Cliente { get; set; }
    }
}
