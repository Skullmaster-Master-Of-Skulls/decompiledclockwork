using System;

namespace ReportFunctions
{
	// Token: 0x0200000D RID: 13
	public class ReportReportNode : ReportNode
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00005EC2 File Offset: 0x00004EC2
		public ReportReportNode()
		{
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00005ED0 File Offset: 0x00004ED0
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00005EE8 File Offset: 0x00004EE8
		public Report Report
		{
			get
			{
				return this.report;
			}
			set
			{
				this.report = value;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005EF2 File Offset: 0x00004EF2
		public ReportReportNode(Report report, int orderNum)
		{
			base.Title = report.ReportTitle;
			this.report = report;
			base.OrderNum = orderNum;
		}

		// Token: 0x040000DD RID: 221
		private Report report;
	}
}
