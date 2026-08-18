using System;

namespace System.Windows.Forms
{
	// Token: 0x020002BC RID: 700
	public class LabelEditEventArgs : EventArgs
	{
		// Token: 0x06002B1F RID: 11039 RVA: 0x000C2303 File Offset: 0x000C0503
		public LabelEditEventArgs(int item)
		{
			this.item = item;
			this.label = null;
		}

		// Token: 0x06002B20 RID: 11040 RVA: 0x000C2319 File Offset: 0x000C0519
		public LabelEditEventArgs(int item, string label)
		{
			this.item = item;
			this.label = label;
		}

		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06002B21 RID: 11041 RVA: 0x000C232F File Offset: 0x000C052F
		public string Label
		{
			get
			{
				return this.label;
			}
		}

		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x000C2337 File Offset: 0x000C0537
		public int Item
		{
			get
			{
				return this.item;
			}
		}

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x06002B23 RID: 11043 RVA: 0x000C233F File Offset: 0x000C053F
		// (set) Token: 0x06002B24 RID: 11044 RVA: 0x000C2347 File Offset: 0x000C0547
		public bool CancelEdit
		{
			get
			{
				return this.cancelEdit;
			}
			set
			{
				this.cancelEdit = value;
			}
		}

		// Token: 0x04001228 RID: 4648
		private readonly string label;

		// Token: 0x04001229 RID: 4649
		private readonly int item;

		// Token: 0x0400122A RID: 4650
		private bool cancelEdit;
	}
}
