using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AE RID: 430
	public static class AppTypeWithExtendedInfoMapper
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00020118 File Offset: 0x0001E318
		static AppTypeWithExtendedInfoMapper()
		{
			AppTypeMapper.CreateMap();
			Mapper.CreateMap<AppTypeWithExtendedInfoDTO, AppTypeWithExtendedInfo>().ForMember((AppTypeWithExtendedInfo pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppTypeWithExtendedInfoDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppTypeWithExtendedInfo, AppTypeWithExtendedInfoDTO>();
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002019C File Offset: 0x0001E39C
		public static AppTypeWithExtendedInfo ToDomainObject(this AppTypeWithExtendedInfoDTO appCancelInfoDTO)
		{
			return Mapper.Map<AppTypeWithExtendedInfoDTO, AppTypeWithExtendedInfo>(appCancelInfoDTO);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x000201B4 File Offset: 0x0001E3B4
		public static AppTypeWithExtendedInfoDTO ToDTO(this AppTypeWithExtendedInfo appCancelInfo)
		{
			return Mapper.Map<AppTypeWithExtendedInfo, AppTypeWithExtendedInfoDTO>(appCancelInfo);
		}
	}
}
