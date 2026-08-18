using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestExamViews;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.AppointmentsTestExamViews.FinalExams;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestExamViews
{
	// Token: 0x020001B8 RID: 440
	public static class FinalExamsViewLightBookingMapper
	{
		// Token: 0x0600077F RID: 1919 RVA: 0x0002097C File Offset: 0x0001EB7C
		static FinalExamsViewLightBookingMapper()
		{
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<FinalExamsViewLightBookingDTO, FinalExamsViewLightBooking>().ForMember((FinalExamsViewLightBooking pb) => pb.Student, delegate(IMemberConfigurationExpression<FinalExamsViewLightBookingDTO> m)
			{
				m.MapFrom<BasicPerson>((FinalExamsViewLightBookingDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			});
			Mapper.CreateMap<FinalExamsViewLightBooking, FinalExamsViewLightBookingDTO>().ForMember((FinalExamsViewLightBookingDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<FinalExamsViewLightBooking> m)
			{
				m.MapFrom<BasicPersonDTO>((FinalExamsViewLightBooking pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			});
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x00020A38 File Offset: 0x0001EC38
		public static FinalExamsViewLightBooking ToDomainObject(this FinalExamsViewLightBookingDTO appointmentWorkshopInfoDTO)
		{
			return Mapper.Map<FinalExamsViewLightBookingDTO, FinalExamsViewLightBooking>(appointmentWorkshopInfoDTO);
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00020A50 File Offset: 0x0001EC50
		public static FinalExamsViewLightBookingDTO ToDTO(this FinalExamsViewLightBooking appointmentWorkshopInfo)
		{
			return Mapper.Map<FinalExamsViewLightBooking, FinalExamsViewLightBookingDTO>(appointmentWorkshopInfo);
		}
	}
}
