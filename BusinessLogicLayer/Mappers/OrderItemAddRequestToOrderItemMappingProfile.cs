﻿
using AutoMapper;
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.DataAccessLayer.Entities;

namespace eCommerce.OrderService.BusinessLogicLayer.Mappers
{
    public class OrderItemAddRequestToOrderItemMappingProfile : Profile
    {
        public OrderItemAddRequestToOrderItemMappingProfile()
        {
            CreateMap<OrderItemAddRequest, OrderItem>()
                .ForMember(dest => dest.ProductID, opt =>
                opt.MapFrom(src => src.ProductID))
                .ForMember(dest => dest.UnitPrice, opt =>
                opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Quantity, opt =>
                opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.TotalPrice, opt =>
                opt.Ignore())
                .ForMember(dest => dest._id, opt => opt.Ignore());

           

        }
    }
}