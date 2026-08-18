using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000086 RID: 134
	public class LoadAccommodationData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004E8 RID: 1256 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadAccommodationData()
		{
		}

		// Token: 0x060004E9 RID: 1257 RVA: 0x0001BC79 File Offset: 0x00019E79
		public LoadAccommodationData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x0001BC8B File Offset: 0x00019E8B
		// (set) Token: 0x060004EB RID: 1259 RVA: 0x0001BC93 File Offset: 0x00019E93
		public OperationContext OpContext { get; set; }

		// Token: 0x060004EC RID: 1260 RVA: 0x0001BC9C File Offset: 0x00019E9C
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			string defaultFunctionParameter = Function.GetDefaultFunctionParameter();
			List<CommonParameter> list = CurrentReportResult.CurrentReportParameters.Select(delegate(ReportParameter g)
			{
				object value = g.Value;
				object value2;
				DbType? dbTypeFromObject = LoadPerStudentData.GetDbTypeFromObject(value, out value2);
				return new CommonParameter
				{
					Name = g.Name,
					Value = value2,
					DbType = dbTypeFromObject
				};
			}).ToList<CommonParameter>();
			LoadDynamicDataOptions loadDynamicDataOptions = LoadPerStudentData.GetLoadDynamicDataOptions(defaultFunctionParameter, list);
			Result.Data.Table = LoadPerStudentData.LoadData(loadDynamicDataOptions, list, CurrentReportResult.GetPrimaryDataTable(), this.OpContext);
		}
	}
}
