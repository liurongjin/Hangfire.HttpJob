namespace Dashboard.HangfireRedis
{
    public class Startup
    {
        public IConfiguration JsonConfig { get; }

        public Startup(IConfiguration configuration)
        {
            JsonConfig = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSelfHangfire(JsonConfig);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSelfHangfire(JsonConfig);
        }
    }
}
