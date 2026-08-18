using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000272 RID: 626
	[ComVisible(true)]
	public class HelpEventArgs : EventArgs
	{
		// Token: 0x0600280F RID: 10255 RVA: 0x000BA994 File Offset: 0x000B8B94
		public HelpEventArgs(Point mousePos)
		{
			this.mousePos = mousePos;
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002810 RID: 10256 RVA: 0x000BA9A3 File Offset: 0x000B8BA3
		public Point MousePos
		{
			get
			{
				return this.mousePos;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x000BA9AB File Offset: 0x000B8BAB
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x000BA9B3 File Offset: 0x000B8BB3
		public bool Handled
		{
			get
			{
				return this.handled;
			}
			set
			{
				this.handled = value;
			}
		}

		// Token: 0x0400108E RID: 4238
		private readonly Point mousePos;

		// Token: 0x0400108F RID: 4239
		private bool handled;
	}
}
