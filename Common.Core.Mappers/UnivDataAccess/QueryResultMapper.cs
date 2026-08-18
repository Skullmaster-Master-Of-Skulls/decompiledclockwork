using System;
using System.Data;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Mappers.UnivDataAccess
{
	// Token: 0x02000028 RID: 40
	public static class QueryResultMapper
	{
		// Token: 0x060000AA RID: 170 RVA: 0x00005800 File Offset: 0x00003A00
		static QueryResultMapper()
		{
			Mapper.CreateMap<QueryResultDTO, QueryResult>().ForMember((QueryResult dto) => dto.DataTable, delegate(IMemberConfigurationExpression<QueryResultDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<QueryResult, QueryResultDTO>().ForMember((QueryResultDTO bo) => bo.DataTable, delegate(IMemberConfigurationExpression<QueryResult> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000058B8 File Offset: 0x00003AB8
		public static QueryResult ToDomainObject(this QueryResultDTO queryResultDTO)
		{
			bool flag = queryResultDTO.DataTable != null && string.IsNullOrEmpty(queryResultDTO.DataTable.TableName);
			if (flag)
			{
				queryResultDTO.DataTable.TableName = "queryResultTable5";
			}
			bool flag2 = queryResultDTO.DataTable == null;
			if (flag2)
			{
				queryResultDTO.DataTable = new DataTable("queryResultTable6");
			}
			QueryResult queryResult = Mapper.Map<QueryResultDTO, QueryResult>(queryResultDTO);
			queryResult.DataTable = queryResultDTO.DataTable;
			return queryResult;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00005934 File Offset: 0x00003B34
		public static QueryResultDTO ToDTO(this QueryResult queryResult)
		{
			bool flag = queryResult.DataTable != null && string.IsNullOrEmpty(queryResult.DataTable.TableName);
			if (flag)
			{
				queryResult.DataTable.TableName = "queryResultTable5";
			}
			bool flag2 = queryResult.DataTable == null;
			if (flag2)
			{
				queryResult.DataTable = new DataTable("queryResultTable6");
			}
			QueryResultDTO queryResultDTO = Mapper.Map<QueryResult, QueryResultDTO>(queryResult);
			queryResultDTO.DataTable = queryResult.DataTable;
			return queryResultDTO;
		}
	}
}
