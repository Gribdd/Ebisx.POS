namespace Ebisx.POS.Presentation.Profiles;

public class PaymentTypeProfile : Profile
{
    public PaymentTypeProfile()
    {
        CreateMap<PaymentType, PaymentTypeRequestDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<PaymentTypeResponseDto, PaymentType>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
    }
}
