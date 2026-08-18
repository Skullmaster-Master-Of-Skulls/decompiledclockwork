using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007B RID: 123
	public static class ServiceProviderMapper
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000C504 File Offset: 0x0000A704
		static ServiceProviderMapper()
		{
			ServiceProviderBaseMapper.CreateMap();
			Mapper.CreateMap<ServiceProviderDTO, ServiceProvider>().ForMember((ServiceProvider ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceProviderDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceProvider, ServiceProviderDTO>();
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000C584 File Offset: 0x0000A784
		public static ServiceProvider ToDomainObject(this ServiceProviderDTO dto)
		{
			return Mapper.Map<ServiceProviderDTO, ServiceProvider>(dto);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000C59C File Offset: 0x0000A79C
		public static ServiceProviderDTO ToDTO(this ServiceProvider item)
		{
			return Mapper.Map<ServiceProvider, ServiceProviderDTO>(item);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		public static IList<ServiceProvider> ToDomainObject(this IList<ServiceProviderDTO> list)
		{
			IList<ServiceProvider> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<ServiceProvider>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public static IList<ServiceProviderDTO> ToDTO(this IList<ServiceProvider> list)
		{
			IList<ServiceProviderDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<ServiceProviderDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
