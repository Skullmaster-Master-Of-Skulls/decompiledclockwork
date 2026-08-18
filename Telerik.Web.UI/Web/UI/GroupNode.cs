using System;
using Telerik.Web.UI.PivotGrid.Core;

namespace Telerik.Web.UI
{
	// Token: 0x02000DC1 RID: 3521
	public class GroupNode
	{
		// Token: 0x17002987 RID: 10631
		// (get) Token: 0x0600836C RID: 33644 RVA: 0x001DF46E File Offset: 0x001DD66E
		// (set) Token: 0x0600836D RID: 33645 RVA: 0x001DF476 File Offset: 0x001DD676
		public IGroup Group { get; set; }

		// Token: 0x17002988 RID: 10632
		// (get) Token: 0x0600836E RID: 33646 RVA: 0x001DF47F File Offset: 0x001DD67F
		// (set) Token: 0x0600836F RID: 33647 RVA: 0x001DF487 File Offset: 0x001DD687
		public bool isCollapsed { get; set; }
	}
}
