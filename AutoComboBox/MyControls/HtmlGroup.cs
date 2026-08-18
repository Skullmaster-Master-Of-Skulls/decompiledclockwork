using System;
using System.Text;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000D7 RID: 215
	internal sealed class HtmlGroup
	{
		// Token: 0x06000867 RID: 2151 RVA: 0x00041D06 File Offset: 0x00040D06
		public HtmlGroup(string title)
		{
			this.title = title;
			this.items = new StringBuilder();
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000868 RID: 2152 RVA: 0x00041D24 File Offset: 0x00040D24
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x00041D3C File Offset: 0x00040D3C
		public StringBuilder Items
		{
			get
			{
				return this.items;
			}
		}

		// Token: 0x04000633 RID: 1587
		private string title;

		// Token: 0x04000634 RID: 1588
		private StringBuilder items;
	}
}
