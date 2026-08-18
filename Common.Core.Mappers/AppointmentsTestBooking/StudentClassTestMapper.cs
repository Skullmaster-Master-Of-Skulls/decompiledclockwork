using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking
{
	// Token: 0x020001C9 RID: 457
	public static class StudentClassTestMapper
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00021AD0 File Offset: 0x0001FCD0
		static StudentClassTestMapper()
		{
			StudentClassTestBaseMapper.CreateMap();
			Mapper.CreateMap<StudentClassTestDTO, StudentClassTest>().ForMember((StudentClassTest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentClassTestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentClassTest, StudentClassTestDTO>();
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00021B54 File Offset: 0x0001FD54
		public static StudentClassTest ToDomainObject(this StudentClassTestDTO studentClassTestDTO)
		{
			return Mapper.Map<StudentClassTestDTO, StudentClassTest>(studentClassTestDTO);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x00021B6C File Offset: 0x0001FD6C
		public static StudentClassTestDTO ToDTO(this StudentClassTest studentClassTest)
		{
			return Mapper.Map<StudentClassTest, StudentClassTestDTO>(studentClassTest);
		}
	}
}
