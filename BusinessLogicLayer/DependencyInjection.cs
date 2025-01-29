

using eCommerce.OrderService.BusinessLogicLayer.Mappers;
using eCommerce.OrderService.BusinessLogicLayer.ServiceContracts;
using eCommerce.OrderService.BusinessLogicLayer.Services;
using eCommerce.OrderService.BusinessLogicLayer.Validators;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.OrderService.BusinessLogicLayer;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection Services)
    { //TO DO: Add data Business LogicLayer services into the IOC container

        Services.AddValidatorsFromAssemblyContaining<OrderAddRequestValidator>();

        Services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile).Assembly);

        Services.AddScoped<IOrderService, OrdersService>();
        return Services;
    }
}
