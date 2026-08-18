using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Core.Mappers.CourseRegistrations
{
	// Token: 0x02000161 RID: 353
	public static class CourseRegistrationWithStudentSpecificInfoMapper
	{
		// Token: 0x06000615 RID: 1557 RVA: 0x0001C132 File Offset: 0x0001A332
		static CourseRegistrationWithStudentSpecificInfoMapper()
		{
			CourseStudentSpecificMapper.CreateMap();
			CourseRegistrationMapper.CreateMap();
			Mapper.CreateMap<CourseRegistrationWithStudentSpecificInfoDTO, CourseRegistrationWithStudentSpecificInfo>();
			Mapper.CreateMap<CourseRegistrationWithStudentSpecificInfo, CourseRegistrationWithStudentSpecificInfoDTO>();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001C150 File Offset: 0x0001A350
		public static CourseRegistrationWithStudentSpecificInfo ToDomainObject(this CourseRegistrationWithStudentSpecificInfoDTO dto)
		{
			return Mapper.Map<CourseRegistrationWithStudentSpecificInfoDTO, CourseRegistrationWithStudentSpecificInfo>(dto);
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001C168 File Offset: 0x0001A368
		public static CourseRegistrationWithStudentSpecificInfoDTO ToDTO(this CourseRegistrationWithStudentSpecificInfo item)
		{
			return Mapper.Map<CourseRegistrationWithStudentSpecificInfo, CourseRegistrationWithStudentSpecificInfoDTO>(item);
		}
	}
}
