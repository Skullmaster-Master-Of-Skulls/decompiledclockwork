using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x02000130 RID: 304
	public static class FormApprovalOptionsMapper
	{
		// Token: 0x06000535 RID: 1333 RVA: 0x0001951C File Offset: 0x0001771C
		static FormApprovalOptionsMapper()
		{
			Mapper.CreateMap<FormApprovalOptionsDTO, FormApprovalOptions>();
			Mapper.CreateMap<FormApprovalOptions, FormApprovalOptionsDTO>();
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001952C File Offset: 0x0001772C
		public static FormApprovalOptions ToDomainObject(this FormApprovalOptionsDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalOptionsDTO, FormApprovalOptions>(dynamicDataDTO);
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00019544 File Offset: 0x00017744
		public static FormApprovalOptionsDTO ToDTO(this FormApprovalOptions dynamicData)
		{
			return Mapper.Map<FormApprovalOptions, FormApprovalOptionsDTO>(dynamicData);
		}
	}
}
