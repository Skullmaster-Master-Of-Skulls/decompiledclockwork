using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007E RID: 126
	public static class ServiceRequestBaseMapper
	{
		// Token: 0x06000224 RID: 548 RVA: 0x0000C838 File Offset: 0x0000AA38
		static ServiceRequestBaseMapper()
		{
			PersonBaseMapper.CreateMap();
			LookupCourseBaseMapper.CreateMap();
			Mapper.CreateMap<ServiceRequestBaseDTO, ServiceRequestBase>().ForMember((ServiceRequestBase ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceRequestBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceRequestBase, ServiceRequestBaseDTO>();
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C8C0 File Offset: 0x0000AAC0
		public static ServiceRequestBase ToDomainObject(this ServiceRequestBaseDTO dto)
		{
			return Mapper.Map<ServiceRequestBaseDTO, ServiceRequestBase>(dto);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C8D8 File Offset: 0x0000AAD8
		public static ServiceRequestBaseDTO ToDTO(this ServiceRequestBase item)
		{
			return Mapper.Map<ServiceRequestBase, ServiceRequestBaseDTO>(item);
		}
	}
}
