using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DA RID: 218
	public static class LookupCourseDateRangeMapper
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00011B54 File Offset: 0x0000FD54
		static LookupCourseDateRangeMapper()
		{
			Mapper.CreateMap<LookupCourseDateRangeDTO, LookupCourseDateRange>();
			Mapper.CreateMap<LookupCourseDateRange, LookupCourseDateRangeDTO>();
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00011B64 File Offset: 0x0000FD64
		public static LookupCourseDateRange ToDomainObject(this LookupCourseDateRangeDTO alternateContactDTO)
		{
			return Mapper.Map<LookupCourseDateRangeDTO, LookupCourseDateRange>(alternateContactDTO);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00011B7C File Offset: 0x0000FD7C
		public static LookupCourseDateRangeDTO ToDTO(this LookupCourseDateRange alternateContact)
		{
			return Mapper.Map<LookupCourseDateRange, LookupCourseDateRangeDTO>(alternateContact);
		}
	}
}
