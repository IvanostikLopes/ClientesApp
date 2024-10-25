namespace ClientesApp.API.Extensions
{
    /// <summary>
    /// Classe de extensão de confiração do Swegger
    /// </summary>
    public static class SwaggerExtension
    {
        /// <summary>
        /// Método de extensão para configuração do Swegger na API
        /// </summary>
        public static IServiceCollection AddSweggerConfig(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }


        /// <summary>
        /// Método de extensão para executar as configurações do Swegger na API
        /// </summary>
        public static IApplicationBuilder UseSweggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            return app;
        }
    }
}
