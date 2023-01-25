using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Dtos
{
    public class ChoferDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cuit { get; set; }
        public Transporte Transporte { get; set; }

    }
}
