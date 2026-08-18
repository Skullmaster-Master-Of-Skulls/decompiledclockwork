using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200092C RID: 2348
	[EditorBrowsable(EditorBrowsableState.Never)]
	public class TimelinePostBackCommand
	{
		// Token: 0x17001D62 RID: 7522
		// (get) Token: 0x06005924 RID: 22820 RVA: 0x0010FEDA File Offset: 0x0010E0DA
		// (set) Token: 0x06005925 RID: 22821 RVA: 0x0010FEE2 File Offset: 0x0010E0E2
		public Dictionary<string, object> DataItem { get; set; }

		// Token: 0x17001D63 RID: 7523
		// (get) Token: 0x06005926 RID: 22822 RVA: 0x0010FEEB File Offset: 0x0010E0EB
		// (set) Token: 0x06005927 RID: 22823 RVA: 0x0010FEF3 File Offset: 0x0010E0F3
		public string Text { get; set; }

		// Token: 0x17001D64 RID: 7524
		// (get) Token: 0x06005928 RID: 22824 RVA: 0x0010FEFC File Offset: 0x0010E0FC
		// (set) Token: 0x06005929 RID: 22825 RVA: 0x0010FF04 File Offset: 0x0010E104
		public string Value { get; set; }
	}
}
