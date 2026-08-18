using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x0200012F RID: 303
	public static class FormApprovalForAppointmentMapper
	{
		// Token: 0x06000531 RID: 1329 RVA: 0x0001938C File Offset: 0x0001758C
		static FormApprovalForAppointmentMapper()
		{
			FormApprovalCommentMapper.CreateMap();
			FormApprovalApprovedInfoMapper.CreateMap();
			Mapper.CreateMap<FormApprovalForAppointmentDTO, FormApprovalForAppointment>().ForMember((FormApprovalForAppointment pb) => pb.Comments, delegate(IMemberConfigurationExpression<FormApprovalForAppointmentDTO> m)
			{
				m.MapFrom<List<FormApprovalComment>>((FormApprovalForAppointmentDTO pbdto) => (pbdto.Comments == null) ? null : (from g in pbdto.Comments
				select g.ToDomainObject()).ToList<FormApprovalComment>());
			}).ForMember((FormApprovalForAppointment pb) => pb.ApprovalInfo, delegate(IMemberConfigurationExpression<FormApprovalForAppointmentDTO> m)
			{
				m.MapFrom<FormApprovalApprovedInfo>((FormApprovalForAppointmentDTO pbdto) => (pbdto.ApprovalInfo == null) ? null : pbdto.ApprovalInfo.ToDomainObject());
			});
			Mapper.CreateMap<FormApprovalForAppointment, FormApprovalForAppointmentDTO>().ForMember((FormApprovalForAppointmentDTO pb) => pb.Comments, delegate(IMemberConfigurationExpression<FormApprovalForAppointment> m)
			{
				m.MapFrom<List<FormApprovalCommentDTO>>((FormApprovalForAppointment pbdto) => (pbdto.Comments == null) ? null : (from g in pbdto.Comments
				select g.ToDTO()).ToList<FormApprovalCommentDTO>());
			}).ForMember((FormApprovalForAppointmentDTO pb) => pb.ApprovalInfo, delegate(IMemberConfigurationExpression<FormApprovalForAppointment> m)
			{
				m.MapFrom<FormApprovalApprovedInfoDTO>((FormApprovalForAppointment pbdto) => (pbdto.ApprovalInfo == null) ? null : pbdto.ApprovalInfo.ToDTO());
			});
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x000194EC File Offset: 0x000176EC
		public static FormApprovalForAppointment ToDomainObject(this FormApprovalForAppointmentDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalForAppointmentDTO, FormApprovalForAppointment>(dynamicDataDTO);
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00019504 File Offset: 0x00017704
		public static FormApprovalForAppointmentDTO ToDTO(this FormApprovalForAppointment dynamicData)
		{
			return Mapper.Map<FormApprovalForAppointment, FormApprovalForAppointmentDTO>(dynamicData);
		}
	}
}
