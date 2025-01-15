﻿
using AutoMapper;
using eCommerce.OrderService.BusinessLogicLayer.DTO;
using eCommerce.OrderService.DataAccessLayer.Entities;

namespace eCommerce.OrderService.BusinessLogicLayer.Mappers
{
    public class OrderToOrderResponseMappingProfile : Profile
    {
        public OrderToOrderResponseMappingProfile()
        {
            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.OrderID, opt =>
                opt.MapFrom(src => src.OrderID))
                .ForMember(dest => dest.UserID, opt =>
                opt.MapFrom(src => src.UserID))
                .ForMember(dest => dest.OrderDate, opt =>
                opt.MapFrom(src => src.OrderDate))
                .ForMember(dest => dest.OrderItems, opt =>
                opt.MapFrom(src => src.OrderItems))
                .ForMember(dest => dest.TotalBill, opt =>
                opt.MapFrom(src => src.TotalBill));
                
        }
    }
}