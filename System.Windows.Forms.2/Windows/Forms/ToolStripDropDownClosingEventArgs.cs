using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020003BD RID: 957
	public class ToolStripDropDownClosingEventArgs : CancelEventArgs
	{
		// Token: 0x060040CF RID: 16591 RVA: 0x00114A4B File Offset: 0x00112C4B
		public ToolStripDropDownClosingEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.closeReason = reason;
		}

		// Token: 0x17000FD1 RID: 4049
		// (get) Token: 0x060040D0 RID: 16592 RVA: 0x00114A5A File Offset: 0x00112C5A
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x040024E3 RID: 9443
		private ToolStripDropDownCloseReason closeReason;
	}
}
