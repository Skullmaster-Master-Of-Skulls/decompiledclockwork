using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Core.Mappers.Appointments
{
	// Token: 0x020001AA RID: 426
	public static class AppTypeGroupMapper
	{
		// Token: 0x0600073D RID: 1853 RVA: 0x0001FD28 File Offset: 0x0001DF28
		static AppTypeGroupMapper()
		{
			Mapper.CreateMap<AppTypeGroupDTO, AppTypeGroup>().ForMember((AppTypeGroup pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AppTypeGroupDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<AppTypeGroup, AppTypeGroupDTO>();
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0001FDA4 File Offset: 0x0001DFA4
		public static AppTypeGroup ToDomainObject(this AppTypeGroupDTO appTypeGroupDTO)
		{
			return Mapper.Map<AppTypeGroupDTO, AppTypeGroup>(appTypeGroupDTO);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0001FDBC File Offset: 0x0001DFBC
		public static AppTypeGroupDTO ToDTO(this AppTypeGroup appTypeGroup)
		{
			return Mapper.Map<AppTypeGroup, AppTypeGroupDTO>(appTypeGroup);
		}
	}
}
