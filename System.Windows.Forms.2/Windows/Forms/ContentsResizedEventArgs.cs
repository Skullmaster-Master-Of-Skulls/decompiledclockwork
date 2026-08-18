using System;
using System.Drawing;

namespace System.Windows.Forms
{
	// Token: 0x02000343 RID: 835
	public class ContentsResizedEventArgs : EventArgs
	{
		// Token: 0x060035E2 RID: 13794 RVA: 0x000F390B File Offset: 0x000F1B0B
		public ContentsResizedEventArgs(Rectangle newRectangle)
		{
			this.newRectangle = newRectangle;
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060035E3 RID: 13795 RVA: 0x000F391A File Offset: 0x000F1B1A
		public Rectangle NewRectangle
		{
			get
			{
				return this.newRectangle;
			}
		}

		// Token: 0x04001F75 RID: 8053
		private readonly Rectangle newRectangle;
	}
}
