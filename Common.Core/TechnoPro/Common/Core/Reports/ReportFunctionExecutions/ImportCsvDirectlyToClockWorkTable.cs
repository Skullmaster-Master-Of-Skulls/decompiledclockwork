using System;
using System.Data;
using TechnoPro.Common.Core.Adapters;
using TechnoPro.Common.DAO.Reports.Impl;
using TechnoPro.Common.DAO.Reports.Impl.Legacy;
using TechnoPro.Common.ICore.Reports;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Reports;
using TechnoPro.Common.Public.Entities.Reports.RunReportResults;

namespace TechnoPro.Common.Core.Reports.ReportFunctionExecutions
{
	// Token: 0x0200007E RID: 126
	public class ImportCsvDirectlyToClockWorkTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0001B5F4 File Offset: 0x000197F4
		public ImportCsvDirectlyToClockWorkTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001B60F File Offset: 0x0001980F
		public ImportCsvDirectlyToClockWorkTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x0001B62D File Offset: 0x0001982D
		// (set) Token: 0x060004C3 RID: 1219 RVA: 0x0001B635 File Offset: 0x00019835
		public OperationContext OpContext { get; set; }

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001B640 File Offset: 0x00019840
		public void ExecuteReportFunction(ref RunFunctionResultWithData result, RunReportResult CurrentWholeReportResult, TechnoPro.Common.Public.Entities.Reports.ReportFunction function)
		{
			string[] array = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(function.GetDefaultFunctionParameter(), true);
			string filename = array[0];
			string text = array[1];
			string text2 = array[2];
			string tableName = array[3];
			bool flag = text2.CompareTo("all") == 0;
			int[] array2;
			if (flag)
			{
				array2 = null;
			}
			else
			{
				bool flag2 = text2.Equals(".");
				if (flag2)
				{
					array2 = new int[0];
				}
				else
				{
					string[] array3 = text2.Split(new char[]
					{
						','
					});
					array2 = new int[array3.Length];
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i] = int.Parse(array3[i]);
					}
				}
			}
			DataTable table = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.ImportCSVDirectlyIntoClockWorkTable(filename, text.Trim().CompareTo("1") == 0, array2, tableName, this.OpContext);
			result.Data.Table = table;
		}

		// Token: 0x040000EB RID: 235
		private ReportDAO dao;
	}
}
