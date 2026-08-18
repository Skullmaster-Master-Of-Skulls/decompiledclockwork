using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001A1 RID: 417
	public static class AppCancelReasonGroupMapper
	{
		// Token: 0x06000717 RID: 1815 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		static AppCancelReasonGroupMapper()
		{
			Mapper.CreateMap<AppCancelReasonGroupDTO, AppCancelReasonGroup>().ForMember((AppCancelReasonGroup pb) => pb.Id, delegate(IMemberConfigurationExpression<AppCancelReasonGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppCancelReasonGroup, AppCancelReasonGroupDTO>();
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001F450 File Offset: 0x0001D650
		public static AppCancelReasonGroup ToDomainObject(this AppCancelReasonGroupDTO dto)
		{
			return Mapper.Map<AppCancelReasonGroupDTO, AppCancelReasonGroup>(dto);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001F468 File Offset: 0x0001D668
		public static AppCancelReasonGroupDTO ToDTO(this AppCancelReasonGroup item)
		{
			return Mapper.Map<AppCancelReasonGroup, AppCancelReasonGroupDTO>(item);
		}
	}
}
