using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DynamicForms;
using TechnoPro.Common.DAO.DynamicQueries;
using TechnoPro.Common.DAO.Impl.DynamicQueries;
using TechnoPro.Common.ICore.DynamicForms;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Entities.UnivDataAccess;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200008A RID: 138
	public class LoadPerDateData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadPerDateData()
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001CE8B File Offset: 0x0001B08B
		public LoadPerDateData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000506 RID: 1286 RVA: 0x0001CE9D File Offset: 0x0001B09D
		// (set) Token: 0x06000507 RID: 1287 RVA: 0x0001CEA5 File Offset: 0x0001B0A5
		public OperationContext OpContext { get; set; }

		// Token: 0x06000508 RID: 1288 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
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
			Result.Data.Table = LoadPerDateData.LoadDataPerDate(loadDynamicDataOptions, list, CurrentReportResult.GetPrimaryDataTable(), this.OpContext);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001CF20 File Offset: 0x0001B120
		private static DataTable LoadDataPerDate(LoadDynamicDataOptions loadDataOptions, IList<CommonParameter> currentParameters, DataTable existingDataTable, OperationContext OpContext)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(OpContext);
			IDynamicFieldManager dynamicFieldManager = new DynamicFieldManager(OpContext);
			bool flag = !string.IsNullOrEmpty(loadDataOptions.SqlQuery);
			DataTable result;
			if (flag)
			{
				bool flag2 = loadDataOptions.ScreenNum > 0;
				if (flag2)
				{
					List<int> controlIds = (from g in dynamicFieldManager.LoadFields(loadDataOptions.ScreenNum, false)
					select g.ControlId).ToList<int>();
					result = dynamicDataForReportsManager.CrossReferencePerDateData(LoadPerDateData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), controlIds);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerDateData(LoadPerDateData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			else
			{
				bool flag3 = loadDataOptions.ScreenNum > 0;
				if (flag3)
				{
					List<int> controlIds2 = (from g in dynamicFieldManager.LoadFields(loadDataOptions.ScreenNum, false)
					select g.ControlId).ToList<int>();
					result = dynamicDataForReportsManager.CrossReferencePerDateData(existingDataTable, controlIds2);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerDateData(existingDataTable, loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			return result;
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x0001D04C File Offset: 0x0001B24C
		private static DataTable LoadDataTable(string SqlQuery, IList<CommonParameter> SqlParameters, OperationContext opContext)
		{
			IDynamicQueryDAO dynamicQueryDAO = new DynamicQueryDAO(opContext);
			QueryResult queryResult = dynamicQueryDAO.ExecuteQuery(new QueryRequest
			{
				Sql = SqlQuery,
				Parameters = (SqlParameters ?? new List<CommonParameter>()).ToList<CommonParameter>()
			});
			return queryResult.DataTable;
		}
	}
}
