using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UnivThroughServer;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.UnivThroughServer
{
	// Token: 0x0200000C RID: 12
	public class UnivThroughServerClientManager : IUnivThroughServerClientManager, IWebService
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003538 File Offset: 0x00001738
		public int Fill(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			FillReq fillReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FillReq>();
			FillReq fillReq2 = fillReq;
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq2.QueryRequest = queryRequestDTO;
			FillResp fillResp = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false).Fill(fillReq);
			t = fillResp.QueryResult.DataTable;
			return fillResp.QueryResult.Id;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000035BC File Offset: 0x000017BC
		public int FillReturnIdentity(ref DataTable t, string tableName, string AutoIncrementColName, string sqlCommandText, List<CommonParameter> parameters)
		{
			FillReturnIdentityReq fillReturnIdentityReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FillReturnIdentityReq>();
			FillReturnIdentityReq fillReturnIdentityReq2 = fillReturnIdentityReq;
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReturnIdentityReq2.QueryRequest = queryRequestDTO;
			fillReturnIdentityReq.AutoIncrementColName = AutoIncrementColName;
			fillReturnIdentityReq.TableName = tableName;
			FillReturnIdentityResp fillReturnIdentityResp = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false).FillReturnIdentity(fillReturnIdentityReq);
			int id = fillReturnIdentityResp.QueryResult.Id;
			bool flag = fillReturnIdentityResp.QueryResult.DataTable == null || fillReturnIdentityResp.QueryResult.DataTable.Rows.Count < 1;
			if (flag)
			{
				t = new DataTable("t");
				t.Columns.Add("id", typeof(int));
				t.Rows.Add(new object[]
				{
					id
				});
			}
			else
			{
				t = fillReturnIdentityResp.QueryResult.DataTable;
			}
			return fillReturnIdentityResp.QueryResult.Id;
		}

		// Token: 0x06000053 RID: 83 RVA: 0x000036D8 File Offset: 0x000018D8
		public int Fill(ref DataSet ds, string tableName, string sqlCommandText, List<CommonParameter> parameters)
		{
			FillReq fillReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FillReq>();
			FillReq fillReq2 = fillReq;
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq2.QueryRequest = queryRequestDTO;
			FillResp fillResp = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false).Fill(fillReq);
			return fillResp.QueryResult.Id;
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003750 File Offset: 0x00001950
		public int Update(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			FillReq fillReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<FillReq>();
			FillReq fillReq2 = fillReq;
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq2.QueryRequest = queryRequestDTO;
			FillResp fillResp = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false).Fill(fillReq);
			t = fillResp.QueryResult.DataTable;
			return fillResp.QueryResult.Id;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x000037D4 File Offset: 0x000019D4
		public int ExecuteNonQuery(string sqlCommandText, List<CommonParameter> parameters)
		{
			DataTable dataTable = new DataTable();
			return this.Fill(ref dataTable, sqlCommandText, parameters);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000037F8 File Offset: 0x000019F8
		public int ExecuteScalar(string sqlCommandText, List<CommonParameter> parameters)
		{
			DataTable dataTable = new DataTable();
			this.Fill(ref dataTable, sqlCommandText, parameters);
			bool flag = dataTable.Rows.Count > 0 && dataTable.Columns[0].DataType == typeof(int);
			int result;
			if (flag)
			{
				DataRow dataRow = dataTable.Rows[0];
				result = ((dataRow[0] == DBNull.Value) ? 0 : ((int)dataRow[0]));
			}
			else
			{
				result = 0;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000387F File Offset: 0x00001A7F
		public IDataReader ExecuteReader(string sqlCommandText, List<CommonParameter> parameters)
		{
			throw new NotImplementedException();
		}
	}
}
