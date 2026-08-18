using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x02000079 RID: 121
	public static class ServiceProviderAssignmentMapper
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000C398 File Offset: 0x0000A598
		static ServiceProviderAssignmentMapper()
		{
			ServiceRequestMapper.CreateMap();
			StudentCommonInfoMapper.CreateMap();
			ServiceProviderMapper.CreateMap();
			Mapper.CreateMap<ServiceProviderAssignmentDTO, ServiceProviderAssignment>().ForMember((ServiceProviderAssignment ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceProviderAssignmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ServiceProviderAssignment, ServiceProviderAssignmentDTO>();
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0000C428 File Offset: 0x0000A628
		public static ServiceProviderAssignment ToDomainObject(this ServiceProviderAssignmentDTO dto)
		{
			return Mapper.Map<ServiceProviderAssignmentDTO, ServiceProviderAssignment>(dto);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000C440 File Offset: 0x0000A640
		public static ServiceProviderAssignmentDTO ToDTO(this ServiceProviderAssignment item)
		{
			return Mapper.Map<ServiceProviderAssignment, ServiceProviderAssignmentDTO>(item);
		}
	}
}
