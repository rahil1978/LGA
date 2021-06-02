using Microsoft.Extensions.DependencyInjection;
using Lga.Id.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lga.Id.Web.Extensions
{
    public static class BusinessExceptionExtensions
    {
        public static IServiceCollection AddBusinessExceptionFilter(this IServiceCollection services)
        {
            services.AddMvc(options => { options.Filters.Add(typeof(BusinessExceptionFilter)); });
            return services;
        }
    }
}
