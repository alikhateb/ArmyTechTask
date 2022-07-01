namespace WebApp.App.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<InvoiceHeader, InvoiceHeaderViewModel>()
                .ReverseMap();
        }
    }
}
