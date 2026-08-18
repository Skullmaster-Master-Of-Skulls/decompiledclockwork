using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AB RID: 427
	public static class AppTypeGroupWithAppTypesMapper
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0001FDD4 File Offset: 0x0001DFD4
		static AppTypeGroupWithAppTypesMapper()
		{
			AppTypeMapper.CreateMap();
			AppTypeGroupMapper.CreateMap();
			Mapper.CreateMap<AppTypeGroupWithAppTypesDTO, AppTypeGroupWithAppTypes>();
			Mapper.CreateMap<AppTypeGroupWithAppTypes, AppTypeGroupWithAppTypesDTO>();
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001FDF0 File Offset: 0x0001DFF0
		public static AppTypeGroupWithAppTypes ToDomainObject(this AppTypeGroupWithAppTypesDTO appCancelInfoDTO)
		{
			return Mapper.Map<AppTypeGroupWithAppTypesDTO, AppTypeGroupWithAppTypes>(appCancelInfoDTO);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0001FE08 File Offset: 0x0001E008
		public static AppTypeGroupWithAppTypesDTO ToDTO(this AppTypeGroupWithAppTypes appCancelInfo)
		{
			return Mapper.Map<AppTypeGroupWithAppTypes, AppTypeGroupWithAppTypesDTO>(appCancelInfo);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0001FE20 File Offset: 0x0001E020
		public static IList<AppTypeGroupWithAppTypes> ToDomainObject(this IList<AppTypeGroupWithAppTypesDTO> list)
		{
			IList<AppTypeGroupWithAppTypes> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AppTypeGroupWithAppTypes>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0001FE64 File Offset: 0x0001E064
		public static IList<AppTypeGroupWithAppTypesDTO> ToDTO(this IList<AppTypeGroupWithAppTypes> list)
		{
			IList<AppTypeGroupWithAppTypesDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AppTypeGroupWithAppTypesDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
