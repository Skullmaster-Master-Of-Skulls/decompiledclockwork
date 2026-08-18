using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000303 RID: 771
	[ComVisible(true)]
	public class MouseEventArgs : EventArgs
	{
		// Token: 0x0600312C RID: 12588 RVA: 0x000DD88C File Offset: 0x000DBA8C
		public MouseEventArgs(MouseButtons button, int clicks, int x, int y, int delta)
		{
			this.button = button;
			this.clicks = clicks;
			this.x = x;
			this.y = y;
			this.delta = delta;
		}

		// Token: 0x17000B8B RID: 2955
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000DD8B9 File Offset: 0x000DBAB9
		public MouseButtons Button
		{
			get
			{
				return this.button;
			}
		}

		// Token: 0x17000B8C RID: 2956
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000DD8C1 File Offset: 0x000DBAC1
		public int Clicks
		{
			get
			{
				return this.clicks;
			}
		}

		// Token: 0x17000B8D RID: 2957
		// (get) Token: 0x0600312F RID: 12591 RVA: 0x000DD8C9 File Offset: 0x000DBAC9
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x000DD8D1 File Offset: 0x000DBAD1
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06003131 RID: 12593 RVA: 0x000DD8D9 File Offset: 0x000DBAD9
		public int Delta
		{
			get
			{
				return this.delta;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x000DD8E1 File Offset: 0x000DBAE1
		public Point Location
		{
			get
			{
				return new Point(this.x, this.y);
			}
		}

		// Token: 0x0400144F RID: 5199
		private readonly MouseButtons button;

		// Token: 0x04001450 RID: 5200
		private readonly int clicks;

		// Token: 0x04001451 RID: 5201
		private readonly int x;

		// Token: 0x04001452 RID: 5202
		private readonly int y;

		// Token: 0x04001453 RID: 5203
		private readonly int delta;
	}
}
