using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A2 RID: 418
	public static class AppCancelReasonMapper
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x0001F480 File Offset: 0x0001D680
		static AppCancelReasonMapper()
		{
			AppCancelReasonGroupMapper.CreateMap();
			Mapper.CreateMap<AppCancelReasonDTO, AppCancelReason>().ForMember((AppCancelReason pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppCancelReasonDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppCancelReason, AppCancelReasonDTO>();
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001F504 File Offset: 0x0001D704
		public static AppCancelReason ToDomainObject(this AppCancelReasonDTO dto)
		{
			return Mapper.Map<AppCancelReasonDTO, AppCancelReason>(dto);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001F51C File Offset: 0x0001D71C
		public static AppCancelReasonDTO ToDTO(this AppCancelReason item)
		{
			return Mapper.Map<AppCancelReason, AppCancelReasonDTO>(item);
		}
	}
}
