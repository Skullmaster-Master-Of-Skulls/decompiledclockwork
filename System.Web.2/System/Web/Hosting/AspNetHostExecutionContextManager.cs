using System;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Hosting
{
	// Token: 0x0200079C RID: 1948
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	internal sealed class AspNetHostExecutionContextManager : HostExecutionContextManager
	{
		// Token: 0x06005CAB RID: 23723 RVA: 0x00140878 File Offset: 0x0013EA78
		public override HostExecutionContext Capture()
		{
			ThreadContext threadContext = ThreadContext.Current;
			if (threadContext != null)
			{
				return new AspNetHostExecutionContextManager.AspNetHostExecutionContext(base.Capture(), threadContext.HttpContext.ThreadContextId);
			}
			return base.Capture();
		}

		// Token: 0x06005CAC RID: 23724 RVA: 0x001408AC File Offset: 0x0013EAAC
		public override void Revert(object previousState)
		{
			AspNetHostExecutionContextManager.RevertAction revertAction = previousState as AspNetHostExecutionContextManager.RevertAction;
			if (revertAction != null)
			{
				revertAction();
				return;
			}
			base.Revert(previousState);
		}

		// Token: 0x06005CAD RID: 23725 RVA: 0x001408D4 File Offset: 0x0013EAD4
		public override object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			AspNetHostExecutionContextManager.AspNetHostExecutionContext aspNetHostExecutionContext = hostExecutionContext as AspNetHostExecutionContextManager.AspNetHostExecutionContext;
			if (aspNetHostExecutionContext == null)
			{
				return base.SetHostExecutionContext(hostExecutionContext);
			}
			object baseRevertParameter = null;
			if (aspNetHostExecutionContext.BaseContext != null)
			{
				baseRevertParameter = base.SetHostExecutionContext(aspNetHostExecutionContext.BaseContext);
			}
			ThreadContext threadContext = ThreadContext.Current;
			if (threadContext != null && threadContext.HttpContext.ThreadContextId == aspNetHostExecutionContext.HttpContextThreadContextId)
			{
				Action threadContextCleanupAction = threadContext.EnterExecutionContext();
				return new AspNetHostExecutionContextManager.RevertAction(delegate()
				{
					threadContextCleanupAction();
					if (baseRevertParameter != null)
					{
						this.<>n__0(baseRevertParameter);
					}
				});
			}
			return baseRevertParameter;
		}

		// Token: 0x02000A54 RID: 2644
		// (Invoke) Token: 0x06006ECF RID: 28367
		private delegate void RevertAction();

		// Token: 0x02000A55 RID: 2645
		private sealed class AspNetHostExecutionContext : HostExecutionContext
		{
			// Token: 0x06006ED2 RID: 28370 RVA: 0x0018B043 File Offset: 0x00189243
			internal AspNetHostExecutionContext(HostExecutionContext baseContext, object httpContextThreadContextId)
			{
				this.BaseContext = baseContext;
				this.HttpContextThreadContextId = httpContextThreadContextId;
			}

			// Token: 0x06006ED3 RID: 28371 RVA: 0x0018B059 File Offset: 0x00189259
			private AspNetHostExecutionContext(AspNetHostExecutionContextManager.AspNetHostExecutionContext original) : this(AspNetHostExecutionContextManager.AspNetHostExecutionContext.CreateCopyHelper(original.BaseContext), original.HttpContextThreadContextId)
			{
			}

			// Token: 0x06006ED4 RID: 28372 RVA: 0x0018B072 File Offset: 0x00189272
			public override HostExecutionContext CreateCopy()
			{
				return new AspNetHostExecutionContextManager.AspNetHostExecutionContext(this);
			}

			// Token: 0x06006ED5 RID: 28373 RVA: 0x0018B07A File Offset: 0x0018927A
			private static HostExecutionContext CreateCopyHelper(HostExecutionContext hostExecutionContext)
			{
				if (hostExecutionContext == null)
				{
					return null;
				}
				return hostExecutionContext.CreateCopy();
			}

			// Token: 0x06006ED6 RID: 28374 RVA: 0x0018B087 File Offset: 0x00189287
			public override void Dispose(bool disposing)
			{
				if (disposing && this.BaseContext != null)
				{
					this.BaseContext.Dispose();
				}
			}

			// Token: 0x04003B6E RID: 15214
			public readonly HostExecutionContext BaseContext;

			// Token: 0x04003B6F RID: 15215
			public readonly object HttpContextThreadContextId;
		}
	}
}
