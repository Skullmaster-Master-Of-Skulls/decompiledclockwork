using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200031F RID: 799
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class GanttClientState
	{
		// Token: 0x170008F9 RID: 2297
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x00056B53 File Offset: 0x00054D53
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00056B5B File Offset: 0x00054D5B
		public int ScrollTop { get; set; }

		// Token: 0x170008FA RID: 2298
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x00056B64 File Offset: 0x00054D64
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x00056B6C File Offset: 0x00054D6C
		public int ScrollLeft { get; set; }

		// Token: 0x170008FB RID: 2299
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x00056B75 File Offset: 0x00054D75
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00056B7D File Offset: 0x00054D7D
		public GanttViewType SelectedView { get; set; }
	}
}
