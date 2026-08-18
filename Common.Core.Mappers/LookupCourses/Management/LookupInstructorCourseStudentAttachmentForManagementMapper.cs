using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses.Management;
using TechnoPro.Common.Public.Entities.LookupCourses.Management;

namespace TechnoPro.Common.Core.Mappers.LookupCourses.Management
{
	// Token: 0x020000E4 RID: 228
	public static class LookupInstructorCourseStudentAttachmentForManagementMapper
	{
		// Token: 0x060003C5 RID: 965 RVA: 0x00012348 File Offset: 0x00010548
		static LookupInstructorCourseStudentAttachmentForManagementMapper()
		{
			Mapper.CreateMap<LookupInstructorCourseStudentAttachmentForManagementDTO, LookupInstructorCourseStudentAttachmentForManagement>();
			Mapper.CreateMap<LookupInstructorCourseStudentAttachmentForManagement, LookupInstructorCourseStudentAttachmentForManagementDTO>();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00012358 File Offset: 0x00010558
		public static LookupInstructorCourseStudentAttachmentForManagement ToDomainObject(this LookupInstructorCourseStudentAttachmentForManagementDTO sessionDTO)
		{
			return Mapper.Map<LookupInstructorCourseStudentAttachmentForManagementDTO, LookupInstructorCourseStudentAttachmentForManagement>(sessionDTO);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012370 File Offset: 0x00010570
		public static LookupInstructorCourseStudentAttachmentForManagementDTO ToDTO(this LookupInstructorCourseStudentAttachmentForManagement session)
		{
			return Mapper.Map<LookupInstructorCourseStudentAttachmentForManagement, LookupInstructorCourseStudentAttachmentForManagementDTO>(session);
		}
	}
}
