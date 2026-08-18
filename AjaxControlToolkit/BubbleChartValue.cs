using System;

namespace AjaxControlToolkit
{
	// Token: 0x0200004A RID: 74
	public class BubbleChartValue
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000283 RID: 643 RVA: 0x00008C38 File Offset: 0x00006E38
		// (set) Token: 0x06000284 RID: 644 RVA: 0x00008C40 File Offset: 0x00006E40
		public string Category
		{
			get
			{
				return this._category;
			}
			set
			{
				this._category = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000285 RID: 645 RVA: 0x00008C49 File Offset: 0x00006E49
		// (set) Token: 0x06000286 RID: 646 RVA: 0x00008C51 File Offset: 0x00006E51
		public decimal X
		{
			get
			{
				return this._x;
			}
			set
			{
				this._x = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000287 RID: 647 RVA: 0x00008C5A File Offset: 0x00006E5A
		// (set) Token: 0x06000288 RID: 648 RVA: 0x00008C62 File Offset: 0x00006E62
		public decimal Y
		{
			get
			{
				return this._y;
			}
			set
			{
				this._y = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000289 RID: 649 RVA: 0x00008C6B File Offset: 0x00006E6B
		// (set) Token: 0x0600028A RID: 650 RVA: 0x00008C73 File Offset: 0x00006E73
		public decimal Data
		{
			get
			{
				return this._data;
			}
			set
			{
				this._data = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600028B RID: 651 RVA: 0x00008C7C File Offset: 0x00006E7C
		// (set) Token: 0x0600028C RID: 652 RVA: 0x00008C84 File Offset: 0x00006E84
		public string BubbleColor
		{
			get
			{
				return this._bubbleColor;
			}
			set
			{
				this._bubbleColor = value;
			}
		}

		// Token: 0x040000E4 RID: 228
		private string _category = string.Empty;

		// Token: 0x040000E5 RID: 229
		private decimal _x;

		// Token: 0x040000E6 RID: 230
		private decimal _y;

		// Token: 0x040000E7 RID: 231
		private decimal _data;

		// Token: 0x040000E8 RID: 232
		private string _bubbleColor = string.Empty;
	}
}
