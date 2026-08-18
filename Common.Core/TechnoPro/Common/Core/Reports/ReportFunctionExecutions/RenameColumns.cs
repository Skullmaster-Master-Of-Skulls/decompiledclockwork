using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000094 RID: 148
	public class RenameColumns : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000540 RID: 1344 RVA: 0x0000672B File Offset: 0x0000492B
		public RenameColumns()
		{
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001EC9A File Offset: 0x0001CE9A
		public RenameColumns(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x0001ECAC File Offset: 0x0001CEAC
		// (set) Token: 0x06000543 RID: 1347 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
		public OperationContext OpContext { get; set; }

		// Token: 0x06000544 RID: 1348 RVA: 0x0001ECC0 File Offset: 0x0001CEC0
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable lastReportResultTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = lastReportResultTable != null;
			if (flag)
			{
				List<RenameColumns.RenameMapping> list = (from n in (from g in function.GetDefaultFunctionParameter().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries)
				select g.Trim() into h
				where h.Length > 0
				select h).Select(delegate(string m)
				{
					int num = (m.Length > 2) ? m.IndexOf('=') : -1;
					RenameColumns.RenameMapping result2;
					if (num >= 1)
					{
						RenameColumns.RenameMapping renameMapping2 = new RenameColumns.RenameMapping();
						renameMapping2.OldColumnName = RenameColumns.MakeColumnNameSafeToUse(m.Substring(0, num));
						result2 = renameMapping2;
						renameMapping2.NewColumnName = RenameColumns.MakeColumnNameSafeToUse(m.Substring(num + 1));
					}
					else
					{
						result2 = null;
					}
					return result2;
				})
				where n != null && n.OldColumnName.Length > 0 && n.NewColumnName.Length > 0 && lastReportResultTable.Columns.Contains(n.OldColumnName)
				select n).ToList<RenameColumns.RenameMapping>();
				foreach (RenameColumns.RenameMapping renameMapping in list)
				{
					bool flag2 = lastReportResultTable.Columns.Contains(renameMapping.OldColumnName) && !lastReportResultTable.Columns.Contains(renameMapping.NewColumnName);
					if (flag2)
					{
						lastReportResultTable.Columns[renameMapping.OldColumnName].ColumnName = renameMapping.NewColumnName;
					}
				}
			}
			result.Data.Table = CurrentWholeReportResult.GetPrimaryDataView();
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x0001EE3C File Offset: 0x0001D03C
		private static string MakeColumnNameSafeToUse(string cname)
		{
			bool flag = cname == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				result = cname.Trim().Replace("\r", "").Replace("\n", "").Replace("\r\n", "");
			}
			return result;
		}

		// Token: 0x02000236 RID: 566
		internal class RenameMapping
		{
			// Token: 0x1700027B RID: 635
			// (get) Token: 0x06001354 RID: 4948 RVA: 0x0008053F File Offset: 0x0007E73F
			// (set) Token: 0x06001355 RID: 4949 RVA: 0x00080547 File Offset: 0x0007E747
			public string OldColumnName { get; set; }

			// Token: 0x1700027C RID: 636
			// (get) Token: 0x06001356 RID: 4950 RVA: 0x00080550 File Offset: 0x0007E750
			// (set) Token: 0x06001357 RID: 4951 RVA: 0x00080558 File Offset: 0x0007E758
			public string NewColumnName { get; set; }
		}
	}
}
