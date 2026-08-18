using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000123 RID: 291
	public static class DynamicListItemMapper
	{
		// Token: 0x060004FF RID: 1279 RVA: 0x00018384 File Offset: 0x00016584
		static DynamicListItemMapper()
		{
			DynamicListGroupMapper.CreateMap();
			Mapper.CreateMap<DynamicListItemDTO, DynamicListItem>().ForMember((DynamicListItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DynamicListItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DynamicListItem, DynamicListItemDTO>();
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00018408 File Offset: 0x00016608
		public static DynamicListItem ToDomainObject(this DynamicListItemDTO dto)
		{
			return Mapper.Map<DynamicListItemDTO, DynamicListItem>(dto);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00018420 File Offset: 0x00016620
		public static DynamicListItemDTO ToDTO(this DynamicListItem entity)
		{
			return Mapper.Map<DynamicListItem, DynamicListItemDTO>(entity);
		}
	}
}
