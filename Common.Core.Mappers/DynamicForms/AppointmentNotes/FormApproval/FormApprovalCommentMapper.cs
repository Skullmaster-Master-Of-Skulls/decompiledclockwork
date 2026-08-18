using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x0200012D RID: 301
	public static class FormApprovalCommentMapper
	{
		// Token: 0x06000529 RID: 1321 RVA: 0x000191BC File Offset: 0x000173BC
		static FormApprovalCommentMapper()
		{
			BasicPersonMapper.CreateMap();
			FormApprovalCommentTextMapper.CreateMap();
			Mapper.CreateMap<FormApprovalCommentDTO, FormApprovalComment>().ForMember((FormApprovalComment pb) => pb.Comment, delegate(IMemberConfigurationExpression<FormApprovalCommentDTO> m)
			{
				m.MapFrom<FormApprovalCommentText>((FormApprovalCommentDTO pbdto) => (pbdto.Comment == null) ? null : pbdto.Comment.ToDomainObject());
			}).ForMember((FormApprovalComment pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<FormApprovalCommentDTO> m)
			{
				m.MapFrom<BasicPerson>((FormApprovalCommentDTO pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDomainObject());
			});
			Mapper.CreateMap<FormApprovalComment, FormApprovalCommentDTO>().ForMember((FormApprovalCommentDTO pb) => pb.Comment, delegate(IMemberConfigurationExpression<FormApprovalComment> m)
			{
				m.MapFrom<FormApprovalCommentTextDTO>((FormApprovalComment pbdto) => (pbdto.Comment == null) ? null : pbdto.Comment.ToDTO());
			}).ForMember((FormApprovalCommentDTO pb) => pb.WhoEntered, delegate(IMemberConfigurationExpression<FormApprovalComment> m)
			{
				m.MapFrom<BasicPersonDTO>((FormApprovalComment pbdto) => (pbdto.WhoEntered == null) ? null : pbdto.WhoEntered.ToDTO());
			});
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0001931C File Offset: 0x0001751C
		public static FormApprovalComment ToDomainObject(this FormApprovalCommentDTO dynamicDataDTO)
		{
			return Mapper.Map<FormApprovalCommentDTO, FormApprovalComment>(dynamicDataDTO);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x00019334 File Offset: 0x00017534
		public static FormApprovalCommentDTO ToDTO(this FormApprovalComment dynamicData)
		{
			return Mapper.Map<FormApprovalComment, FormApprovalCommentDTO>(dynamicData);
		}
	}
}
