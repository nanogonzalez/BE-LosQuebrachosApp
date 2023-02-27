namespace BE_LosQuebrachosApp.Entities
{
    public static class EmailBody
    {
        public static string EmailStringBody(string email, string emailToken)
        {
            return $@"<html>
    <head>
    </head>
        <body style=""margin:0;padding:0;font-family: Arial, Helvetica, sans-serif;"">
            <div style=""height: auto; background: rgb(209, 209, 238) no-repeat;width:400px;padding:30px;"">
              <div>
                <div>
                    <h1>Crea tu nueva Contraseña</h1>
                    <hr>
                    <p>Estas recibiendo este correo porque has requerido un cambio de contraseña para tu cuenta en Los Quebrachos.</p>
                    <p>Por favor hace click en el botón para elegir una nueva contraseña.</p>

                    <a href=""http://localhost:4200/reset?email={email}&code={emailToken}"" target=""_blank"" style=""background: blue;padding:10px;border:none;color:white;border-radius:4px;display:block;margin:0;width:50%;text-align:center;text-decoration:none;"">Nueva Contraseña</a><br>

                    <p>Muchas Gracias,<br><br>
                    Los Quebrachos</p>
                </div>
              </div>
            </div>
        </body>
</html>";
        }
    }
}
