using System;

namespace Telerik.Web.UI
{
	// Token: 0x020000D5 RID: 213
	public class ImageButtonClickEventArgs : EventArgs
	{
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x0001E43D File Offset: 0x0001C63D
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x0001E445 File Offset: 0x0001C645
		public int X { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x0001E44E File Offset: 0x0001C64E
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001E456 File Offset: 0x0001C656
		public int Y { get; set; }

		// Token: 0x06000815 RID: 2069 RVA: 0x0001E45F File Offset: 0x0001C65F
		public ImageButtonClickEventArgs(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}
	}
}
