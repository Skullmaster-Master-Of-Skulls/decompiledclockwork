using System;
using System.IO;

namespace System.Web.Instrumentation
{
	// Token: 0x020001B2 RID: 434
	public class PageExecutionContext
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x00047467 File Offset: 0x00045667
		// (set) Token: 0x0600166E RID: 5742 RVA: 0x0004746F File Offset: 0x0004566F
		public bool IsLiteral { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x0600166F RID: 5743 RVA: 0x00047478 File Offset: 0x00045678
		// (set) Token: 0x06001670 RID: 5744 RVA: 0x00047480 File Offset: 0x00045680
		public int Length { get; set; }

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001671 RID: 5745 RVA: 0x00047489 File Offset: 0x00045689
		// (set) Token: 0x06001672 RID: 5746 RVA: 0x00047491 File Offset: 0x00045691
		public int StartPosition { get; set; }

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001673 RID: 5747 RVA: 0x0004749A File Offset: 0x0004569A
		// (set) Token: 0x06001674 RID: 5748 RVA: 0x000474A2 File Offset: 0x000456A2
		public TextWriter TextWriter { get; set; }

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001675 RID: 5749 RVA: 0x000474AB File Offset: 0x000456AB
		// (set) Token: 0x06001676 RID: 5750 RVA: 0x000474B3 File Offset: 0x000456B3
		public string VirtualPath { get; set; }
	}
}
