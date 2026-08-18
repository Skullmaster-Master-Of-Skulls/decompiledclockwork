using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.Common.Core.Mappers.ClockWorkServerJob
{
	// Token: 0x0200016C RID: 364
	public static class ClockWorkServerJobExecutionLogMapper
	{
		// Token: 0x06000645 RID: 1605 RVA: 0x0001CA88 File Offset: 0x0001AC88
		static ClockWorkServerJobExecutionLogMapper()
		{
			Mapper.CreateMap<ClockWorkServerJobExecutionLog, ClockWorkServerJobExecutionLogDTO>();
			Mapper.CreateMap<ClockWorkServerJobExecutionLogDTO, ClockWorkServerJobExecutionLog>().ForMember((ClockWorkServerJobExecutionLog dto) => (object)dto.Id, delegate(IMemberConfigurationExpression<ClockWorkServerJobExecutionLogDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public static ClockWorkServerJobExecutionLog ToDomainObject(this ClockWorkServerJobExecutionLogDTO dto)
		{
			return Mapper.Map<ClockWorkServerJobExecutionLogDTO, ClockWorkServerJobExecutionLog>(dto);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x0001CB1C File Offset: 0x0001AD1C
		public static ClockWorkServerJobExecutionLogDTO ToDTO(this ClockWorkServerJobExecutionLog bo)
		{
			return Mapper.Map<ClockWorkServerJobExecutionLog, ClockWorkServerJobExecutionLogDTO>(bo);
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x0001CB34 File Offset: 0x0001AD34
		public static IList<ClockWorkServerJobExecutionLog> ToDomainObject(this IList<ClockWorkServerJobExecutionLogDTO> list)
		{
			IList<ClockWorkServerJobExecutionLog> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<ClockWorkServerJobExecutionLog>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x0001CB78 File Offset: 0x0001AD78
		public static IList<ClockWorkServerJobExecutionLogDTO> ToDTO(this IList<ClockWorkServerJobExecutionLog> list)
		{
			IList<ClockWorkServerJobExecutionLogDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<ClockWorkServerJobExecutionLogDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}
