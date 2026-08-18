using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006B RID: 107
	public static class SPApplicationMapper
	{
		// Token: 0x060001BA RID: 442 RVA: 0x0000B258 File Offset: 0x00009458
		static SPApplicationMapper()
		{
			SPProviderMapper.CreateMap();
			SPProviderTypeMapper.CreateMap();
			SPApplicationAvailabilityTypeMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			SPRateOfPayTypeMapper.CreateMap();
			Mapper.CreateMap<SPApplication, SPApplicationDTO>();
			Mapper.CreateMap<SPApplicationDTO, SPApplication>().ForMember((SPApplication pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPApplicationDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public static SPApplication ToDomainObject(this SPApplicationDTO dto)
		{
			return Mapper.Map<SPApplicationDTO, SPApplication>(dto);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000B30C File Offset: 0x0000950C
		public static SPApplicationDTO ToDTO(this SPApplication item)
		{
			return Mapper.Map<SPApplication, SPApplicationDTO>(item);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000B324 File Offset: 0x00009524
		public static IList<SPApplication> ToDomainObject(this IList<SPApplicationDTO> list)
		{
			IList<SPApplication> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPApplication>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000B368 File Offset: 0x00009568
		public static IList<SPApplicationDTO> ToDTO(this IList<SPApplication> list)
		{
			IList<SPApplicationDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPApplicationDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
