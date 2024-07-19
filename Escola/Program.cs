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
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // essa forma só 1 usuario por API
        }).AddJwtBearer(x => 
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(chaveSeguranca),
                ValidateIssuer = false, // para api maior que 1 usuáiro
                ValidateAudience = false // para api maior que 1 usuáiro
            };
        });

        builder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options => { options.SuppressModelStateInvalidFilter = true; });
        builder.Services.AddDbContext<EscolaDataContext>();

        builder.Services.AddTransient<TokenService>(); // Sempre cria um novo
        //builder.Services.AddScoped; // É por requisição (se não existisse o AddDbContext, esse AddScoped seria a melhor opção)
        //builder.Services.AddScoped; // É um por aplicação, uma vez carregado sempre vai estar na memória
        var app = builder.Build();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}