using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000D9 RID: 217
	public static class LookupCourseToLookupCourseBaseWithPrimaryInstructorMapper
	{
		// Token: 0x06000399 RID: 921 RVA: 0x00011A4C File Offset: 0x0000FC4C
		static LookupCourseToLookupCourseBaseWithPrimaryInstructorMapper()
		{
			LookupCourseBaseMapper.CreateMap();
			AlternateContactMapper.CreateMap();
			LookupInstructorMapper.CreateMap();
			LookupTimetableItemMapper.CreateMap();
			LookupCourseToLookupCourseBaseMapper.CreateMap();
			LookupCourseBaseWithPrimaryInstructorMapper.CreateMap();
			LookupSubjectMapper.CreateMap();
			Mapper.CreateMap<LookupCourseDTO, LookupCourseBaseWithPrimaryInstructorDTO>().ForMember((LookupCourseBaseWithPrimaryInstructorDTO ar) => ar.PrimaryInstructor, delegate(IMemberConfigurationExpression<LookupCourseDTO> m)
			{
				m.MapFrom<LookupInstructorDTO>((LookupCourseDTO pb) => LookupCourseToLookupCourseBaseWithPrimaryInstructorMapper.ExtractPrimaryInstructor(pb.Instructors));
			});
		}

		// Token: 0x0600039A RID: 922 RVA: 0x00011AD8 File Offset: 0x0000FCD8
		private static LookupInstructorDTO ExtractPrimaryInstructor(IList<LookupInstructorDTO> Instructors)
		{
			bool flag = Instructors == null || Instructors.Count < 1;
			LookupInstructorDTO result;
			if (flag)
			{
				result = null;
			}
			else
			{
				LookupInstructorDTO lookupInstructorDTO = Instructors.FirstOrDefault((LookupInstructorDTO g) => g.IsPrimary);
				bool flag2 = lookupInstructorDTO != null;
				if (flag2)
				{
					result = lookupInstructorDTO;
				}
				else
				{
					result = Instructors[0];
				}
			}
			return result;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00011B3C File Offset: 0x0000FD3C
		public static LookupCourseBaseWithPrimaryInstructorDTO ToLookupCourseBaseWithPrimaryInstructor(this LookupCourseDTO lookupCourse)
		{
			return Mapper.Map<LookupCourseDTO, LookupCourseBaseWithPrimaryInstructorDTO>(lookupCourse);
		}
	}
}
