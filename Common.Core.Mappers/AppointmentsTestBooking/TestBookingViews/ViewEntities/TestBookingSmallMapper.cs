using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x020001D5 RID: 469
	public static class TestBookingSmallMapper
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00022468 File Offset: 0x00020668
		static TestBookingSmallMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<TestBookingSmallDTO, TestBookingSmall>().ForMember((TestBookingSmall pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TestBookingSmallDTO> m)
			{
				m.Ignore();
			}).ForMember((TestBookingSmall pb) => pb.Student, delegate(IMemberConfigurationExpression<TestBookingSmallDTO> m)
			{
				m.MapFrom<PersonBase>((TestBookingSmallDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			});
			Mapper.CreateMap<TestBookingSmall, TestBookingSmallDTO>().ForMember((TestBookingSmallDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<TestBookingSmall> m)
			{
				m.MapFrom<PersonBaseDTO>((TestBookingSmall pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			});
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x0002258C File Offset: 0x0002078C
		public static TestBookingSmall ToDomainObject(this TestBookingSmallDTO dto)
		{
			return Mapper.Map<TestBookingSmallDTO, TestBookingSmall>(dto);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x000225A4 File Offset: 0x000207A4
		public static TestBookingSmallDTO ToDTO(this TestBookingSmall item)
		{
			return Mapper.Map<TestBookingSmall, TestBookingSmallDTO>(item);
		}
	}
}
