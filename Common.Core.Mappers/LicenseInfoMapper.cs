using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x0200000C RID: 12
	public static class LicenseInfoMapper
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003190 File Offset: 0x00001390
		static LicenseInfoMapper()
		{
			Mapper.CreateMap<LicenseKeyInfo, LicenseInfoDTO>().ForMember((LicenseInfoDTO lidto) => (object)lidto.LicenseType, delegate(IMemberConfigurationExpression<LicenseKeyInfo> m)
			{
				m.MapFrom<TechnoPro.ClockWorkServer.Contracts.DataContracts.LicenseType>((LicenseKeyInfo li) => (TechnoPro.ClockWorkServer.Contracts.DataContracts.LicenseType)li.LicenseType);
			}).ForMember((LicenseInfoDTO lidto) => (object)lidto.LicenseStatus, delegate(IMemberConfigurationExpression<LicenseKeyInfo> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<LicenseInfoDTO, LicenseKeyInfo>().ForMember((LicenseKeyInfo li) => li.Id, delegate(IMemberConfigurationExpression<LicenseInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((LicenseKeyInfo li) => (object)li.LicenseType, delegate(IMemberConfigurationExpression<LicenseInfoDTO> m)
			{
				m.MapFrom<TechnoPro.Common.Public.Entities.LicenseType>((LicenseInfoDTO lidto) => (TechnoPro.Common.Public.Entities.LicenseType)lidto.LicenseType);
			});
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003314 File Offset: 0x00001514
		public static LicenseKeyInfo ToDomainObject(this LicenseInfoDTO licenseInfo)
		{
			return Mapper.Map<LicenseInfoDTO, LicenseKeyInfo>(licenseInfo);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x0000332C File Offset: 0x0000152C
		public static IList<LicenseKeyInfo> ToDomainObject(this IList<LicenseInfoDTO> list)
		{
			IList<LicenseKeyInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<LicenseKeyInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00003370 File Offset: 0x00001570
		public static LicenseInfoDTO ToDTO(this LicenseKeyInfo licenseKeyInfo)
		{
			return Mapper.Map<LicenseKeyInfo, LicenseInfoDTO>(licenseKeyInfo);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003388 File Offset: 0x00001588
		public static IList<LicenseInfoDTO> ToDTO(this IList<LicenseKeyInfo> list)
		{
			IList<LicenseInfoDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<LicenseInfoDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
