using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.Common.Core.Mappers.Reports;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkDailyJob
{
	// Token: 0x02000171 RID: 369
	public static class DailyJobTaskMapper
	{
		// Token: 0x06000659 RID: 1625 RVA: 0x0001D144 File Offset: 0x0001B344
		static DailyJobTaskMapper()
		{
			ReportBaseMapper.CreateMap();
			Mapper.CreateMap<DailyJobTaskDTO, DailyJobTask>().ForMember((DailyJobTask pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DailyJobTaskDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DailyJobTask, DailyJobTaskDTO>();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x0001D1C8 File Offset: 0x0001B3C8
		public static DailyJobTask ToDomainObject(this DailyJobTaskDTO dto)
		{
			return Mapper.Map<DailyJobTaskDTO, DailyJobTask>(dto);
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x0001D1E0 File Offset: 0x0001B3E0
		public static DailyJobTaskDTO ToDTO(this DailyJobTask item)
		{
			return Mapper.Map<DailyJobTask, DailyJobTaskDTO>(item);
		}
	}
}
