using System;

namespace System.Windows.Forms
{
	// Token: 0x02000260 RID: 608
	public class FormClosedEventArgs : EventArgs
	{
		// Token: 0x06002780 RID: 10112 RVA: 0x000B8D5A File Offset: 0x000B6F5A
		public FormClosedEventArgs(CloseReason closeReason)
		{
			this.closeReason = closeReason;
		}

		// Token: 0x17000921 RID: 2337
		// (get) Token: 0x06002781 RID: 10113 RVA: 0x000B8D69 File Offset: 0x000B6F69
		public CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x04001047 RID: 4167
		private CloseReason closeReason;
	}
}
