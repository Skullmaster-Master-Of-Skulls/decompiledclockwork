using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.FullTest;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.FullTest;

namespace TechnoPro.Common.Core.Mappers.AppointmentsTestBooking.FullTest
{
	// Token: 0x020001D7 RID: 471
	public static class InstructorAcknowledgedStudentMapper
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x000225FC File Offset: 0x000207FC
		static InstructorAcknowledgedStudentMapper()
		{
			Mapper.CreateMap<InstructorAcknowledgedStudentDTO, InstructorAcknowledgedStudent>();
			Mapper.CreateMap<InstructorAcknowledgedStudent, InstructorAcknowledgedStudentDTO>();
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x0002260C File Offset: 0x0002080C
		public static InstructorAcknowledgedStudent ToDomainObject(this InstructorAcknowledgedStudentDTO dto)
		{
			return Mapper.Map<InstructorAcknowledgedStudentDTO, InstructorAcknowledgedStudent>(dto);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00022624 File Offset: 0x00020824
		public static InstructorAcknowledgedStudentDTO ToDTO(this InstructorAcknowledgedStudent item)
		{
			return Mapper.Map<InstructorAcknowledgedStudent, InstructorAcknowledgedStudentDTO>(item);
		}
	}
}
