using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000521 RID: 1313
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class CancelEventArgs : EventArgs
	{
		// Token: 0x060031D9 RID: 12761 RVA: 0x000E0539 File Offset: 0x000DE739
		[__DynamicallyInvokable]
		public CancelEventArgs() : this(false)
		{
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000E0542 File Offset: 0x000DE742
		[__DynamicallyInvokable]
		public CancelEventArgs(bool cancel)
		{
			this.cancel = cancel;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060031DB RID: 12763 RVA: 0x000E0551 File Offset: 0x000DE751
		// (set) Token: 0x060031DC RID: 12764 RVA: 0x000E0559 File Offset: 0x000DE759
		[__DynamicallyInvokable]
		public bool Cancel
		{
			[__DynamicallyInvokable]
			get
			{
				return this.cancel;
			}
			[__DynamicallyInvokable]
			set
			{
				this.cancel = value;
			}
		}

		// Token: 0x04002945 RID: 10565
		private bool cancel;
	}
}
