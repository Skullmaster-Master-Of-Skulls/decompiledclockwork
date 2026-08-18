using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DC RID: 220
	public static class LookupDurationTermSubjectMapper
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00011E2C File Offset: 0x0001002C
		static LookupDurationTermSubjectMapper()
		{
			Mapper.CreateMap<LookupDurationTermSubjectDTO, LookupDurationTermSubject>();
			Mapper.CreateMap<LookupDurationTermSubject, LookupDurationTermSubjectDTO>();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00011E3C File Offset: 0x0001003C
		public static LookupDurationTermSubject ToDomainObject(this LookupDurationTermSubjectDTO sessionDTO)
		{
			return Mapper.Map<LookupDurationTermSubjectDTO, LookupDurationTermSubject>(sessionDTO);
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00011E54 File Offset: 0x00010054
		public static LookupDurationTermSubjectDTO ToDTO(this LookupDurationTermSubject session)
		{
			return Mapper.Map<LookupDurationTermSubject, LookupDurationTermSubjectDTO>(session);
		}
	}
}
