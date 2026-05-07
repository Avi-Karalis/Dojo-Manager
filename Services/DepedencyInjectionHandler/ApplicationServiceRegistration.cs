using DojoManager.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DojoManager.Services.DependencyInjectionHandler {
    public static class ApplicationServiceRegistration {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<ISessionService, SessionService>();
        }
    }
}
