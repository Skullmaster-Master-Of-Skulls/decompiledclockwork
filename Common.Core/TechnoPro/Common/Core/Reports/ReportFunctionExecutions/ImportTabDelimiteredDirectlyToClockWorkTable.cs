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
	// Token: 0x02000083 RID: 131
	public class ImportTabDelimiteredDirectlyToClockWorkTable : IReportFunctionExecute, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x060004D9 RID: 1241 RVA: 0x0001B921 File Offset: 0x00019B21
		public ImportTabDelimiteredDirectlyToClockWorkTable()
		{
			this.dao = new ReportDAO(this.OpContext);
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x0001B93C File Offset: 0x00019B3C
		public ImportTabDelimiteredDirectlyToClockWorkTable(OperationContext opContext)
		{
			this.OpContext = opContext;
			this.dao = new ReportDAO(opContext);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0001B95A File Offset: 0x00019B5A
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0001B962 File Offset: 0x00019B62
		public OperationContext OpContext { get; set; }

		// Token: 0x060004DD RID: 1245 RVA: 0x0001B96C File Offset: 0x00019B6C
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
			string text3 = (array.Length > 4) ? array[4].Trim() : "";
			char delimiter = (text3.Length > 0) ? text3[0] : '\t';
			DataTable table = TechnoPro.Common.DAO.Reports.Impl.Legacy.ReportFunction.Import_Tab_Delimitered_Directly_to_ClockWork_Table(filename, text.Trim().CompareTo("1") == 0, array2, tableName, delimiter, this.OpContext);
			result.Data.Table = table;
		}

		// Token: 0x040000F3 RID: 243
		private ReportDAO dao;
	}
}
