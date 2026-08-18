using System;

namespace System.Windows.Forms
{
	// Token: 0x020003BB RID: 955
	public class ToolStripDropDownClosedEventArgs : EventArgs
	{
		// Token: 0x060040C9 RID: 16585 RVA: 0x00114A34 File Offset: 0x00112C34
		public ToolStripDropDownClosedEventArgs(ToolStripDropDownCloseReason reason)
		{
			this.closeReason = reason;
		}

		// Token: 0x17000FD0 RID: 4048
		// (get) Token: 0x060040CA RID: 16586 RVA: 0x00114A43 File Offset: 0x00112C43
		public ToolStripDropDownCloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x040024E2 RID: 9442
		private ToolStripDropDownCloseReason closeReason;
	}
}
