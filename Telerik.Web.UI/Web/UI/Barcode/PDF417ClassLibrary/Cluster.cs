using System;

namespace Telerik.Web.UI.Barcode.PDF417ClassLibrary
{
	// Token: 0x02000098 RID: 152
	internal class Cluster
	{
		// Token: 0x0600059D RID: 1437 RVA: 0x0000E2C6 File Offset: 0x0000C4C6
		internal Cluster(int position, bool value, int number)
		{
			this.Position = position;
			this.ValueOfModule = value;
			this.NumberOfModulesAtPosition = number;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000E2E3 File Offset: 0x0000C4E3
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x0000E2EB File Offset: 0x0000C4EB
		internal int Position { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000E2F4 File Offset: 0x0000C4F4
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0000E2FC File Offset: 0x0000C4FC
		internal bool ValueOfModule { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000E305 File Offset: 0x0000C505
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0000E30D File Offset: 0x0000C50D
		internal int NumberOfModulesAtPosition { get; set; }
	}
}
