using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using System.Security.Principal;
using System.Threading;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200001F RID: 31
	internal sealed class ThreadContext : ISyncContextLock
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00003EBE File Offset: 0x000020BE
		internal ThreadContext(HttpContext httpContext)
		{
			this.HttpContext = httpContext;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003ECD File Offset: 0x000020CD
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00003ED4 File Offset: 0x000020D4
		internal static ThreadContext Current
		{
			get
			{
				return ThreadContext._currentThreadContext;
			}
			private set
			{
				ThreadContext._currentThreadContext = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003EDC File Offset: 0x000020DC
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003EE4 File Offset: 0x000020E4
		internal bool HasBeenDisassociatedFromThread { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003EED File Offset: 0x000020ED
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00003EF5 File Offset: 0x000020F5
		internal HttpContext HttpContext { get; private set; }

		// Token: 0x060000E2 RID: 226 RVA: 0x00003F00 File Offset: 0x00002100
		internal void AssociateWithCurrentThread(bool setImpersonationContext)
		{
			this._originalHttpContext = DisposableHttpContextWrapper.SwitchContext(this.HttpContext);
			if (setImpersonationContext)
			{
				this.SetImpersonationContext();
			}
			this._originalSynchronizationContext = AsyncOperationManager.SynchronizationContext;
			AspNetSynchronizationContextBase syncContext = this.HttpContext.SyncContext;
			AsyncOperationManager.SynchronizationContext = syncContext;
			Guid requestTraceIdentifier = this.HttpContext.WorkerRequest.RequestTraceIdentifier;
			if (requestTraceIdentifier != Guid.Empty)
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", requestTraceIdentifier);
			}
			this.HttpContext.ResetSqlDependencyCookie();
			this._originalThreadPrincipal = Thread.CurrentPrincipal;
			HttpApplication.SetCurrentPrincipalWithAssert(this.HttpContext.User);
			this.SetRequestLevelCulture();
			if (this.HttpContext.CurrentThread == null)
			{
				this._setCurrentThreadOnHttpContext = true;
				this.HttpContext.CurrentThread = Thread.CurrentThread;
			}
			this._originalThreadContextCurrent = ThreadContext.Current;
			ThreadContext.Current = this;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003FD3 File Offset: 0x000021D3
		private ClientImpersonationContext CreateNewClientImpersonationContext()
		{
			return new ClientImpersonationContext(this.HttpContext);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003FE0 File Offset: 0x000021E0
		internal void DisassociateFromCurrentThread()
		{
			ThreadContext.Current = this._originalThreadContextCurrent;
			this.HasBeenDisassociatedFromThread = true;
			if (this._setCurrentThreadOnHttpContext)
			{
				this.HttpContext.CurrentThread = null;
			}
			HttpApplicationFactory.ApplicationState.EnsureUnLock();
			this.UndoImpersonationContext();
			this.RestoreRequestLevelCulture();
			AsyncOperationManager.SynchronizationContext = this._originalSynchronizationContext;
			HttpApplication.SetCurrentPrincipalWithAssert(this._originalThreadPrincipal);
			this.HttpContext.RemoveSqlDependencyCookie();
			DisposableHttpContextWrapper.SwitchContext(this._originalHttpContext);
			this._originalHttpContext = null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004060 File Offset: 0x00002260
		internal Action EnterExecutionContext()
		{
			ClientImpersonationContext executionContextClientImpersonationContext = null;
			if (this._newImpersonationContext != null)
			{
				executionContextClientImpersonationContext = this.CreateNewClientImpersonationContext();
			}
			DisposableHttpContextWrapper.SwitchContext(this.HttpContext);
			Guid requestTraceIdentifier = this.HttpContext.WorkerRequest.RequestTraceIdentifier;
			if (requestTraceIdentifier != Guid.Empty)
			{
				CallContext.LogicalSetData("E2ETrace.ActivityID", requestTraceIdentifier);
			}
			this.HttpContext.ResetSqlDependencyCookie();
			HttpApplication.SetCurrentPrincipalWithAssert(this.HttpContext.User);
			return delegate()
			{
				if (executionContextClientImpersonationContext != null)
				{
					executionContextClientImpersonationContext.Undo();
				}
			};
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003ABB File Offset: 0x00001CBB
		private static string GetTraceMessage(string tag)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000040F0 File Offset: 0x000022F0
		private void RestoreRequestLevelCulture()
		{
			CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
			CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (this._originalThreadCurrentCulture != null)
			{
				if (currentCulture != this._originalThreadCurrentCulture)
				{
					HttpRuntime.SetCurrentThreadCultureWithAssert(this._originalThreadCurrentCulture);
					if (this.HttpContext != null)
					{
						this.HttpContext.DynamicCulture = currentCulture;
					}
				}
				this._originalThreadCurrentCulture = null;
			}
			if (this._originalThreadCurrentUICulture != null)
			{
				if (currentUICulture != this._originalThreadCurrentUICulture)
				{
					Thread.CurrentThread.CurrentUICulture = this._originalThreadCurrentUICulture;
					if (this.HttpContext != null)
					{
						this.HttpContext.DynamicUICulture = currentUICulture;
					}
				}
				this._originalThreadCurrentUICulture = null;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004186 File Offset: 0x00002386
		internal void SetImpersonationContext()
		{
			if (this._newImpersonationContext == null)
			{
				this._newImpersonationContext = this.CreateNewClientImpersonationContext();
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000419C File Offset: 0x0000239C
		private void SetRequestLevelCulture()
		{
			CultureInfo cultureInfo = null;
			CultureInfo cultureInfo2 = null;
			GlobalizationSection globalization = RuntimeConfig.GetConfig(this.HttpContext).Globalization;
			if (!string.IsNullOrEmpty(globalization.Culture))
			{
				cultureInfo = this.HttpContext.CultureFromConfig(globalization.Culture, true);
			}
			if (!string.IsNullOrEmpty(globalization.UICulture))
			{
				cultureInfo2 = this.HttpContext.CultureFromConfig(globalization.UICulture, false);
			}
			if (this.HttpContext.DynamicCulture != null)
			{
				cultureInfo = this.HttpContext.DynamicCulture;
			}
			if (this.HttpContext.DynamicUICulture != null)
			{
				cultureInfo2 = this.HttpContext.DynamicUICulture;
			}
			Page page = this.HttpContext.CurrentHandler as Page;
			if (page != null)
			{
				if (page.DynamicCulture != null)
				{
					cultureInfo = page.DynamicCulture;
				}
				if (page.DynamicUICulture != null)
				{
					cultureInfo2 = page.DynamicUICulture;
				}
			}
			this._originalThreadCurrentCulture = Thread.CurrentThread.CurrentCulture;
			this._originalThreadCurrentUICulture = Thread.CurrentThread.CurrentUICulture;
			if (cultureInfo != null && cultureInfo != Thread.CurrentThread.CurrentCulture)
			{
				HttpRuntime.SetCurrentThreadCultureWithAssert(cultureInfo);
			}
			if (cultureInfo2 != null && cultureInfo2 != Thread.CurrentThread.CurrentUICulture)
			{
				Thread.CurrentThread.CurrentUICulture = cultureInfo2;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000042B3 File Offset: 0x000024B3
		internal void Synchronize()
		{
			this.HttpContext.DynamicCulture = Thread.CurrentThread.CurrentCulture;
			this.HttpContext.DynamicUICulture = Thread.CurrentThread.CurrentUICulture;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000042DF File Offset: 0x000024DF
		internal void UndoImpersonationContext()
		{
			if (this._newImpersonationContext != null)
			{
				this._newImpersonationContext.Undo();
				this._newImpersonationContext = null;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000042FB File Offset: 0x000024FB
		void ISyncContextLock.Leave()
		{
			this.DisassociateFromCurrentThread();
		}

		// Token: 0x040000F7 RID: 247
		[ThreadStatic]
		private static ThreadContext _currentThreadContext;

		// Token: 0x040000F8 RID: 248
		private ImpersonationContext _newImpersonationContext;

		// Token: 0x040000F9 RID: 249
		private HttpContext _originalHttpContext;

		// Token: 0x040000FA RID: 250
		private SynchronizationContext _originalSynchronizationContext;

		// Token: 0x040000FB RID: 251
		private ThreadContext _originalThreadContextCurrent;

		// Token: 0x040000FC RID: 252
		private CultureInfo _originalThreadCurrentCulture;

		// Token: 0x040000FD RID: 253
		private CultureInfo _originalThreadCurrentUICulture;

		// Token: 0x040000FE RID: 254
		private IPrincipal _originalThreadPrincipal;

		// Token: 0x040000FF RID: 255
		private bool _setCurrentThreadOnHttpContext;
	}
}
