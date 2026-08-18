using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007A RID: 122
	public static class ServiceProviderBaseMapper
	{
		// Token: 0x06000212 RID: 530 RVA: 0x0000C458 File Offset: 0x0000A658
		static ServiceProviderBaseMapper()
		{
			Mapper.CreateMap<ServiceProviderBaseDTO, ServiceProviderBase>().ForMember((ServiceProviderBase ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceProviderBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceProviderBase, ServiceProviderBaseDTO>();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		public static ServiceProviderBase ToDomainObject(this ServiceProviderBaseDTO dto)
		{
			return Mapper.Map<ServiceProviderBaseDTO, ServiceProviderBase>(dto);
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000C4EC File Offset: 0x0000A6EC
		public static ServiceProviderBaseDTO ToDTO(this ServiceProviderBase item)
		{
			return Mapper.Map<ServiceProviderBase, ServiceProviderBaseDTO>(item);
		}
	}
}
