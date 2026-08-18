using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace ReportFunctions
{
	// Token: 0x0200002A RID: 42
	public class ReportDataCollection : CollectionBase
	{
		// Token: 0x060002D7 RID: 727 RVA: 0x0003B4C0 File Offset: 0x0003A4C0
		public int Add(DataSet ds)
		{
			int result = 0;
			foreach (object obj in ds.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				result = base.List.Add(new ReportData(dataTable.DefaultView, dataTable.TableName, base.List.Count));
			}
			return result;
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060002D8 RID: 728 RVA: 0x0003B558 File Offset: 0x0003A558
		// (set) Token: 0x060002D9 RID: 729 RVA: 0x0003B570 File Offset: 0x0003A570
		public ReportData CurrentReportData
		{
			get
			{
				return this.currentReportData;
			}
			set
			{
				this.currentReportData = value;
			}
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0003B57C File Offset: 0x0003A57C
		public int Add(ReportData reportData)
		{
			this.currentReportData = reportData;
			return base.List.Add(reportData);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0003B5A4 File Offset: 0x0003A5A4
		public int Add(DataView dv, string name)
		{
			this.currentReportData = new ReportData(dv, name, base.List.Count);
			return base.List.Add(this.currentReportData);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0003B5E0 File Offset: 0x0003A5E0
		public int Add(DataTable t, string name)
		{
			this.currentReportData = new ReportData(t, name, base.List.Count);
			return base.List.Add(this.currentReportData);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0003B61C File Offset: 0x0003A61C
		public int AddDontSetCurrentReportData(DataView dv, string name)
		{
			ReportData value = new ReportData(dv, name, base.List.Count);
			return base.List.Add(value);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0003B650 File Offset: 0x0003A650
		public void Remove(string name)
		{
			ReportData reportData = this[name];
			if (reportData != null)
			{
				this.Remove(reportData);
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0003B678 File Offset: 0x0003A678
		public void RemoveAllBut(DataView dv)
		{
			List<ReportData> list = new List<ReportData>();
			foreach (object obj in base.List)
			{
				ReportData reportData = (ReportData)obj;
				DataView dataView = reportData.DataView;
				if (dataView != dv)
				{
					list.Add(reportData);
				}
			}
			foreach (ReportData reportData in list)
			{
				ReportData reportData;
				base.List.Remove(reportData);
			}
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0003B748 File Offset: 0x0003A748
		public void Remove(ReportData reportData)
		{
			base.List.Remove(reportData);
			if (this.currentReportData != null && this.currentReportData == reportData)
			{
				if (base.List.Count > 0)
				{
					this.currentReportData = this[base.List.Count - 1];
				}
				else
				{
					this.currentReportData = null;
				}
			}
		}

		// Token: 0x17000061 RID: 97
		public ReportData this[string name, int id]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					if (reportData.Is(name, id))
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x17000062 RID: 98
		public ReportData this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					if (reportData.Is(name))
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x17000063 RID: 99
		public ReportData this[int index]
		{
			get
			{
				return (ReportData)base.List[index];
			}
		}

		// Token: 0x17000064 RID: 100
		public ReportData this[DataView dv]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					if (reportData.Is(dv))
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0003B950 File Offset: 0x0003A950
		public ReportData Find(int id)
		{
			foreach (object obj in base.List)
			{
				ReportData reportData = (ReportData)obj;
				if (reportData.Id == id)
				{
					return reportData;
				}
			}
			return null;
		}

		// Token: 0x04000152 RID: 338
		private ReportData currentReportData;
	}
}
