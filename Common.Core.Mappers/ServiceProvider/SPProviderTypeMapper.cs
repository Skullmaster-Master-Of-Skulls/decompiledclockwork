using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006E RID: 110
	public static class SPProviderTypeMapper
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000B630 File Offset: 0x00009830
		static SPProviderTypeMapper()
		{
			Mapper.CreateMap<SPProviderType, SPProviderTypeDTO>();
			Mapper.CreateMap<SPProviderTypeDTO, SPProviderType>().ForMember((SPProviderType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPProviderTypeDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000B6AC File Offset: 0x000098AC
		public static SPProviderType ToDomainObject(this SPProviderTypeDTO dto)
		{
			return Mapper.Map<SPProviderTypeDTO, SPProviderType>(dto);
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public static SPProviderTypeDTO ToDTO(this SPProviderType item)
		{
			return Mapper.Map<SPProviderType, SPProviderTypeDTO>(item);
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000B6DC File Offset: 0x000098DC
		public static IList<SPProviderType> ToDomainObject(this IList<SPProviderTypeDTO> list)
		{
			IList<SPProviderType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPProviderType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000B720 File Offset: 0x00009920
		public static IList<SPProviderTypeDTO> ToDTO(this IList<SPProviderType> list)
		{
			IList<SPProviderTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPProviderTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
