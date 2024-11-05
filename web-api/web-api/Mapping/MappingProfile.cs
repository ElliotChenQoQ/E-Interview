using AutoMapper;
using System.Globalization;
using WebApp.Models;

namespace WebApp.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CsvCompanyRevenue, InsertRevenue>()
                            .ForMember(dest => dest.CompanyID, opt => opt.MapFrom(src => int.Parse(src.CompanyID)))
                            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyName))
                            .ForMember(dest => dest.Industry, opt => opt.MapFrom(src => src.Industry))
                            .ForMember(dest => dest.YearMonth, opt => opt.MapFrom(src => src.YearMonth))
                            .ForMember(dest => dest.ReportDate, opt => opt.MapFrom(src => src.ReportDate))
                            .ForMember(dest => dest.CurrentMonthRevenue, opt => opt.MapFrom(src => src.CurrentMonthRevenue))
                            .ForMember(dest => dest.LastMonthRevenue, opt => opt.MapFrom(src => src.LastMonthRevenue))
                            .ForMember(dest => dest.LastYearRevenue, opt => opt.MapFrom(src => src.LastYearRevenue))
                            .ForMember(dest => dest.MonthGrowth, opt => opt.MapFrom(src => src.MonthGrowth))
                            .ForMember(dest => dest.YearGrowth, opt => opt.MapFrom(src => src.YearGrowth))
                            .ForMember(dest => dest.CumulativeMonthRevenue, opt => opt.MapFrom(src => src.CumulativeMonthRevenue))
                            .ForMember(dest => dest.CumulativeLastYearRevenue, opt => opt.MapFrom(src => src.CumulativeLastYearRevenue))
                            .ForMember(dest => dest.CumulativeGrowth, opt => opt.MapFrom(src => src.CumulativeGrowth))
                            .ForMember(dest => dest.Remarks, opt => opt.MapFrom(src => src.Remarks));
        }
    }
}
