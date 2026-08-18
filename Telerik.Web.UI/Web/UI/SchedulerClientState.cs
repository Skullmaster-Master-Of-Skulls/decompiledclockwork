using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02001A3D RID: 6717
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class SchedulerClientState
	{
		// Token: 0x17004EF5 RID: 20213
		// (get) Token: 0x06010499 RID: 66713 RVA: 0x003A3957 File Offset: 0x003A1B57
		// (set) Token: 0x0601049A RID: 66714 RVA: 0x003A395F File Offset: 0x003A1B5F
		public int ScrollTop { get; set; }

		// Token: 0x17004EF6 RID: 20214
		// (get) Token: 0x0601049B RID: 66715 RVA: 0x003A3968 File Offset: 0x003A1B68
		// (set) Token: 0x0601049C RID: 66716 RVA: 0x003A3970 File Offset: 0x003A1B70
		public int ScrollLeft { get; set; }

		// Token: 0x17004EF7 RID: 20215
		// (get) Token: 0x0601049D RID: 66717 RVA: 0x003A3979 File Offset: 0x003A1B79
		// (set) Token: 0x0601049E RID: 66718 RVA: 0x003A3981 File Offset: 0x003A1B81
		public bool IsDirty { get; set; }
	}
}
