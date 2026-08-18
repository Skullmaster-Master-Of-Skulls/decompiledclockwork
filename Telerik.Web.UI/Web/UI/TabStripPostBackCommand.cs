using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000F16 RID: 3862
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TabStripPostBackCommand
	{
		// Token: 0x17002E77 RID: 11895
		// (get) Token: 0x060092EF RID: 37615 RVA: 0x002107A3 File Offset: 0x0020E9A3
		// (set) Token: 0x060092F0 RID: 37616 RVA: 0x002107AB File Offset: 0x0020E9AB
		public TabStripCommand Type { get; set; }

		// Token: 0x17002E78 RID: 11896
		// (get) Token: 0x060092F1 RID: 37617 RVA: 0x002107B4 File Offset: 0x0020E9B4
		// (set) Token: 0x060092F2 RID: 37618 RVA: 0x002107BC File Offset: 0x0020E9BC
		public string Index { get; set; }

		// Token: 0x17002E79 RID: 11897
		// (get) Token: 0x060092F3 RID: 37619 RVA: 0x002107C5 File Offset: 0x0020E9C5
		// (set) Token: 0x060092F4 RID: 37620 RVA: 0x002107CD File Offset: 0x0020E9CD
		public int Offset { get; set; }
	}
}
