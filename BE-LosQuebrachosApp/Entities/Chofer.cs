﻿namespace BE_LosQuebrachosApp.Entities
{
    public class Chofer
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Cuit { get; set; }
        public Transporte Transporte { get; set; }
       
    }
}
