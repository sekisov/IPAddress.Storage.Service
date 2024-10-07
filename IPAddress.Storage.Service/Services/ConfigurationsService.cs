using FluentValidation;
using IPAddress.Storage.Service.DataAccess;
using IPAddress.Storage.Service.DataAccess.Models;
using IPAddress.Storage.Service.DataAccess.Repositories;
using IPAddress.Storage.Service.Domain.Models;
using IPAddress.Storage.Service.Domain.Repositories.Abstract;
using IPAddress.Storage.Service.Domain.Services;
using IPAddress.Storage.Service.Domain.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace IPAddress.Storage.Service.Services
{
    public class ConfigurationService
    {
        private readonly string _sqlConnection = string.Empty;
        private IConfiguration _configuration { get; }
        private readonly IServiceCollection _services;
        public ConfigurationService(IServiceCollection? services, IConfiguration configuration)
        {
            _sqlConnection = configuration.GetConnectionString("DefaultConnection");
            _configuration = configuration;

            services ??= new ServiceCollection();
            _services = services;
            ConfigureDbContext();
        }
        public void ConfigureDbContext()
        {
            _services.AddDbContext<DataContext>(options =>
            {
                options.UseMySQL(_sqlConnection);
            });
        }
        public IServiceCollection Configure()
        {
            var automapperService = new AutoMapperService();
            var mapper = automapperService.GetMapper();
            _services.AddSingleton(mapper);
            _services.AddMvc();
            _services.AddControllers();

            _services.AddValidatorsFromAssembly(typeof(Domain.Validators.UserValidator).Assembly);

            RegisterRepositories();
            RegisterServices();

            _services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAll",
                builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });

            _services.AddEndpointsApiExplorer();
            _services.AddSwaggerGen();

            return _services;
        }
        private void RegisterRepositories()
        {
            //_services.AddScoped<IRepository<UserDTO>, BaseRepository<UserDTO, User>>();
            _services.AddScoped<IRepository<UserDTO>>(x => x.GetService<IEditableRepository<UserDTO>>());
            _services.AddScoped<IRepository<UserIPAddressDTO>>(x => x.GetService<IEditableRepository<UserIPAddressDTO>>());

            _services.AddScoped<IEditableRepository<UserDTO>, BaseEditableRepository<UserDTO, User>>();
            _services.AddScoped<IEditableRepository<UserIPAddressDTO>, BaseEditableRepository<UserIPAddressDTO, UserIPAddress>>();
        }

        private void RegisterServices()
        {
            _services.AddScoped<IUserIPAddressesService, UserIPAddressesService>();
            _services.AddScoped<IUsersService, UsersService>();
        }
    }
}

