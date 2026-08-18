using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x02000080 RID: 128
	public static class ServiceRequestPartBaseMapper
	{
		// Token: 0x0600022C RID: 556 RVA: 0x0000C9BC File Offset: 0x0000ABBC
		static ServiceRequestPartBaseMapper()
		{
			Mapper.CreateMap<ServiceRequestPartBaseDTO, ServiceRequestPartBase>().ForMember((ServiceRequestPartBase ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceRequestPartBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceRequestPartBase, ServiceRequestPartBaseDTO>();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000CA38 File Offset: 0x0000AC38
		public static ServiceRequestPartBase ToDomainObject(this ServiceRequestPartBaseDTO dto)
		{
			return Mapper.Map<ServiceRequestPartBaseDTO, ServiceRequestPartBase>(dto);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public static ServiceRequestPartBaseDTO ToDTO(this ServiceRequestPartBase item)
		{
			return Mapper.Map<ServiceRequestPartBase, ServiceRequestPartBaseDTO>(item);
		}
	}
}
