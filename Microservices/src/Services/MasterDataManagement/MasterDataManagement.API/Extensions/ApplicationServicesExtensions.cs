using MasterDataManagement.API.Errors;
using MasterDataManagement.Core.IRepository;
using MasterDataManagement.Infrastructure.Data.Generic;
using Microsoft.AspNetCore.Mvc;

namespace MasterDataManagement.API.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();           
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext => 
                {
                    var errors =   actionContext.ModelState
                                                .Where(e => e.Value.Errors.Count > 0)
                                                .SelectMany(x => x.Value.Errors)
                                                .Select(x => x.ErrorMessage)
                                                .ToArray();

                    var errorResponse = new ApiValidationErrorResponse
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            });

            return services;
        }
    }
}