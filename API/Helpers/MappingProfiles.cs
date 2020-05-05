using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Entities.Identity;
using Core.Entities.OrderAggregate;

namespace API.Helpers
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDto>()
            .ForMember(d=>d.ProductBrand, o=>o.MapFrom(x=>x.ProductBrand.Name))
            .ForMember(d=>d.ProductType, o=>o.MapFrom(x=>x.ProductType.Name))
            .ForMember(d=>d.PictureUrl, o=>o.MapFrom<ProductUrlResolver>());

            CreateMap<Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            CreateMap<CustomerBasketDto, CustomerBasket>();
            CreateMap<BasketItemDto, BasketItem>();
            CreateMap<AddressDto, Core.Entities.OrderAggregate.Address>();
            CreateMap<Order, OrderToReturnDto>()
                    .ForMember(d=> d.DeliveryMethod, s=>s.MapFrom(x=>x.DeliveryMethod.ShortName))
                    .ForMember(d=> d.ShippingPrice, s=>s.MapFrom(x=>x.DeliveryMethod.Price));
            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(d=> d.ProductId, s=>s.MapFrom(x=>x.ItemOrdered.ProductItemId))
                    .ForMember(d=> d.ProductName, s=>s.MapFrom(x=>x.ItemOrdered.ProductName))
                    .ForMember(d=> d.PictureUrl, s=>s.MapFrom(x=>x.ItemOrdered.PictureUrl))
                    .ForMember(d=>d.PictureUrl, s=>s.MapFrom<OrderItemUrlResolver>());
        }
    }
}