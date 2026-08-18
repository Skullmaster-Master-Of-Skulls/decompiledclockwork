using System;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x020005A8 RID: 1448
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class RunWorkerCompletedEventArgs : AsyncCompletedEventArgs
	{
		// Token: 0x06003615 RID: 13845 RVA: 0x000EC708 File Offset: 0x000EA908
		[__DynamicallyInvokable]
		public RunWorkerCompletedEventArgs(object result, Exception error, bool cancelled) : base(error, cancelled, null)
		{
			this.result = result;
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x06003616 RID: 13846 RVA: 0x000EC71A File Offset: 0x000EA91A
		[__DynamicallyInvokable]
		public object Result
		{
			[__DynamicallyInvokable]
			get
			{
				base.RaiseExceptionIfNecessary();
				return this.result;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x06003617 RID: 13847 RVA: 0x000EC728 File Offset: 0x000EA928
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[__DynamicallyInvokable]
		public new object UserState
		{
			[__DynamicallyInvokable]
			get
			{
				return base.UserState;
			}
		}

		// Token: 0x04002A9A RID: 10906
		private object result;
	}
}
