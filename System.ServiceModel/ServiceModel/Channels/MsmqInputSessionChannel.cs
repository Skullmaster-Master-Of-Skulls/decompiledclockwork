using System;
using System.Runtime;
using System.Threading;
using System.Transactions;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008E5 RID: 2277
	internal sealed class MsmqInputSessionChannel : InputChannel, IInputSessionChannel, IInputChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x060056C6 RID: 22214 RVA: 0x0013E8CC File Offset: 0x0013CACC
		public MsmqInputSessionChannel(MsmqInputSessionChannelListener listener, Transaction associatedTx, ReceiveContext sessiongramReceiveContext) : base(listener, new EndpointAddress(listener.Uri, new AddressHeader[0]))
		{
			this.session = new MsmqInputSessionChannel.InputSession();
			this.incompleteMessageCount = 0;
			if (sessiongramReceiveContext == null)
			{
				this.receiveContextEnabled = false;
				this.associatedTx = associatedTx;
				this.associatedTx.EnlistVolatile(new MsmqInputSessionChannel.TransactionEnlistment(this, this.associatedTx), EnlistmentOptions.None);
				return;
			}
			this.receiveContextEnabled = true;
			this.sessiongramReceiveContext = sessiongramReceiveContext;
			this.sessiongramDoomed = false;
		}

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x060056C7 RID: 22215 RVA: 0x0013E943 File Offset: 0x0013CB43
		public IInputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x1700152F RID: 5423
		// (get) Token: 0x060056C8 RID: 22216 RVA: 0x0013E94B File Offset: 0x0013CB4B
		private int TotalPendingItems
		{
			get
			{
				return base.InternalPendingItems + this.incompleteMessageCount;
			}
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x0013E95A File Offset: 0x0013CB5A
		private void DetachTransaction(bool aborted)
		{
			this.associatedTx = null;
			if (aborted)
			{
				this.incompleteMessageCount += this.uncommittedMessageCount;
			}
			this.uncommittedMessageCount = 0;
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x0013E980 File Offset: 0x0013CB80
		private void AbandonMessage(TimeSpan timeout)
		{
			base.ThrowIfFaulted();
			this.sessiongramDoomed = true;
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x0013E98F File Offset: 0x0013CB8F
		private void CompleteMessage(TimeSpan timeout)
		{
			base.ThrowIfFaulted();
			this.EnsureReceiveContextTransaction();
			Interlocked.Increment(ref this.uncommittedMessageCount);
			Interlocked.Decrement(ref this.incompleteMessageCount);
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x0013E9B5 File Offset: 0x0013CBB5
		public override Message Receive()
		{
			return this.Receive(base.DefaultReceiveTimeout);
		}

		// Token: 0x060056CD RID: 22221 RVA: 0x0013E9C3 File Offset: 0x0013CBC3
		public override Message Receive(TimeSpan timeout)
		{
			return InputChannel.HelpReceive(this, timeout);
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x0013E9CC File Offset: 0x0013CBCC
		public override IAsyncResult BeginReceive(AsyncCallback callback, object state)
		{
			return this.BeginReceive(base.DefaultReceiveTimeout, callback, state);
		}

		// Token: 0x060056CF RID: 22223 RVA: 0x0013E9DC File Offset: 0x0013CBDC
		public override IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return InputChannel.HelpBeginReceive(this, timeout, callback, state);
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x0013E9E8 File Offset: 0x0013CBE8
		public override bool TryReceive(TimeSpan timeout, out Message message)
		{
			base.ThrowIfFaulted();
			if (CommunicationState.Closed == base.State || CommunicationState.Closing == base.State)
			{
				message = null;
				return true;
			}
			if (!this.receiveContextEnabled)
			{
				this.VerifyTransaction();
			}
			bool flag = base.TryReceive(timeout, out message);
			if (flag && message != null && this.receiveContextEnabled)
			{
				message.Properties[ReceiveContext.Name] = new MsmqInputSessionChannel.MsmqSessionReceiveContext(this);
				Interlocked.Increment(ref this.incompleteMessageCount);
			}
			return flag;
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x0013EA5C File Offset: 0x0013CC5C
		public override IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
		{
			base.ThrowIfFaulted();
			if (CommunicationState.Closed == base.State || CommunicationState.Closing == base.State)
			{
				return new CompletedAsyncResult<bool, Message>(true, null, callback, state);
			}
			if (!this.receiveContextEnabled)
			{
				this.VerifyTransaction();
			}
			return base.BeginTryReceive(timeout, callback, state);
		}

		// Token: 0x060056D2 RID: 22226 RVA: 0x0013EA98 File Offset: 0x0013CC98
		public override bool EndTryReceive(IAsyncResult result, out Message message)
		{
			CompletedAsyncResult<bool, Message> completedAsyncResult = result as CompletedAsyncResult<bool, Message>;
			if (completedAsyncResult != null)
			{
				return CompletedAsyncResult<bool, Message>.End(result, out message);
			}
			bool flag = base.EndTryReceive(result, out message);
			if (flag && message != null && this.receiveContextEnabled)
			{
				message.Properties[ReceiveContext.Name] = new MsmqInputSessionChannel.MsmqSessionReceiveContext(this);
				Interlocked.Increment(ref this.incompleteMessageCount);
			}
			return flag;
		}

		// Token: 0x060056D3 RID: 22227 RVA: 0x0013EAF3 File Offset: 0x0013CCF3
		public void FaultChannel()
		{
			base.Fault();
		}

		// Token: 0x060056D4 RID: 22228 RVA: 0x0013EAFC File Offset: 0x0013CCFC
		private void OnCloseReceiveContext(bool isAborting)
		{
			if (isAborting)
			{
				if (this.associatedTx != null)
				{
					Exception exception = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionChannelAbort")));
					this.RollbackTransaction(exception);
				}
				this.sessiongramReceiveContext.Abandon(TimeSpan.MaxValue);
				return;
			}
			if (this.TotalPendingItems > 0)
			{
				base.Fault();
				this.sessiongramReceiveContext.Abandon(TimeSpan.MaxValue);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionPrematureClose")));
			}
		}

		// Token: 0x060056D5 RID: 22229 RVA: 0x0013EB88 File Offset: 0x0013CD88
		private void OnCloseTransactional(bool isAborting)
		{
			if (isAborting)
			{
				this.RollbackTransaction(null);
				return;
			}
			this.VerifyTransaction();
			if (base.InternalPendingItems > 0)
			{
				this.RollbackTransaction(null);
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionMessagesNotConsumed")));
			}
		}

		// Token: 0x060056D6 RID: 22230 RVA: 0x0013EBD6 File Offset: 0x0013CDD6
		private void OnCloseCore(bool isAborting)
		{
			if (this.receiveContextEnabled)
			{
				this.OnCloseReceiveContext(isAborting);
				return;
			}
			this.OnCloseTransactional(isAborting);
		}

		// Token: 0x060056D7 RID: 22231 RVA: 0x0013EBEF File Offset: 0x0013CDEF
		protected override void OnAbort()
		{
			this.OnCloseCore(true);
			base.OnAbort();
		}

		// Token: 0x060056D8 RID: 22232 RVA: 0x0013EBFE File Offset: 0x0013CDFE
		protected override void OnClose(TimeSpan timeout)
		{
			this.OnCloseCore(false);
			base.OnClose(timeout);
		}

		// Token: 0x060056D9 RID: 22233 RVA: 0x0013EC0E File Offset: 0x0013CE0E
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnCloseCore(false);
			return base.OnBeginClose(timeout, callback, state);
		}

		// Token: 0x060056DA RID: 22234 RVA: 0x0013EC20 File Offset: 0x0013CE20
		private void RollbackTransaction(Exception exception)
		{
			try
			{
				if (this.associatedTx.TransactionInformation.Status == TransactionStatus.Active)
				{
					this.associatedTx.Rollback(exception);
				}
			}
			catch (TransactionAbortedException ex)
			{
				MsmqDiagnostics.ExpectedException(ex);
			}
			catch (ObjectDisposedException ex2)
			{
				MsmqDiagnostics.ExpectedException(ex2);
			}
		}

		// Token: 0x060056DB RID: 22235 RVA: 0x0013EC7C File Offset: 0x0013CE7C
		private void EnsureReceiveContextTransaction()
		{
			if (Transaction.Current == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqTransactionRequired")));
			}
			if (this.associatedTx == null)
			{
				this.associatedTx = Transaction.Current;
				this.associatedTx.EnlistVolatile(new MsmqInputSessionChannel.ReceiveContextTransactionEnlistment(this, this.associatedTx, this.sessiongramReceiveContext), EnlistmentOptions.EnlistDuringPrepareRequired);
				return;
			}
			if (this.associatedTx != Transaction.Current)
			{
				this.RollbackTransaction(null);
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqSameTransactionExpected")));
			}
			if (Transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
			{
				base.Fault();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqTransactionNotActive")));
			}
		}

		// Token: 0x060056DC RID: 22236 RVA: 0x0013ED54 File Offset: 0x0013CF54
		private void VerifyTransaction()
		{
			if (base.InternalPendingItems > 0)
			{
				if (this.associatedTx != Transaction.Current)
				{
					this.RollbackTransaction(null);
					base.Fault();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqSameTransactionExpected")));
				}
				if (Transaction.Current.TransactionInformation.Status != TransactionStatus.Active)
				{
					this.RollbackTransaction(null);
					base.Fault();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCritical(new InvalidOperationException(SR.GetString("MsmqTransactionNotActive")));
				}
			}
		}

		// Token: 0x04003588 RID: 13704
		private IInputSession session;

		// Token: 0x04003589 RID: 13705
		private Transaction associatedTx;

		// Token: 0x0400358A RID: 13706
		private ReceiveContext sessiongramReceiveContext;

		// Token: 0x0400358B RID: 13707
		private bool receiveContextEnabled;

		// Token: 0x0400358C RID: 13708
		private bool sessiongramDoomed;

		// Token: 0x0400358D RID: 13709
		private int incompleteMessageCount;

		// Token: 0x0400358E RID: 13710
		private int uncommittedMessageCount;

		// Token: 0x02000D8D RID: 3469
		private class InputSession : IInputSession, ISession
		{
			// Token: 0x17001C39 RID: 7225
			// (get) Token: 0x06007E98 RID: 32408 RVA: 0x001D7C75 File Offset: 0x001D5E75
			public string Id
			{
				get
				{
					return this.id;
				}
			}

			// Token: 0x040048A5 RID: 18597
			private string id = "uuid://session-gram/" + Guid.NewGuid().ToString();
		}

		// Token: 0x02000D8E RID: 3470
		private class MsmqSessionReceiveContext : ReceiveContext
		{
			// Token: 0x06007E9A RID: 32410 RVA: 0x001D7CB6 File Offset: 0x001D5EB6
			public MsmqSessionReceiveContext(MsmqInputSessionChannel channel)
			{
				this.channel = channel;
			}

			// Token: 0x06007E9B RID: 32411 RVA: 0x001D7CC5 File Offset: 0x001D5EC5
			protected override void OnAbandon(TimeSpan timeout)
			{
				this.channel.AbandonMessage(timeout);
			}

			// Token: 0x06007E9C RID: 32412 RVA: 0x001D7CD3 File Offset: 0x001D5ED3
			protected override IAsyncResult OnBeginAbandon(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.CreateAbandon(this, timeout, callback, state);
			}

			// Token: 0x06007E9D RID: 32413 RVA: 0x001D7CDE File Offset: 0x001D5EDE
			protected override IAsyncResult OnBeginComplete(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.CreateComplete(this, timeout, callback, state);
			}

			// Token: 0x06007E9E RID: 32414 RVA: 0x001D7CE9 File Offset: 0x001D5EE9
			protected override void OnComplete(TimeSpan timeout)
			{
				this.channel.CompleteMessage(timeout);
			}

			// Token: 0x06007E9F RID: 32415 RVA: 0x001D7CF7 File Offset: 0x001D5EF7
			protected override void OnEndAbandon(IAsyncResult result)
			{
				MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.End(result);
			}

			// Token: 0x06007EA0 RID: 32416 RVA: 0x001D7CFF File Offset: 0x001D5EFF
			protected override void OnEndComplete(IAsyncResult result)
			{
				MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.End(result);
			}

			// Token: 0x040048A6 RID: 18598
			private MsmqInputSessionChannel channel;

			// Token: 0x02000F70 RID: 3952
			private class SessionReceiveContextAsyncResult : AsyncResult
			{
				// Token: 0x060087B8 RID: 34744 RVA: 0x001F8828 File Offset: 0x001F6A28
				private SessionReceiveContextAsyncResult(MsmqInputSessionChannel.MsmqSessionReceiveContext receiveContext, TimeSpan timeout, AsyncCallback callback, object state, Action<object> target) : base(callback, state)
				{
					this.completionTransaction = Transaction.Current;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.receiveContext = receiveContext;
					ActionItem.Schedule(target, this);
				}

				// Token: 0x060087B9 RID: 34745 RVA: 0x001F8859 File Offset: 0x001F6A59
				public static IAsyncResult CreateComplete(MsmqInputSessionChannel.MsmqSessionReceiveContext receiveContext, TimeSpan timeout, AsyncCallback callback, object state)
				{
					if (MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onComplete == null)
					{
						MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onComplete = new Action<object>(MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.OnComplete);
					}
					return new MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult(receiveContext, timeout, callback, state, MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onComplete);
				}

				// Token: 0x060087BA RID: 34746 RVA: 0x001F8881 File Offset: 0x001F6A81
				public static IAsyncResult CreateAbandon(MsmqInputSessionChannel.MsmqSessionReceiveContext receiveContext, TimeSpan timeout, AsyncCallback callback, object state)
				{
					if (MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onAbandon == null)
					{
						MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onAbandon = new Action<object>(MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.OnAbandon);
					}
					return new MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult(receiveContext, timeout, callback, state, MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult.onAbandon);
				}

				// Token: 0x060087BB RID: 34747 RVA: 0x001F88AC File Offset: 0x001F6AAC
				private static void OnComplete(object parameter)
				{
					MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult sessionReceiveContextAsyncResult = parameter as MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult;
					Transaction value = Transaction.Current;
					Transaction.Current = sessionReceiveContextAsyncResult.completionTransaction;
					try
					{
						Exception exception = null;
						try
						{
							sessionReceiveContextAsyncResult.receiveContext.OnComplete(sessionReceiveContextAsyncResult.timeoutHelper.RemainingTime());
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							exception = ex;
						}
						sessionReceiveContextAsyncResult.Complete(false, exception);
					}
					finally
					{
						Transaction.Current = value;
					}
				}

				// Token: 0x060087BC RID: 34748 RVA: 0x001F8928 File Offset: 0x001F6B28
				private static void OnAbandon(object parameter)
				{
					MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult sessionReceiveContextAsyncResult = parameter as MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult;
					Exception exception = null;
					try
					{
						sessionReceiveContextAsyncResult.receiveContext.OnAbandon(sessionReceiveContextAsyncResult.timeoutHelper.RemainingTime());
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					sessionReceiveContextAsyncResult.Complete(false, exception);
				}

				// Token: 0x060087BD RID: 34749 RVA: 0x001F8980 File Offset: 0x001F6B80
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<MsmqInputSessionChannel.MsmqSessionReceiveContext.SessionReceiveContextAsyncResult>(result);
				}

				// Token: 0x04004F22 RID: 20258
				private MsmqInputSessionChannel.MsmqSessionReceiveContext receiveContext;

				// Token: 0x04004F23 RID: 20259
				private Transaction completionTransaction;

				// Token: 0x04004F24 RID: 20260
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F25 RID: 20261
				private static Action<object> onComplete;

				// Token: 0x04004F26 RID: 20262
				private static Action<object> onAbandon;
			}
		}

		// Token: 0x02000D8F RID: 3471
		private class ReceiveContextTransactionEnlistment : IEnlistmentNotification
		{
			// Token: 0x06007EA1 RID: 32417 RVA: 0x001D7D07 File Offset: 0x001D5F07
			public ReceiveContextTransactionEnlistment(MsmqInputSessionChannel channel, Transaction transaction, ReceiveContext receiveContext)
			{
				this.channel = channel;
				this.transaction = transaction;
				this.sessiongramReceiveContext = receiveContext;
			}

			// Token: 0x06007EA2 RID: 32418 RVA: 0x001D7D24 File Offset: 0x001D5F24
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				if (this.channel.TotalPendingItems > 0 || this.channel.sessiongramDoomed)
				{
					Exception e = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionChannelHasPendingItems")));
					this.sessiongramReceiveContext.Abandon(TimeSpan.MaxValue);
					preparingEnlistment.ForceRollback(e);
					this.channel.Fault();
					return;
				}
				Transaction value = Transaction.Current;
				try
				{
					Transaction.Current = this.transaction;
					try
					{
						this.sessiongramReceiveContext.Complete(TimeSpan.MaxValue);
						preparingEnlistment.Done();
					}
					catch (MsmqException e2)
					{
						preparingEnlistment.ForceRollback(e2);
						this.channel.Fault();
					}
				}
				finally
				{
					Transaction.Current = value;
				}
			}

			// Token: 0x06007EA3 RID: 32419 RVA: 0x001D7DEC File Offset: 0x001D5FEC
			public void Commit(Enlistment enlistment)
			{
				this.channel.DetachTransaction(false);
				enlistment.Done();
			}

			// Token: 0x06007EA4 RID: 32420 RVA: 0x001D7E00 File Offset: 0x001D6000
			public void Rollback(Enlistment enlistment)
			{
				this.channel.DetachTransaction(true);
				enlistment.Done();
			}

			// Token: 0x06007EA5 RID: 32421 RVA: 0x001D7E14 File Offset: 0x001D6014
			public void InDoubt(Enlistment enlistment)
			{
				enlistment.Done();
			}

			// Token: 0x040048A7 RID: 18599
			private MsmqInputSessionChannel channel;

			// Token: 0x040048A8 RID: 18600
			private Transaction transaction;

			// Token: 0x040048A9 RID: 18601
			private ReceiveContext sessiongramReceiveContext;
		}

		// Token: 0x02000D90 RID: 3472
		private class TransactionEnlistment : IEnlistmentNotification
		{
			// Token: 0x06007EA6 RID: 32422 RVA: 0x001D7E1C File Offset: 0x001D601C
			public TransactionEnlistment(MsmqInputSessionChannel channel, Transaction transaction)
			{
				this.channel = channel;
				this.transaction = transaction;
			}

			// Token: 0x06007EA7 RID: 32423 RVA: 0x001D7E34 File Offset: 0x001D6034
			public void Prepare(PreparingEnlistment preparingEnlistment)
			{
				if (this.channel.State == CommunicationState.Opened && this.channel.InternalPendingItems > 0)
				{
					Exception e = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("MsmqSessionChannelsMustBeClosed")));
					preparingEnlistment.ForceRollback(e);
					this.channel.Fault();
					return;
				}
				preparingEnlistment.Done();
			}

			// Token: 0x06007EA8 RID: 32424 RVA: 0x001D7E90 File Offset: 0x001D6090
			public void Commit(Enlistment enlistment)
			{
				enlistment.Done();
			}

			// Token: 0x06007EA9 RID: 32425 RVA: 0x001D7E98 File Offset: 0x001D6098
			public void Rollback(Enlistment enlistment)
			{
				this.channel.Fault();
				enlistment.Done();
			}

			// Token: 0x06007EAA RID: 32426 RVA: 0x001D7EAB File Offset: 0x001D60AB
			public void InDoubt(Enlistment enlistment)
			{
				enlistment.Done();
			}

			// Token: 0x040048AA RID: 18602
			private MsmqInputSessionChannel channel;

			// Token: 0x040048AB RID: 18603
			private Transaction transaction;
		}
	}
}
