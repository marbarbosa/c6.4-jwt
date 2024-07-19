namespace Escola
{
    public static class Configuration
    {
        //TOKEN - JWT - Json Web Token
        public static string JwtKey { get; set; } = "076519f9-c240-4e7c-9d89-c98c3d074cba";
        //Horas para expirar o Token do login do usuário
        public static int TempoExpiracaoHoras { get; set; } = 8;

        //Api_Key
        public static string ApiKeyName = "api_key"; // sempre tem que declarar o nome, porque é a forma de ir buscar a chave
        public static string ApiKey = "curso_api-_P@JWYGMkO0yODxNmcs3omwTcqXHLDi!vfg9"; //senha
    }
}

