using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A4 RID: 420
	public static class AppCancelResaonOrGroupMapper
	{
		// Token: 0x06000723 RID: 1827 RVA: 0x0001F6A9 File Offset: 0x0001D8A9
		static AppCancelResaonOrGroupMapper()
		{
			AppCancelReasonMapper.CreateMap();
			AppCancelReasonGroupMapper.CreateMap();
			Mapper.CreateMap<AppCancelReasonOrGroupDTO, AppCancelReasonOrGroup>();
			Mapper.CreateMap<AppCancelReasonOrGroup, AppCancelReasonOrGroupDTO>();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		public static AppCancelReasonOrGroup ToDomainObject(this AppCancelReasonOrGroupDTO dto)
		{
			return Mapper.Map<AppCancelReasonOrGroupDTO, AppCancelReasonOrGroup>(dto);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		public static AppCancelReasonOrGroupDTO ToDTO(this AppCancelReasonOrGroup item)
		{
			return Mapper.Map<AppCancelReasonOrGroup, AppCancelReasonOrGroupDTO>(item);
		}
	}
}
