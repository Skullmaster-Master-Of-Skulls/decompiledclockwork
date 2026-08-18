using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000372 RID: 882
	[ComVisible(true)]
	public class SplitterEventArgs : EventArgs
	{
		// Token: 0x0600399B RID: 14747 RVA: 0x000FFE61 File Offset: 0x000FE061
		public SplitterEventArgs(int x, int y, int splitX, int splitY)
		{
			this.x = x;
			this.y = y;
			this.splitX = splitX;
			this.splitY = splitY;
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x0600399C RID: 14748 RVA: 0x000FFE86 File Offset: 0x000FE086
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x0600399D RID: 14749 RVA: 0x000FFE8E File Offset: 0x000FE08E
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x0600399E RID: 14750 RVA: 0x000FFE96 File Offset: 0x000FE096
		// (set) Token: 0x0600399F RID: 14751 RVA: 0x000FFE9E File Offset: 0x000FE09E
		public int SplitX
		{
			get
			{
				return this.splitX;
			}
			set
			{
				this.splitX = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x060039A0 RID: 14752 RVA: 0x000FFEA7 File Offset: 0x000FE0A7
		// (set) Token: 0x060039A1 RID: 14753 RVA: 0x000FFEAF File Offset: 0x000FE0AF
		public int SplitY
		{
			get
			{
				return this.splitY;
			}
			set
			{
				this.splitY = value;
			}
		}

		// Token: 0x040022D6 RID: 8918
		private readonly int x;

		// Token: 0x040022D7 RID: 8919
		private readonly int y;

		// Token: 0x040022D8 RID: 8920
		private int splitX;

		// Token: 0x040022D9 RID: 8921
		private int splitY;
	}
}
