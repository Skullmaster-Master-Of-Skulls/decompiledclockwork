using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MonitoringLists;
using TechnoPro.Common.Public.Entities.MonitoringLists;

namespace TechnoPro.Common.Core.Mappers.MonitoringLists
{
	// Token: 0x020000BA RID: 186
	public static class MonitorListMapper
	{
		// Token: 0x06000318 RID: 792 RVA: 0x000100AC File Offset: 0x0000E2AC
		static MonitorListMapper()
		{
			Mapper.CreateMap<MonitorListDTO, MonitorList>().ForMember((MonitorList pb) => pb.Id, delegate(IMemberConfigurationExpression<MonitorListDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<MonitorList, MonitorListDTO>();
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001011C File Offset: 0x0000E31C
		public static MonitorList ToDomainObject(this MonitorListDTO monitorListDTO)
		{
			return Mapper.Map<MonitorListDTO, MonitorList>(monitorListDTO);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00010134 File Offset: 0x0000E334
		public static MonitorListDTO ToDTO(this MonitorList monitorList)
		{
			return Mapper.Map<MonitorList, MonitorListDTO>(monitorList);
		}
	}
}
