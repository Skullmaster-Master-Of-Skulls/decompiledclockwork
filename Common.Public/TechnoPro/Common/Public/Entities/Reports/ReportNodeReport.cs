using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000228 RID: 552
	public class ReportNodeReport : IReportNode
	{
		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x060010FC RID: 4348 RVA: 0x00017D64 File Offset: 0x00015F64
		// (set) Token: 0x060010FD RID: 4349 RVA: 0x00017D6C File Offset: 0x00015F6C
		public ReportGroup Group { get; set; }

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x060010FE RID: 4350 RVA: 0x00017D78 File Offset: 0x00015F78
		public ReportNodeType NodeType
		{
			get
			{
				return ReportNodeType.Report;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x00017D8C File Offset: 0x00015F8C
		public string Title
		{
			get
			{
				return (this.Group == null) ? "" : this.Group.Title;
			}
		}
	}
}
