using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000754 RID: 1876
	internal class LifetimeManager
	{
		// Token: 0x060047B5 RID: 18357 RVA: 0x00109C57 File Offset: 0x00107E57
		public LifetimeManager(object mutex)
		{
			this.mutex = mutex;
			this.state = LifetimeState.Opened;
		}

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060047B6 RID: 18358 RVA: 0x00109C6D File Offset: 0x00107E6D
		public int BusyCount
		{
			get
			{
				return this.busyCount;
			}
		}

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060047B7 RID: 18359 RVA: 0x00109C75 File Offset: 0x00107E75
		protected LifetimeState State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060047B8 RID: 18360 RVA: 0x00109C7D File Offset: 0x00107E7D
		protected object ThisLock
		{
			get
			{
				return this.mutex;
			}
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x00109C88 File Offset: 0x00107E88
		public void Abort()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.State == LifetimeState.Closed || this.aborted)
				{
					return;
				}
				this.aborted = true;
				this.state = LifetimeState.Closing;
			}
			this.OnAbort();
			this.state = LifetimeState.Closed;
		}

		// Token: 0x060047BA RID: 18362 RVA: 0x00109CF0 File Offset: 0x00107EF0
		private void ThrowIfNotOpened()
		{
			if (!this.aborted && this.state != LifetimeState.Opened)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
			}
		}

		// Token: 0x060047BB RID: 18363 RVA: 0x00109D20 File Offset: 0x00107F20
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ThrowIfNotOpened();
				this.state = LifetimeState.Closing;
			}
			return this.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x00109D70 File Offset: 0x00107F70
		public void Close(TimeSpan timeout)
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.ThrowIfNotOpened();
				this.state = LifetimeState.Closing;
			}
			this.OnClose(timeout);
			this.state = LifetimeState.Closed;
		}

		// Token: 0x060047BD RID: 18365 RVA: 0x00109DC8 File Offset: 0x00107FC8
		private CommunicationWaitResult CloseCore(TimeSpan timeout, bool aborting)
		{
			ICommunicationWaiter communicationWaiter = null;
			CommunicationWaitResult result = CommunicationWaitResult.Succeeded;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.busyCount > 0)
				{
					if (this.busyWaiter != null)
					{
						if (!aborting && this.aborted)
						{
							return CommunicationWaitResult.Aborted;
						}
						communicationWaiter = this.busyWaiter;
					}
					else
					{
						communicationWaiter = new SyncCommunicationWaiter(this.ThisLock);
						this.busyWaiter = communicationWaiter;
					}
					Interlocked.Increment(ref this.busyWaiterCount);
				}
			}
			if (communicationWaiter != null)
			{
				result = communicationWaiter.Wait(timeout, aborting);
				if (Interlocked.Decrement(ref this.busyWaiterCount) == 0)
				{
					communicationWaiter.Dispose();
					this.busyWaiter = null;
				}
			}
			return result;
		}

		// Token: 0x060047BE RID: 18366 RVA: 0x00109E7C File Offset: 0x0010807C
		protected void DecrementBusyCount()
		{
			ICommunicationWaiter communicationWaiter = null;
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.busyCount <= 0)
				{
					throw Fx.AssertAndThrow("LifetimeManager.DecrementBusyCount: (this.busyCount > 0)");
				}
				int num = this.busyCount - 1;
				this.busyCount = num;
				if (num == 0)
				{
					if (this.busyWaiter != null)
					{
						communicationWaiter = this.busyWaiter;
						Interlocked.Increment(ref this.busyWaiterCount);
					}
					flag = true;
				}
			}
			if (communicationWaiter != null)
			{
				communicationWaiter.Signal();
				if (Interlocked.Decrement(ref this.busyWaiterCount) == 0)
				{
					communicationWaiter.Dispose();
					this.busyWaiter = null;
				}
			}
			if (flag && this.State == LifetimeState.Opened)
			{
				this.OnEmpty();
			}
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x00109F38 File Offset: 0x00108138
		public void EndClose(IAsyncResult result)
		{
			this.OnEndClose(result);
			this.state = LifetimeState.Closed;
		}

		// Token: 0x060047C0 RID: 18368 RVA: 0x00109F48 File Offset: 0x00108148
		protected virtual void IncrementBusyCount()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				this.busyCount++;
			}
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x00109F90 File Offset: 0x00108190
		protected virtual void IncrementBusyCountWithoutLock()
		{
			this.busyCount++;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x00109FA0 File Offset: 0x001081A0
		protected virtual void OnAbort()
		{
			this.CloseCore(TimeSpan.FromSeconds(1.0), true);
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x00109FB8 File Offset: 0x001081B8
		protected virtual IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			CloseCommunicationAsyncResult closeCommunicationAsyncResult = null;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.busyCount > 0)
				{
					if (this.busyWaiter != null)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
					}
					closeCommunicationAsyncResult = new CloseCommunicationAsyncResult(timeout, callback, state, this.ThisLock);
					this.busyWaiter = closeCommunicationAsyncResult;
					Interlocked.Increment(ref this.busyWaiterCount);
				}
			}
			if (closeCommunicationAsyncResult != null)
			{
				return closeCommunicationAsyncResult;
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0010A050 File Offset: 0x00108250
		protected virtual void OnClose(TimeSpan timeout)
		{
			CommunicationWaitResult communicationWaitResult = this.CloseCore(timeout, false);
			if (communicationWaitResult == CommunicationWaitResult.Expired)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("SFxCloseTimedOut1", new object[]
				{
					timeout
				})));
			}
			if (communicationWaitResult != CommunicationWaitResult.Aborted)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0010A0B2 File Offset: 0x001082B2
		protected virtual void OnEmpty()
		{
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0010A0B4 File Offset: 0x001082B4
		protected virtual void OnEndClose(IAsyncResult result)
		{
			if (result is CloseCommunicationAsyncResult)
			{
				CloseCommunicationAsyncResult.End(result);
				if (Interlocked.Decrement(ref this.busyWaiterCount) == 0)
				{
					this.busyWaiter.Dispose();
					this.busyWaiter = null;
					return;
				}
			}
			else
			{
				CompletedAsyncResult.End(result);
			}
		}

		// Token: 0x04002DB9 RID: 11705
		private bool aborted;

		// Token: 0x04002DBA RID: 11706
		private int busyCount;

		// Token: 0x04002DBB RID: 11707
		private ICommunicationWaiter busyWaiter;

		// Token: 0x04002DBC RID: 11708
		private int busyWaiterCount;

		// Token: 0x04002DBD RID: 11709
		private object mutex;

		// Token: 0x04002DBE RID: 11710
		private LifetimeState state;
	}
}
