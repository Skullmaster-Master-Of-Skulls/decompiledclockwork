using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Veteran;
using TechnoPro.Common.Public.Entities.Veteran;

namespace TechnoPro.Common.Core.Mappers.Veteran
{
	// Token: 0x02000014 RID: 20
	public static class ChangeInBenefitRequestMapper
	{
		// Token: 0x06000056 RID: 86 RVA: 0x00003D60 File Offset: 0x00001F60
		static ChangeInBenefitRequestMapper()
		{
			Mapper.CreateMap<ChangeInBenefitRequestDTO, ChangeInBenefitRequest>().ForMember((ChangeInBenefitRequest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ChangeInBenefitRequestDTO> m)
			{
				m.Ignore();
			}).ForMember((ChangeInBenefitRequest pb) => (object)pb.SecondId, delegate(IMemberConfigurationExpression<ChangeInBenefitRequestDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<ChangeInBenefitRequest, ChangeInBenefitRequestDTO>();
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003E40 File Offset: 0x00002040
		public static ChangeInBenefitRequest ToDomainObject(this ChangeInBenefitRequestDTO dto)
		{
			return Mapper.Map<ChangeInBenefitRequestDTO, ChangeInBenefitRequest>(dto);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003E58 File Offset: 0x00002058
		public static ChangeInBenefitRequestDTO ToDTO(this ChangeInBenefitRequest item)
		{
			return Mapper.Map<ChangeInBenefitRequest, ChangeInBenefitRequestDTO>(item);
		}
	}
}
