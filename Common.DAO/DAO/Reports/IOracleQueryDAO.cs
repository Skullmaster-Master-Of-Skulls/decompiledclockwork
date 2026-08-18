using System;
using System.Data;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;

namespace TechnoPro.Common.DAO.Reports
{
	// Token: 0x0200003A RID: 58
	public interface IOracleQueryDAO : IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060000F4 RID: 244
		DataTable ExecuteOracleQuery(string ConnectionString, OracleQueryRequest QueryRequest);
	}
}
