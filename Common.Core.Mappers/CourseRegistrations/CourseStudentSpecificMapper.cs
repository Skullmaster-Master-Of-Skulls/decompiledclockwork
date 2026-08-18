using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000162 RID: 354
	public static class CourseStudentSpecificMapper
	{
		// Token: 0x06000619 RID: 1561 RVA: 0x0001C180 File Offset: 0x0001A380
		static CourseStudentSpecificMapper()
		{
			Mapper.CreateMap<CourseStudentSpecificDTO, CourseStudentSpecific>();
			Mapper.CreateMap<CourseStudentSpecific, CourseStudentSpecificDTO>();
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001C190 File Offset: 0x0001A390
		public static CourseStudentSpecific ToDomainObject(this CourseStudentSpecificDTO dto)
		{
			return Mapper.Map<CourseStudentSpecificDTO, CourseStudentSpecific>(dto);
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		public static CourseStudentSpecificDTO ToDTO(this CourseStudentSpecific item)
		{
			return Mapper.Map<CourseStudentSpecific, CourseStudentSpecificDTO>(item);
		}
	}
}
