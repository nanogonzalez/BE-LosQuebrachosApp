namespace BE_LosQuebrachosApp.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string DestinoCarga { get; set; }
        public string DestinoDescarga { get; set; }
        public DateTime DiaCarga { get; set; }
        public DateTime HoraCarga { get; set; }
        public string TipoMercaderia { get; set; }
    }
}
