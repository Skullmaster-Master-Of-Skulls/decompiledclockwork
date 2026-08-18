using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AD RID: 429
	public static class AppShowTimeAsTypeMapper
	{
		// Token: 0x0600074D RID: 1869 RVA: 0x0001FFE4 File Offset: 0x0001E1E4
		static AppShowTimeAsTypeMapper()
		{
			Mapper.CreateMap<AppShowTimeAsTypeDTO, AppShowTimeAsType>().ForMember((AppShowTimeAsType ar) => (object)ar.Id, delegate(IMemberConfigurationExpression<AppShowTimeAsTypeDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppShowTimeAsType, AppShowTimeAsTypeDTO>();
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x00020060 File Offset: 0x0001E260
		public static AppShowTimeAsType ToDomainObject(this AppShowTimeAsTypeDTO dto)
		{
			return Mapper.Map<AppShowTimeAsTypeDTO, AppShowTimeAsType>(dto);
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x00020078 File Offset: 0x0001E278
		public static AppShowTimeAsTypeDTO ToDTO(this AppShowTimeAsType item)
		{
			return Mapper.Map<AppShowTimeAsType, AppShowTimeAsTypeDTO>(item);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x00020090 File Offset: 0x0001E290
		public static IList<AppShowTimeAsType> ToDomainObject(this IList<AppShowTimeAsTypeDTO> list)
		{
			IList<AppShowTimeAsType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AppShowTimeAsType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x000200D4 File Offset: 0x0001E2D4
		public static IList<AppShowTimeAsTypeDTO> ToDTO(this IList<AppShowTimeAsType> list)
		{
			IList<AppShowTimeAsTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AppShowTimeAsTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
