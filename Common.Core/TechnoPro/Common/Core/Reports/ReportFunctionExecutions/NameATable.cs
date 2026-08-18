using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200008D RID: 141
	public class NameATable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x0001D5EB File Offset: 0x0001B7EB
		public NameATable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0001D606 File Offset: 0x0001B806
		public NameATable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001D624 File Offset: 0x0001B824
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0001D62C File Offset: 0x0001B82C
		public OperationContext OpContext { get; set; }

		// Token: 0x0600051E RID: 1310 RVA: 0x0001D638 File Offset: 0x0001B838
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			string defaultFunctionParameter = function.GetDefaultFunctionParameter();
			int num = defaultFunctionParameter.IndexOf(',');
			string name = (num > 0) ? defaultFunctionParameter.Substring(0, num) : defaultFunctionParameter;
			List<string> source = defaultFunctionParameter.Substring(num + 1).Trim().Split(new char[]
			{
				','
			}).ToList<string>().ConvertAll<string>((string g) => g.Trim().ToLower());
			bool flag = source.FirstOrDefault((string g) => g == "removeallothers") != null;
			if (flag)
			{
				CurrentWholeReportResult.AdditionalData = new List<RunFunctionData>();
			}
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag2 = primaryDataTable != null;
			if (flag2)
			{
				this.AddTableToAdditionalData(name, primaryDataTable, CurrentWholeReportResult, (CurrentWholeReportResult.PrimaryData != null) ? (CurrentWholeReportResult.PrimaryData.TableSort ?? "") : "");
			}
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0001D72C File Offset: 0x0001B92C
		public void AddTableToAdditionalData(string name, DataTable t, RunReportResult CurrentWholeReportResult, string tableSort)
		{
			bool flag = CurrentWholeReportResult.AdditionalData == null;
			if (flag)
			{
				CurrentWholeReportResult.AdditionalData = new List<RunFunctionData>();
			}
			RunFunctionData runFunctionData = CurrentWholeReportResult.AdditionalData.FirstOrDefault((RunFunctionData g) => g.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
			bool flag2 = runFunctionData == null;
			if (flag2)
			{
				CurrentWholeReportResult.AdditionalData.Add(new RunFunctionData
				{
					Name = name,
					AddToAdditionalData = true,
					IsPrimary = false,
					Table = t.Copy(),
					TableSort = (tableSort ?? "")
				});
			}
			else
			{
				runFunctionData.Table = t.Copy();
				runFunctionData.TableSort = ((CurrentWholeReportResult.PrimaryData != null) ? (CurrentWholeReportResult.PrimaryData.TableSort ?? "") : "");
			}
		}

		// Token: 0x04000100 RID: 256
		private ReportDAO dao;
	}
}
