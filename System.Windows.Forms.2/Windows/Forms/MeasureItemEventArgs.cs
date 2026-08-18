using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020002F1 RID: 753
	public class MeasureItemEventArgs : EventArgs
	{
		// Token: 0x06002F9A RID: 12186 RVA: 0x000D6EAD File Offset: 0x000D50AD
		public MeasureItemEventArgs(Graphics graphics, int index, int itemHeight)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = itemHeight;
			this.itemWidth = 0;
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000D6ED1 File Offset: 0x000D50D1
		public MeasureItemEventArgs(Graphics graphics, int index)
		{
			this.graphics = graphics;
			this.index = index;
			this.itemHeight = 0;
			this.itemWidth = 0;
		}

		// Token: 0x17000B32 RID: 2866
		// (get) Token: 0x06002F9C RID: 12188 RVA: 0x000D6EF5 File Offset: 0x000D50F5
		public Graphics Graphics
		{
			get
			{
				return this.graphics;
			}
		}

		// Token: 0x17000B33 RID: 2867
		// (get) Token: 0x06002F9D RID: 12189 RVA: 0x000D6EFD File Offset: 0x000D50FD
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x06002F9E RID: 12190 RVA: 0x000D6F05 File Offset: 0x000D5105
		// (set) Token: 0x06002F9F RID: 12191 RVA: 0x000D6F0D File Offset: 0x000D510D
		public int ItemHeight
		{
			get
			{
				return this.itemHeight;
			}
			set
			{
				this.itemHeight = value;
			}
		}

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x000D6F16 File Offset: 0x000D5116
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x000D6F1E File Offset: 0x000D511E
		public int ItemWidth
		{
			get
			{
				return this.itemWidth;
			}
			set
			{
				this.itemWidth = value;
			}
		}

		// Token: 0x040013AD RID: 5037
		private int itemHeight;

		// Token: 0x040013AE RID: 5038
		private int itemWidth;

		// Token: 0x040013AF RID: 5039
		private int index;

		// Token: 0x040013B0 RID: 5040
		private readonly Graphics graphics;
	}
}
