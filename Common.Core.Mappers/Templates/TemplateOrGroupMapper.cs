using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x0200003B RID: 59
	public static class TemplateOrGroupMapper
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00007308 File Offset: 0x00005508
		static TemplateOrGroupMapper()
		{
			TemplateMapper.CreateMap();
			TemplateGroupMapper.CreateMap();
			Mapper.CreateMap<TemplateOrGroupDTO, TemplateOrGroup>().ForMember((TemplateOrGroup pb) => pb.Item, delegate(IMemberConfigurationExpression<TemplateOrGroupDTO> m)
			{
				m.Ignore();
			}).ForMember((TemplateOrGroup pb) => pb.Template, delegate(IMemberConfigurationExpression<TemplateOrGroupDTO> m)
			{
				m.MapFrom<Template>((TemplateOrGroupDTO q) => (q.Template == null) ? null : q.Template.ToDomainObject());
			}).ForMember((TemplateOrGroup pb) => pb.Group, delegate(IMemberConfigurationExpression<TemplateOrGroupDTO> m)
			{
				m.MapFrom<TemplateGroup>((TemplateOrGroupDTO q) => (q.Group == null) ? null : q.Group.ToDomainObject());
			});
			Mapper.CreateMap<TemplateOrGroup, TemplateOrGroupDTO>().ForMember((TemplateOrGroupDTO pb) => pb.Item, delegate(IMemberConfigurationExpression<TemplateOrGroup> m)
			{
				m.Ignore();
			}).ForMember((TemplateOrGroupDTO pb) => pb.Template, delegate(IMemberConfigurationExpression<TemplateOrGroup> m)
			{
				m.MapFrom<TemplateDTO>((TemplateOrGroup q) => (q.Template == null) ? null : q.Template.ToDTO());
			}).ForMember((TemplateOrGroupDTO pb) => pb.Group, delegate(IMemberConfigurationExpression<TemplateOrGroup> m)
			{
				m.MapFrom<TemplateGroupDTO>((TemplateOrGroup q) => (q.Group == null) ? null : q.Group.ToDTO());
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00007504 File Offset: 0x00005704
		public static TemplateOrGroup ToDomainObject(this TemplateOrGroupDTO dto)
		{
			return Mapper.Map<TemplateOrGroupDTO, TemplateOrGroup>(dto);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x0000751C File Offset: 0x0000571C
		public static TemplateOrGroupDTO ToDTO(this TemplateOrGroup docTemplate)
		{
			return Mapper.Map<TemplateOrGroup, TemplateOrGroupDTO>(docTemplate);
		}
	}
}
