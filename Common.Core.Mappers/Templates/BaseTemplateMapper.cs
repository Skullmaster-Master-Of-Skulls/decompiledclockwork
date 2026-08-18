using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Core.Mappers.Files;
using TechnoPro.Common.Core.Mappers.TPMailMan;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x02000036 RID: 54
	public static class BaseTemplateMapper
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00006854 File Offset: 0x00004A54
		static BaseTemplateMapper()
		{
			TPMailMessageMapper.CreateMap();
			BinaryFileMapper.CreateMap();
			Mapper.CreateMap<BaseTemplateDTO, BaseTemplate>().ForMember((BaseTemplate pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<BaseTemplateDTO> m)
			{
				m.Ignore();
			}).ForMember((BaseTemplate pb) => pb.TemplateGroupId, delegate(IMemberConfigurationExpression<BaseTemplateDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<BaseTemplate, BaseTemplateDTO>().ForMember((BaseTemplateDTO pb) => pb.TemplateGroupId, delegate(IMemberConfigurationExpression<BaseTemplate> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006978 File Offset: 0x00004B78
		public static BaseTemplate ToDomainObject(this BaseTemplateDTO dto)
		{
			return Mapper.Map<BaseTemplateDTO, BaseTemplate>(dto);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00006990 File Offset: 0x00004B90
		public static BaseTemplateDTO ToDTO(this BaseTemplate docTemplate)
		{
			return Mapper.Map<BaseTemplate, BaseTemplateDTO>(docTemplate);
		}
	}
}
