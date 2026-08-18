using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000213 RID: 531
	public class ReportCollection
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00017597 File Offset: 0x00015797
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x0001759F File Offset: 0x0001579F
		public IList<Report> Reports { get; set; }

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x000175A8 File Offset: 0x000157A8
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x000175B0 File Offset: 0x000157B0
		public IList<ReportGroup> ReportGroups { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x000175BC File Offset: 0x000157BC
		public static ReportCollection Empty
		{
			get
			{
				return new ReportCollection
				{
					Reports = new List<Report>(),
					ReportGroups = new List<ReportGroup>()
				};
			}
		}
	}
}
