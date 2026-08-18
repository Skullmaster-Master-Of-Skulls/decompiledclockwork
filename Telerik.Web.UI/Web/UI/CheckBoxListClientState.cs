using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020000AD RID: 173
	[EditorBrowsable(EditorBrowsableState.Never)]
	internal class CheckBoxListClientState : ButtonListClientState
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0001ACE5 File Offset: 0x00018EE5
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0001ACED File Offset: 0x00018EED
		public int[] SelectedIndices { get; set; }
	}
}
