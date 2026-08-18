using System;

namespace Telerik.Web.UI
{
	// Token: 0x02001127 RID: 4391
	public class GridItemEventArgs : EventArgs
	{
		// Token: 0x0600B35C RID: 45916 RVA: 0x00271624 File Offset: 0x0026F824
		public GridItemEventArgs(GridItem item, GridItemEventInfo ItemEvent)
		{
			this.item = item;
			this._eventInfo = ItemEvent;
		}

		// Token: 0x170039F2 RID: 14834
		// (get) Token: 0x0600B35D RID: 45917 RVA: 0x0027163A File Offset: 0x0026F83A
		public GridItem Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x170039F3 RID: 14835
		// (get) Token: 0x0600B35E RID: 45918 RVA: 0x00271642 File Offset: 0x0026F842
		public GridItemEventInfo EventInfo
		{
			get
			{
				return this._eventInfo;
			}
		}

		// Token: 0x170039F4 RID: 14836
		// (get) Token: 0x0600B35F RID: 45919 RVA: 0x0027164A File Offset: 0x0026F84A
		// (set) Token: 0x0600B360 RID: 45920 RVA: 0x00271652 File Offset: 0x0026F852
		public bool Canceled
		{
			get
			{
				return this._canceled;
			}
			set
			{
				this._canceled = value;
			}
		}

		// Token: 0x04002F34 RID: 12084
		private bool _canceled;

		// Token: 0x04002F35 RID: 12085
		private GridItemEventInfo _eventInfo;

		// Token: 0x04002F36 RID: 12086
		private GridItem item;
	}
}
