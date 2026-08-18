using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.ReportFunctionExecutions;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200006F RID: 111
	public class DataSync_FixAddressesForNotetaking : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x00019608 File Offset: 0x00017808
		public DataSync_FixAddressesForNotetaking()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00019623 File Offset: 0x00017823
		public DataSync_FixAddressesForNotetaking(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x00019641 File Offset: 0x00017841
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x00019649 File Offset: 0x00017849
		public OperationContext OpContext { get; set; }

		// Token: 0x0600046D RID: 1133 RVA: 0x00019654 File Offset: 0x00017854
		private static string GetAddress(string label, DataRow dr)
		{
			DataTable t = dr.Table;
			Regex regex = new Regex("#<[^#>]*>#");
			List<DataSync_FixAddressesForNotetaking.AddressField> list = (from Match m in regex.Matches(label ?? "")
			select m.Value.Trim() into h
			where h.Length > 0
			select h into m
			select new DataSync_FixAddressesForNotetaking.AddressField(m) into n
			where t.Columns.Contains(n.ColumnName)
			select n).Select(delegate(DataSync_FixAddressesForNotetaking.AddressField q)
			{
				string text = dr[q.ColumnName].ToString().Trim();
				bool flag = text.Length > 0;
				if (flag)
				{
					q.ReplaceValue = text;
				}
				return q;
			}).ToList<DataSync_FixAddressesForNotetaking.AddressField>();
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(label);
			foreach (DataSync_FixAddressesForNotetaking.AddressField addressField in list)
			{
				stringBuilder.Replace(addressField.CodeName, addressField.ReplaceValue);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x000197A4 File Offset: 0x000179A4
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, ReportFunction function)
		{
			DataTable primaryDataTable = CurrentWholeReportResult.GetPrimaryDataTable();
			bool flag = primaryDataTable == null;
			if (!flag)
			{
				bool flag2 = string.IsNullOrEmpty(primaryDataTable.TableName);
				if (flag2)
				{
					primaryDataTable.TableName = "t";
				}
				DataTable dataTable = primaryDataTable;
				try
				{
					string defaultFunctionParameter = function.GetDefaultFunctionParameter();
					DataSyncFixAddressesForNotetakingParameters dataSyncFixAddressesForNotetakingParameters = defaultFunctionParameter.ConvertXmlToDataSyncFixAddressesForNotetakingParameters();
					dataTable.Columns.Add("address");
					dataTable.Columns.Add("paddress");
					foreach (object obj in dataTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						dataRow["address"] = DataSync_FixAddressesForNotetaking.GetAddress(dataSyncFixAddressesForNotetakingParameters.LocalAddressLabel, dataRow);
						dataRow["paddress"] = DataSync_FixAddressesForNotetaking.GetAddress(dataSyncFixAddressesForNotetakingParameters.PermAddressLabel, dataRow);
					}
					result.Data.Table = dataTable;
				}
				catch (Exception ex)
				{
					string errorMessage = string.Format("Common.Core.Reports.ReportFunctionExecutions.DataSync_FixAddressesForNotetaking:err={0}", ex.ToString());
					result.Result = new RunFunctionResult
					{
						Status = new RunStatus
						{
							ErrorMessage = errorMessage,
							LastStatusStep = eRunStatusStep.Failed
						},
						Function = function
					};
					throw;
				}
			}
		}

		// Token: 0x040000CF RID: 207
		private ReportDAO dao;

		// Token: 0x0200021B RID: 539
		internal class AddressField
		{
			// Token: 0x060012D5 RID: 4821 RVA: 0x0007FD79 File Offset: 0x0007DF79
			public AddressField()
			{
				this.ReplaceValue = "";
			}

			// Token: 0x060012D6 RID: 4822 RVA: 0x0007FD90 File Offset: 0x0007DF90
			public AddressField(string codeName)
			{
				this.CodeName = (codeName ?? "");
				this.ColumnName = ((this.CodeName.StartsWith("#") && this.CodeName.EndsWith("#") && this.CodeName.Length > 4) ? this.CodeName.Substring(2, this.CodeName.Length - 4) : this.CodeName);
				this.ReplaceValue = "";
			}

			// Token: 0x17000275 RID: 629
			// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0007FE1C File Offset: 0x0007E01C
			// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0007FE24 File Offset: 0x0007E024
			public string CodeName { get; set; }

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0007FE2D File Offset: 0x0007E02D
			// (set) Token: 0x060012DA RID: 4826 RVA: 0x0007FE35 File Offset: 0x0007E035
			public string ColumnName { get; set; }

			// Token: 0x17000277 RID: 631
			// (get) Token: 0x060012DB RID: 4827 RVA: 0x0007FE3E File Offset: 0x0007E03E
			// (set) Token: 0x060012DC RID: 4828 RVA: 0x0007FE46 File Offset: 0x0007E046
			public string ReplaceValue { get; set; }
		}
	}
}
