using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200128D RID: 4749
	[Serializable]
	public struct TreeListSiblingState
	{
		// Token: 0x17004000 RID: 16384
		// (get) Token: 0x0600C62B RID: 50731 RVA: 0x002C3923 File Offset: 0x002C1B23
		// (set) Token: 0x0600C62C RID: 50732 RVA: 0x002C392B File Offset: 0x002C1B2B
		public bool HasPrevPageSiblings { get; set; }

		// Token: 0x17004001 RID: 16385
		// (get) Token: 0x0600C62D RID: 50733 RVA: 0x002C3934 File Offset: 0x002C1B34
		// (set) Token: 0x0600C62E RID: 50734 RVA: 0x002C393C File Offset: 0x002C1B3C
		public bool HasNextPageSiblings { get; set; }
	}
}
