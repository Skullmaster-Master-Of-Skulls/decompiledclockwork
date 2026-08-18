using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x02000308 RID: 776
	[ComVisible(true)]
	public class NavigateEventArgs : EventArgs
	{
		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x0600316A RID: 12650 RVA: 0x000DF870 File Offset: 0x000DDA70
		public bool Forward
		{
			get
			{
				return this.isForward;
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000DF878 File Offset: 0x000DDA78
		public NavigateEventArgs(bool isForward)
		{
			this.isForward = isForward;
		}

		// Token: 0x04001E2D RID: 7725
		private bool isForward = true;
	}
}
