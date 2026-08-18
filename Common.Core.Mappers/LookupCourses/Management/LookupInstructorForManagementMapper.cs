using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.Core.Mappers.LookupCourses.Management
{
	// Token: 0x020000E5 RID: 229
	public static class LookupInstructorForManagementMapper
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00012388 File Offset: 0x00010588
		static LookupInstructorForManagementMapper()
		{
			LookupInstructorMapper.CreateMap();
			LookupInstructorCourseAttachmentForManagementMapper.CreateMap();
			Mapper.CreateMap<LookupInstructorForManagementDTO, LookupInstructorForManagement>().ForMember((LookupInstructorForManagement pb) => pb.AttachedCourses, delegate(IMemberConfigurationExpression<LookupInstructorForManagementDTO> m)
			{
				m.MapFrom<List<LookupInstructorCourseAttachmentForManagement>>((LookupInstructorForManagementDTO pbdto) => (pbdto.AttachedCourses == null) ? null : (from g in pbdto.AttachedCourses
				select g.ToDomainObject()).ToList<LookupInstructorCourseAttachmentForManagement>());
			}).ForMember((LookupInstructorForManagement pb) => pb.Instructor, delegate(IMemberConfigurationExpression<LookupInstructorForManagementDTO> m)
			{
				m.MapFrom<LookupInstructor>((LookupInstructorForManagementDTO pbdto) => (pbdto.Instructor == null) ? null : pbdto.Instructor.ToDomainObject());
			});
			Mapper.CreateMap<LookupInstructorForManagement, LookupInstructorForManagementDTO>().ForMember((LookupInstructorForManagementDTO pb) => pb.AttachedCourses, delegate(IMemberConfigurationExpression<LookupInstructorForManagement> m)
			{
				m.MapFrom<List<LookupInstructorCourseAttachmentForManagementDTO>>((LookupInstructorForManagement pbdto) => (pbdto.AttachedCourses == null) ? null : (from g in pbdto.AttachedCourses
				select g.ToDTO()).ToList<LookupInstructorCourseAttachmentForManagementDTO>());
			}).ForMember((LookupInstructorForManagementDTO pb) => pb.Instructor, delegate(IMemberConfigurationExpression<LookupInstructorForManagement> m)
			{
				m.MapFrom<LookupInstructorDTO>((LookupInstructorForManagement pbdto) => (pbdto.Instructor == null) ? null : pbdto.Instructor.ToDTO());
			});
		}

		// Token: 0x060003CA RID: 970 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000124E8 File Offset: 0x000106E8
		public static LookupInstructorForManagement ToDomainObject(this LookupInstructorForManagementDTO sessionDTO)
		{
			return Mapper.Map<LookupInstructorForManagementDTO, LookupInstructorForManagement>(sessionDTO);
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00012500 File Offset: 0x00010700
		public static LookupInstructorForManagementDTO ToDTO(this LookupInstructorForManagement session)
		{
			return Mapper.Map<LookupInstructorForManagement, LookupInstructorForManagementDTO>(session);
		}
	}
}
