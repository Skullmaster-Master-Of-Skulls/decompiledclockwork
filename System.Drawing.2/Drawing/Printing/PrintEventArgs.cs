using System;
using System.ComponentModel;

namespace System.Drawing.Printing
{
	// Token: 0x02000067 RID: 103
	public class PrintEventArgs : CancelEventArgs
	{
		// Token: 0x060007F0 RID: 2032 RVA: 0x000207E0 File Offset: 0x0001E9E0
		public PrintEventArgs()
		{
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x000207E8 File Offset: 0x0001E9E8
		internal PrintEventArgs(PrintAction action)
		{
			this.printAction = action;
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x060007F2 RID: 2034 RVA: 0x000207F7 File Offset: 0x0001E9F7
		public PrintAction PrintAction
		{
			get
			{
				return this.printAction;
			}
		}

		// Token: 0x040006EB RID: 1771
		private PrintAction printAction;
	}
}
