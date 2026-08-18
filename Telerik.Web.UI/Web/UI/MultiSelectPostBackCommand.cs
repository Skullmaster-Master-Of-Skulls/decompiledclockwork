using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000601 RID: 1537
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class MultiSelectPostBackCommand
	{
		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x06003773 RID: 14195 RVA: 0x000B7772 File Offset: 0x000B5972
		// (set) Token: 0x06003774 RID: 14196 RVA: 0x000B777A File Offset: 0x000B597A
		public MultiSelectCommand Type { get; set; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x06003775 RID: 14197 RVA: 0x000B7783 File Offset: 0x000B5983
		// (set) Token: 0x06003776 RID: 14198 RVA: 0x000B778B File Offset: 0x000B598B
		public Dictionary<string, object> DataItem { get; set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x06003777 RID: 14199 RVA: 0x000B7794 File Offset: 0x000B5994
		// (set) Token: 0x06003778 RID: 14200 RVA: 0x000B779C File Offset: 0x000B599C
		public string Text { get; set; }

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x06003779 RID: 14201 RVA: 0x000B77A5 File Offset: 0x000B59A5
		// (set) Token: 0x0600377A RID: 14202 RVA: 0x000B77AD File Offset: 0x000B59AD
		public string Value { get; set; }
	}
}
