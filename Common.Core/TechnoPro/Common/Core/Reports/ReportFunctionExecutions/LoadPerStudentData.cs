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
	// Token: 0x0200008B RID: 139
	public class LoadPerStudentData : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600050B RID: 1291 RVA: 0x0000672B File Offset: 0x0000492B
		public LoadPerStudentData()
		{
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x0001D095 File Offset: 0x0001B295
		public LoadPerStudentData(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0001D0A7 File Offset: 0x0001B2A7
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0001D0AF File Offset: 0x0001B2AF
		public OperationContext OpContext { get; set; }

		// Token: 0x0600050F RID: 1295 RVA: 0x0001D0B8 File Offset: 0x0001B2B8
		public static DbType? GetDbTypeFromObject(object obj, out object newObject)
		{
			bool flag = obj == null || obj is DBNull;
			DbType? result;
			if (flag)
			{
				newObject = null;
				result = null;
			}
			else
			{
				newObject = obj;
				bool flag2 = obj is int;
				if (flag2)
				{
					result = new DbType?(DbType.Int32);
				}
				else
				{
					bool flag3 = obj is DateTime;
					if (flag3)
					{
						result = new DbType?(DbType.DateTime);
					}
					else
					{
						bool flag4 = obj is double;
						if (flag4)
						{
							result = new DbType?(DbType.Double);
						}
						else
						{
							bool flag5 = obj is byte[];
							if (flag5)
							{
								result = new DbType?(DbType.Binary);
							}
							else
							{
								bool flag6 = obj is string;
								if (flag6)
								{
									result = new DbType?(DbType.String);
								}
								else
								{
									bool flag7 = obj is bool;
									if (flag7)
									{
										result = new DbType?(DbType.Boolean);
									}
									else
									{
										newObject = obj.ToString();
										result = new DbType?(DbType.String);
									}
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000510 RID: 1296 RVA: 0x0001D198 File Offset: 0x0001B398
		public static LoadDynamicDataOptions GetLoadDynamicDataOptions(string sql, IList<CommonParameter> parameters)
		{
			CommonParameter commonParameter = parameters.FirstOrDefault((CommonParameter g) => g.Name.Equals("form", StringComparison.OrdinalIgnoreCase));
			CommonParameter commonParameter2 = parameters.FirstOrDefault((CommonParameter g) => g.Name.Equals("fields", StringComparison.OrdinalIgnoreCase));
			int screenNum = 0;
			bool flag = commonParameter != null && commonParameter.Value != null;
			if (flag)
			{
				bool flag2 = commonParameter.Value is int;
				if (flag2)
				{
					screenNum = (int)commonParameter.Value;
				}
				else
				{
					string s = commonParameter.Value.ToString();
					bool flag3 = !int.TryParse(s, out screenNum);
					if (flag3)
					{
						screenNum = 0;
					}
				}
			}
			List<int> list;
			if (commonParameter2 == null || commonParameter2.Value == null)
			{
				list = new List<int>();
			}
			else
			{
				list = (from h in commonParameter2.Value.ToString().Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries).Select(delegate(string g)
				{
					int num;
					bool flag4 = int.TryParse(g.Trim(), out num);
					int result;
					if (flag4)
					{
						result = num;
					}
					else
					{
						result = 0;
					}
					return result;
				})
				where h > 0
				select h).ToList<int>();
			}
			List<int> controlIds = list;
			return new LoadDynamicDataOptions
			{
				SqlQuery = sql,
				ScreenNum = screenNum,
				ControlIds = controlIds
			};
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x0001D2F0 File Offset: 0x0001B4F0
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

		// Token: 0x06000512 RID: 1298 RVA: 0x0001D360 File Offset: 0x0001B560
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

		// Token: 0x06000513 RID: 1299 RVA: 0x0001D3AC File Offset: 0x0001B5AC
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
					result = dynamicDataForReportsManager.CrossReferencePerStudentData(LoadPerStudentData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), controlIds);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerStudentData(LoadPerStudentData.LoadDataTable(loadDataOptions.SqlQuery, currentParameters, OpContext), loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			else
			{
				bool flag3 = loadDataOptions.ScreenNum > 0;
				if (flag3)
				{
					List<int> controlIds2 = (from g in dynamicFieldManager.LoadFields(loadDataOptions.ScreenNum, false)
					select g.ControlId).ToList<int>();
					result = dynamicDataForReportsManager.CrossReferencePerStudentData(existingDataTable, controlIds2);
				}
				else
				{
					result = dynamicDataForReportsManager.CrossReferencePerStudentData(existingDataTable, loadDataOptions.ControlIds ?? new List<int>());
				}
			}
			return result;
		}
	}
}
