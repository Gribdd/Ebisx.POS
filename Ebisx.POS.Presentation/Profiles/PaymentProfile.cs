namespace Ebisx.POS.Presentation.Profiles;

public class PaymentProfile : Profile
{
    public PaymentProfile()
    {
        CreateMap<Payment, PaymentRequestDto>()
            .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
            .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.PaymentTypeId))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.NonCashPaymentMethodID, opt => opt.MapFrom(src => src.NonCashPaymentMethodID))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId));

        CreateMap<PaymentResponseDto, Payment>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.AmountPaid, opt => opt.MapFrom(src => src.AmountPaid))
            .ForMember(dest => dest.PaymentTypeId, opt => opt.MapFrom(src => src.PaymentType.Id))
            .ForMember(dest => dest.PaymentType, opt => opt.MapFrom(src => src.PaymentType))
            .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
            .ForMember(dest => dest.NonCashPaymentMethodID, opt => opt.MapFrom(src => src.NonCashPaymentMethod!.Id))
            .ForMember(dest => dest.NonCashPaymentMethod, opt => opt.MapFrom(src => src.NonCashPaymentMethod))
            .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Customer!.Id))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer));
    }
}
