using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob;
using TechnoPro.Common.Public.Entities.ClockWorkDailyJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkDailyJob
{
	// Token: 0x02000172 RID: 370
	public static class DailyJobTaskResultMapper
	{
		// Token: 0x0600065D RID: 1629 RVA: 0x0001D1F8 File Offset: 0x0001B3F8
		static DailyJobTaskResultMapper()
		{
			Mapper.CreateMap<DailyJobTaskResultDTO, DailyJobTaskResult>().ForMember((DailyJobTaskResult pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<DailyJobTaskResultDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DailyJobTaskResult, DailyJobTaskResultDTO>();
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001D274 File Offset: 0x0001B474
		public static DailyJobTaskResult ToDomainObject(this DailyJobTaskResultDTO dto)
		{
			return Mapper.Map<DailyJobTaskResultDTO, DailyJobTaskResult>(dto);
		}

		// Token: 0x06000660 RID: 1632 RVA: 0x0001D28C File Offset: 0x0001B48C
		public static DailyJobTaskResultDTO ToDTO(this DailyJobTaskResult item)
		{
			return Mapper.Map<DailyJobTaskResult, DailyJobTaskResultDTO>(item);
		}
	}
}
