using System;
using System.Collections.Generic;
using System.Data;

namespace ReportFunctions
{
	// Token: 0x02000024 RID: 36
	public class ReportResults
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00039B58 File Offset: 0x00038B58
		public ReportDataCollection Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00039B70 File Offset: 0x00038B70
		public ReportResults()
		{
			this.results = new ReportDataCollection();
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00039B88 File Offset: 0x00038B88
		public DataTable[] GetTablesExceptCurrent()
		{
			ReportData currentReportData = this.results.CurrentReportData;
			DataView dataView = (currentReportData == null) ? null : currentReportData.DataView;
			List<DataTable> list = new List<DataTable>();
			foreach (object obj in this.results)
			{
				ReportData reportData = (ReportData)obj;
				DataView dataView2 = reportData.DataView;
				if (dataView2 != null && (dataView == null || dataView != dataView2))
				{
					list.Add(dataView2.Table);
				}
			}
			DataTable[] array = new DataTable[list.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = list[i];
			}
			return array;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00039C7C File Offset: 0x00038C7C
		public DataView GetCurrentDataView()
		{
			ReportData currentReportData = this.results.CurrentReportData;
			return (currentReportData == null) ? null : currentReportData.DataView;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00039CA8 File Offset: 0x00038CA8
		public DataView GetDataView(string tableName)
		{
			ReportData reportData = this.results[tableName];
			return (reportData == null) ? null : reportData.DataView;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00039CD4 File Offset: 0x00038CD4
		public DataView GetDataView(int id)
		{
			ReportData reportData = this.results.Find(id);
			return (reportData == null) ? null : reportData.DataView;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00039CFF File Offset: 0x00038CFF
		public void RemoveAllBut(DataView dv)
		{
			this.results.RemoveAllBut(dv);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00039D10 File Offset: 0x00038D10
		public void MakeATableTheCurrentTable(string tableName)
		{
			ReportData reportData = this.results[tableName];
			if (reportData != null)
			{
				this.results.CurrentReportData = reportData;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00039D44 File Offset: 0x00038D44
		public int Count
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00039D64 File Offset: 0x00038D64
		public int AddResult(DataSet ds)
		{
			return this.results.Add(ds);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00039D84 File Offset: 0x00038D84
		public int AddResult(DataView dv)
		{
			return this.AddResult(dv, "unknown");
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00039DA4 File Offset: 0x00038DA4
		public int AddResult(DataView dv, string name)
		{
			return this.results.Add(dv, name);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00039DC4 File Offset: 0x00038DC4
		public int AddResultNotPrimary(DataView dv)
		{
			return this.AddResultNotPrimary(dv, "unknown_notprimary");
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00039DE4 File Offset: 0x00038DE4
		public int AddResultNotPrimary(DataView dv, string name)
		{
			return this.results.AddDontSetCurrentReportData(dv, name);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x00039E04 File Offset: 0x00038E04
		public void MergeInReportResults(ReportResults rr)
		{
			ReportDataCollection reportDataCollection = rr.Results;
			foreach (object obj in reportDataCollection)
			{
				ReportData reportData = (ReportData)obj;
				this.results.Add(reportData);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00039E78 File Offset: 0x00038E78
		public void NameCurrentTable(string name)
		{
			ReportData currentReportData = this.results.CurrentReportData;
			if (currentReportData != null)
			{
				currentReportData.Name = name;
				DataTable dataTable = currentReportData.DataTable;
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x00039EAB File Offset: 0x00038EAB
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep)
		{
			this.ReplaceDataView(dvToReplace, dvToKeep, "unknown");
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00039EBC File Offset: 0x00038EBC
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep, string name)
		{
			ReportData rdToRemove = this.results[dvToReplace];
			this.ReplaceDataView(rdToRemove, dvToKeep, name);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x00039EE1 File Offset: 0x00038EE1
		public void ReplaceDataView(ReportData rdToRemove, DataView dvToKeep, string name)
		{
			this.results.Add(dvToKeep, name);
			this.results.Remove(rdToRemove);
		}

		// Token: 0x04000137 RID: 311
		private ReportDataCollection results;
	}
}
