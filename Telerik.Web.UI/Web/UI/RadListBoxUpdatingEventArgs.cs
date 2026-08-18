using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001939 RID: 6457
	public class RadListBoxUpdatingEventArgs : RadListBoxEventArgs
	{
		// Token: 0x0600F9C2 RID: 63938 RVA: 0x003851C4 File Offset: 0x003833C4
		public RadListBoxUpdatingEventArgs(IList<RadListBoxItem> items) : base(items)
		{
		}

		// Token: 0x17004B6B RID: 19307
		// (get) Token: 0x0600F9C3 RID: 63939 RVA: 0x003851CD File Offset: 0x003833CD
		// (set) Token: 0x0600F9C4 RID: 63940 RVA: 0x003851D5 File Offset: 0x003833D5
		public bool Cancel { get; set; }
	}
}
