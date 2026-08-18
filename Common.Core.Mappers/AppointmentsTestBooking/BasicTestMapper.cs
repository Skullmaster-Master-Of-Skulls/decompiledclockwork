using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001BD RID: 445
	public static class BasicTestMapper
	{
		// Token: 0x06000793 RID: 1939 RVA: 0x00020E7C File Offset: 0x0001F07C
		static BasicTestMapper()
		{
			BaseBasicAppointmentMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<BasicTestDTO, BasicTest>().ForMember((BasicTest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<BasicTestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<BasicTest, BasicTestDTO>();
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00020F04 File Offset: 0x0001F104
		public static BasicTest ToDomainObject(this BasicTestDTO dto)
		{
			return Mapper.Map<BasicTestDTO, BasicTest>(dto);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00020F1C File Offset: 0x0001F11C
		public static BasicTestDTO ToDTO(this BasicTest item)
		{
			return Mapper.Map<BasicTest, BasicTestDTO>(item);
		}
	}
}
