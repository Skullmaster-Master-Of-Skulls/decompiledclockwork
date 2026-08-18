using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;

namespace TechnoPro.Common.UI.ClientManager.UnivAccessThroughServer
{
	// Token: 0x02000003 RID: 3
	public static class UnivThroughServer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020C3 File Offset: 0x000002C3
		public static int GetWhoAmI
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C8 File Offset: 0x000002C8
		public static int Fill(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			IUnivDataAccess clientInstance = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false);
			FillReq fillReq = new FillReq();
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq.QueryRequest = queryRequestDTO;
			fillReq.WhoAmI = UnivThroughServer.GetWhoAmI;
			FillResp fillResp = clientInstance.Fill(fillReq);
			t = fillResp.QueryResult.DataTable;
			return fillResp.QueryResult.Id;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002148 File Offset: 0x00000348
		public static int FillReturnIdentity(ref DataTable t, string tableName, string AutoIncrementColName, string sqlCommandText, List<CommonParameter> parameters)
		{
			IUnivDataAccess clientInstance = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false);
			FillReturnIdentityReq fillReturnIdentityReq = new FillReturnIdentityReq();
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReturnIdentityReq.QueryRequest = queryRequestDTO;
			fillReturnIdentityReq.AutoIncrementColName = AutoIncrementColName;
			fillReturnIdentityReq.TableName = tableName;
			fillReturnIdentityReq.WhoAmI = UnivThroughServer.GetWhoAmI;
			FillReturnIdentityResp fillReturnIdentityResp = clientInstance.FillReturnIdentity(fillReturnIdentityReq);
			int id = fillReturnIdentityResp.QueryResult.Id;
			if (fillReturnIdentityResp.QueryResult.DataTable == null || fillReturnIdentityResp.QueryResult.DataTable.Rows.Count < 1)
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

		// Token: 0x0600000D RID: 13 RVA: 0x00002250 File Offset: 0x00000450
		public static int Fill(ref DataSet ds, string tableName, string sqlCommandText, List<CommonParameter> parameters)
		{
			IUnivDataAccess clientInstance = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false);
			FillReq fillReq = new FillReq();
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq.QueryRequest = queryRequestDTO;
			fillReq.WhoAmI = UnivThroughServer.GetWhoAmI;
			return clientInstance.Fill(fillReq).QueryResult.Id;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022C0 File Offset: 0x000004C0
		public static int Update(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			IUnivDataAccess clientInstance = ClientServiceFactory.GetClientInstance<IUnivDataAccess>(true, false);
			FillReq fillReq = new FillReq();
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			fillReq.QueryRequest = queryRequestDTO;
			fillReq.WhoAmI = UnivThroughServer.GetWhoAmI;
			FillResp fillResp = clientInstance.Fill(fillReq);
			t = fillResp.QueryResult.DataTable;
			return fillResp.QueryResult.Id;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000233F File Offset: 0x0000053F
		public static SqlDataReader ExecuteReader(string sqlCommandText, List<CommonParameter> parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002348 File Offset: 0x00000548
		public static int ExecuteNonQuery(string sqlCommandText, List<CommonParameter> parameters)
		{
			DataTable dataTable = new DataTable();
			return UnivThroughServer.Fill(ref dataTable, sqlCommandText, parameters);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002364 File Offset: 0x00000564
		public static int ExecuteScalar(string sqlCommandText, List<CommonParameter> parameters)
		{
			DataTable dataTable = new DataTable();
			UnivThroughServer.Fill(ref dataTable, sqlCommandText, parameters);
			if (dataTable.Rows.Count <= 0 || !(dataTable.Columns[0].DataType == typeof(int)))
			{
				return 0;
			}
			DataRow dataRow = dataTable.Rows[0];
			if (dataRow[0] != DBNull.Value)
			{
				return (int)dataRow[0];
			}
			return 0;
		}
	}
}
