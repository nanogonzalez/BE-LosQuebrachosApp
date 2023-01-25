using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Dtos
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string RazonSocial { get; set; }
        public string Cuit { get; set; }
    }
}
