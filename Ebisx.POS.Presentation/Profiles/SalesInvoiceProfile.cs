namespace Ebisx.POS.Presentation.Profiles;

internal class SalesInvoiceProfile : Profile
{
    public SalesInvoiceProfile()
    {
        CreateMap<SalesInvoice, SalesInvoiceResponseDto>()
            .ForMember(dest => dest.PrivateId, opt => opt.MapFrom(src => src.PrivateId))
            .ForMember(dest => dest.PublicId, opt => opt.MapFrom(src => src.PublicId))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order))
            .ForMember(dest => dest.MachineInfo, opt => opt.MapFrom(src => src.MachineInfo))
            .ForMember(dest => dest.BusinessInfo, opt => opt.MapFrom(src => src.BusinessInfo))
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));

        CreateMap<SalesInvoice, SalesInvoiceRequestDto>()
            .ForMember(dest => dest.MachineInfoId, opt => opt.MapFrom(src => src.MachineInfo!.Id))
            .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order!.Id))
            .ForMember(dest => dest.BusinessInfoId, opt => opt.MapFrom(src => src.BusinessInfo!.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.PrivateId));
            
    }
}
