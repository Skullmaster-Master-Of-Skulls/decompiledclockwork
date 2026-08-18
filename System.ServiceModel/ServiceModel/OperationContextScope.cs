using System;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x0200010F RID: 271
	[__DynamicallyInvokable]
	public sealed class OperationContextScope : IDisposable
	{
		// Token: 0x0600066E RID: 1646 RVA: 0x0001BC2B File Offset: 0x00019E2B
		[__DynamicallyInvokable]
		public OperationContextScope(IContextChannel channel)
		{
			this.PushContext(new OperationContext(channel));
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x0001BC60 File Offset: 0x00019E60
		[__DynamicallyInvokable]
		public OperationContextScope(OperationContext context)
		{
			this.PushContext(context);
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001BC90 File Offset: 0x00019E90
		// (set) Token: 0x06000671 RID: 1649 RVA: 0x0001BCA9 File Offset: 0x00019EA9
		private static OperationContextScope CurrentScope
		{
			get
			{
				if (!ServiceModelAppSettings.DisableOperationContextAsyncFlow)
				{
					return OperationContextScope.currentScope.Value;
				}
				return OperationContextScope.legacyCurrentScope;
			}
			set
			{
				if (ServiceModelAppSettings.DisableOperationContextAsyncFlow)
				{
					OperationContextScope.legacyCurrentScope = value;
					return;
				}
				OperationContextScope.currentScope.Value = value;
			}
		}

		// Token: 0x06000672 RID: 1650 RVA: 0x0001BCC4 File Offset: 0x00019EC4
		[__DynamicallyInvokable]
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
				this.PopContext();
			}
		}

		// Token: 0x06000673 RID: 1651 RVA: 0x0001BCDC File Offset: 0x00019EDC
		private void PushContext(OperationContext context)
		{
			bool shouldUseAsyncLocalContext = OperationContext.ShouldUseAsyncLocalContext;
			this.currentContext = context;
			if (shouldUseAsyncLocalContext)
			{
				OperationContext.EnableAsyncFlow(this.currentContext);
			}
			OperationContextScope.CurrentScope = this;
			OperationContext.Current = this.currentContext;
		}

		// Token: 0x06000674 RID: 1652 RVA: 0x0001BD18 File Offset: 0x00019F18
		private void PopContext()
		{
			if (ServiceModelAppSettings.DisableOperationContextAsyncFlow && this.thread != Thread.CurrentThread)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInvalidContextScopeThread0")));
			}
			if (OperationContextScope.CurrentScope != this)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInterleavedContextScopes0")));
			}
			if (OperationContext.Current != this.currentContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxContextModifiedInsideScope0")));
			}
			OperationContextScope.CurrentScope = this.originalScope;
			OperationContext.Current = this.originalContext;
			if (this.currentContext != null)
			{
				this.currentContext.SetClientReply(null, false);
			}
		}

		// Token: 0x04000A7D RID: 2685
		[ThreadStatic]
		private static OperationContextScope legacyCurrentScope;

		// Token: 0x04000A7E RID: 2686
		private static AsyncLocal<OperationContextScope> currentScope = new AsyncLocal<OperationContextScope>();

		// Token: 0x04000A7F RID: 2687
		private OperationContext currentContext;

		// Token: 0x04000A80 RID: 2688
		private bool disposed;

		// Token: 0x04000A81 RID: 2689
		private readonly OperationContext originalContext = OperationContext.Current;

		// Token: 0x04000A82 RID: 2690
		private readonly OperationContextScope originalScope = OperationContextScope.CurrentScope;

		// Token: 0x04000A83 RID: 2691
		private readonly Thread thread = Thread.CurrentThread;
	}
}
