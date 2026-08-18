using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x02000262 RID: 610
	public class FormClosingEventArgs : CancelEventArgs
	{
		// Token: 0x06002786 RID: 10118 RVA: 0x000B8D71 File Offset: 0x000B6F71
		public FormClosingEventArgs(CloseReason closeReason, bool cancel) : base(cancel)
		{
			this.closeReason = closeReason;
		}

		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002787 RID: 10119 RVA: 0x000B8D81 File Offset: 0x000B6F81
		public CloseReason CloseReason
		{
			get
			{
				return this.closeReason;
			}
		}

		// Token: 0x04001048 RID: 4168
		private CloseReason closeReason;
	}
}
