using System;
using System.Data;
using System.IO;
using System.Reflection;
using TechnoPro.Common.DAO.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;

namespace TechnoPro.Common.DAO.Impl.Reports
{
	// Token: 0x0200006C RID: 108
	public class OracleQueryDAO : IOracleQueryDAO, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000ED1A File Offset: 0x0000CF1A
		public OracleQueryDAO()
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x00015FD8 File Offset: 0x000141D8
		public OracleQueryDAO(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000290 RID: 656 RVA: 0x00015FEA File Offset: 0x000141EA
		// (set) Token: 0x06000291 RID: 657 RVA: 0x00015FF2 File Offset: 0x000141F2
		public OperationContext OpContext { get; set; }

		// Token: 0x06000292 RID: 658 RVA: 0x00015FFC File Offset: 0x000141FC
		public DataTable ExecuteOracleQuery(string ConnectionString, OracleQueryRequest QueryRequest)
		{
			Assembly assembly = Assembly.LoadFile(Path.Combine(this.OpContext.AppContext.ExecutingPath, "Common.OracleDatabase.dll"));
			Type type = assembly.GetType("TechnoPro.Common.OracleDatabase.OracleQuery");
			MethodInfo method = type.GetMethod("ExecuteQuery");
			return (DataTable)method.Invoke(null, new object[]
			{
				ConnectionString,
				QueryRequest
			});
		}
	}
}
