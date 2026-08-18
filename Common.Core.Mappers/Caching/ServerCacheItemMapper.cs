using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Caching;
using TechnoPro.Common.Public.Entities.Caching;

namespace TechnoPro.Common.Core.Mappers.Caching
{
	// Token: 0x0200017C RID: 380
	public static class ServerCacheItemMapper
	{
		// Token: 0x06000685 RID: 1669 RVA: 0x0001DBA8 File Offset: 0x0001BDA8
		static ServerCacheItemMapper()
		{
			Mapper.CreateMap<ServerCacheItemDTO, ServerCacheItem>().ForMember((ServerCacheItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ServerCacheItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServerCacheItem, ServerCacheItemDTO>();
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001DC24 File Offset: 0x0001BE24
		public static ServerCacheItem ToDomainObject(this ServerCacheItemDTO dynamicFormDTO)
		{
			return Mapper.Map<ServerCacheItemDTO, ServerCacheItem>(dynamicFormDTO);
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001DC3C File Offset: 0x0001BE3C
		public static ServerCacheItemDTO ToDTO(this ServerCacheItem dynamicForm)
		{
			return Mapper.Map<ServerCacheItem, ServerCacheItemDTO>(dynamicForm);
		}
	}
}
