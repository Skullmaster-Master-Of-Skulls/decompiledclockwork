using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x02000132 RID: 306
	public static class FormApprovalScreenUserOptionsMapper
	{
		// Token: 0x0600053D RID: 1341 RVA: 0x00019648 File Offset: 0x00017848
		static FormApprovalScreenUserOptionsMapper()
		{
			Mapper.CreateMap<FormApprovalScreenUserOptionsDTO, FormApprovalScreenUserOptions>();
			Mapper.CreateMap<FormApprovalScreenUserOptions, FormApprovalScreenUserOptionsDTO>();
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x00019658 File Offset: 0x00017858
		public static FormApprovalScreenUserOptions ToDomainObject(this FormApprovalScreenUserOptionsDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalScreenUserOptionsDTO, FormApprovalScreenUserOptions>(dynamicDataDTO);
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x00019670 File Offset: 0x00017870
		public static FormApprovalScreenUserOptionsDTO ToDTO(this FormApprovalScreenUserOptions dynamicData)
		{
			return Mapper.Map<FormApprovalScreenUserOptions, FormApprovalScreenUserOptionsDTO>(dynamicData);
		}
	}
}
