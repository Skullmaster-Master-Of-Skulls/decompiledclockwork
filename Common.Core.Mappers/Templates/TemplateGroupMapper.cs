using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x02000038 RID: 56
	public static class TemplateGroupMapper
	{
		// Token: 0x060000E8 RID: 232 RVA: 0x00006B38 File Offset: 0x00004D38
		static TemplateGroupMapper()
		{
			Mapper.CreateMap<TemplateGroupDTO, TemplateGroup>().ForMember((TemplateGroup pb) => pb.Id, delegate(IMemberConfigurationExpression<TemplateGroupDTO> m)
			{
				m.Ignore();
			}).ForMember((TemplateGroup pb) => (object)pb.Meaning, delegate(IMemberConfigurationExpression<TemplateGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TemplateGroup, TemplateGroupDTO>().ForMember((TemplateGroupDTO pb) => (object)pb.Meaning, delegate(IMemberConfigurationExpression<TemplateGroup> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00006C60 File Offset: 0x00004E60
		public static TemplateGroup ToDomainObject(this TemplateGroupDTO dto)
		{
			return Mapper.Map<TemplateGroupDTO, TemplateGroup>(dto);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00006C78 File Offset: 0x00004E78
		public static TemplateGroupDTO ToDTO(this TemplateGroup docTemplate)
		{
			return Mapper.Map<TemplateGroup, TemplateGroupDTO>(docTemplate);
		}
	}
}
