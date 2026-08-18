using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000B1B RID: 2843
	public class DropDownListEventArgs : EventArgs
	{
		// Token: 0x06006A50 RID: 27216 RVA: 0x0018EAF5 File Offset: 0x0018CCF5
		public DropDownListEventArgs(int index, string text, string value)
		{
			this.Index = index;
			this.Text = text;
			this.Value = value;
		}

		// Token: 0x170022CA RID: 8906
		// (get) Token: 0x06006A51 RID: 27217 RVA: 0x0018EB12 File Offset: 0x0018CD12
		// (set) Token: 0x06006A52 RID: 27218 RVA: 0x0018EB1A File Offset: 0x0018CD1A
		public int Index { get; set; }

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x06006A53 RID: 27219 RVA: 0x0018EB23 File Offset: 0x0018CD23
		// (set) Token: 0x06006A54 RID: 27220 RVA: 0x0018EB2B File Offset: 0x0018CD2B
		public string Text { get; set; }

		// Token: 0x170022CC RID: 8908
		// (get) Token: 0x06006A55 RID: 27221 RVA: 0x0018EB34 File Offset: 0x0018CD34
		// (set) Token: 0x06006A56 RID: 27222 RVA: 0x0018EB3C File Offset: 0x0018CD3C
		public string Value { get; set; }
	}
}
