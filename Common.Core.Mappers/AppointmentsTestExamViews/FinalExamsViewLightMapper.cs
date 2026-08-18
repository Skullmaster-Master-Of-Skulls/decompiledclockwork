using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews
{
	// Token: 0x020001B9 RID: 441
	public static class FinalExamsViewLightMapper
	{
		// Token: 0x06000783 RID: 1923 RVA: 0x00020A68 File Offset: 0x0001EC68
		static FinalExamsViewLightMapper()
		{
			FinalExamsViewLightBookingMapper.CreateMap();
			Mapper.CreateMap<FinalExamsViewLightDTO, FinalExamsViewLight>().ForMember((FinalExamsViewLight pb) => pb.Bookings, delegate(IMemberConfigurationExpression<FinalExamsViewLightDTO> m)
			{
				m.MapFrom<IEnumerable<FinalExamsViewLightBooking>>((FinalExamsViewLightDTO pbdto) => (pbdto.Bookings == null) ? null : (from g in pbdto.Bookings
				select g.ToDomainObject()));
			});
			Mapper.CreateMap<FinalExamsViewLight, FinalExamsViewLightDTO>().ForMember((FinalExamsViewLightDTO pb) => pb.Bookings, delegate(IMemberConfigurationExpression<FinalExamsViewLight> m)
			{
				m.MapFrom<IEnumerable<FinalExamsViewLightBookingDTO>>((FinalExamsViewLight pbdto) => (pbdto.Bookings == null) ? null : (from g in pbdto.Bookings
				select g.ToDTO()));
			});
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x00020B24 File Offset: 0x0001ED24
		public static FinalExamsViewLight ToDomainObject(this FinalExamsViewLightDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<FinalExamsViewLightDTO, FinalExamsViewLight>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x00020B3C File Offset: 0x0001ED3C
		public static FinalExamsViewLightDTO ToDTO(this FinalExamsViewLight appointmentWorkshopInfo)
		{
			return Mapper.Map<FinalExamsViewLight, FinalExamsViewLightDTO>(appointmentWorkshopInfo);
		}
	}
}
