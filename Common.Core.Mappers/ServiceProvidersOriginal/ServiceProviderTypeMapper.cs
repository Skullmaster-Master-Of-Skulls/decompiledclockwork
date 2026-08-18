using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007D RID: 125
	public static class ServiceProviderTypeMapper
	{
		// Token: 0x06000220 RID: 544 RVA: 0x0000C78C File Offset: 0x0000A98C
		static ServiceProviderTypeMapper()
		{
			Mapper.CreateMap<ServiceProviderTypeDTO, ServiceProviderType>().ForMember((ServiceProviderType ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceProviderTypeDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceProviderType, ServiceProviderTypeDTO>();
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C808 File Offset: 0x0000AA08
		public static ServiceProviderType ToDomainObject(this ServiceProviderTypeDTO dto)
		{
			return Mapper.Map<ServiceProviderTypeDTO, ServiceProviderType>(dto);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000C820 File Offset: 0x0000AA20
		public static ServiceProviderTypeDTO ToDTO(this ServiceProviderType item)
		{
			return Mapper.Map<ServiceProviderType, ServiceProviderTypeDTO>(item);
		}
	}
}
