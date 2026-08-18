using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000079 RID: 121
	public class ExpandListViewOrFileList : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x0000672B File Offset: 0x0000492B
		public ExpandListViewOrFileList()
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001AF91 File Offset: 0x00019191
		public ExpandListViewOrFileList(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0001AFA3 File Offset: 0x000191A3
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0001AFAB File Offset: 0x000191AB
		public OperationContext OpContext { get; set; }

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			List<DynamicDataColumn> cols = defaultFunctionParameter.Split(new char[]
			{
				','
			}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
			{
				int num = g.IndexOf('[');
				bool flag = num > 0;
				if (flag)
				{
					int num2 = g.IndexOf(']', num + 1);
					bool flag2 = num2 > 0;
					if (flag2)
					{
						int controlId;
						int.TryParse(g.Substring(num + 1, num2 - num - 1), out controlId);
						return new DynamicDataColumn
						{
							ColumnName = g.Substring(0, num),
							ControlId = controlId
						};
					}
				}
				return new DynamicDataColumn
				{
					ColumnName = g
				};
			}).ToList<DynamicDataColumn>();
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
			result.Data.Table = dynamicDataForReportsManager.ExpandListViewOrFileList(primaryDataTable, cols);
		}
	}
}
