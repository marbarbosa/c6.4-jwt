using Escola.Data;
using Escola.Services;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Escola.Models;
using SecureIdentity.Password;

internal class Program
{
    private static void Main(string[] args)
    {
        //Gerar senha
        //var senha = PasswordGenerator.Generate(32, false, false);

        var builder = WebApplication.CreateBuilder(args);

        var chaveSeguranca = Encoding.ASCII.GetBytes(Escola.Configuration.JwtKey);
        builder.Services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // essa forma s� 1 usuario por API
        }).AddJwtBearer(x => 
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(chaveSeguranca),
                ValidateIssuer = false, // para api maior que 1 usu�iro
                ValidateAudience = false // para api maior que 1 usu�iro
            };
        });

        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
        builder.Services.AddDbContext<EscolaDataContext>();

        builder.Services.AddTransient<TokenService>(); // Sempre cria um novo
        //builder.Services.AddScoped; // � por requisi��o (se n�o existisse o AddDbContext, esse AddScoped seria a melhor op��o)
        //builder.Services.AddScoped; // � um por aplica��o, uma vez carregado sempre vai estar na mem�ria
        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}