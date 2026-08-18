using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000596 RID: 1430
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class ProgressChangedEventArgs : EventArgs
	{
		// Token: 0x06003527 RID: 13607 RVA: 0x000E7D28 File Offset: 0x000E5F28
		[__DynamicallyInvokable]
		public ProgressChangedEventArgs(int progressPercentage, object userState)
		{
			this.progressPercentage = progressPercentage;
			this.userState = userState;
		}

		// Token: 0x17000CFE RID: 3326
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000E7D3E File Offset: 0x000E5F3E
		[SRDescription("Async_ProgressChangedEventArgs_ProgressPercentage")]
		[__DynamicallyInvokable]
		public int ProgressPercentage
		{
			[__DynamicallyInvokable]
			get
			{
				return this.progressPercentage;
			}
		}

		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000E7D46 File Offset: 0x000E5F46
		[SRDescription("Async_ProgressChangedEventArgs_UserState")]
		[__DynamicallyInvokable]
		public object UserState
		{
			[__DynamicallyInvokable]
			get
			{
				return this.userState;
			}
		}

		// Token: 0x04002A40 RID: 10816
		private readonly int progressPercentage;

		// Token: 0x04002A41 RID: 10817
		private readonly object userState;
	}
}
