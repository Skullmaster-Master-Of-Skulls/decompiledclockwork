using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.Core.Mappers.LookupCourses.Management
{
	// Token: 0x020000E3 RID: 227
	public static class LookupInstructorCourseAttachmentForManagementMapper
	{
		// Token: 0x060003C1 RID: 961 RVA: 0x00012258 File Offset: 0x00010458
		static LookupInstructorCourseAttachmentForManagementMapper()
		{
			LookupInstructorCourseStudentAttachmentForManagementMapper.CreateMap();
			Mapper.CreateMap<LookupInstructorCourseAttachmentForManagementDTO, LookupInstructorCourseAttachmentForManagement>().ForMember((LookupInstructorCourseAttachmentForManagement pb) => pb.Students, delegate(IMemberConfigurationExpression<LookupInstructorCourseAttachmentForManagementDTO> m)
			{
				m.MapFrom<List<LookupInstructorCourseStudentAttachmentForManagement>>((LookupInstructorCourseAttachmentForManagementDTO pbdto) => (pbdto.Students == null) ? null : (from g in pbdto.Students
				select g.ToDomainObject()).ToList<LookupInstructorCourseStudentAttachmentForManagement>());
			});
			Mapper.CreateMap<LookupInstructorCourseAttachmentForManagement, LookupInstructorCourseAttachmentForManagementDTO>().ForMember((LookupInstructorCourseAttachmentForManagementDTO pb) => pb.Students, delegate(IMemberConfigurationExpression<LookupInstructorCourseAttachmentForManagement> m)
			{
				m.MapFrom<List<LookupInstructorCourseStudentAttachmentForManagementDTO>>((LookupInstructorCourseAttachmentForManagement pbdto) => (pbdto.Students == null) ? null : (from g in pbdto.Students
				select g.ToDTO()).ToList<LookupInstructorCourseStudentAttachmentForManagementDTO>());
			});
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00012318 File Offset: 0x00010518
		public static LookupInstructorCourseAttachmentForManagement ToDomainObject(this LookupInstructorCourseAttachmentForManagementDTO sessionDTO)
		{
			return Mapper.Map<LookupInstructorCourseAttachmentForManagementDTO, LookupInstructorCourseAttachmentForManagement>(sessionDTO);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012330 File Offset: 0x00010530
		public static LookupInstructorCourseAttachmentForManagementDTO ToDTO(this LookupInstructorCourseAttachmentForManagement session)
		{
			return Mapper.Map<LookupInstructorCourseAttachmentForManagement, LookupInstructorCourseAttachmentForManagementDTO>(session);
		}
	}
}
