using System;
using System.Runtime;
using System.Runtime.Diagnostics;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200075A RID: 1882
	public abstract class ReceiveContext
	{
		// Token: 0x060047E5 RID: 18405 RVA: 0x0010A645 File Offset: 0x00108845
		protected ReceiveContext()
		{
			this.thisLock = new object();
			this.State = ReceiveContextState.Received;
			this.stateLock = new ThreadNeutralSemaphore(1);
		}

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060047E6 RID: 18406 RVA: 0x0010A66B File Offset: 0x0010886B
		// (set) Token: 0x060047E7 RID: 18407 RVA: 0x0010A673 File Offset: 0x00108873
		public ReceiveContextState State { get; protected set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060047E8 RID: 18408 RVA: 0x0010A67C File Offset: 0x0010887C
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x14000034 RID: 52
		// (add) Token: 0x060047E9 RID: 18409 RVA: 0x0010A684 File Offset: 0x00108884
		// (remove) Token: 0x060047EA RID: 18410 RVA: 0x0010A6BC File Offset: 0x001088BC
		public event EventHandler Faulted;

		// Token: 0x060047EB RID: 18411 RVA: 0x0010A6F4 File Offset: 0x001088F4
		public static bool TryGet(Message message, out ReceiveContext property)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			bool flag = ReceiveContext.TryGet(message.Properties, out property);
			if (flag && FxTrace.Trace.IsEnd2EndActivityTracingEnabled && property.eventTraceActivity == null)
			{
				property.eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(message);
			}
			return flag;
		}

		// Token: 0x060047EC RID: 18412 RVA: 0x0010A748 File Offset: 0x00108948
		public static bool TryGet(MessageProperties properties, out ReceiveContext property)
		{
			if (properties == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("properties");
			}
			property = null;
			object obj;
			if (properties.TryGetValue(ReceiveContext.Name, out obj))
			{
				property = (ReceiveContext)obj;
				return true;
			}
			return false;
		}

		// Token: 0x060047ED RID: 18413 RVA: 0x0010A785 File Offset: 0x00108985
		public virtual void Abandon(TimeSpan timeout)
		{
			this.Abandon(null, timeout);
		}

		// Token: 0x060047EE RID: 18414 RVA: 0x0010A790 File Offset: 0x00108990
		public virtual void Abandon(Exception exception, TimeSpan timeout)
		{
			this.EnsureValidTimeout(timeout);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.WaitForStateLock(timeoutHelper.RemainingTime());
			try
			{
				if (this.PreAbandon())
				{
					return;
				}
			}
			finally
			{
				this.ReleaseStateLock();
			}
			bool flag = false;
			try
			{
				if (exception == null)
				{
					this.OnAbandon(timeoutHelper.RemainingTime());
				}
				else
				{
					if (TD.ReceiveContextAbandonWithExceptionIsEnabled())
					{
						TD.ReceiveContextAbandonWithException(this.eventTraceActivity, base.GetType().ToString(), exception.GetType().ToString());
					}
					this.OnAbandon(exception, timeoutHelper.RemainingTime());
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfFaulted();
					this.ThrowIfNotAbandoning();
					this.State = ReceiveContextState.Abandoned;
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (TD.ReceiveContextAbandonFailedIsEnabled())
					{
						TD.ReceiveContextAbandonFailed(this.eventTraceActivity, base.GetType().ToString());
					}
					this.Fault();
				}
			}
		}

		// Token: 0x060047EF RID: 18415 RVA: 0x0010A89C File Offset: 0x00108A9C
		public virtual IAsyncResult BeginAbandon(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.BeginAbandon(null, timeout, callback, state);
		}

		// Token: 0x060047F0 RID: 18416 RVA: 0x0010A8A8 File Offset: 0x00108AA8
		public virtual IAsyncResult BeginAbandon(Exception exception, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.EnsureValidTimeout(timeout);
			return new ReceiveContext.AbandonAsyncResult(this, exception, timeout, callback, state);
		}

		// Token: 0x060047F1 RID: 18417 RVA: 0x0010A8BC File Offset: 0x00108ABC
		public virtual IAsyncResult BeginComplete(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.EnsureValidTimeout(timeout);
			return new ReceiveContext.CompleteAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x060047F2 RID: 18418 RVA: 0x0010A8D0 File Offset: 0x00108AD0
		public virtual void Complete(TimeSpan timeout)
		{
			this.EnsureValidTimeout(timeout);
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.WaitForStateLock(timeoutHelper.RemainingTime());
			bool flag = false;
			try
			{
				this.PreComplete();
				flag = true;
			}
			finally
			{
				if (!flag || Transaction.Current == null)
				{
					this.ReleaseStateLock();
				}
			}
			flag = false;
			try
			{
				this.OnComplete(timeoutHelper.RemainingTime());
				object obj = this.ThisLock;
				lock (obj)
				{
					this.ThrowIfFaulted();
					this.ThrowIfNotCompleting();
					this.State = ReceiveContextState.Completed;
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					if (TD.ReceiveContextCompleteFailedIsEnabled())
					{
						TD.ReceiveContextCompleteFailed(this.eventTraceActivity, base.GetType().ToString());
					}
					this.Fault();
				}
			}
		}

		// Token: 0x060047F3 RID: 18419 RVA: 0x0010A9B0 File Offset: 0x00108BB0
		public virtual void EndAbandon(IAsyncResult result)
		{
			ReceiveContext.AbandonAsyncResult.End(result);
		}

		// Token: 0x060047F4 RID: 18420 RVA: 0x0010A9B8 File Offset: 0x00108BB8
		public virtual void EndComplete(IAsyncResult result)
		{
			ReceiveContext.CompleteAsyncResult.End(result);
		}

		// Token: 0x060047F5 RID: 18421 RVA: 0x0010A9C0 File Offset: 0x00108BC0
		private void EnsureValidTimeout(TimeSpan timeout)
		{
			if (timeout < TimeSpan.Zero)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", SR.GetString("SFxTimeoutOutOfRange0")));
			}
			if (TimeoutHelper.IsTooLarge(timeout))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("timeout", timeout, SR.GetString("SFxTimeoutOutOfRangeTooBig")));
			}
		}

		// Token: 0x060047F6 RID: 18422 RVA: 0x0010AA28 File Offset: 0x00108C28
		protected internal virtual void Fault()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.State == ReceiveContextState.Completed || this.State == ReceiveContextState.Abandoned || this.State == ReceiveContextState.Faulted)
				{
					return;
				}
				this.State = ReceiveContextState.Faulted;
			}
			this.OnFaulted();
		}

		// Token: 0x060047F7 RID: 18423
		protected abstract void OnAbandon(TimeSpan timeout);

		// Token: 0x060047F8 RID: 18424 RVA: 0x0010AA8C File Offset: 0x00108C8C
		protected virtual void OnAbandon(Exception exception, TimeSpan timeout)
		{
			this.OnAbandon(timeout);
		}

		// Token: 0x060047F9 RID: 18425
		protected abstract IAsyncResult OnBeginAbandon(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060047FA RID: 18426 RVA: 0x0010AA95 File Offset: 0x00108C95
		protected virtual IAsyncResult OnBeginAbandon(Exception exception, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.OnBeginAbandon(timeout, callback, state);
		}

		// Token: 0x060047FB RID: 18427
		protected abstract IAsyncResult OnBeginComplete(TimeSpan timeout, AsyncCallback callback, object state);

		// Token: 0x060047FC RID: 18428
		protected abstract void OnComplete(TimeSpan timeout);

		// Token: 0x060047FD RID: 18429
		protected abstract void OnEndAbandon(IAsyncResult result);

		// Token: 0x060047FE RID: 18430
		protected abstract void OnEndComplete(IAsyncResult result);

		// Token: 0x060047FF RID: 18431 RVA: 0x0010AAA4 File Offset: 0x00108CA4
		protected virtual void OnFaulted()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.contextFaulted)
				{
					return;
				}
				this.contextFaulted = true;
			}
			if (TD.ReceiveContextFaultedIsEnabled())
			{
				TD.ReceiveContextFaulted(this.eventTraceActivity, this);
			}
			EventHandler faulted = this.Faulted;
			if (faulted != null)
			{
				try
				{
					faulted(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}
		}

		// Token: 0x06004800 RID: 18432 RVA: 0x0010AB40 File Offset: 0x00108D40
		private void OnTransactionStatusNotification(TransactionStatus status)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (status == TransactionStatus.Aborted && (this.State == ReceiveContextState.Completing || this.State == ReceiveContextState.Completed))
				{
					this.State = ReceiveContextState.Received;
				}
			}
			if (status != TransactionStatus.Active)
			{
				this.ReleaseStateLock();
			}
		}

		// Token: 0x06004801 RID: 18433 RVA: 0x0010ABA0 File Offset: 0x00108DA0
		private bool PreAbandon()
		{
			bool result = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.State == ReceiveContextState.Abandoning || this.State == ReceiveContextState.Abandoned)
				{
					result = true;
				}
				else
				{
					this.ThrowIfFaulted();
					this.ThrowIfNotReceived();
					this.State = ReceiveContextState.Abandoning;
				}
			}
			return result;
		}

		// Token: 0x06004802 RID: 18434 RVA: 0x0010AC08 File Offset: 0x00108E08
		private void PreComplete()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.ThrowIfFaulted();
				this.ThrowIfNotReceived();
				if (Transaction.Current != null)
				{
					Transaction.Current.EnlistVolatile(new ReceiveContext.EnlistmentNotifications(this), EnlistmentOptions.None);
				}
				this.State = ReceiveContextState.Completing;
			}
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0010AC74 File Offset: 0x00108E74
		private void ReleaseStateLock()
		{
			this.stateLock.Exit();
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0010AC82 File Offset: 0x00108E82
		private void ThrowIfFaulted()
		{
			if (this.State == ReceiveContextState.Faulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("ReceiveContextFaulted", new object[]
				{
					base.GetType().ToString()
				})));
			}
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0010ACBC File Offset: 0x00108EBC
		private void ThrowIfNotAbandoning()
		{
			if (this.State != ReceiveContextState.Abandoning)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ReceiveContextInInvalidState", new object[]
				{
					base.GetType().ToString(),
					this.State.ToString()
				})));
			}
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0010AD18 File Offset: 0x00108F18
		private void ThrowIfNotCompleting()
		{
			if (this.State != ReceiveContextState.Completing)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ReceiveContextInInvalidState", new object[]
				{
					base.GetType().ToString(),
					this.State.ToString()
				})));
			}
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0010AD74 File Offset: 0x00108F74
		private void ThrowIfNotReceived()
		{
			if (this.State != ReceiveContextState.Received)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ReceiveContextCannotBeUsed", new object[]
				{
					base.GetType().ToString(),
					this.State.ToString()
				})));
			}
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0010ADD0 File Offset: 0x00108FD0
		private void WaitForStateLock(TimeSpan timeout)
		{
			try
			{
				this.stateLock.Enter(timeout);
			}
			catch (TimeoutException exception)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(this.WrapStateException(exception));
			}
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0010AE10 File Offset: 0x00109010
		private bool WaitForStateLockAsync(TimeSpan timeout, FastAsyncCallback callback, object state)
		{
			return this.stateLock.EnterAsync(timeout, callback, state);
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0010AE20 File Offset: 0x00109020
		private Exception WrapStateException(Exception exception)
		{
			return new InvalidOperationException(SR.GetString("ReceiveContextInInvalidState", new object[]
			{
				base.GetType().ToString(),
				this.State.ToString()
			}), exception);
		}

		// Token: 0x04002DCD RID: 11725
		public static readonly string Name = "ReceiveContext";

		// Token: 0x04002DCE RID: 11726
		private ThreadNeutralSemaphore stateLock;

		// Token: 0x04002DCF RID: 11727
		private bool contextFaulted;

		// Token: 0x04002DD0 RID: 11728
		private object thisLock;

		// Token: 0x04002DD1 RID: 11729
		private EventTraceActivity eventTraceActivity;

		// Token: 0x02000CDA RID: 3290
		private sealed class AbandonAsyncResult : ReceiveContext.WaitAndContinueOperationAsyncResult
		{
			// Token: 0x060079F2 RID: 31218 RVA: 0x001C6CBD File Offset: 0x001C4EBD
			public AbandonAsyncResult(ReceiveContext receiveContext, Exception exception, TimeSpan timeout, AsyncCallback callback, object state) : base(receiveContext, timeout, callback, state)
			{
				this.exception = exception;
				base.Begin();
			}

			// Token: 0x060079F3 RID: 31219 RVA: 0x001C6CD8 File Offset: 0x001C4ED8
			protected override bool ContinueOperation()
			{
				try
				{
					if (base.ReceiveContext.PreAbandon())
					{
						return true;
					}
				}
				finally
				{
					base.ReceiveContext.ReleaseStateLock();
				}
				bool flag = false;
				IAsyncResult result;
				try
				{
					if (this.exception == null)
					{
						result = base.ReceiveContext.OnBeginAbandon(base.TimeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(ReceiveContext.AbandonAsyncResult.handleOperationComplete), this);
					}
					else
					{
						if (TD.ReceiveContextAbandonWithExceptionIsEnabled())
						{
							TD.ReceiveContextAbandonWithException(base.ReceiveContext.eventTraceActivity, base.GetType().ToString(), this.exception.GetType().ToString());
						}
						result = base.ReceiveContext.OnBeginAbandon(this.exception, base.TimeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(ReceiveContext.AbandonAsyncResult.handleOperationComplete), this);
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						if (TD.ReceiveContextAbandonFailedIsEnabled())
						{
							TD.ReceiveContextAbandonFailed((base.ReceiveContext != null) ? base.ReceiveContext.eventTraceActivity : null, base.GetType().ToString());
						}
						base.ReceiveContext.Fault();
					}
				}
				return base.SyncContinue(result);
			}

			// Token: 0x060079F4 RID: 31220 RVA: 0x001C6DFC File Offset: 0x001C4FFC
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReceiveContext.AbandonAsyncResult>(result);
			}

			// Token: 0x060079F5 RID: 31221 RVA: 0x001C6E08 File Offset: 0x001C5008
			private void EndAbandon(IAsyncResult result)
			{
				base.ReceiveContext.OnEndAbandon(result);
				object thisLock = base.ReceiveContext.ThisLock;
				lock (thisLock)
				{
					base.ReceiveContext.ThrowIfFaulted();
					base.ReceiveContext.ThrowIfNotAbandoning();
					base.ReceiveContext.State = ReceiveContextState.Abandoned;
				}
			}

			// Token: 0x060079F6 RID: 31222 RVA: 0x001C6E78 File Offset: 0x001C5078
			private static bool HandleOperationComplete(IAsyncResult result)
			{
				bool flag = false;
				ReceiveContext.AbandonAsyncResult abandonAsyncResult = (ReceiveContext.AbandonAsyncResult)result.AsyncState;
				bool result2;
				try
				{
					abandonAsyncResult.EndAbandon(result);
					flag = true;
					result2 = true;
				}
				finally
				{
					if (!flag)
					{
						if (TD.ReceiveContextAbandonFailedIsEnabled())
						{
							TD.ReceiveContextAbandonFailed(abandonAsyncResult.ReceiveContext.eventTraceActivity, abandonAsyncResult.GetType().ToString());
						}
						abandonAsyncResult.ReceiveContext.Fault();
					}
				}
				return result2;
			}

			// Token: 0x040045C4 RID: 17860
			private Exception exception;

			// Token: 0x040045C5 RID: 17861
			private static AsyncResult.AsyncCompletion handleOperationComplete = new AsyncResult.AsyncCompletion(ReceiveContext.AbandonAsyncResult.HandleOperationComplete);
		}

		// Token: 0x02000CDB RID: 3291
		private sealed class CompleteAsyncResult : ReceiveContext.WaitAndContinueOperationAsyncResult
		{
			// Token: 0x060079F8 RID: 31224 RVA: 0x001C6EF7 File Offset: 0x001C50F7
			public CompleteAsyncResult(ReceiveContext receiveContext, TimeSpan timeout, AsyncCallback callback, object state) : base(receiveContext, timeout, callback, state)
			{
				this.transaction = Transaction.Current;
				base.Begin();
			}

			// Token: 0x060079F9 RID: 31225 RVA: 0x001C6F18 File Offset: 0x001C5118
			protected override bool ContinueOperation()
			{
				IAsyncResult result;
				using (base.PrepareTransactionalCall(this.transaction))
				{
					bool flag = false;
					try
					{
						base.ReceiveContext.PreComplete();
						flag = true;
					}
					finally
					{
						if (!flag || this.transaction == null)
						{
							base.ReceiveContext.ReleaseStateLock();
						}
					}
					flag = false;
					try
					{
						result = base.ReceiveContext.OnBeginComplete(base.TimeoutHelper.RemainingTime(), base.PrepareAsyncCompletion(ReceiveContext.CompleteAsyncResult.handleOperationComplete), this);
						flag = true;
					}
					finally
					{
						if (!flag)
						{
							if (TD.ReceiveContextCompleteFailedIsEnabled())
							{
								TD.ReceiveContextCompleteFailed(base.ReceiveContext.eventTraceActivity, base.GetType().ToString());
							}
							base.ReceiveContext.Fault();
						}
					}
				}
				return base.SyncContinue(result);
			}

			// Token: 0x060079FA RID: 31226 RVA: 0x001C6FF8 File Offset: 0x001C51F8
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<ReceiveContext.CompleteAsyncResult>(result);
			}

			// Token: 0x060079FB RID: 31227 RVA: 0x001C7004 File Offset: 0x001C5204
			private void EndComplete(IAsyncResult result)
			{
				base.ReceiveContext.OnEndComplete(result);
				object thisLock = base.ReceiveContext.ThisLock;
				lock (thisLock)
				{
					base.ReceiveContext.ThrowIfFaulted();
					base.ReceiveContext.ThrowIfNotCompleting();
					base.ReceiveContext.State = ReceiveContextState.Completed;
				}
			}

			// Token: 0x060079FC RID: 31228 RVA: 0x001C7074 File Offset: 0x001C5274
			private static bool HandleOperationComplete(IAsyncResult result)
			{
				ReceiveContext.CompleteAsyncResult completeAsyncResult = (ReceiveContext.CompleteAsyncResult)result.AsyncState;
				bool flag = false;
				bool result2;
				try
				{
					completeAsyncResult.EndComplete(result);
					flag = true;
					result2 = true;
				}
				finally
				{
					if (!flag)
					{
						if (TD.ReceiveContextCompleteFailedIsEnabled())
						{
							TD.ReceiveContextCompleteFailed(completeAsyncResult.ReceiveContext.eventTraceActivity, completeAsyncResult.GetType().ToString());
						}
						completeAsyncResult.ReceiveContext.Fault();
					}
				}
				return result2;
			}

			// Token: 0x040045C6 RID: 17862
			private Transaction transaction;

			// Token: 0x040045C7 RID: 17863
			private static AsyncResult.AsyncCompletion handleOperationComplete = new AsyncResult.AsyncCompletion(ReceiveContext.CompleteAsyncResult.HandleOperationComplete);
		}

		// Token: 0x02000CDC RID: 3292
		private class EnlistmentNotifications : IEnlistmentNotification
		{
			// Token: 0x060079FE RID: 31230 RVA: 0x001C70F3 File Offset: 0x001C52F3
			public EnlistmentNotifications(ReceiveContext context)
			{
				this.context = context;
			}

			// Token: 0x060079FF RID: 31231 RVA: 0x001C7102 File Offset: 0x001C5302
			public void Commit(Enlistment enlistment)
			{
				this.context.OnTransactionStatusNotification(TransactionStatus.Committed);
				enlistment.Done();
			}

			// Token: 0x06007A00 RID: 31232 RVA: 0x001C7116 File Offset: 0x001C5316
			public void InDoubt(Enlistment enlistment)
			{
				this.context.OnTransactionStatusNotification(TransactionStatus.InDoubt);
				enlistment.Done();
			}

			// Token: 0x06007A01 RID: 31233 RVA: 0x001C712A File Offset: 0x001C532A
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				this.context.OnTransactionStatusNotification(TransactionStatus.Active);
				preparingEnlistment.Prepared();
			}

			// Token: 0x06007A02 RID: 31234 RVA: 0x001C713E File Offset: 0x001C533E
			public void Rollback(Enlistment enlistment)
			{
				this.context.OnTransactionStatusNotification(TransactionStatus.Aborted);
				enlistment.Done();
			}

			// Token: 0x040045C8 RID: 17864
			private ReceiveContext context;
		}

		// Token: 0x02000CDD RID: 3293
		private abstract class WaitAndContinueOperationAsyncResult : TransactedAsyncResult
		{
			// Token: 0x06007A03 RID: 31235 RVA: 0x001C7152 File Offset: 0x001C5352
			public WaitAndContinueOperationAsyncResult(ReceiveContext receiveContext, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.ReceiveContext = receiveContext;
				this.TimeoutHelper = new TimeoutHelper(timeout);
			}

			// Token: 0x17001B9D RID: 7069
			// (get) Token: 0x06007A04 RID: 31236 RVA: 0x001C7170 File Offset: 0x001C5370
			// (set) Token: 0x06007A05 RID: 31237 RVA: 0x001C7178 File Offset: 0x001C5378
			private protected ReceiveContext ReceiveContext { protected get; private set; }

			// Token: 0x17001B9E RID: 7070
			// (get) Token: 0x06007A06 RID: 31238 RVA: 0x001C7181 File Offset: 0x001C5381
			// (set) Token: 0x06007A07 RID: 31239 RVA: 0x001C7189 File Offset: 0x001C5389
			private protected TimeoutHelper TimeoutHelper { protected get; private set; }

			// Token: 0x06007A08 RID: 31240 RVA: 0x001C7194 File Offset: 0x001C5394
			protected void Begin()
			{
				if (!this.ReceiveContext.WaitForStateLockAsync(this.TimeoutHelper.RemainingTime(), ReceiveContext.WaitAndContinueOperationAsyncResult.onWaitForStateLockComplete, this))
				{
					return;
				}
				if (this.ContinueOperation())
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007A09 RID: 31241
			protected abstract bool ContinueOperation();

			// Token: 0x06007A0A RID: 31242 RVA: 0x001C71D4 File Offset: 0x001C53D4
			private static void OnWaitForStateLockComplete(object state, Exception asyncException)
			{
				ReceiveContext.WaitAndContinueOperationAsyncResult waitAndContinueOperationAsyncResult = (ReceiveContext.WaitAndContinueOperationAsyncResult)state;
				bool flag = true;
				Exception exception = null;
				if (asyncException != null)
				{
					if (asyncException is TimeoutException)
					{
						asyncException = waitAndContinueOperationAsyncResult.ReceiveContext.WrapStateException(asyncException);
					}
					exception = asyncException;
				}
				else
				{
					try
					{
						flag = waitAndContinueOperationAsyncResult.ContinueOperation();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
				}
				if (flag)
				{
					waitAndContinueOperationAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x040045C9 RID: 17865
			private static FastAsyncCallback onWaitForStateLockComplete = new FastAsyncCallback(ReceiveContext.WaitAndContinueOperationAsyncResult.OnWaitForStateLockComplete);
		}
	}
}
