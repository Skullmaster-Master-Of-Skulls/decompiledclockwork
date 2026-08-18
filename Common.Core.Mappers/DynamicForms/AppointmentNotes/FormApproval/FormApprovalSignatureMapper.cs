using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x02000133 RID: 307
	public static class FormApprovalSignatureMapper
	{
		// Token: 0x06000541 RID: 1345 RVA: 0x00019688 File Offset: 0x00017888
		static FormApprovalSignatureMapper()
		{
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<FormApprovalSignatureDTO, FormApprovalSignature>().ForMember((FormApprovalSignature pb) => pb.WhoSigned, delegate(IMemberConfigurationExpression<FormApprovalSignatureDTO> m)
			{
				m.MapFrom<BasicPerson>((FormApprovalSignatureDTO pbdto) => (pbdto.WhoSigned == null) ? null : pbdto.WhoSigned.ToDomainObject());
			});
			Mapper.CreateMap<FormApprovalSignature, FormApprovalSignatureDTO>().ForMember((FormApprovalSignatureDTO pb) => pb.WhoSigned, delegate(IMemberConfigurationExpression<FormApprovalSignature> m)
			{
				m.MapFrom<BasicPersonDTO>((FormApprovalSignature pbdto) => (pbdto.WhoSigned == null) ? null : pbdto.WhoSigned.ToDTO());
			});
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x00019744 File Offset: 0x00017944
		public static FormApprovalSignature ToDomainObject(this FormApprovalSignatureDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalSignatureDTO, FormApprovalSignature>(dynamicDataDTO);
		}

		// Token: 0x06000544 RID: 1348 RVA: 0x0001975C File Offset: 0x0001795C
		public static FormApprovalSignatureDTO ToDTO(this FormApprovalSignature dynamicData)
		{
			return Mapper.Map<FormApprovalSignature, FormApprovalSignatureDTO>(dynamicData);
		}
	}
}
