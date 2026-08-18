using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x02000075 RID: 117
	public static class SPRequestMapper
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x0000BEFC File Offset: 0x0000A0FC
		static SPRequestMapper()
		{
			SPProviderTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			SPRequestStatusTypeMapper.CreateMap();
			SPRequestAssignmentStatusTypeMapper.CreateMap();
			SPUrgencyLevelTypeMapper.CreateMap();
			Mapper.CreateMap<SPRequest, SPRequestDTO>();
			Mapper.CreateMap<SPRequestDTO, SPRequest>().ForMember((SPRequest pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPRequestDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000BF98 File Offset: 0x0000A198
		public static SPRequest ToDomainObject(this SPRequestDTO dto)
		{
			return Mapper.Map<SPRequestDTO, SPRequest>(dto);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000BFB0 File Offset: 0x0000A1B0
		public static SPRequestDTO ToDTO(this SPRequest item)
		{
			return Mapper.Map<SPRequest, SPRequestDTO>(item);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public static IList<SPRequest> ToDomainObject(this IList<SPRequestDTO> list)
		{
			IList<SPRequest> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPRequest>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000C00C File Offset: 0x0000A20C
		public static IList<SPRequestDTO> ToDTO(this IList<SPRequest> list)
		{
			IList<SPRequestDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPRequestDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
