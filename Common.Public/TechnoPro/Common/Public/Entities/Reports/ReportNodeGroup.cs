using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000227 RID: 551
	public class ReportNodeGroup : IReportNode
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x00017D11 File Offset: 0x00015F11
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x00017D19 File Offset: 0x00015F19
		public Report Report { get; set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x00017D24 File Offset: 0x00015F24
		public ReportNodeType NodeType
		{
			get
			{
				return ReportNodeType.Group;
			}
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x060010FA RID: 4346 RVA: 0x00017D38 File Offset: 0x00015F38
		public string Title
		{
			get
			{
				return (this.Report == null) ? "" : this.Report.Title;
			}
		}
	}
}
