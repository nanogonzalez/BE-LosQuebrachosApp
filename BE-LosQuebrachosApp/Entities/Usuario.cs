﻿namespace BE_LosQuebrachosApp.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenTime { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime ResetPasswordExpiry { get; set; }

    }
}
