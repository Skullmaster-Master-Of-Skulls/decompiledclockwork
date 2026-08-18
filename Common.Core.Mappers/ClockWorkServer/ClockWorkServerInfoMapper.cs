using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServer;
using TechnoPro.Common.Public.Entities.ClockWorkServer;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServer
{
	// Token: 0x02000168 RID: 360
	public static class ClockWorkServerInfoMapper
	{
		// Token: 0x06000631 RID: 1585 RVA: 0x0001C568 File Offset: 0x0001A768
		static ClockWorkServerInfoMapper()
		{
			Mapper.CreateMap<ClockWorkServerInfoDTO, ClockWorkServerInfo>();
			Mapper.CreateMap<ClockWorkServerInfo, ClockWorkServerInfoDTO>().ForMember((ClockWorkServerInfoDTO dto) => dto.DiscoveryEnpointAddress, delegate(IMemberConfigurationExpression<ClockWorkServerInfo> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
		public static ClockWorkServerInfo ToDomainObject(this ClockWorkServerInfoDTO dto)
		{
			return Mapper.Map<ClockWorkServerInfoDTO, ClockWorkServerInfo>(dto);
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001C5E8 File Offset: 0x0001A7E8
		public static ClockWorkServerInfoDTO ToDTO(this ClockWorkServerInfo item)
		{
			return Mapper.Map<ClockWorkServerInfo, ClockWorkServerInfoDTO>(item);
		}
	}
}
