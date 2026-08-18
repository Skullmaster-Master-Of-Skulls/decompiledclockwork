using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Public.Entities.Files;
using TechnoPro.Common.Public.Entities.Templates;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x02000039 RID: 57
	public static class TemplateMapper
	{
		// Token: 0x060000EC RID: 236 RVA: 0x00006C90 File Offset: 0x00004E90
		static TemplateMapper()
		{
			BaseTemplateMapper.CreateMap();
			BinaryFileMapper.CreateMap();
			TPMailMessageMapper.CreateMap();
			BinaryFileMapper.CreateMap();
			TemplateGroupMapper.CreateMap();
			Mapper.CreateMap<TemplateDTO, Template>().ForMember((Template pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.Ignore();
			}).ForMember((Template pb) => pb.TemplateGroupId, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.Ignore();
			}).ForMember((Template pb) => (object)pb.IsEmpty, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.Ignore();
			}).ForMember((Template pb) => (object)pb.IsTproTemplate, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.Ignore();
			}).ForMember((Template pb) => pb.EmailBehindDocumentTemplate, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.MapFrom<TPMailMessage>((TemplateDTO pbdto) => (pbdto.EmailBehindDocumentTemplate == null) ? null : pbdto.EmailBehindDocumentTemplate.ToDomainObject());
			}).ForMember((Template pb) => pb.EmailTemplate, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.MapFrom<TPMailMessage>((TemplateDTO pbdto) => (pbdto.EmailTemplate == null) ? null : pbdto.EmailTemplate.ToDomainObject());
			}).ForMember((Template pb) => pb.Group, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.MapFrom<TemplateGroup>((TemplateDTO pbdto) => (pbdto.Group == null) ? null : pbdto.Group.ToDomainObject());
			}).ForMember((Template pb) => pb.Document, delegate(IMemberConfigurationExpression<TemplateDTO> m)
			{
				m.MapFrom<BinaryFile>((TemplateDTO pbdto) => (pbdto.Document == null) ? null : pbdto.Document.ToDomainObject());
			});
			Mapper.CreateMap<Template, TemplateDTO>().ForMember((TemplateDTO pb) => pb.TemplateGroupId, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.Ignore();
			}).ForMember((TemplateDTO pb) => (object)pb.IsEmpty, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.Ignore();
			}).ForMember((TemplateDTO pb) => pb.EmailBehindDocumentTemplate, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.MapFrom<TPMailMessageDTO>((Template pbdto) => (pbdto.EmailBehindDocumentTemplate == null) ? null : pbdto.EmailBehindDocumentTemplate.ToDTO());
			}).ForMember((TemplateDTO pb) => pb.EmailTemplate, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.MapFrom<TPMailMessageDTO>((Template pbdto) => (pbdto.EmailTemplate == null) ? null : pbdto.EmailTemplate.ToDTO());
			}).ForMember((TemplateDTO pb) => pb.Group, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.MapFrom<TemplateGroupDTO>((Template pbdto) => (pbdto.Group == null) ? null : pbdto.Group.ToDTO());
			}).ForMember((TemplateDTO pb) => pb.Document, delegate(IMemberConfigurationExpression<Template> m)
			{
				m.MapFrom<BinaryFileDTO>((Template pbdto) => (pbdto.Document == null) ? null : pbdto.Document.ToDTO());
			});
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007150 File Offset: 0x00005350
		public static Template ToDomainObject(this TemplateDTO dto)
		{
			return Mapper.Map<TemplateDTO, Template>(dto);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007168 File Offset: 0x00005368
		public static TemplateDTO ToDTO(this Template docTemplate)
		{
			return Mapper.Map<Template, TemplateDTO>(docTemplate);
		}
	}
}
