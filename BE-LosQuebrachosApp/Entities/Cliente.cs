namespace BE_LosQuebrachosApp.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public long Cuit { get; set; }
        public string DestinoCarga { get; set; }
    }
}
