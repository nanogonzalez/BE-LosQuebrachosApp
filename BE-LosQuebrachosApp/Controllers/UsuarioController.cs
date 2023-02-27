using BE_LosQuebrachosApp.Data;
using BE_LosQuebrachosApp.Dtos;
using BE_LosQuebrachosApp.Entities;
using BE_LosQuebrachosApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BE_LosQuebrachosApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public UsuarioController(ApplicationDbContext context, IConfiguration configuration, IEmailService emailService)
        {
            _emailService = emailService;
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("Authenticate")]

        public async Task<IActionResult> Authenticate([FromBody] Usuario ObjUsuario)
        {
            if(ObjUsuario == null)
                return BadRequest();

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == ObjUsuario.NombreUsuario);

            if(usuario == null)
                return NotFound();

            if(!PasswordHasher.VerifyPassword(ObjUsuario.Password, usuario.Password))
            {
                return BadRequest(new { Message = "Contraseña incorrecta" });
            }

            usuario.Token = CreateJwt(usuario);
            var newAccessToken = usuario.Token;
            var newRefreshToken = CreateRefreshToken();
            usuario.RefreshToken = newRefreshToken;
            usuario.RefreshTokenTime = DateTime.Now.AddDays(5);
            await _context.SaveChangesAsync();

            return Ok(new TokenDto()
            {
               AccessToken = newAccessToken,
               RefreshToken = newRefreshToken 
            });
            
        }

        [HttpPost("Register")]

        public async Task<IActionResult> Register([FromBody] Usuario ObjUsuario)
        {
            if (ObjUsuario == null)
                return BadRequest();

            if (await CheckNombreUsuarioExistAsync(ObjUsuario.NombreUsuario))
                return BadRequest(new { Message = "Este nombre de Usuario ya existe!" });

            if (await CheckEmailExistAsync(ObjUsuario.Email))
                return BadRequest(new { Message = "Este email ya existe!" });

            var pass = CheckPasswordStrength(ObjUsuario.Password);

            if (!string.IsNullOrEmpty(pass))
            {
                return BadRequest(new { Message = pass.ToString() });
            }

            ObjUsuario.Password = PasswordHasher.HashPassword(ObjUsuario.Password);
            ObjUsuario.Role = "Usuario";
            ObjUsuario.Token = "";
            await _context.Usuarios.AddAsync(ObjUsuario);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Se ha registrado con exito!" });
        }

        private async Task<bool> CheckNombreUsuarioExistAsync(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(x => x.NombreUsuario == nombreUsuario);
        }

        private async Task<bool> CheckEmailExistAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(x => x.Email == email);
        }

        private string CheckPasswordStrength(string password)
        {
            StringBuilder sb = new StringBuilder();
            if(password.Length < 8)
            {
                sb.Append("Ingrese como mínimo 8 caracteres"+Environment.NewLine);
            }
            if (!(Regex.IsMatch(password, "[a-z]") && Regex.IsMatch(password, "[A-Z]") && Regex.IsMatch(password, "[0-9]")))
            {
                sb.Append("La contraseña debe contener una letra mayúscula y un número" + Environment.NewLine);
            }

            return sb.ToString();
        }

        private string CreateJwt(Usuario usuario)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Role, usuario.Role),
                new Claim(ClaimTypes.Name, $"{usuario.NombreUsuario}")
            });

            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            DateTime now = DateTime.UtcNow;
            DateTime expires = now.AddSeconds(10);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = expires,
                SigningCredentials = credentials
            };
            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            return jwtTokenHandler.WriteToken(token);   
        }

        private string CreateRefreshToken()
        {
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var refreshToken = Convert.ToBase64String(tokenBytes);

            var tokenInUser = _context.Usuarios.Any(x =>x.RefreshToken == refreshToken);

            if (tokenInUser)
            {
                return CreateRefreshToken();
            }
            return refreshToken;
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var key = Encoding.ASCII.GetBytes("veryverysecret.....");
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Este token es inválido");
            }
            return principal;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetAllUsers()
        {
            return Ok(await _context.Usuarios.ToListAsync());
        }

        [HttpPost("Refresh")]
        public async Task<IActionResult> Refresh(TokenDto tokenDto)
        {
            if(tokenDto is null)
            {
                return BadRequest("Petición inválida del cliente");
            }
            string accessToken = tokenDto.AccessToken;
            string refreshToken = tokenDto.RefreshToken;
            var principal = GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = await _context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == username);
            if(user is null || user.RefreshToken != refreshToken || user.RefreshTokenTime <= DateTime.Now)
            {
                return BadRequest("Token inválido");
            }
            var newAccessToken = CreateJwt(user);
            var newRefreshToken = CreateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();

            return Ok(new TokenDto()
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }

        [HttpPost("send-reset-email/{email}")]
        public async Task<IActionResult> SendEmail(string email)
        {
            var user = await _context.Usuarios.FirstOrDefaultAsync(x =>x.Email == email);
            if(user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Este email no existe"
                });
            }
            var tokenBytes = RandomNumberGenerator.GetBytes(64);
            var emailToken = Convert.ToBase64String(tokenBytes);
            user.ResetPasswordToken = emailToken;
            user.ResetPasswordExpiry = DateTime.Now.AddMinutes(15);
            string from = _configuration["EmailSettings:From"];
            var emailModel = new Email(email, "Cambio de Contraseña", EmailBody.EmailStringBody(email, emailToken));
            _emailService.SendEmail(emailModel);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Email enviado"
            });
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
            var user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(x => x.Email == resetPasswordDto.Email);
            if (user is null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "Este usuario no existe"
                });
            }
            var tokenCode = user.ResetPasswordToken;
            DateTime emailTokenExpiry = user.ResetPasswordExpiry;
            if(tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "Reset link inválido"
                });
            }
            user.Password = PasswordHasher.HashPassword(resetPasswordDto.NewPassword);
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                StatusCode = 200,
                Message = "Creo su nueva contraseña con exito!"
            });
        }
    }
}
