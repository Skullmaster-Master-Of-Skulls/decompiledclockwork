using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200081F RID: 2079
	internal class SynchronizedMessageSource
	{
		// Token: 0x06004DB3 RID: 19891 RVA: 0x0011BD44 File Offset: 0x00119F44
		public SynchronizedMessageSource(IMessageSource source)
		{
			this.source = source;
			this.sourceLock = new ThreadNeutralSemaphore(1);
		}

		// Token: 0x06004DB4 RID: 19892 RVA: 0x0011BD5F File Offset: 0x00119F5F
		public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SynchronizedMessageSource.WaitForMessageAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004DB5 RID: 19893 RVA: 0x0011BD6A File Offset: 0x00119F6A
		public bool EndWaitForMessage(IAsyncResult result)
		{
			return SynchronizedMessageSource.SynchronizedAsyncResult<bool>.End(result);
		}

		// Token: 0x06004DB6 RID: 19894 RVA: 0x0011BD74 File Offset: 0x00119F74
		public bool WaitForMessage(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.sourceLock.TryEnter(timeoutHelper.RemainingTime()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("WaitForMessageTimedOut", new object[]
				{
					timeout
				}), ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout)));
			}
			bool result;
			try
			{
				result = this.source.WaitForMessage(timeoutHelper.RemainingTime());
			}
			finally
			{
				this.sourceLock.Exit();
			}
			return result;
		}

		// Token: 0x06004DB7 RID: 19895 RVA: 0x0011BE00 File Offset: 0x0011A000
		public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SynchronizedMessageSource.ReceiveAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004DB8 RID: 19896 RVA: 0x0011BE0B File Offset: 0x0011A00B
		public Message EndReceive(IAsyncResult result)
		{
			return SynchronizedMessageSource.SynchronizedAsyncResult<Message>.End(result);
		}

		// Token: 0x06004DB9 RID: 19897 RVA: 0x0011BE14 File Offset: 0x0011A014
		public Message Receive(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (!this.sourceLock.TryEnter(timeoutHelper.RemainingTime()))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("ReceiveTimedOut2", new object[]
				{
					timeout
				}), ThreadNeutralSemaphore.CreateEnterTimedOutException(timeout)));
			}
			Message result;
			try
			{
				result = this.source.Receive(timeoutHelper.RemainingTime());
			}
			finally
			{
				this.sourceLock.Exit();
			}
			return result;
		}

		// Token: 0x0400309B RID: 12443
		private IMessageSource source;

		// Token: 0x0400309C RID: 12444
		private ThreadNeutralSemaphore sourceLock;

		// Token: 0x02000D20 RID: 3360
		private abstract class SynchronizedAsyncResult<T> : AsyncResult
		{
			// Token: 0x06007BA2 RID: 31650 RVA: 0x001CD59C File Offset: 0x001CB79C
			public SynchronizedAsyncResult(SynchronizedMessageSource syncSource, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.syncSource = syncSource;
				this.timeoutHelper = new TimeoutHelper(timeout);
				if (!syncSource.sourceLock.EnterAsync(this.timeoutHelper.RemainingTime(), SynchronizedMessageSource.SynchronizedAsyncResult<T>.onEnterComplete, this))
				{
					return;
				}
				this.exitLock = true;
				bool flag = false;
				bool flag2;
				try
				{
					flag2 = this.PerformOperation(this.timeoutHelper.RemainingTime());
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.ExitLock();
					}
				}
				if (flag2)
				{
					this.CompleteWithUnlock(true);
				}
			}

			// Token: 0x17001BD1 RID: 7121
			// (get) Token: 0x06007BA3 RID: 31651 RVA: 0x001CD628 File Offset: 0x001CB828
			protected IMessageSource Source
			{
				get
				{
					return this.syncSource.source;
				}
			}

			// Token: 0x06007BA4 RID: 31652 RVA: 0x001CD635 File Offset: 0x001CB835
			protected void SetReturnValue(T returnValue)
			{
				this.returnValue = returnValue;
			}

			// Token: 0x06007BA5 RID: 31653
			protected abstract bool PerformOperation(TimeSpan timeout);

			// Token: 0x06007BA6 RID: 31654 RVA: 0x001CD63E File Offset: 0x001CB83E
			private void ExitLock()
			{
				if (this.exitLock)
				{
					this.syncSource.sourceLock.Exit();
					this.exitLock = false;
				}
			}

			// Token: 0x06007BA7 RID: 31655 RVA: 0x001CD660 File Offset: 0x001CB860
			protected void CompleteWithUnlock(bool synchronous)
			{
				this.CompleteWithUnlock(synchronous, null);
			}

			// Token: 0x06007BA8 RID: 31656 RVA: 0x001CD66A File Offset: 0x001CB86A
			protected void CompleteWithUnlock(bool synchronous, Exception exception)
			{
				this.ExitLock();
				base.Complete(synchronous, exception);
			}

			// Token: 0x06007BA9 RID: 31657 RVA: 0x001CD67C File Offset: 0x001CB87C
			public static T End(IAsyncResult result)
			{
				SynchronizedMessageSource.SynchronizedAsyncResult<T> synchronizedAsyncResult = AsyncResult.End<SynchronizedMessageSource.SynchronizedAsyncResult<T>>(result);
				return synchronizedAsyncResult.returnValue;
			}

			// Token: 0x06007BAA RID: 31658 RVA: 0x001CD698 File Offset: 0x001CB898
			private static void OnEnterComplete(object state, Exception asyncException)
			{
				SynchronizedMessageSource.SynchronizedAsyncResult<T> synchronizedAsyncResult = (SynchronizedMessageSource.SynchronizedAsyncResult<T>)state;
				Exception ex = asyncException;
				bool flag;
				if (ex != null)
				{
					flag = true;
				}
				else
				{
					try
					{
						synchronizedAsyncResult.exitLock = true;
						flag = synchronizedAsyncResult.PerformOperation(synchronizedAsyncResult.timeoutHelper.RemainingTime());
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						flag = true;
						ex = ex2;
					}
				}
				if (flag)
				{
					synchronizedAsyncResult.CompleteWithUnlock(false, ex);
				}
			}

			// Token: 0x040046F5 RID: 18165
			private T returnValue;

			// Token: 0x040046F6 RID: 18166
			private bool exitLock;

			// Token: 0x040046F7 RID: 18167
			private SynchronizedMessageSource syncSource;

			// Token: 0x040046F8 RID: 18168
			private static FastAsyncCallback onEnterComplete = new FastAsyncCallback(SynchronizedMessageSource.SynchronizedAsyncResult<T>.OnEnterComplete);

			// Token: 0x040046F9 RID: 18169
			private TimeoutHelper timeoutHelper;
		}

		// Token: 0x02000D21 RID: 3361
		private class ReceiveAsyncResult : SynchronizedMessageSource.SynchronizedAsyncResult<Message>
		{
			// Token: 0x06007BAC RID: 31660 RVA: 0x001CD713 File Offset: 0x001CB913
			public ReceiveAsyncResult(SynchronizedMessageSource syncSource, TimeSpan timeout, AsyncCallback callback, object state) : base(syncSource, timeout, callback, state)
			{
			}

			// Token: 0x06007BAD RID: 31661 RVA: 0x001CD720 File Offset: 0x001CB920
			protected override bool PerformOperation(TimeSpan timeout)
			{
				if (base.Source.BeginReceive(timeout, SynchronizedMessageSource.ReceiveAsyncResult.onReceiveComplete, this) == AsyncReceiveResult.Completed)
				{
					base.SetReturnValue(base.Source.EndReceive());
					return true;
				}
				return false;
			}

			// Token: 0x06007BAE RID: 31662 RVA: 0x001CD74C File Offset: 0x001CB94C
			private static void OnReceiveComplete(object state)
			{
				SynchronizedMessageSource.ReceiveAsyncResult receiveAsyncResult = (SynchronizedMessageSource.ReceiveAsyncResult)state;
				Exception exception = null;
				try
				{
					receiveAsyncResult.SetReturnValue(receiveAsyncResult.Source.EndReceive());
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				receiveAsyncResult.CompleteWithUnlock(false, exception);
			}

			// Token: 0x040046FA RID: 18170
			private static WaitCallback onReceiveComplete = new WaitCallback(SynchronizedMessageSource.ReceiveAsyncResult.OnReceiveComplete);
		}

		// Token: 0x02000D22 RID: 3362
		private class WaitForMessageAsyncResult : SynchronizedMessageSource.SynchronizedAsyncResult<bool>
		{
			// Token: 0x06007BB0 RID: 31664 RVA: 0x001CD7AF File Offset: 0x001CB9AF
			public WaitForMessageAsyncResult(SynchronizedMessageSource syncSource, TimeSpan timeout, AsyncCallback callback, object state) : base(syncSource, timeout, callback, state)
			{
			}

			// Token: 0x06007BB1 RID: 31665 RVA: 0x001CD7BC File Offset: 0x001CB9BC
			protected override bool PerformOperation(TimeSpan timeout)
			{
				if (base.Source.BeginWaitForMessage(timeout, SynchronizedMessageSource.WaitForMessageAsyncResult.onWaitForMessageComplete, this) == AsyncReceiveResult.Completed)
				{
					base.SetReturnValue(base.Source.EndWaitForMessage());
					return true;
				}
				return false;
			}

			// Token: 0x06007BB2 RID: 31666 RVA: 0x001CD7E8 File Offset: 0x001CB9E8
			private static void OnWaitForMessageComplete(object state)
			{
				SynchronizedMessageSource.WaitForMessageAsyncResult waitForMessageAsyncResult = (SynchronizedMessageSource.WaitForMessageAsyncResult)state;
				Exception exception = null;
				try
				{
					waitForMessageAsyncResult.SetReturnValue(waitForMessageAsyncResult.Source.EndWaitForMessage());
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				waitForMessageAsyncResult.CompleteWithUnlock(false, exception);
			}

			// Token: 0x040046FB RID: 18171
			private static WaitCallback onWaitForMessageComplete = new WaitCallback(SynchronizedMessageSource.WaitForMessageAsyncResult.OnWaitForMessageComplete);
		}
	}
}
