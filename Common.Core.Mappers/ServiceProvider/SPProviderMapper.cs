using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;
using TechnoPro.Common.Public.Entities.ServiceProvider;

namespace TechnoPro.Common.Core.Mappers.ServiceProvider
{
	// Token: 0x0200006D RID: 109
	public static class SPProviderMapper
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000B4F4 File Offset: 0x000096F4
		static SPProviderMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<SPProvider, SPProviderDTO>();
			Mapper.CreateMap<SPProviderDTO, SPProvider>().ForMember((SPProvider pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<SPProviderDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000B578 File Offset: 0x00009778
		public static SPProvider ToDomainObject(this SPProviderDTO dto)
		{
			return Mapper.Map<SPProviderDTO, SPProvider>(dto);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000B590 File Offset: 0x00009790
		public static SPProviderDTO ToDTO(this SPProvider item)
		{
			return Mapper.Map<SPProvider, SPProviderDTO>(item);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000B5A8 File Offset: 0x000097A8
		public static IList<SPProvider> ToDomainObject(this IList<SPProviderDTO> list)
		{
			IList<SPProvider> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<SPProvider>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000B5EC File Offset: 0x000097EC
		public static IList<SPProviderDTO> ToDTO(this IList<SPProvider> list)
		{
			IList<SPProviderDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<SPProviderDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
