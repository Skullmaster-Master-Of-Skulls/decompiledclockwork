using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace TechnoPro.Common.DAO.Reports.Impl.Legacy.Entity
{
	// Token: 0x02000021 RID: 33
	public class ReportDataCollection : CollectionBase
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000290F4 File Offset: 0x000272F4
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0002917C File Offset: 0x0002737C
		// (set) Token: 0x06000256 RID: 598 RVA: 0x00029194 File Offset: 0x00027394
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

		// Token: 0x06000257 RID: 599 RVA: 0x000291A0 File Offset: 0x000273A0
		public int Add(ReportData reportData)
		{
			this.currentReportData = reportData;
			return base.List.Add(reportData);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000291C8 File Offset: 0x000273C8
		public int Add(DataView dv, string name)
		{
			this.currentReportData = new ReportData(dv, name, base.List.Count);
			return base.List.Add(this.currentReportData);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00029204 File Offset: 0x00027404
		public int Add(DataTable t, string name)
		{
			this.currentReportData = new ReportData(t, name, base.List.Count);
			return base.List.Add(this.currentReportData);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00029240 File Offset: 0x00027440
		public int AddDontSetCurrentReportData(DataView dv, string name)
		{
			ReportData value = new ReportData(dv, name, base.List.Count);
			return base.List.Add(value);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00029274 File Offset: 0x00027474
		public void Remove(string name)
		{
			ReportData reportData = this[name];
			bool flag = reportData != null;
			if (flag)
			{
				this.Remove(reportData);
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0002929C File Offset: 0x0002749C
		public void RemoveAllBut(DataView dv)
		{
			List<ReportData> list = new List<ReportData>();
			foreach (object obj in base.List)
			{
				ReportData reportData = (ReportData)obj;
				DataView dataView = reportData.DataView;
				bool flag = dataView != dv;
				if (flag)
				{
					list.Add(reportData);
				}
			}
			foreach (ReportData value in list)
			{
				base.List.Remove(value);
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00029360 File Offset: 0x00027560
		public void Remove(ReportData reportData)
		{
			base.List.Remove(reportData);
			bool flag = this.currentReportData != null && this.currentReportData == reportData;
			if (flag)
			{
				bool flag2 = base.List.Count > 0;
				if (flag2)
				{
					this.currentReportData = this[base.List.Count - 1];
				}
				else
				{
					this.currentReportData = null;
				}
			}
		}

		// Token: 0x1700008A RID: 138
		public ReportData this[string name, int id]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					bool flag = reportData.Is(name, id);
					if (flag)
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x1700008B RID: 139
		public ReportData this[string name]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					bool flag = reportData.Is(name);
					if (flag)
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x1700008C RID: 140
		public ReportData this[int index]
		{
			get
			{
				return (ReportData)base.List[index];
			}
		}

		// Token: 0x1700008D RID: 141
		public ReportData this[DataView dv]
		{
			get
			{
				foreach (object obj in base.List)
				{
					ReportData reportData = (ReportData)obj;
					bool flag = reportData.Is(dv);
					if (flag)
					{
						return reportData;
					}
				}
				return null;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00029540 File Offset: 0x00027740
		public ReportData Find(int id)
		{
			foreach (object obj in base.List)
			{
				ReportData reportData = (ReportData)obj;
				bool flag = reportData.Id == id;
				if (flag)
				{
					return reportData;
				}
			}
			return null;
		}

		// Token: 0x040000EA RID: 234
		private ReportData currentReportData;
	}
}
