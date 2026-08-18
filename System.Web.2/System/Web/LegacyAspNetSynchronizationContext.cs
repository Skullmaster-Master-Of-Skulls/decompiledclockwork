using System;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x0200003A RID: 58
	internal sealed class LegacyAspNetSynchronizationContext : AspNetSynchronizationContextBase
	{
		// Token: 0x060004E4 RID: 1252 RVA: 0x000061A5 File Offset: 0x000043A5
		internal LegacyAspNetSynchronizationContext(HttpApplication app)
		{
			this._application = app;
			this._appVerifierCallback = AppVerifier.GetSyncContextCheckDelegate(app);
			this._lastCompletionCallbackLock = new object();
		}

		// Token: 0x060004E5 RID: 1253 RVA: 0x000061CB File Offset: 0x000043CB
		private void CheckForRequestStateIfRequired()
		{
			if (this._appVerifierCallback != null)
			{
				this._appVerifierCallback(false);
			}
		}

		// Token: 0x060004E6 RID: 1254 RVA: 0x000061E4 File Offset: 0x000043E4
		private void CallCallback(SendOrPostCallback callback, object state)
		{
			this.CheckForRequestStateIfRequired();
			if (this._syncCaller)
			{
				this.CallCallbackPossiblyUnderLock(callback, state);
				return;
			}
			HttpApplication application = this._application;
			lock (application)
			{
				this.CallCallbackPossiblyUnderLock(callback, state);
			}
		}

		// Token: 0x060004E7 RID: 1255 RVA: 0x00006240 File Offset: 0x00004440
		private void CallCallbackPossiblyUnderLock(SendOrPostCallback callback, object state)
		{
			ThreadContext threadContext = null;
			try
			{
				threadContext = this._application.OnThreadEnter();
				try
				{
					callback(state);
				}
				catch (Exception source)
				{
					this._error = ExceptionDispatchInfo.Capture(source);
				}
			}
			finally
			{
				if (threadContext != null)
				{
					threadContext.DisassociateFromCurrentThread();
				}
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000629C File Offset: 0x0000449C
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x000062A4 File Offset: 0x000044A4
		internal override bool AllowAsyncDuringSyncStages { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000062AD File Offset: 0x000044AD
		internal override int PendingOperationsCount
		{
			get
			{
				return this._pendingCount;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000062B5 File Offset: 0x000044B5
		internal override ExceptionDispatchInfo ExceptionDispatchInfo
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x000062BD File Offset: 0x000044BD
		internal override void ClearError()
		{
			this._error = null;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000062C8 File Offset: 0x000044C8
		internal override bool PendingCompletion(WaitCallback callback)
		{
			bool result = false;
			if (this._pendingCount != 0)
			{
				object lastCompletionCallbackLock = this._lastCompletionCallbackLock;
				lock (lastCompletionCallbackLock)
				{
					if (this._pendingCount != 0)
					{
						result = true;
						this._lastCompletionCallback = callback;
					}
				}
			}
			return result;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00006320 File Offset: 0x00004520
		public override void Send(SendOrPostCallback callback, object state)
		{
			this.CallCallback(callback, state);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00006320 File Offset: 0x00004520
		public override void Post(SendOrPostCallback callback, object state)
		{
			this.CallCallback(callback, state);
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000632C File Offset: 0x0000452C
		public override SynchronizationContext CreateCopy()
		{
			return new LegacyAspNetSynchronizationContext(this._application)
			{
				_disabled = this._disabled,
				_syncCaller = this._syncCaller,
				AllowAsyncDuringSyncStages = this.AllowAsyncDuringSyncStages
			};
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000636A File Offset: 0x0000456A
		public override void OperationStarted()
		{
			if (this._invalidOperationEncountered || (this._disabled && this._pendingCount == 0))
			{
				this._invalidOperationEncountered = true;
				throw new InvalidOperationException(SR.GetString("Async_operation_disabled"));
			}
			Interlocked.Increment(ref this._pendingCount);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x000063A8 File Offset: 0x000045A8
		public override void OperationCompleted()
		{
			if (this._invalidOperationEncountered || (this._disabled && this._pendingCount == 0))
			{
				return;
			}
			if (Interlocked.Decrement(ref this._pendingCount) == 0)
			{
				WaitCallback waitCallback = null;
				object lastCompletionCallbackLock = this._lastCompletionCallbackLock;
				lock (lastCompletionCallbackLock)
				{
					waitCallback = this._lastCompletionCallback;
					this._lastCompletionCallback = null;
				}
				if (waitCallback != null)
				{
					ThreadPool.QueueUserWorkItem(waitCallback);
				}
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00006424 File Offset: 0x00004624
		internal override bool Enabled
		{
			get
			{
				return !this._disabled;
			}
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000642F File Offset: 0x0000462F
		internal override void Enable()
		{
			this._disabled = false;
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00006438 File Offset: 0x00004638
		internal override void Disable()
		{
			this._disabled = true;
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00006441 File Offset: 0x00004641
		internal override void SetSyncCaller()
		{
			this._syncCaller = true;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000644A File Offset: 0x0000464A
		internal override void ResetSyncCaller()
		{
			this._syncCaller = false;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x00006453 File Offset: 0x00004653
		internal override void AssociateWithCurrentThread()
		{
			Monitor.Enter(this._application);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00006460 File Offset: 0x00004660
		internal override void DisassociateFromCurrentThread()
		{
			Monitor.Exit(this._application);
		}

		// Token: 0x04000113 RID: 275
		private HttpApplication _application;

		// Token: 0x04000114 RID: 276
		private Action<bool> _appVerifierCallback;

		// Token: 0x04000115 RID: 277
		private bool _disabled;

		// Token: 0x04000116 RID: 278
		private bool _syncCaller;

		// Token: 0x04000117 RID: 279
		private bool _invalidOperationEncountered;

		// Token: 0x04000118 RID: 280
		private int _pendingCount;

		// Token: 0x04000119 RID: 281
		private ExceptionDispatchInfo _error;

		// Token: 0x0400011A RID: 282
		private WaitCallback _lastCompletionCallback;

		// Token: 0x0400011B RID: 283
		private object _lastCompletionCallbackLock;
	}
}
