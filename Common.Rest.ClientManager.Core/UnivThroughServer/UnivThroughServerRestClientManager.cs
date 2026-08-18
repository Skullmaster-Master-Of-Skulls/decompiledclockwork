using System;
using System.Collections.Generic;
using System.Data;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.UnivThroughServer;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UnivDataAccess;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.UnivThroughServer
{
	// Token: 0x02000008 RID: 8
	public class UnivThroughServerRestClientManager : BearerTokenRestProxy<IUnivThroughServerClientManager>, IUnivThroughServerClientManager, IWebService
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002928 File Offset: 0x00000B28
		public UnivThroughServerRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002932 File Offset: 0x00000B32
		public UnivThroughServerRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002940 File Offset: 0x00000B40
		public int Fill(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			QueryRequestDTO model = queryRequestDTO;
			QueryResultDTO queryResultDTO = base.Post<QueryRequestDTO, QueryResultDTO>(model, "univdataaccess/fill");
			t = queryResultDTO.DataTable;
			return queryResultDTO.Id;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x000029A0 File Offset: 0x00000BA0
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
			QueryResultDTO queryResultDTO = base.Post<FillReturnIdentityReq, QueryResultDTO>(fillReturnIdentityReq, "univdataaccess/fillreturnidentity");
			int id = queryResultDTO.Id;
			if (queryResultDTO.DataTable == null || queryResultDTO.DataTable.Rows.Count < 1)
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
				t = queryResultDTO.DataTable;
			}
			return queryResultDTO.Id;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A88 File Offset: 0x00000C88
		public int Fill(ref DataSet ds, string tableName, string sqlCommandText, List<CommonParameter> parameters)
		{
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			QueryRequestDTO model = queryRequestDTO;
			return base.Post<QueryRequestDTO, QueryResultDTO>(model, "univdataaccess/fill").Id;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AE0 File Offset: 0x00000CE0
		public int Update(ref DataTable t, string sqlCommandText, List<CommonParameter> parameters)
		{
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			QueryRequestDTO model = queryRequestDTO;
			QueryResultDTO queryResultDTO = base.Post<QueryRequestDTO, QueryResultDTO>(model, "univdataaccess/fill");
			t = queryResultDTO.DataTable;
			return queryResultDTO.Id;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B40 File Offset: 0x00000D40
		public int ExecuteNonQuery(string sqlCommandText, List<CommonParameter> parameters)
		{
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			QueryRequestDTO model = queryRequestDTO;
			return base.Post<QueryRequestDTO, QueryResultDTO>(model, "univdataaccess/executenonquery").Id;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B98 File Offset: 0x00000D98
		public int ExecuteScalar(string sqlCommandText, List<CommonParameter> parameters)
		{
			QueryRequestDTO queryRequestDTO = new QueryRequestDTO();
			queryRequestDTO.Sql = sqlCommandText;
			queryRequestDTO.Parameters = parameters.ConvertAll<CommonParameterDTO>((CommonParameter f) => new CommonParameterDTO
			{
				DbType = f.DbType,
				Name = f.Name,
				Value = f.Value
			});
			QueryRequestDTO model = queryRequestDTO;
			return base.Post<QueryRequestDTO, QueryResultDTO>(model, "univdataaccess/executescalar").Id;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002BEE File Offset: 0x00000DEE
		public IDataReader ExecuteReader(string sqlCommandText, List<CommonParameter> parameters)
		{
			throw new NotImplementedException();
		}
	}
}
