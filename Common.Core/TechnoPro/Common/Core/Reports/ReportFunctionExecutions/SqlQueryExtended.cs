using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200009E RID: 158
	public class SqlQueryExtended : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600057A RID: 1402 RVA: 0x00020448 File Offset: 0x0001E648
		public SqlQueryExtended()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00020463 File Offset: 0x0001E663
		public SqlQueryExtended(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x00020481 File Offset: 0x0001E681
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x00020489 File Offset: 0x0001E689
		public OperationContext OpContext { get; set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x00020494 File Offset: 0x0001E694
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			SqlQueryExtendedParameters sqlQueryExtendedParameters = defaultFunctionParameter.SqlQueryExtendedParametersFromXml();
			bool flag = string.IsNullOrEmpty((sqlQueryExtendedParameters != null) ? sqlQueryExtendedParameters.Sql : null);
			if (flag)
			{
				throw new Exception("Common.Core.Reports.ReportFunctionExecutions.SqlQueryExtended:Failed to load sql:pp=" + (defaultFunctionParameter ?? "NULL"));
			}
			string text = sqlQueryExtendedParameters.Sql;
			text = (SqlQuery.ReplaceWebSettingsWithValues(text) ?? text);
			DataTable table = this.dao.RunReportSql(SqlQuery.ExtractReportParameters(CurrentWholeReportResult), text, sqlQueryExtendedParameters.OverrideTimeout);
			result.Data.Table = table;
		}

		// Token: 0x04000118 RID: 280
		private ReportDAO dao;
	}
}
