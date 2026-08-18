using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A0 RID: 416
	public static class AppCancelInfoMapper
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0001F394 File Offset: 0x0001D594
		static AppCancelInfoMapper()
		{
			PersonBaseMapper.CreateMap();
			AppCancelReasonMapper.CreateMap();
			Mapper.CreateMap<AppCancelInfoDTO, AppCancelInfo>();
			Mapper.CreateMap<AppCancelInfo, AppCancelInfoDTO>();
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0001F3B0 File Offset: 0x0001D5B0
		public static AppCancelInfo ToDomainObject(this AppCancelInfoDTO appCancelInfoDTO)
		{
			return Mapper.Map<AppCancelInfoDTO, AppCancelInfo>(appCancelInfoDTO);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0001F3C8 File Offset: 0x0001D5C8
		public static AppCancelInfoDTO ToDTO(this AppCancelInfo appCancelInfo)
		{
			return Mapper.Map<AppCancelInfo, AppCancelInfoDTO>(appCancelInfo);
		}
	}
}
