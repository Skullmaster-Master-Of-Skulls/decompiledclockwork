using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007F RID: 127
	public static class ServiceRequestMapper
	{
		// Token: 0x06000228 RID: 552 RVA: 0x0000C8F0 File Offset: 0x0000AAF0
		static ServiceRequestMapper()
		{
			ServiceRequestBaseMapper.CreateMap();
			ServiceProviderTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			ServiceProviderRequestDetailBaseMapper.CreateMap();
			ServiceRequestPartBaseMapper.CreateMap();
			Mapper.CreateMap<ServiceRequestDTO, ServiceRequest>().ForMember((ServiceRequest ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceRequestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceRequest, ServiceRequestDTO>();
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C98C File Offset: 0x0000AB8C
		public static ServiceRequest ToDomainObject(this ServiceRequestDTO dto)
		{
			return Mapper.Map<ServiceRequestDTO, ServiceRequest>(dto);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C9A4 File Offset: 0x0000ABA4
		public static ServiceRequestDTO ToDTO(this ServiceRequest item)
		{
			return Mapper.Map<ServiceRequest, ServiceRequestDTO>(item);
		}
	}
}
