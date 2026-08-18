using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x02000038 RID: 56
	internal sealed class AspNetSynchronizationContext : AspNetSynchronizationContextBase
	{
		// Token: 0x060004CC RID: 1228 RVA: 0x00005EE9 File Offset: 0x000040E9
		internal AspNetSynchronizationContext(ISyncContext syncContext) : this(new AspNetSynchronizationContext.State(new SynchronizationHelper(syncContext)))
		{
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00005EFC File Offset: 0x000040FC
		private AspNetSynchronizationContext(AspNetSynchronizationContext.State state)
		{
			this._state = state;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00005F0B File Offset: 0x0000410B
		// (set) Token: 0x060004CF RID: 1231 RVA: 0x00005F18 File Offset: 0x00004118
		internal override bool AllowAsyncDuringSyncStages
		{
			get
			{
				return this._state.AllowAsyncDuringSyncStages;
			}
			set
			{
				this._state.AllowAsyncDuringSyncStages = value;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00005F26 File Offset: 0x00004126
		internal override bool Enabled
		{
			get
			{
				return this._state.Enabled;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x00005F33 File Offset: 0x00004133
		internal override ExceptionDispatchInfo ExceptionDispatchInfo
		{
			get
			{
				return this._state.Helper.Error;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x00005F45 File Offset: 0x00004145
		internal override int PendingOperationsCount
		{
			get
			{
				return this._state.Helper.PendingCount;
			}
		}

		// Token: 0x060004D3 RID: 1235 RVA: 0x00005F57 File Offset: 0x00004157
		internal override void AllowVoidAsyncOperations()
		{
			this._state.AllowVoidAsyncOperations = true;
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x00005F68 File Offset: 0x00004168
		internal override void AssociateWithCurrentThread()
		{
			IDisposable item = this._state.Helper.EnterSynchronousControl();
			this._state.SyncControlDisassociationActions.Push(item);
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x00005F97 File Offset: 0x00004197
		internal override void ClearError()
		{
			this._state.Helper.Error = null;
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x00005FAA File Offset: 0x000041AA
		public override SynchronizationContext CreateCopy()
		{
			return new AspNetSynchronizationContext(this._state);
		}

		// Token: 0x060004D7 RID: 1239 RVA: 0x00005FB7 File Offset: 0x000041B7
		internal override void Disable()
		{
			this._state.Enabled = false;
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x00005FC8 File Offset: 0x000041C8
		internal override void DisassociateFromCurrentThread()
		{
			IDisposable disposable = this._state.SyncControlDisassociationActions.Pop();
			disposable.Dispose();
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x00005FEC File Offset: 0x000041EC
		internal override void Enable()
		{
			this._state.Enabled = true;
		}

		// Token: 0x060004DA RID: 1242 RVA: 0x00005FFA File Offset: 0x000041FA
		public override void OperationCompleted()
		{
			Interlocked.Decrement(ref this._state.VoidAsyncOutstandingOperationCount);
			this._state.Helper.ChangeOperationCount(-1);
		}

		// Token: 0x060004DB RID: 1243 RVA: 0x00006020 File Offset: 0x00004220
		public override void OperationStarted()
		{
			if (!this.AllowAsyncDuringSyncStages && !this._state.AllowVoidAsyncOperations)
			{
				InvalidOperationException ex = new InvalidOperationException(SR.GetString("Async_operation_cannot_be_started"));
				throw ex;
			}
			this._state.Helper.ChangeOperationCount(1);
			Interlocked.Increment(ref this._state.VoidAsyncOutstandingOperationCount);
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0000607C File Offset: 0x0000427C
		internal override bool PendingCompletion(WaitCallback callback)
		{
			return this._state.Helper.TrySetCompletionContinuation(delegate
			{
				callback(null);
			});
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x000060B4 File Offset: 0x000042B4
		public override void Post(SendOrPostCallback callback, object state)
		{
			this._state.Helper.QueueAsynchronous(delegate
			{
				callback(state);
			});
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x000060F1 File Offset: 0x000042F1
		internal void PostAsync(Func<object, Task> callback, object state)
		{
			this._state.Helper.QueueAsynchronousAsync(callback, state);
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00006108 File Offset: 0x00004308
		internal override void ProhibitVoidAsyncOperations()
		{
			this._state.AllowVoidAsyncOperations = false;
			if (!this.AllowAsyncDuringSyncStages && Volatile.Read(ref this._state.VoidAsyncOutstandingOperationCount) > 0)
			{
				InvalidOperationException source = new InvalidOperationException(SR.GetString("Async_operation_cannot_be_pending"));
				this._state.Helper.Error = ExceptionDispatchInfo.Capture(source);
			}
		}

		// Token: 0x060004E0 RID: 1248 RVA: 0x00006164 File Offset: 0x00004364
		internal override void ResetSyncCaller()
		{
		}

		// Token: 0x060004E1 RID: 1249 RVA: 0x00006164 File Offset: 0x00004364
		internal override void SetSyncCaller()
		{
		}

		// Token: 0x060004E2 RID: 1250 RVA: 0x00006168 File Offset: 0x00004368
		public override void Send(SendOrPostCallback callback, object state)
		{
			this._state.Helper.QueueSynchronous(delegate
			{
				callback(state);
			});
		}

		// Token: 0x04000112 RID: 274
		private readonly AspNetSynchronizationContext.State _state;

		// Token: 0x020008B6 RID: 2230
		private sealed class State
		{
			// Token: 0x060067B0 RID: 26544 RVA: 0x00170528 File Offset: 0x0016E728
			internal State(SynchronizationHelper helper)
			{
				this.Helper = helper;
			}

			// Token: 0x040035D8 RID: 13784
			internal bool AllowAsyncDuringSyncStages = AppSettings.AllowAsyncDuringSyncStages;

			// Token: 0x040035D9 RID: 13785
			internal volatile bool AllowVoidAsyncOperations;

			// Token: 0x040035DA RID: 13786
			internal bool Enabled = true;

			// Token: 0x040035DB RID: 13787
			internal readonly SynchronizationHelper Helper;

			// Token: 0x040035DC RID: 13788
			internal Stack<IDisposable> SyncControlDisassociationActions = new Stack<IDisposable>(1);

			// Token: 0x040035DD RID: 13789
			internal int VoidAsyncOutstandingOperationCount;
		}
	}
}
