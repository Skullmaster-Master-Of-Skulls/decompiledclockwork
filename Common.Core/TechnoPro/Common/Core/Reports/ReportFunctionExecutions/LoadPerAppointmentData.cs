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
	// Token: 0x02000089 RID: 137
	public class LoadPerAppointmentData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004FD RID: 1277 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadPerAppointmentData()
		{
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public LoadPerAppointmentData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0001CC86 File Offset: 0x0001AE86
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0001CC8E File Offset: 0x0001AE8E
		public OperationContext OpContext { get; set; }

		// Token: 0x06000501 RID: 1281 RVA: 0x0001CC98 File Offset: 0x0001AE98
		public void ExecuteReportFunction(ref RunFunctionResultWithData Result, RunReportResult CurrentReportResult, ReportFunction Function)
		{
			IDynamicDataForReportsManager dynamicDataForReportsManager = new DynamicDataForReportsManager(this.OpContext);
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
			Result.Data.Table = LoadPerAppointmentData.LoadData(loadDynamicDataOptions, list, CurrentReportResult.GetPrimaryDataTable(), this.OpContext);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0001CD14 File Offset: 0x0001AF14
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

		// Token: 0x06000503 RID: 1283 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public static DataTable LoadData(LoadDynamicDataOptions loadDataOptions, IList<CommonParameter> currentParameters, DataTable existingDataTable, OperationContext OpContext)
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
					result = dynamicDataForReportsManager.CrossReferencePerAppointmentData(LoadPerAppointmentData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), controlIds);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerAppointmentData(LoadPerAppointmentData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			else
			{
				bool flag3 = loadDataOptions.ScreenNum > 0;
				if (flag3)
				{
					List<int> controlIds2 = (from g in dynamicFieldManager.LoadFields(loadDataOptions.ScreenNum, false)
					select g.ControlId).ToList<int>();
					result = dynamicDataForReportsManager.CrossReferencePerAppointmentData(existingDataTable, controlIds2);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerAppointmentData(existingDataTable, loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			return result;
		}
	}
}
