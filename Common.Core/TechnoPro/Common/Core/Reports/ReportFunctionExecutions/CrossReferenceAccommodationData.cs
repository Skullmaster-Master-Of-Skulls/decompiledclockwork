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
	// Token: 0x02000064 RID: 100
	public class CrossReferenceAccommodationData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000672B File Offset: 0x0000492B
		public CrossReferenceAccommodationData()
		{
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00017BEF File Offset: 0x00015DEF
		public CrossReferenceAccommodationData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x00017C01 File Offset: 0x00015E01
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x00017C09 File Offset: 0x00015E09
		public OperationContext OpContext { get; set; }

		// Token: 0x06000430 RID: 1072 RVA: 0x00017C14 File Offset: 0x00015E14
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
				bool flag2 = primaryDataTable.Columns.Contains("lucourseid") || primaryDataTable.Columns.Contains("coursesid");
				if (flag2)
				{
					result.Data.Table = dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOrCourseSpecific(primaryDataTable, controlIds);
				}
				else
				{
					result.Data.Table = dynamicDataForReportsManager.CrossReferenceAccommodationDataTemplateOnly(primaryDataTable, controlIds);
				}
			}
		}
	}
}
