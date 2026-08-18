using System;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000023 RID: 35
	public class ReportResults
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000269 RID: 617 RVA: 0x000295F8 File Offset: 0x000277F8
		public ReportDataCollection Results
		{
			get
			{
				return this.results;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00029610 File Offset: 0x00027810
		public ReportResults()
		{
			this.results = new ReportDataCollection();
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00029628 File Offset: 0x00027828
		public DataTable[] GetTablesExceptCurrent()
		{
			ReportData currentReportData = this.results.CurrentReportData;
			DataView dataView = (currentReportData == null) ? null : currentReportData.DataView;
			List<DataTable> list = new List<DataTable>();
			foreach (object obj in this.results)
			{
				ReportData reportData = (ReportData)obj;
				DataView dataView2 = reportData.DataView;
				bool flag = dataView2 != null && (dataView == null || dataView != dataView2);
				if (flag)
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

		// Token: 0x0600026C RID: 620 RVA: 0x00029714 File Offset: 0x00027914
		public DataView GetCurrentDataView()
		{
			ReportData currentReportData = this.results.CurrentReportData;
			return (currentReportData == null) ? null : currentReportData.DataView;
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00029740 File Offset: 0x00027940
		public DataView GetDataView(string tableName)
		{
			ReportData reportData = this.results[tableName];
			return (reportData == null) ? null : reportData.DataView;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0002976C File Offset: 0x0002796C
		public DataView GetDataView(int id)
		{
			ReportData reportData = this.results.Find(id);
			return (reportData == null) ? null : reportData.DataView;
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00029797 File Offset: 0x00027997
		public void RemoveAllBut(DataView dv)
		{
			this.results.RemoveAllBut(dv);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000297A8 File Offset: 0x000279A8
		public void MakeATableTheCurrentTable(string tableName)
		{
			ReportData reportData = this.results[tableName];
			bool flag = reportData != null;
			if (flag)
			{
				this.results.CurrentReportData = reportData;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000271 RID: 625 RVA: 0x000297DC File Offset: 0x000279DC
		public int Count
		{
			get
			{
				return this.results.Count;
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x000297FC File Offset: 0x000279FC
		public int AddResult(DataSet ds)
		{
			return this.results.Add(ds);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0002981C File Offset: 0x00027A1C
		public int AddResult(DataView dv)
		{
			return this.AddResult(dv, "unknown");
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0002983C File Offset: 0x00027A3C
		public int AddResult(DataView dv, string name)
		{
			return this.results.Add(dv, name);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0002985C File Offset: 0x00027A5C
		public int AddResultNotPrimary(DataView dv)
		{
			return this.AddResultNotPrimary(dv, "unknown_notprimary");
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0002987C File Offset: 0x00027A7C
		public int AddResultNotPrimary(DataView dv, string name)
		{
			return this.results.AddDontSetCurrentReportData(dv, name);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0002989C File Offset: 0x00027A9C
		public void MergeInReportResults(ReportResults rr)
		{
			ReportDataCollection reportDataCollection = rr.Results;
			foreach (object obj in reportDataCollection)
			{
				ReportData reportData = (ReportData)obj;
				this.results.Add(reportData);
			}
		}

		// Token: 0x06000278 RID: 632 RVA: 0x00029904 File Offset: 0x00027B04
		public void NameCurrentTable(string name)
		{
			ReportData currentReportData = this.results.CurrentReportData;
			bool flag = currentReportData != null;
			if (flag)
			{
				currentReportData.Name = name;
				DataTable dataTable = currentReportData.DataTable;
			}
		}

		// Token: 0x06000279 RID: 633 RVA: 0x00029937 File Offset: 0x00027B37
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep)
		{
			this.ReplaceDataView(dvToReplace, dvToKeep, "unknown");
		}

		// Token: 0x0600027A RID: 634 RVA: 0x00029948 File Offset: 0x00027B48
		public void ReplaceDataView(DataView dvToReplace, DataView dvToKeep, string name)
		{
			ReportData rdToRemove = this.results[dvToReplace];
			this.ReplaceDataView(rdToRemove, dvToKeep, name);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0002996D File Offset: 0x00027B6D
		public void ReplaceDataView(ReportData rdToRemove, DataView dvToKeep, string name)
		{
			this.results.Add(dvToKeep, name);
			this.results.Remove(rdToRemove);
		}

		// Token: 0x040000ED RID: 237
		private ReportDataCollection results;
	}
}
