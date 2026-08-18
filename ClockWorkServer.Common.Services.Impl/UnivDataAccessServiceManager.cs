using System;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess;
using TechnoPro.Common.Core.Mappers.UnivDataAccess;
using TechnoPro.Common.Core.UnivDataAccess;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200009B RID: 155
	public class UnivDataAccessServiceManager : IUnivDataAccess, IService
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x0001A59C File Offset: 0x0001879C
		public DoesTableExistResp DoesTableExist(DoesTableExistReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			bool tableExists = univDataAccessManager.DoesTableExist(Request.TableName);
			return new DoesTableExistResp
			{
				TableExists = tableExists
			};
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x0001A5D4 File Offset: 0x000187D4
		public DoesColumnExistResp DoesColumnExist(DoesColumnExistReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			bool columnExists = univDataAccessManager.DoesColumnExist(Request.TableName, Request.ColumnName);
			return new DoesColumnExistResp
			{
				ColumnExists = columnExists
			};
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0001A614 File Offset: 0x00018814
		public GetSQLCommandParametersFilledInResp GetSQLCommandParametersFilledIn(GetSQLCommandParametersFilledInReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			string sqlcommandParametersFilledIn = univDataAccessManager.GetSQLCommandParametersFilledIn(Request.QueryRequest.ToDomainObject());
			return new GetSQLCommandParametersFilledInResp
			{
				SqlWithParameters = sqlcommandParametersFilledIn
			};
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001A654 File Offset: 0x00018854
		public FillReturnIdentityResp FillReturnIdentity(FillReturnIdentityReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			QueryResult queryResult = univDataAccessManager.FillReturnIdentity(Request.QueryRequest.ToDomainObject(), Request.AutoIncrementColName, Request.TableName);
			return new FillReturnIdentityResp
			{
				QueryResult = ((queryResult == null) ? null : queryResult.ToDTO())
			};
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0001A6A8 File Offset: 0x000188A8
		public FillResp Fill(FillReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			QueryResult queryResult = univDataAccessManager.Fill(Request.QueryRequest.ToDomainObject());
			return new FillResp
			{
				QueryResult = ((queryResult == null) ? null : queryResult.ToDTO())
			};
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0001A6F0 File Offset: 0x000188F0
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0001A704 File Offset: 0x00018904
		public ExecuteScalarResp ExecuteScalar(ExecuteScalarReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			QueryResult queryResult = univDataAccessManager.ExecuteScalar(Request.QueryRequest.ToDomainObject());
			return new ExecuteScalarResp
			{
				QueryResult = ((queryResult == null) ? null : queryResult.ToDTO())
			};
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0001A74C File Offset: 0x0001894C
		public ExecuteNonQueryResp ExecuteNonQuery(ExecuteNonQueryReq Request)
		{
			UnivDataAccessManager univDataAccessManager = new UnivDataAccessManager(Request.GetOperationContext());
			QueryResult queryResult = univDataAccessManager.ExecuteNonQuery(Request.QueryRequest.ToDomainObject());
			return new ExecuteNonQueryResp
			{
				QueryResult = ((queryResult == null) ? null : queryResult.ToDTO())
			};
		}
	}
}
