using System;
using System.Diagnostics;
using System.Threading;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000F6 RID: 246
	[DebuggerNonUserCode]
	internal static class AsyncResultWrapper
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x00011E88 File Offset: 0x00010088
		public static IAsyncResult Begin<TResult>(AsyncCallback callback, object state, BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate, object tag = null, int timeout = -1)
		{
			AsyncResultWrapper.WrappedAsyncResult<TResult> wrappedAsyncResult = new AsyncResultWrapper.WrappedAsyncResult<TResult>(beginDelegate, endDelegate, tag, null);
			wrappedAsyncResult.Begin(callback, state, timeout);
			return wrappedAsyncResult;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00011EAC File Offset: 0x000100AC
		public static IAsyncResult Begin<TResult, TState>(AsyncCallback callback, object callbackState, BeginInvokeDelegate<TState> beginDelegate, EndInvokeDelegate<TState, TResult> endDelegate, TState invokeState, object tag = null, int timeout = -1, SynchronizationContext callbackSyncContext = null)
		{
			AsyncResultWrapper.WrappedAsyncResult<TResult, TState> wrappedAsyncResult = new AsyncResultWrapper.WrappedAsyncResult<TResult, TState>(beginDelegate, endDelegate, invokeState, tag, callbackSyncContext);
			wrappedAsyncResult.Begin(callback, callbackState, timeout);
			return wrappedAsyncResult;
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00011ED4 File Offset: 0x000100D4
		public static IAsyncResult Begin<TState>(AsyncCallback callback, object callbackState, BeginInvokeDelegate<TState> beginDelegate, EndInvokeVoidDelegate<TState> endDelegate, TState invokeState, object tag = null, int timeout = -1, SynchronizationContext callbackSyncContext = null)
		{
			AsyncResultWrapper.WrappedAsyncVoid<TState> wrappedAsyncVoid = new AsyncResultWrapper.WrappedAsyncVoid<TState>(beginDelegate, endDelegate, invokeState, tag, callbackSyncContext);
			wrappedAsyncVoid.Begin(callback, callbackState, timeout);
			return wrappedAsyncVoid;
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x00011EFC File Offset: 0x000100FC
		public static IAsyncResult BeginSynchronous<TResult, TState>(AsyncCallback callback, object callbackState, EndInvokeDelegate<TState, TResult> func, TState funcState, object tag)
		{
			BeginInvokeDelegate<TState> completedBeginInvoke = AsyncResultWrapper.CachedDelegates<TState>.CompletedBeginInvoke;
			AsyncResultWrapper.WrappedAsyncResult<TResult, TState> wrappedAsyncResult = new AsyncResultWrapper.WrappedAsyncResult<TResult, TState>(completedBeginInvoke, func, funcState, tag, null);
			wrappedAsyncResult.Begin(callback, callbackState, -1);
			return wrappedAsyncResult;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00011F25 File Offset: 0x00010125
		public static IAsyncResult BeginSynchronous(AsyncCallback callback, object state, Action action, object tag)
		{
			return AsyncResultWrapper.BeginSynchronous<AsyncVoid, Action>(callback, state, AsyncResultWrapper._voidEndInvoke, action, tag);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00011F35 File Offset: 0x00010135
		public static TResult End<TResult>(IAsyncResult asyncResult)
		{
			return AsyncResultWrapper.End<TResult>(asyncResult, null);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00011F3E File Offset: 0x0001013E
		public static TResult End<TResult>(IAsyncResult asyncResult, object tag)
		{
			return AsyncResultWrapper.WrappedAsyncResultBase<TResult>.Cast(asyncResult, tag).End();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00011F4C File Offset: 0x0001014C
		public static void End(IAsyncResult asyncResult)
		{
			AsyncResultWrapper.End(asyncResult, null);
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00011F55 File Offset: 0x00010155
		public static void End(IAsyncResult asyncResult, object tag)
		{
			AsyncResultWrapper.End<AsyncVoid>(asyncResult, tag);
		}

		// Token: 0x040001C6 RID: 454
		private static readonly EndInvokeDelegate<Action, AsyncVoid> _voidEndInvoke = delegate(IAsyncResult asyncResult, Action action)
		{
			action();
			return default(AsyncVoid);
		};

		// Token: 0x020000F7 RID: 247
		private static class CachedDelegates<TState>
		{
			// Token: 0x040001C8 RID: 456
			internal static BeginInvokeDelegate<TState> CompletedBeginInvoke = delegate(AsyncCallback asyncCallback, object asyncState, TState invokeState)
			{
				SimpleAsyncResult simpleAsyncResult = new SimpleAsyncResult(asyncState);
				simpleAsyncResult.MarkCompleted(true, asyncCallback);
				return simpleAsyncResult;
			};
		}

		// Token: 0x020000F8 RID: 248
		[DebuggerNonUserCode]
		private abstract class WrappedAsyncResultBase<TResult> : IAsyncResult
		{
			// Token: 0x0600065E RID: 1630 RVA: 0x00011FE1 File Offset: 0x000101E1
			protected WrappedAsyncResultBase(object tag, SynchronizationContext callbackSyncContext)
			{
				this._tag = tag;
				this._callbackSyncContext = callbackSyncContext;
			}

			// Token: 0x170001D2 RID: 466
			// (get) Token: 0x0600065F RID: 1631 RVA: 0x00012018 File Offset: 0x00010218
			public object AsyncState
			{
				get
				{
					return this._innerAsyncResult.AsyncState;
				}
			}

			// Token: 0x170001D3 RID: 467
			// (get) Token: 0x06000660 RID: 1632 RVA: 0x00012025 File Offset: 0x00010225
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170001D4 RID: 468
			// (get) Token: 0x06000661 RID: 1633 RVA: 0x00012028 File Offset: 0x00010228
			// (set) Token: 0x06000662 RID: 1634 RVA: 0x00012030 File Offset: 0x00010230
			public bool CompletedSynchronously { get; private set; }

			// Token: 0x170001D5 RID: 469
			// (get) Token: 0x06000663 RID: 1635 RVA: 0x00012039 File Offset: 0x00010239
			public bool IsCompleted
			{
				get
				{
					return this._timedOut || this._innerAsyncResult.IsCompleted;
				}
			}

			// Token: 0x06000664 RID: 1636 RVA: 0x00012054 File Offset: 0x00010254
			public void Begin(AsyncCallback callback, object state, int timeout)
			{
				this._originalCallback = callback;
				lock (this._beginDelegateLockObj)
				{
					this._innerAsyncResult = this.CallBeginDelegate(new AsyncCallback(this.HandleAsynchronousCompletion), state);
					int num = Interlocked.Exchange(ref this._asyncState, 1);
					this.CompletedSynchronously = (num == 2 || this._innerAsyncResult.CompletedSynchronously);
					if (!this.CompletedSynchronously && timeout > -1)
					{
						this.CreateTimer(timeout);
					}
				}
				if (this.CompletedSynchronously && callback != null)
				{
					callback(this);
				}
			}

			// Token: 0x06000665 RID: 1637
			protected abstract IAsyncResult CallBeginDelegate(AsyncCallback callback, object callbackState);

			// Token: 0x06000666 RID: 1638
			protected abstract TResult CallEndDelegate(IAsyncResult asyncResult);

			// Token: 0x06000667 RID: 1639 RVA: 0x000120FC File Offset: 0x000102FC
			public static AsyncResultWrapper.WrappedAsyncResultBase<TResult> Cast(IAsyncResult asyncResult, object tag)
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				AsyncResultWrapper.WrappedAsyncResultBase<TResult> wrappedAsyncResultBase = asyncResult as AsyncResultWrapper.WrappedAsyncResultBase<TResult>;
				if (wrappedAsyncResultBase != null && object.Equals(wrappedAsyncResultBase._tag, tag))
				{
					return wrappedAsyncResultBase;
				}
				throw Error.AsyncCommon_InvalidAsyncResult("asyncResult");
			}

			// Token: 0x06000668 RID: 1640 RVA: 0x00012149 File Offset: 0x00010349
			private void CallbackUsingSyncContext()
			{
				this._callbackSyncContext.Sync(delegate()
				{
					this._originalCallback(this);
				});
			}

			// Token: 0x06000669 RID: 1641 RVA: 0x00012162 File Offset: 0x00010362
			private void CreateTimer(int timeout)
			{
				this._timer = new Timer(new TimerCallback(this.HandleTimeout), null, timeout, -1);
			}

			// Token: 0x0600066A RID: 1642 RVA: 0x0001217E File Offset: 0x0001037E
			public TResult End()
			{
				if (!this._endExecutedGate.TryEnter())
				{
					throw Error.AsyncCommon_AsyncResultAlreadyConsumed();
				}
				if (this._timedOut)
				{
					throw new TimeoutException();
				}
				this.WaitForBeginToCompleteAndDestroyTimer();
				return this.CallEndDelegate(this._innerAsyncResult);
			}

			// Token: 0x0600066B RID: 1643 RVA: 0x000121B8 File Offset: 0x000103B8
			private void ExecuteAsynchronousCallback(bool timedOut)
			{
				this.WaitForBeginToCompleteAndDestroyTimer();
				if (this._handleCallbackGate.TryEnter())
				{
					this._timedOut = timedOut;
					if (this._originalCallback != null)
					{
						if (this._callbackSyncContext != null)
						{
							this.CallbackUsingSyncContext();
							return;
						}
						this._originalCallback(this);
					}
				}
			}

			// Token: 0x0600066C RID: 1644 RVA: 0x00012204 File Offset: 0x00010404
			private void HandleAsynchronousCompletion(IAsyncResult asyncResult)
			{
				int num = Interlocked.Exchange(ref this._asyncState, 2);
				if (num != 1)
				{
					return;
				}
				this.ExecuteAsynchronousCallback(false);
			}

			// Token: 0x0600066D RID: 1645 RVA: 0x0001222A File Offset: 0x0001042A
			private void HandleTimeout(object state)
			{
				this.ExecuteAsynchronousCallback(true);
			}

			// Token: 0x0600066E RID: 1646 RVA: 0x00012234 File Offset: 0x00010434
			private void WaitForBeginToCompleteAndDestroyTimer()
			{
				lock (this._beginDelegateLockObj)
				{
					if (this._timer != null)
					{
						this._timer.Dispose();
					}
					this._timer = null;
				}
			}

			// Token: 0x040001CA RID: 458
			private const int AsyncStateNone = 0;

			// Token: 0x040001CB RID: 459
			private const int AsyncStateBeginUnwound = 1;

			// Token: 0x040001CC RID: 460
			private const int AsyncStateCallbackFired = 2;

			// Token: 0x040001CD RID: 461
			private int _asyncState;

			// Token: 0x040001CE RID: 462
			private readonly object _beginDelegateLockObj = new object();

			// Token: 0x040001CF RID: 463
			private readonly SingleEntryGate _endExecutedGate = new SingleEntryGate();

			// Token: 0x040001D0 RID: 464
			private readonly SingleEntryGate _handleCallbackGate = new SingleEntryGate();

			// Token: 0x040001D1 RID: 465
			private readonly object _tag;

			// Token: 0x040001D2 RID: 466
			private IAsyncResult _innerAsyncResult;

			// Token: 0x040001D3 RID: 467
			private AsyncCallback _originalCallback;

			// Token: 0x040001D4 RID: 468
			private volatile bool _timedOut;

			// Token: 0x040001D5 RID: 469
			private Timer _timer;

			// Token: 0x040001D6 RID: 470
			private readonly SynchronizationContext _callbackSyncContext;
		}

		// Token: 0x020000F9 RID: 249
		private sealed class WrappedAsyncResult<TResult> : AsyncResultWrapper.WrappedAsyncResultBase<TResult>
		{
			// Token: 0x06000670 RID: 1648 RVA: 0x00012288 File Offset: 0x00010488
			public WrappedAsyncResult(BeginInvokeDelegate beginDelegate, EndInvokeDelegate<TResult> endDelegate, object tag, SynchronizationContext callbackSyncContext) : base(tag, callbackSyncContext)
			{
				this._beginDelegate = beginDelegate;
				this._endDelegate = endDelegate;
			}

			// Token: 0x06000671 RID: 1649 RVA: 0x000122A1 File Offset: 0x000104A1
			protected override IAsyncResult CallBeginDelegate(AsyncCallback callback, object callbackState)
			{
				return this._beginDelegate(callback, callbackState);
			}

			// Token: 0x06000672 RID: 1650 RVA: 0x000122B0 File Offset: 0x000104B0
			protected override TResult CallEndDelegate(IAsyncResult asyncResult)
			{
				return this._endDelegate(asyncResult);
			}

			// Token: 0x040001D8 RID: 472
			private readonly BeginInvokeDelegate _beginDelegate;

			// Token: 0x040001D9 RID: 473
			private readonly EndInvokeDelegate<TResult> _endDelegate;
		}

		// Token: 0x020000FA RID: 250
		private sealed class WrappedAsyncResult<TResult, TState> : AsyncResultWrapper.WrappedAsyncResultBase<TResult>
		{
			// Token: 0x06000673 RID: 1651 RVA: 0x000122BE File Offset: 0x000104BE
			public WrappedAsyncResult(BeginInvokeDelegate<TState> beginDelegate, EndInvokeDelegate<TState, TResult> endDelegate, TState state, object tag, SynchronizationContext callbackSyncContext) : base(tag, callbackSyncContext)
			{
				this._beginDelegate = beginDelegate;
				this._endDelegate = endDelegate;
				this._state = state;
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000122DF File Offset: 0x000104DF
			protected override TResult CallEndDelegate(IAsyncResult asyncResult)
			{
				return this._endDelegate(asyncResult, this._state);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x000122F3 File Offset: 0x000104F3
			protected override IAsyncResult CallBeginDelegate(AsyncCallback callback, object callbackState)
			{
				return this._beginDelegate(callback, callbackState, this._state);
			}

			// Token: 0x040001DA RID: 474
			private readonly BeginInvokeDelegate<TState> _beginDelegate;

			// Token: 0x040001DB RID: 475
			private readonly EndInvokeDelegate<TState, TResult> _endDelegate;

			// Token: 0x040001DC RID: 476
			private readonly TState _state;
		}

		// Token: 0x020000FB RID: 251
		private sealed class WrappedAsyncVoid<TState> : AsyncResultWrapper.WrappedAsyncResultBase<AsyncVoid>
		{
			// Token: 0x06000676 RID: 1654 RVA: 0x00012308 File Offset: 0x00010508
			public WrappedAsyncVoid(BeginInvokeDelegate<TState> beginDelegate, EndInvokeVoidDelegate<TState> endDelegate, TState state, object tag, SynchronizationContext callbackSyncContext) : base(tag, callbackSyncContext)
			{
				this._beginDelegate = beginDelegate;
				this._endDelegate = endDelegate;
				this._state = state;
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x0001232C File Offset: 0x0001052C
			protected override AsyncVoid CallEndDelegate(IAsyncResult asyncResult)
			{
				this._endDelegate(asyncResult, this._state);
				return default(AsyncVoid);
			}

			// Token: 0x06000678 RID: 1656 RVA: 0x00012354 File Offset: 0x00010554
			protected override IAsyncResult CallBeginDelegate(AsyncCallback callback, object callbackState)
			{
				return this._beginDelegate(callback, callbackState, this._state);
			}

			// Token: 0x040001DD RID: 477
			private readonly BeginInvokeDelegate<TState> _beginDelegate;

			// Token: 0x040001DE RID: 478
			private readonly EndInvokeVoidDelegate<TState> _endDelegate;

			// Token: 0x040001DF RID: 479
			private readonly TState _state;
		}
	}
}
