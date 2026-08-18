using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x0200012E RID: 302
	public static class FormApprovalCommentTextMapper
	{
		// Token: 0x0600052D RID: 1325 RVA: 0x0001934C File Offset: 0x0001754C
		static FormApprovalCommentTextMapper()
		{
			Mapper.CreateMap<FormApprovalCommentTextDTO, FormApprovalCommentText>();
			Mapper.CreateMap<FormApprovalCommentText, FormApprovalCommentTextDTO>();
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0001935C File Offset: 0x0001755C
		public static FormApprovalCommentText ToDomainObject(this FormApprovalCommentTextDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalCommentTextDTO, FormApprovalCommentText>(dynamicDataDTO);
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00019374 File Offset: 0x00017574
		public static FormApprovalCommentTextDTO ToDTO(this FormApprovalCommentText dynamicData)
		{
			return Mapper.Map<FormApprovalCommentText, FormApprovalCommentTextDTO>(dynamicData);
		}
	}
}
