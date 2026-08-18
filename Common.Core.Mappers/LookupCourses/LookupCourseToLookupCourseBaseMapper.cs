using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D7 RID: 215
	public static class LookupCourseToLookupCourseBaseMapper
	{
		// Token: 0x06000392 RID: 914 RVA: 0x00011914 File Offset: 0x0000FB14
		static LookupCourseToLookupCourseBaseMapper()
		{
			LookupSubjectMapper.CreateMap();
			Mapper.CreateMap<LookupCourseDTO, LookupCourseBaseDTO>().ForMember((LookupCourseBaseDTO ar) => ar.Subject, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.MapFrom<LookupSubjectDTO>((LookupCourseDTO pb) => pb.Subject);
			});
		}

		// Token: 0x06000393 RID: 915 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0001197C File Offset: 0x0000FB7C
		public static LookupCourseBaseDTO ToLookupCourseBase(this LookupCourseDTO item)
		{
			return Mapper.Map<LookupCourseDTO, LookupCourseBaseDTO>(item);
		}
	}
}
