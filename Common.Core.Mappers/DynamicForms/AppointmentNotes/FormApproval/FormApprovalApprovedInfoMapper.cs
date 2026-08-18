using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x0200012C RID: 300
	public static class FormApprovalApprovedInfoMapper
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x000190D0 File Offset: 0x000172D0
		static FormApprovalApprovedInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<FormApprovalApprovedInfoDTO, FormApprovalApprovedInfo>().ForMember((FormApprovalApprovedInfo pb) => pb.WhoApproved, delegate(IMemberConfigurationExpression<FormApprovalApprovedInfoDTO> m)
			{
				m.MapFrom<PersonBase>((FormApprovalApprovedInfoDTO pbdto) => (pbdto.WhoApproved == null) ? null : pbdto.WhoApproved.ToDomainObject());
			});
			Mapper.CreateMap<FormApprovalApprovedInfo, FormApprovalApprovedInfoDTO>().ForMember((FormApprovalApprovedInfoDTO pb) => pb.WhoApproved, delegate(IMemberConfigurationExpression<FormApprovalApprovedInfo> m)
			{
				m.MapFrom<PersonBaseDTO>((FormApprovalApprovedInfo pbdto) => (pbdto.WhoApproved == null) ? null : pbdto.WhoApproved.ToDTO());
			});
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001918C File Offset: 0x0001738C
		public static FormApprovalApprovedInfo ToDomainObject(this FormApprovalApprovedInfoDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalApprovedInfoDTO, FormApprovalApprovedInfo>(dynamicDataDTO);
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x000191A4 File Offset: 0x000173A4
		public static FormApprovalApprovedInfoDTO ToDTO(this FormApprovalApprovedInfo dynamicData)
		{
			return Mapper.Map<FormApprovalApprovedInfo, FormApprovalApprovedInfoDTO>(dynamicData);
		}
	}
}
