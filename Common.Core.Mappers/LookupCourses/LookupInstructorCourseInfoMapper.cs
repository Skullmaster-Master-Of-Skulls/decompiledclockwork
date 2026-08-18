using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DD RID: 221
	public static class LookupInstructorCourseInfoMapper
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00011E6C File Offset: 0x0001006C
		static LookupInstructorCourseInfoMapper()
		{
			Mapper.CreateMap<LookupInstructorCourseInfoDTO, LookupInstructorCourseInfo>().ForMember((LookupInstructorCourseInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupInstructorCourseInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupInstructorCourseInfo, LookupInstructorCourseInfoDTO>();
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011EE8 File Offset: 0x000100E8
		public static LookupInstructorCourseInfo ToDomainObject(this LookupInstructorCourseInfoDTO dto)
		{
			return Mapper.Map<LookupInstructorCourseInfoDTO, LookupInstructorCourseInfo>(dto);
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011F00 File Offset: 0x00010100
		public static LookupInstructorCourseInfoDTO ToDTO(this LookupInstructorCourseInfo item)
		{
			return Mapper.Map<LookupInstructorCourseInfo, LookupInstructorCourseInfoDTO>(item);
		}
	}
}
