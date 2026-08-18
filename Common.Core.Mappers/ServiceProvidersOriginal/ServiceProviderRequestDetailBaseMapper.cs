using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.ServiceProvidersOriginal;

namespace TechnoPro.Common.Core.Mappers.ServiceProvidersOriginal
{
	// Token: 0x0200007C RID: 124
	public static class ServiceProviderRequestDetailBaseMapper
	{
		// Token: 0x0600021C RID: 540 RVA: 0x0000C63C File Offset: 0x0000A83C
		static ServiceProviderRequestDetailBaseMapper()
		{
			BasicPersonMapper.CreateMap();
			Mapper.CreateMap<ServiceProviderRequestDetailBaseDTO, ServiceProviderRequestDetailBase>().ForMember((ServiceProviderRequestDetailBase ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<ServiceProviderRequestDetailBaseDTO> m)
			{
				m.Ignore();
			}).ForMember((ServiceProviderRequestDetailBase pb) => pb.CounsellorWhoEntered, delegate(IMemberConfigurationExpression<ServiceProviderRequestDetailBaseDTO> m)
			{
				m.MapFrom<BasicPerson>((ServiceProviderRequestDetailBaseDTO pbdto) => (pbdto.CounsellorWhoEntered == null) ? null : pbdto.CounsellorWhoEntered.ToDomainObject());
			});
			Mapper.CreateMap<ServiceProviderRequestDetailBase, ServiceProviderRequestDetailBaseDTO>().ForMember((ServiceProviderRequestDetailBaseDTO pb) => pb.CounsellorWhoEntered, delegate(IMemberConfigurationExpression<ServiceProviderRequestDetailBase> m)
			{
				m.MapFrom<BasicPersonDTO>((ServiceProviderRequestDetailBase pbdto) => (pbdto.CounsellorWhoEntered == null) ? null : pbdto.CounsellorWhoEntered.ToDTO());
			});
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000C75C File Offset: 0x0000A95C
		public static ServiceProviderRequestDetailBase ToDomainObject(this ServiceProviderRequestDetailBaseDTO dto)
		{
			return Mapper.Map<ServiceProviderRequestDetailBaseDTO, ServiceProviderRequestDetailBase>(dto);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000C774 File Offset: 0x0000A974
		public static ServiceProviderRequestDetailBaseDTO ToDTO(this ServiceProviderRequestDetailBase item)
		{
			return Mapper.Map<ServiceProviderRequestDetailBase, ServiceProviderRequestDetailBaseDTO>(item);
		}
	}
}
