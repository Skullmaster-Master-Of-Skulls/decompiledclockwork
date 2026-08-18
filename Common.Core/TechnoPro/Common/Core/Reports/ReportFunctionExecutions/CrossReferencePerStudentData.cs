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
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000066 RID: 102
	public class CrossReferencePerStudentData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000436 RID: 1078 RVA: 0x0000672B File Offset: 0x0000492B
		public CrossReferencePerStudentData()
		{
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000181D8 File Offset: 0x000163D8
		public CrossReferencePerStudentData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x000181EA File Offset: 0x000163EA
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x000181F2 File Offset: 0x000163F2
		public OperationContext OpContext { get; set; }

		// Token: 0x0600043A RID: 1082 RVA: 0x000181FC File Offset: 0x000163FC
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null || primaryDataTable.Rows.Count < 1 || string.IsNullOrEmpty(defaultFunctionParameter);
			if (!flag)
			{
				List<int> controlIds = (from h in defaultFunctionParameter.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
				{
					int result2;
					int.TryParse(g, out result2);
					return result2;
				})
				where h > 0
				select h).ToList<int>();
				IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
				result.Data.Table = dynamicDataForReportsManager.CrossReferencePerStudentData(primaryDataTable, controlIds);
			}
		}
	}
}
