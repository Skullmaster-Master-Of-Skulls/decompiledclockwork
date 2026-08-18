using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000370 RID: 880
	public class SplitterCancelEventArgs : CancelEventArgs
	{
		// Token: 0x06003990 RID: 14736 RVA: 0x000FFE09 File Offset: 0x000FE009
		public SplitterCancelEventArgs(int mouseCursorX, int mouseCursorY, int splitX, int splitY) : base(false)
		{
			this.mouseCursorX = mouseCursorX;
			this.mouseCursorY = mouseCursorY;
			this.splitX = splitX;
			this.splitY = splitY;
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06003991 RID: 14737 RVA: 0x000FFE2F File Offset: 0x000FE02F
		public int MouseCursorX
		{
			get
			{
				return this.mouseCursorX;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06003992 RID: 14738 RVA: 0x000FFE37 File Offset: 0x000FE037
		public int MouseCursorY
		{
			get
			{
				return this.mouseCursorY;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06003993 RID: 14739 RVA: 0x000FFE3F File Offset: 0x000FE03F
		// (set) Token: 0x06003994 RID: 14740 RVA: 0x000FFE47 File Offset: 0x000FE047
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

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06003995 RID: 14741 RVA: 0x000FFE50 File Offset: 0x000FE050
		// (set) Token: 0x06003996 RID: 14742 RVA: 0x000FFE58 File Offset: 0x000FE058
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

		// Token: 0x040022D2 RID: 8914
		private readonly int mouseCursorX;

		// Token: 0x040022D3 RID: 8915
		private readonly int mouseCursorY;

		// Token: 0x040022D4 RID: 8916
		private int splitX;

		// Token: 0x040022D5 RID: 8917
		private int splitY;
	}
}
