using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D8 RID: 216
	public static class LookupCourseBaseWithPrimaryInstructorMapper
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00011994 File Offset: 0x0000FB94
		static LookupCourseBaseWithPrimaryInstructorMapper()
		{
			LookupInstructorMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<LookupCourseBaseWithPrimaryInstructorDTO, LookupCourseBaseWithPrimaryInstructor>().ForMember((LookupCourseBaseWithPrimaryInstructor pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupCourseBaseWithPrimaryInstructorDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupCourseBaseWithPrimaryInstructor, LookupCourseBaseWithPrimaryInstructorDTO>();
		}

		// Token: 0x06000396 RID: 918 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00011A1C File Offset: 0x0000FC1C
		public static LookupCourseBaseWithPrimaryInstructor ToDomainObject(this LookupCourseBaseWithPrimaryInstructorDTO dto)
		{
			return Mapper.Map<LookupCourseBaseWithPrimaryInstructorDTO, LookupCourseBaseWithPrimaryInstructor>(dto);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00011A34 File Offset: 0x0000FC34
		public static LookupCourseBaseWithPrimaryInstructorDTO ToDTO(this LookupCourseBaseWithPrimaryInstructor item)
		{
			return Mapper.Map<LookupCourseBaseWithPrimaryInstructor, LookupCourseBaseWithPrimaryInstructorDTO>(item);
		}
	}
}
