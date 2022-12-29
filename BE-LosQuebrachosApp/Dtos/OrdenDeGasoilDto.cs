using BE_LosQuebrachosApp.Entities;

namespace BE_LosQuebrachosApp.Dtos
{
    public class OrdenDeGasoilDto
    {
        public int Id { get; set; }
        public int NumeroOrden { get; set; }
        public DateTime Fecha { get; set; }
        public int CuitTransporte { get; set; }
        public Transporte Transporte { get; set; }
        public Chofer Chofer { get; set; }
        public Vehiculo Vehiculo { get; set; }
        public int Litros { get; set; }
        public string Estacion { get; set; }

    }
}
