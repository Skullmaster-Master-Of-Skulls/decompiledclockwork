using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.Core.DataSync;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.DataSync;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;
using TechnoPro.Common.Public.Exceptions.InvalidParameters;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x02000067 RID: 103
	public class CustomDataLoad : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600043B RID: 1083 RVA: 0x000182BE File Offset: 0x000164BE
		public CustomDataLoad()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x000182D9 File Offset: 0x000164D9
		public CustomDataLoad(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x000182F7 File Offset: 0x000164F7
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x000182FF File Offset: 0x000164FF
		public OperationContext OpContext { get; set; }

		// Token: 0x0600043F RID: 1087 RVA: 0x00018308 File Offset: 0x00016508
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			CustomDataParameters customDataParameters = defaultFunctionParameter.CustomDataParametersFromXml();
			bool flag = string.IsNullOrEmpty(customDataParameters.ExternalStudentNumberColumnName);
			if (flag)
			{
				throw new InvalidParameterException("CustomDataLoad:Empty ExternalStudentNumberColumnName");
			}
			bool flag2 = string.IsNullOrEmpty(customDataParameters.CustomTableNameWithoutCustomPrefix);
			if (flag2)
			{
				throw new InvalidParameterException("CustomDataLoad:Empty CustomTableNameWithoutCustomPrefix");
			}
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag3 = primaryDataTable == null || primaryDataTable.Rows.Count < 1;
			string text;
			if (flag3)
			{
				text = "";
			}
			else
			{
				bool flag4 = primaryDataTable.Columns.Contains("student_no");
				if (flag4)
				{
					text = primaryDataTable.Rows[0]["student_no"].ToString().Trim().ToUpper();
				}
				else
				{
					bool flag5 = primaryDataTable.Columns.Contains("studentno");
					if (flag5)
					{
						text = primaryDataTable.Rows[0]["student_no"].ToString().Trim().ToUpper();
					}
					else
					{
						text = "";
					}
				}
			}
			bool flag6 = text.Length < 1;
			if (flag6)
			{
				throw new InvalidParameterException("CustomDataLoad:Empty snum (tried student_no and studentno)");
			}
			IDataSyncManager dataSyncManager = new DataSyncManager(this.OpContext);
			DataTable dataTable = dataSyncManager.LoadCustomData(customDataParameters.CustomTableNameWithoutCustomPrefix, text, customDataParameters.ExternalStudentNumberColumnName);
			dataTable.TableName = "q";
			result.Data.Table = dataTable;
		}

		// Token: 0x040000C4 RID: 196
		private ReportDAO dao;
	}
}
