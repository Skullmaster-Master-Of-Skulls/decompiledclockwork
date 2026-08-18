using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Templates;
using TechnoPro.Common.Public.Entities.Templates;

namespace TechnoPro.Common.Core.Mappers.Templates
{
	// Token: 0x02000037 RID: 55
	public static class TemplateCollectionMapper
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x000069A8 File Offset: 0x00004BA8
		static TemplateCollectionMapper()
		{
			TemplateMapper.CreateMap();
			TemplateGroupMapper.CreateMap();
			Mapper.CreateMap<TemplateCollectionDTO, TemplateCollection>().ForMember((TemplateCollection pb) => pb.Groups, delegate(IMemberConfigurationExpression<TemplateCollectionDTO> m)
			{
				m.MapFrom<List<TemplateGroup>>((TemplateCollectionDTO q) => (q.Groups == null) ? null : q.Groups.ToList<TemplateGroupDTO>().ConvertAll<TemplateGroup>((TemplateGroupDTO g) => g.ToDomainObject()));
			}).ForMember((TemplateCollection pb) => pb.Templates, delegate(IMemberConfigurationExpression<TemplateCollectionDTO> m)
			{
				m.MapFrom<List<Template>>((TemplateCollectionDTO q) => (q.Templates == null) ? null : q.Templates.ToList<TemplateDTO>().ConvertAll<Template>((TemplateDTO g) => g.ToDomainObject()));
			});
			Mapper.CreateMap<TemplateCollection, TemplateCollectionDTO>().ForMember((TemplateCollectionDTO pb) => pb.Groups, delegate(IMemberConfigurationExpression<TemplateCollection> m)
			{
				m.MapFrom<List<TemplateGroupDTO>>((TemplateCollection q) => (q.Groups == null) ? null : q.Groups.ToList<TemplateGroup>().ConvertAll<TemplateGroupDTO>((TemplateGroup g) => g.ToDTO()));
			}).ForMember((TemplateCollectionDTO pb) => pb.Templates, delegate(IMemberConfigurationExpression<TemplateCollection> m)
			{
				m.MapFrom<List<TemplateDTO>>((TemplateCollection q) => (q.Templates == null) ? null : q.Templates.ToList<Template>().ConvertAll<TemplateDTO>((Template g) => g.ToDTO()));
			});
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00006B08 File Offset: 0x00004D08
		public static TemplateCollection ToDomainObject(this TemplateCollectionDTO dto)
		{
			return Mapper.Map<TemplateCollectionDTO, TemplateCollection>(dto);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x00006B20 File Offset: 0x00004D20
		public static TemplateCollectionDTO ToDTO(this TemplateCollection docTemplate)
		{
			return Mapper.Map<TemplateCollection, TemplateCollectionDTO>(docTemplate);
		}
	}
}
