using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentBookingStudent.BookingRequest;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Core.Mappers.AppointmentBookingStudent.BookingRequest
{
	// Token: 0x0200020C RID: 524
	public static class BookingFilterParametersMapper
	{
		// Token: 0x060008D6 RID: 2262 RVA: 0x00026204 File Offset: 0x00024404
		static BookingFilterParametersMapper()
		{
			CutoffTimeMapper.CreateMap();
			Mapper.CreateMap<AppointmentBookingFilterParametersDTO, AppointmentBookingFilterParameters>().ForMember((AppointmentBookingFilterParameters pb) => pb.CutoffTime, delegate(IMemberConfigurationExpression<AppointmentBookingFilterParametersDTO> m)
			{
				m.MapFrom<CutoffTime>((AppointmentBookingFilterParametersDTO pbdto) => (pbdto.CutoffTime == null) ? null : pbdto.CutoffTime.ToDomainObject());
			});
			Mapper.CreateMap<AppointmentBookingFilterParameters, AppointmentBookingFilterParametersDTO>().ForMember((AppointmentBookingFilterParametersDTO pb) => pb.CutoffTime, delegate(IMemberConfigurationExpression<AppointmentBookingFilterParameters> m)
			{
				m.MapFrom<CutoffTimeDTO>((AppointmentBookingFilterParameters pbdto) => (pbdto.CutoffTime == null) ? null : pbdto.CutoffTime.ToDTO());
			});
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x000262C0 File Offset: 0x000244C0
		public static AppointmentBookingFilterParameters ToDomainObject(this AppointmentBookingFilterParametersDTO dto)
		{
			return Mapper.Map<AppointmentBookingFilterParametersDTO, AppointmentBookingFilterParameters>(dto);
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000262D8 File Offset: 0x000244D8
		public static AppointmentBookingFilterParametersDTO ToDTO(this AppointmentBookingFilterParameters item)
		{
			return Mapper.Map<AppointmentBookingFilterParameters, AppointmentBookingFilterParametersDTO>(item);
		}
	}
}
