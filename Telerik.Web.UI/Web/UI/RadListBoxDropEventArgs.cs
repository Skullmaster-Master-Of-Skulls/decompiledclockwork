using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001926 RID: 6438
	public class RadListBoxDropEventArgs : EventArgs
	{
		// Token: 0x0600F972 RID: 63858 RVA: 0x00385028 File Offset: 0x00383228
		public RadListBoxDropEventArgs(string htmlElementId, IList<RadListBoxItem> sourceDragItems)
		{
			this._sourceDragItems = sourceDragItems;
			this._htmlElementId = htmlElementId;
		}

		// Token: 0x17004B59 RID: 19289
		// (get) Token: 0x0600F973 RID: 63859 RVA: 0x0038503E File Offset: 0x0038323E
		public IList<RadListBoxItem> SourceDragItems
		{
			get
			{
				return this._sourceDragItems;
			}
		}

		// Token: 0x17004B5A RID: 19290
		// (get) Token: 0x0600F974 RID: 63860 RVA: 0x00385046 File Offset: 0x00383246
		public string HtmlElementID
		{
			get
			{
				return this._htmlElementId;
			}
		}

		// Token: 0x04004718 RID: 18200
		private readonly IList<RadListBoxItem> _sourceDragItems;

		// Token: 0x04004719 RID: 18201
		private readonly string _htmlElementId;
	}
}
