using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AC RID: 428
	public static class AppTypeMapper
	{
		// Token: 0x06000747 RID: 1863 RVA: 0x0001FEA8 File Offset: 0x0001E0A8
		static AppTypeMapper()
		{
			AppTypeGroupMapper.CreateMap();
			Mapper.CreateMap<AppTypeDTO, AppType>().ForMember((AppType pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppTypeDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppType, AppTypeDTO>();
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0001FF2C File Offset: 0x0001E12C
		public static AppType ToDomainObject(this AppTypeDTO appTypeDTO)
		{
			return Mapper.Map<AppTypeDTO, AppType>(appTypeDTO);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0001FF44 File Offset: 0x0001E144
		public static AppTypeDTO ToDTO(this AppType appType)
		{
			return Mapper.Map<AppType, AppTypeDTO>(appType);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0001FF5C File Offset: 0x0001E15C
		public static IList<AppType> ToDomainObject(this IList<AppTypeDTO> list)
		{
			IList<AppType> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<AppType>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0001FFA0 File Offset: 0x0001E1A0
		public static IList<AppTypeDTO> ToDTO(this IList<AppType> list)
		{
			IList<AppTypeDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AppTypeDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
