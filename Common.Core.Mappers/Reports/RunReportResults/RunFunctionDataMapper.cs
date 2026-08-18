using System;
using System.Data;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Mappers.Reports.RunReportResults
{
	// Token: 0x0200009B RID: 155
	public static class RunFunctionDataMapper
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000E578 File Offset: 0x0000C778
		static RunFunctionDataMapper()
		{
			Mapper.CreateMap<RunFunctionDataDTO, RunFunctionData>().ForMember((RunFunctionData pb) => pb.Id, delegate(IMemberConfigurationExpression<RunFunctionDataDTO> m)
			{
				m.Ignore();
			}).ForMember((RunFunctionData pb) => pb.Table, delegate(IMemberConfigurationExpression<RunFunctionDataDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<RunFunctionData, RunFunctionDataDTO>().ForMember((RunFunctionDataDTO pb) => pb.Table, delegate(IMemberConfigurationExpression<RunFunctionData> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x0600029B RID: 667 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000E684 File Offset: 0x0000C884
		public static RunFunctionData ToDomainObject(this RunFunctionDataDTO dto)
		{
			DataTable table = dto.Table;
			bool flag = table != null && string.IsNullOrEmpty(table.TableName);
			if (flag)
			{
				table.TableName = "t1";
			}
			dto.Table = null;
			RunFunctionData runFunctionData = Mapper.Map<RunFunctionDataDTO, RunFunctionData>(dto);
			runFunctionData.Table = table;
			return runFunctionData;
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		public static RunFunctionDataDTO ToDTO(this RunFunctionData item)
		{
			DataTable table = item.Table;
			bool flag = table != null && string.IsNullOrEmpty(table.TableName);
			if (flag)
			{
				table.TableName = "t1";
			}
			item.Table = null;
			RunFunctionDataDTO runFunctionDataDTO = Mapper.Map<RunFunctionData, RunFunctionDataDTO>(item);
			runFunctionDataDTO.Table = table;
			return runFunctionDataDTO;
		}
	}
}
