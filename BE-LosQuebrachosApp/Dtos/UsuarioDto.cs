namespace BE_LosQuebrachosApp.Dtos
{
    public class UsuarioDto
    {  
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
    }
}
