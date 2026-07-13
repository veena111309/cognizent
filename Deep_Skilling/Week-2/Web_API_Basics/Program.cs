namespace Web_API_Basics
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            var app = builder.Build();

            // Configure HTTP request pipeline.
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
