using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x02000131 RID: 305
	public static class FormApprovalPendingItemMapper
	{
		// Token: 0x06000539 RID: 1337 RVA: 0x0001955C File Offset: 0x0001775C
		static FormApprovalPendingItemMapper()
		{
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<FormApprovalPendingItemDTO, FormApprovalPendingItem>().ForMember((FormApprovalPendingItem pb) => pb.Student, delegate(IMemberConfigurationExpression<FormApprovalPendingItemDTO> m)
			{
				m.MapFrom<BasicPerson>((FormApprovalPendingItemDTO pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDomainObject());
			});
			Mapper.CreateMap<FormApprovalPendingItem, FormApprovalPendingItemDTO>().ForMember((FormApprovalPendingItemDTO pb) => pb.Student, delegate(IMemberConfigurationExpression<FormApprovalPendingItem> m)
			{
				m.MapFrom<BasicPersonDTO>((FormApprovalPendingItem pbdto) => (pbdto.Student == null) ? null : pbdto.Student.ToDTO());
			});
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x00019618 File Offset: 0x00017818
		public static FormApprovalPendingItem ToDomainObject(this FormApprovalPendingItemDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalPendingItemDTO, FormApprovalPendingItem>(dynamicDataDTO);
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x00019630 File Offset: 0x00017830
		public static FormApprovalPendingItemDTO ToDTO(this FormApprovalPendingItem dynamicData)
		{
			return Mapper.Map<FormApprovalPendingItem, FormApprovalPendingItemDTO>(dynamicData);
		}
	}
}
