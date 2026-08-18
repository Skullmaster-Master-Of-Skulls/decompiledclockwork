using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x020002A1 RID: 673
	public class InvalidateEventArgs : EventArgs
	{
		// Token: 0x06002A2B RID: 10795 RVA: 0x000BFB4B File Offset: 0x000BDD4B
		public InvalidateEventArgs(Rectangle invalidRect)
		{
			this.invalidRect = invalidRect;
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002A2C RID: 10796 RVA: 0x000BFB5A File Offset: 0x000BDD5A
		public Rectangle InvalidRect
		{
			get
			{
				return this.invalidRect;
			}
		}

		// Token: 0x04001124 RID: 4388
		private readonly Rectangle invalidRect;
	}
}
