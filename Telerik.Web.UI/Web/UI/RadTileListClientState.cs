using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000910 RID: 2320
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class RadTileListClientState
	{
		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x060057A0 RID: 22432 RVA: 0x0010BBE0 File Offset: 0x00109DE0
		// (set) Token: 0x060057A1 RID: 22433 RVA: 0x0010BBE8 File Offset: 0x00109DE8
		public int[] SelectedIndices { get; set; }

		// Token: 0x17001CFE RID: 7422
		// (get) Token: 0x060057A2 RID: 22434 RVA: 0x0010BBF1 File Offset: 0x00109DF1
		// (set) Token: 0x060057A3 RID: 22435 RVA: 0x0010BBF9 File Offset: 0x00109DF9
		public ArrayList TileGroupIndices { get; set; }

		// Token: 0x17001CFF RID: 7423
		// (get) Token: 0x060057A4 RID: 22436 RVA: 0x0010BC02 File Offset: 0x00109E02
		// (set) Token: 0x060057A5 RID: 22437 RVA: 0x0010BC0A File Offset: 0x00109E0A
		public ArrayList TileGroupTitles { get; set; }

		// Token: 0x17001D00 RID: 7424
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x0010BC13 File Offset: 0x00109E13
		// (set) Token: 0x060057A7 RID: 22439 RVA: 0x0010BC1B File Offset: 0x00109E1B
		public ArrayList TileGroupNames { get; set; }

		// Token: 0x17001D01 RID: 7425
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x0010BC24 File Offset: 0x00109E24
		// (set) Token: 0x060057A9 RID: 22441 RVA: 0x0010BC2C File Offset: 0x00109E2C
		public bool IsEnabled { get; set; }
	}
}
