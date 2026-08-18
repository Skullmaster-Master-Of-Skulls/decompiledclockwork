using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000221 RID: 545
	public class ReportOrGroup
	{
		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00017A4A File Offset: 0x00015C4A
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00017A52 File Offset: 0x00015C52
		public Report Report { get; set; }

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x00017A5B File Offset: 0x00015C5B
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00017A63 File Offset: 0x00015C63
		public ReportGroup Group { get; set; }
	}
}
