using Isthmus.Produtos.Api.Configurations;

namespace Isthmus.Produtos.Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            DependencyInjectionConfig.Configure(services, _configuration);
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
        }
    }
}