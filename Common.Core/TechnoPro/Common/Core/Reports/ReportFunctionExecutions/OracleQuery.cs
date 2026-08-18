using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Impl.Reports;
using TechnoPro.Common.DAO.Reports;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200008F RID: 143
	public class OracleQuery : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000525 RID: 1317 RVA: 0x0000672B File Offset: 0x0000492B
		public OracleQuery()
		{
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0001D83C File Offset: 0x0001BA3C
		public OracleQuery(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001D84E File Offset: 0x0001BA4E
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0001D856 File Offset: 0x0001BA56
		public OperationContext OpContext { get; set; }

		// Token: 0x06000529 RID: 1321 RVA: 0x0001D860 File Offset: 0x0001BA60
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			OracleQueryParameters oracleQueryParameters = defaultFunctionParameter.OracleQueryParametersFromXml();
			bool flag = oracleQueryParameters == null || oracleQueryParameters.Query == null;
			if (flag)
			{
				throw new Exception("Invalid oracle parameters xml");
			}
			string text = oracleQueryParameters.Query.Sql ?? "";
			bool flag2 = text.Length < 1;
			if (flag2)
			{
				throw new Exception("Missing oracle sql");
			}
			bool flag3 = oracleQueryParameters.Query.Parameters == null;
			if (flag3)
			{
				oracleQueryParameters.Query.Parameters = new List<OracleParameter>();
			}
			foreach (OracleParameter oracleParameter in from g in oracleQueryParameters.Query.Parameters
			where !g.IsOutParameter
			select g)
			{
				string text2 = oracleParameter.Name ?? "";
				int num = text2.IndexOf(":");
				bool flag4 = num < 1;
				string name;
				string text3;
				if (flag4)
				{
					name = text2;
					text3 = "";
				}
				else
				{
					text3 = text2.Substring(0, num);
					name = text2.Substring(num + 1);
				}
				oracleParameter.Name = name;
				OracleParameter oracleParameter2 = oracleParameter;
				object value;
				if (text3.Length <= 0)
				{
					value = null;
				}
				else
				{
					value = CurrentReportResult.ExtractParameterValue((oracleParameter.Name ?? "").Split(new char[]
					{
						','
					}).Select((string g) => g.Trim()).Where((string h) => h.Length > 0).ToArray<string>());
				}
				oracleParameter2.Value = value;
			}
			IOracleQueryDAO oracleQueryDAO = new OracleQueryDAO(this.OpContext);
			Result.Data.Table = oracleQueryDAO.ExecuteOracleQuery(oracleQueryParameters.ConnectionString, oracleQueryParameters.Query);
		}
	}
}
