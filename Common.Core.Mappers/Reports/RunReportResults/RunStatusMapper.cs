using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x0200009F RID: 159
	public static class RunStatusMapper
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000EAD0 File Offset: 0x0000CCD0
		static RunStatusMapper()
		{
			Mapper.CreateMap<RunStatusDTO, RunStatus>();
			Mapper.CreateMap<RunStatus, RunStatusDTO>();
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000EAE0 File Offset: 0x0000CCE0
		public static RunStatus ToDomainObject(this RunStatusDTO dto)
		{
			return Mapper.Map<RunStatusDTO, RunStatus>(dto);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public static RunStatusDTO ToDTO(this RunStatus item)
		{
			return Mapper.Map<RunStatus, RunStatusDTO>(item);
		}
	}
}
