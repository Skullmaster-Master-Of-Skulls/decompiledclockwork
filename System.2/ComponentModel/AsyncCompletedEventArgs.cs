using System;
using System.Reflection;
using System.Security.Permissions;

namespace System.ComponentModel
{
	// Token: 0x02000511 RID: 1297
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public class AsyncCompletedEventArgs : EventArgs
	{
		// Token: 0x0600312A RID: 12586 RVA: 0x000DEF5B File Offset: 0x000DD15B
		[Obsolete("This API supports the .NET Framework infrastructure and is not intended to be used directly from your code.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public AsyncCompletedEventArgs()
		{
		}

		// Token: 0x0600312B RID: 12587 RVA: 0x000DEF63 File Offset: 0x000DD163
		[__DynamicallyInvokable]
		public AsyncCompletedEventArgs(Exception error, bool cancelled, object userState)
		{
			this.error = error;
			this.cancelled = cancelled;
			this.userState = userState;
		}

		// Token: 0x17000C06 RID: 3078
		// (get) Token: 0x0600312C RID: 12588 RVA: 0x000DEF80 File Offset: 0x000DD180
		[SRDescription("Async_AsyncEventArgs_Cancelled")]
		[__DynamicallyInvokable]
		public bool Cancelled
		{
			[__DynamicallyInvokable]
			get
			{
				return this.cancelled;
			}
		}

		// Token: 0x17000C07 RID: 3079
		// (get) Token: 0x0600312D RID: 12589 RVA: 0x000DEF88 File Offset: 0x000DD188
		[SRDescription("Async_AsyncEventArgs_Error")]
		[__DynamicallyInvokable]
		public Exception Error
		{
			[__DynamicallyInvokable]
			get
			{
				return this.error;
			}
		}

		// Token: 0x17000C08 RID: 3080
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x000DEF90 File Offset: 0x000DD190
		[SRDescription("Async_AsyncEventArgs_UserState")]
		[__DynamicallyInvokable]
		public object UserState
		{
			[__DynamicallyInvokable]
			get
			{
				return this.userState;
			}
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000DEF98 File Offset: 0x000DD198
		[__DynamicallyInvokable]
		protected void RaiseExceptionIfNecessary()
		{
			if (this.Error != null)
			{
				throw new TargetInvocationException(SR.GetString("Async_ExceptionOccurred"), this.Error);
			}
			if (this.Cancelled)
			{
				throw new InvalidOperationException(SR.GetString("Async_OperationCancelled"));
			}
		}

		// Token: 0x0400290D RID: 10509
		private readonly Exception error;

		// Token: 0x0400290E RID: 10510
		private readonly bool cancelled;

		// Token: 0x0400290F RID: 10511
		private readonly object userState;
	}
}
