using System;
using System.Security.Permissions;
using System.Threading;

namespace System.ComponentModel
{
	// Token: 0x02000513 RID: 1299
	[__DynamicallyInvokable]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true)]
	public sealed class AsyncOperation
	{
		// Token: 0x06003134 RID: 12596 RVA: 0x000DEFD0 File Offset: 0x000DD1D0
		private AsyncOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			this.userSuppliedState = userSuppliedState;
			this.syncContext = syncContext;
			this.alreadyCompleted = false;
			this.syncContext.OperationStarted();
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000DEFF8 File Offset: 0x000DD1F8
		~AsyncOperation()
		{
			if (!this.alreadyCompleted && this.syncContext != null)
			{
				this.syncContext.OperationCompleted();
			}
		}

		// Token: 0x17000C09 RID: 3081
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x000DF03C File Offset: 0x000DD23C
		[__DynamicallyInvokable]
		public object UserSuppliedState
		{
			[__DynamicallyInvokable]
			get
			{
				return this.userSuppliedState;
			}
		}

		// Token: 0x17000C0A RID: 3082
		// (get) Token: 0x06003137 RID: 12599 RVA: 0x000DF044 File Offset: 0x000DD244
		[__DynamicallyInvokable]
		public SynchronizationContext SynchronizationContext
		{
			[__DynamicallyInvokable]
			get
			{
				return this.syncContext;
			}
		}

		// Token: 0x06003138 RID: 12600 RVA: 0x000DF04C File Offset: 0x000DD24C
		[__DynamicallyInvokable]
		public void Post(SendOrPostCallback d, object arg)
		{
			this.VerifyNotCompleted();
			this.VerifyDelegateNotNull(d);
			this.syncContext.Post(d, arg);
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000DF068 File Offset: 0x000DD268
		[__DynamicallyInvokable]
		public void PostOperationCompleted(SendOrPostCallback d, object arg)
		{
			this.Post(d, arg);
			this.OperationCompletedCore();
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000DF078 File Offset: 0x000DD278
		[__DynamicallyInvokable]
		public void OperationCompleted()
		{
			this.VerifyNotCompleted();
			this.OperationCompletedCore();
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000DF088 File Offset: 0x000DD288
		private void OperationCompletedCore()
		{
			try
			{
				this.syncContext.OperationCompleted();
			}
			finally
			{
				this.alreadyCompleted = true;
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600313C RID: 12604 RVA: 0x000DF0C0 File Offset: 0x000DD2C0
		private void VerifyNotCompleted()
		{
			if (this.alreadyCompleted)
			{
				throw new InvalidOperationException(SR.GetString("Async_OperationAlreadyCompleted"));
			}
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000DF0DA File Offset: 0x000DD2DA
		private void VerifyDelegateNotNull(SendOrPostCallback d)
		{
			if (d == null)
			{
				throw new ArgumentNullException(SR.GetString("Async_NullDelegate"), "d");
			}
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000DF0F4 File Offset: 0x000DD2F4
		internal static AsyncOperation CreateOperation(object userSuppliedState, SynchronizationContext syncContext)
		{
			return new AsyncOperation(userSuppliedState, syncContext);
		}

		// Token: 0x04002910 RID: 10512
		private SynchronizationContext syncContext;

		// Token: 0x04002911 RID: 10513
		private object userSuppliedState;

		// Token: 0x04002912 RID: 10514
		private bool alreadyCompleted;
	}
}
