namespace BE_LosQuebrachosApp.Dtos
{
    public class OrdenDeCargaDto
    {
        public int Id { get; set; }
        public string DestinoCarga { get; set; }
        public string DestinoDescarga { get; set; }
        public DateTime DiaCarga { get; set; }
        public TimeSpan HoraCarga { get; set; }
        public string TipoMercaderia { get; set; }
    }
}
