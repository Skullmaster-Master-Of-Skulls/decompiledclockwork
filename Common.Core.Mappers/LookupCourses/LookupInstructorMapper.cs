using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Core.Mappers.LookupCourses
{
	// Token: 0x020000DE RID: 222
	public static class LookupInstructorMapper
	{
		// Token: 0x060003AD RID: 941 RVA: 0x00011F18 File Offset: 0x00010118
		static LookupInstructorMapper()
		{
			LookupInstructorCourseInfoMapper.CreateMap();
			Mapper.CreateMap<LookupInstructorDTO, LookupInstructor>().ForMember((LookupInstructor pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<LookupInstructorDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LookupInstructor, LookupInstructorDTO>();
		}

		// Token: 0x060003AE RID: 942 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00011F9C File Offset: 0x0001019C
		public static LookupInstructor ToDomainObject(this LookupInstructorDTO lookupInstructorDTO)
		{
			return Mapper.Map<LookupInstructorDTO, LookupInstructor>(lookupInstructorDTO);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00011FB4 File Offset: 0x000101B4
		public static LookupInstructorDTO ToDTO(this LookupInstructor lookupInstructor)
		{
			return Mapper.Map<LookupInstructor, LookupInstructorDTO>(lookupInstructor);
		}
	}
}
