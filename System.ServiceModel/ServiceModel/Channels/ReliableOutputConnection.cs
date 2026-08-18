using System;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000951 RID: 2385
	internal sealed class ReliableOutputConnection
	{
		// Token: 0x06005B9F RID: 23455 RVA: 0x0014FDC0 File Offset: 0x0014DFC0
		public ReliableOutputConnection(UniqueId id, int maxTransferWindowSize, MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, TimeSpan initialRtt, bool requestAcks, TimeSpan sendTimeout)
		{
			this.id = id;
			this.messageVersion = messageVersion;
			this.reliableMessagingVersion = reliableMessagingVersion;
			this.sendTimeout = sendTimeout;
			this.strategy = new TransmissionStrategy(reliableMessagingVersion, initialRtt, maxTransferWindowSize, requestAcks, id);
			this.strategy.RetryTimeoutElapsed = new RetryHandler(this.OnRetryTimeoutElapsed);
			this.strategy.OnException = new ComponentExceptionHandler(this.RaiseOnException);
		}

		// Token: 0x17001605 RID: 5637
		// (get) Token: 0x06005BA0 RID: 23456 RVA: 0x0014FE59 File Offset: 0x0014E059
		private MessageVersion MessageVersion
		{
			get
			{
				return this.messageVersion;
			}
		}

		// Token: 0x17001606 RID: 5638
		// (set) Token: 0x06005BA1 RID: 23457 RVA: 0x0014FE61 File Offset: 0x0014E061
		public BeginSendHandler BeginSendHandler
		{
			set
			{
				this.beginSendHandler = value;
			}
		}

		// Token: 0x17001607 RID: 5639
		// (set) Token: 0x06005BA2 RID: 23458 RVA: 0x0014FE6A File Offset: 0x0014E06A
		public OperationWithTimeoutBeginCallback BeginSendAckRequestedHandler
		{
			set
			{
				this.beginSendAckRequestedHandler = value;
			}
		}

		// Token: 0x17001608 RID: 5640
		// (get) Token: 0x06005BA3 RID: 23459 RVA: 0x0014FE73 File Offset: 0x0014E073
		public bool Closed
		{
			get
			{
				return this.closed;
			}
		}

		// Token: 0x17001609 RID: 5641
		// (set) Token: 0x06005BA4 RID: 23460 RVA: 0x0014FE7B File Offset: 0x0014E07B
		public EndSendHandler EndSendHandler
		{
			set
			{
				this.endSendHandler = value;
			}
		}

		// Token: 0x1700160A RID: 5642
		// (set) Token: 0x06005BA5 RID: 23461 RVA: 0x0014FE84 File Offset: 0x0014E084
		public OperationEndCallback EndSendAckRequestedHandler
		{
			set
			{
				this.endSendAckRequestedHandler = value;
			}
		}

		// Token: 0x1700160B RID: 5643
		// (get) Token: 0x06005BA6 RID: 23462 RVA: 0x0014FE8D File Offset: 0x0014E08D
		public long Last
		{
			get
			{
				return this.strategy.Last;
			}
		}

		// Token: 0x1700160C RID: 5644
		// (set) Token: 0x06005BA7 RID: 23463 RVA: 0x0014FE9A File Offset: 0x0014E09A
		public SendHandler SendHandler
		{
			set
			{
				this.sendHandler = value;
			}
		}

		// Token: 0x1700160D RID: 5645
		// (set) Token: 0x06005BA8 RID: 23464 RVA: 0x0014FEA3 File Offset: 0x0014E0A3
		public OperationWithTimeoutCallback SendAckRequestedHandler
		{
			set
			{
				this.sendAckRequestedHandler = value;
			}
		}

		// Token: 0x1700160E RID: 5646
		// (get) Token: 0x06005BA9 RID: 23465 RVA: 0x0014FEAC File Offset: 0x0014E0AC
		public TransmissionStrategy Strategy
		{
			get
			{
				return this.strategy;
			}
		}

		// Token: 0x1700160F RID: 5647
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x0014FEB4 File Offset: 0x0014E0B4
		private object ThisLock
		{
			get
			{
				return this.mutex;
			}
		}

		// Token: 0x06005BAB RID: 23467 RVA: 0x0014FEBC File Offset: 0x0014E0BC
		public void Abort(ChannelBase channel)
		{
			this.sendGuard.Abort();
			this.shutdownHandle.Abort(channel);
			this.strategy.Abort(channel);
		}

		// Token: 0x06005BAC RID: 23468 RVA: 0x0014FEE4 File Offset: 0x0014E0E4
		private void CompleteTransfer(TimeSpan timeout)
		{
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				Message message = Message.CreateMessage(this.MessageVersion, "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage");
				message.Properties.AllowOutputBatching = false;
				this.InternalAddMessage(message, timeout, null, true);
				return;
			}
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("Unsupported version.");
			}
			if (this.strategy.SetLast())
			{
				this.shutdownHandle.Set();
				return;
			}
			this.sendAckRequestedHandler(timeout);
		}

		// Token: 0x06005BAD RID: 23469 RVA: 0x0014FF64 File Offset: 0x0014E164
		public bool AddMessage(Message message, TimeSpan timeout, object state)
		{
			return this.InternalAddMessage(message, timeout, state, false);
		}

		// Token: 0x06005BAE RID: 23470 RVA: 0x0014FF70 File Offset: 0x0014E170
		public IAsyncResult BeginAddMessage(Message message, TimeSpan timeout, object state, AsyncCallback callback, object asyncState)
		{
			return new ReliableOutputConnection.AddAsyncResult(message, false, timeout, state, this, callback, asyncState);
		}

		// Token: 0x06005BAF RID: 23471 RVA: 0x0014FF80 File Offset: 0x0014E180
		public bool EndAddMessage(IAsyncResult result)
		{
			return ReliableOutputConnection.AddAsyncResult.End(result);
		}

		// Token: 0x06005BB0 RID: 23472 RVA: 0x0014FF88 File Offset: 0x0014E188
		private IAsyncResult BeginCompleteTransfer(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				Message message = Message.CreateMessage(this.MessageVersion, "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage");
				message.Properties.AllowOutputBatching = false;
				return new ReliableOutputConnection.AddAsyncResult(message, true, timeout, null, this, callback, state);
			}
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("Unsupported version.");
			}
			if (this.strategy.SetLast())
			{
				this.shutdownHandle.Set();
				return new ReliableOutputConnection.AlreadyCompletedTransferAsyncResult(callback, state);
			}
			return this.beginSendAckRequestedHandler(timeout, callback, state);
		}

		// Token: 0x06005BB1 RID: 23473 RVA: 0x00150014 File Offset: 0x0014E214
		private void EndCompleteTransfer(IAsyncResult result)
		{
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				ReliableOutputConnection.AddAsyncResult.End(result);
				return;
			}
			if (this.reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("Unsupported version.");
			}
			ReliableOutputConnection.AlreadyCompletedTransferAsyncResult alreadyCompletedTransferAsyncResult = result as ReliableOutputConnection.AlreadyCompletedTransferAsyncResult;
			if (alreadyCompletedTransferAsyncResult != null)
			{
				alreadyCompletedTransferAsyncResult.End();
				return;
			}
			this.endSendAckRequestedHandler(result);
		}

		// Token: 0x06005BB2 RID: 23474 RVA: 0x0015006C File Offset: 0x0014E26C
		public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				flag = !this.closed;
				this.closed = true;
			}
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				flag ? new OperationWithTimeoutBeginCallback(this.BeginCompleteTransfer) : null,
				new OperationWithTimeoutBeginCallback(this.shutdownHandle.BeginWait),
				new OperationWithTimeoutBeginCallback(this.sendGuard.BeginClose),
				this.beginSendAckRequestedHandler
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				flag ? new OperationEndCallback(this.EndCompleteTransfer) : null,
				new OperationEndCallback(this.shutdownHandle.EndWait),
				new OperationEndCallback(this.sendGuard.EndClose),
				this.endSendAckRequestedHandler
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005BB3 RID: 23475 RVA: 0x00150160 File Offset: 0x0014E360
		public bool CheckForTermination()
		{
			return this.strategy.DoneTransmitting;
		}

		// Token: 0x06005BB4 RID: 23476 RVA: 0x00150170 File Offset: 0x0014E370
		public void Close(TimeSpan timeout)
		{
			bool flag = false;
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				flag = !this.closed;
				this.closed = true;
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (flag)
			{
				this.CompleteTransfer(timeoutHelper.RemainingTime());
			}
			this.shutdownHandle.Wait(timeoutHelper.RemainingTime());
			this.sendGuard.Close(timeoutHelper.RemainingTime());
			this.strategy.Close();
		}

		// Token: 0x06005BB5 RID: 23477 RVA: 0x00150208 File Offset: 0x0014E408
		private void CompleteSendRetries(IAsyncResult result)
		{
			do
			{
				this.endSendHandler(result);
				this.sendGuard.Exit();
				this.strategy.DequeuePending();
				if (!this.sendGuard.Enter())
				{
					return;
				}
				MessageAttemptInfo messageInfoForRetry = this.strategy.GetMessageInfoForRetry(true);
				if (messageInfoForRetry.Message == null)
				{
					goto IL_6A;
				}
				result = this.beginSendHandler(messageInfoForRetry, this.sendTimeout, true, ReliableOutputConnection.onSendRetriesComplete, this);
			}
			while (result.CompletedSynchronously);
			return;
			IL_6A:
			this.sendGuard.Exit();
			this.OnTransferComplete();
		}

		// Token: 0x06005BB6 RID: 23478 RVA: 0x00150290 File Offset: 0x0014E490
		private void CompleteSendRetry(IAsyncResult result)
		{
			try
			{
				this.endSendHandler(result);
			}
			finally
			{
				this.sendGuard.Exit();
			}
		}

		// Token: 0x06005BB7 RID: 23479 RVA: 0x001502C8 File Offset: 0x0014E4C8
		public void EndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
			this.strategy.Close();
		}

		// Token: 0x06005BB8 RID: 23480 RVA: 0x001502DB File Offset: 0x0014E4DB
		public void Fault(ChannelBase channel)
		{
			this.sendGuard.Abort();
			this.shutdownHandle.Fault(channel);
			this.strategy.Fault(channel);
		}

		// Token: 0x06005BB9 RID: 23481 RVA: 0x00150300 File Offset: 0x0014E500
		private bool InternalAddMessage(Message message, TimeSpan timeout, object state, bool isLast)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			MessageAttemptInfo attemptInfo;
			try
			{
				if (isLast)
				{
					if (state != null)
					{
						throw Fx.AssertAndThrow("The isLast overload does not take a state.");
					}
					attemptInfo = this.strategy.AddLast(message, timeoutHelper.RemainingTime(), null);
				}
				else if (!this.strategy.Add(message, timeoutHelper.RemainingTime(), state, out attemptInfo))
				{
					return false;
				}
			}
			catch (TimeoutException)
			{
				if (isLast)
				{
					this.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.id, SR.GetString("SequenceTerminatedAddLastToWindowTimedOut"), null));
				}
				throw;
			}
			catch (Exception exception)
			{
				if (!Fx.IsFatal(exception))
				{
					this.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.id, SR.GetString("SequenceTerminatedUnknownAddToWindowError"), null));
				}
				throw;
			}
			if (this.sendGuard.Enter())
			{
				try
				{
					this.sendHandler(attemptInfo, timeoutHelper.RemainingTime(), false);
				}
				catch (QuotaExceededException)
				{
					this.RaiseFault(null, SequenceTerminatedFault.CreateQuotaExceededFault(this.id));
					throw;
				}
				finally
				{
					this.sendGuard.Exit();
				}
			}
			return true;
		}

		// Token: 0x06005BBA RID: 23482 RVA: 0x00150428 File Offset: 0x0014E628
		public bool IsFinalAckConsistent(SequenceRangeCollection ranges)
		{
			return this.strategy.IsFinalAckConsistent(ranges);
		}

		// Token: 0x06005BBB RID: 23483 RVA: 0x00150438 File Offset: 0x0014E638
		private void OnRetryTimeoutElapsed(MessageAttemptInfo attemptInfo)
		{
			if (this.sendGuard.Enter())
			{
				IAsyncResult asyncResult = this.beginSendHandler(attemptInfo, this.sendTimeout, true, ReliableOutputConnection.onSendRetryComplete, this);
				if (asyncResult.CompletedSynchronously)
				{
					this.CompleteSendRetry(asyncResult);
				}
			}
		}

		// Token: 0x06005BBC RID: 23484 RVA: 0x0015047C File Offset: 0x0014E67C
		private static void OnSendRetryComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableOutputConnection reliableOutputConnection = (ReliableOutputConnection)result.AsyncState;
				try
				{
					reliableOutputConnection.CompleteSendRetry(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					reliableOutputConnection.RaiseOnException(exception);
				}
			}
		}

		// Token: 0x06005BBD RID: 23485 RVA: 0x001504CC File Offset: 0x0014E6CC
		private static void OnSendRetriesComplete(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				ReliableOutputConnection reliableOutputConnection = (ReliableOutputConnection)result.AsyncState;
				try
				{
					reliableOutputConnection.CompleteSendRetries(result);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					reliableOutputConnection.RaiseOnException(exception);
				}
			}
		}

		// Token: 0x06005BBE RID: 23486 RVA: 0x0015051C File Offset: 0x0014E71C
		private void OnTransferComplete()
		{
			this.strategy.DequeuePending();
			if (this.strategy.DoneTransmitting)
			{
				this.Terminate();
			}
		}

		// Token: 0x06005BBF RID: 23487 RVA: 0x0015053C File Offset: 0x0014E73C
		public void ProcessTransferred(long transferred, SequenceRangeCollection ranges, int quotaRemaining)
		{
			if (transferred < 0L)
			{
				throw Fx.AssertAndThrow("Argument transferred must be a valid sequence number or 0 for protocol messages.");
			}
			bool flag;
			bool flag2;
			this.strategy.ProcessAcknowledgement(ranges, out flag, out flag2);
			flag = (flag || (transferred != 0L && !ranges.Contains(transferred)));
			if (flag)
			{
				WsrmFault wsrmFault = new InvalidAcknowledgementFault(this.id, ranges);
				this.RaiseFault(wsrmFault.CreateException(), wsrmFault);
				return;
			}
			if (transferred > 0L && this.strategy.ProcessTransferred(transferred, quotaRemaining))
			{
				ActionItem.Schedule(ReliableOutputConnection.sendRetries, this);
				return;
			}
			this.OnTransferComplete();
		}

		// Token: 0x06005BC0 RID: 23488 RVA: 0x001505C4 File Offset: 0x0014E7C4
		public void ProcessTransferred(SequenceRangeCollection ranges, int quotaRemaining)
		{
			bool flag;
			bool flag2;
			this.strategy.ProcessAcknowledgement(ranges, out flag, out flag2);
			if (flag || flag2)
			{
				WsrmFault wsrmFault = new InvalidAcknowledgementFault(this.id, ranges);
				this.RaiseFault(wsrmFault.CreateException(), wsrmFault);
				return;
			}
			if (this.strategy.ProcessTransferred(ranges, quotaRemaining))
			{
				ActionItem.Schedule(ReliableOutputConnection.sendRetries, this);
				return;
			}
			this.OnTransferComplete();
		}

		// Token: 0x06005BC1 RID: 23489 RVA: 0x00150624 File Offset: 0x0014E824
		private void RaiseFault(Exception faultException, WsrmFault fault)
		{
			ComponentFaultedHandler faulted = this.Faulted;
			if (faulted != null)
			{
				faulted(faultException, fault);
			}
		}

		// Token: 0x06005BC2 RID: 23490 RVA: 0x00150644 File Offset: 0x0014E844
		private void RaiseOnException(Exception exception)
		{
			ComponentExceptionHandler onException = this.OnException;
			if (onException != null)
			{
				onException(exception);
			}
		}

		// Token: 0x06005BC3 RID: 23491 RVA: 0x00150664 File Offset: 0x0014E864
		private void SendRetries()
		{
			IAsyncResult asyncResult = null;
			if (this.sendGuard.Enter())
			{
				MessageAttemptInfo messageInfoForRetry = this.strategy.GetMessageInfoForRetry(false);
				if (messageInfoForRetry.Message != null)
				{
					asyncResult = this.beginSendHandler(messageInfoForRetry, this.sendTimeout, true, ReliableOutputConnection.onSendRetriesComplete, this);
				}
				if (asyncResult != null)
				{
					if (asyncResult.CompletedSynchronously)
					{
						this.CompleteSendRetries(asyncResult);
						return;
					}
				}
				else
				{
					this.sendGuard.Exit();
					this.OnTransferComplete();
				}
			}
		}

		// Token: 0x06005BC4 RID: 23492 RVA: 0x001506D4 File Offset: 0x0014E8D4
		private static void SendRetries(object state)
		{
			ReliableOutputConnection reliableOutputConnection = (ReliableOutputConnection)state;
			try
			{
				reliableOutputConnection.SendRetries();
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				reliableOutputConnection.RaiseOnException(exception);
			}
		}

		// Token: 0x06005BC5 RID: 23493 RVA: 0x00150714 File Offset: 0x0014E914
		public void Terminate()
		{
			object thisLock = this.ThisLock;
			lock (thisLock)
			{
				if (this.terminated)
				{
					return;
				}
				this.terminated = true;
			}
			this.shutdownHandle.Set();
		}

		// Token: 0x040036FC RID: 14076
		private BeginSendHandler beginSendHandler;

		// Token: 0x040036FD RID: 14077
		private OperationWithTimeoutBeginCallback beginSendAckRequestedHandler;

		// Token: 0x040036FE RID: 14078
		private bool closed;

		// Token: 0x040036FF RID: 14079
		private EndSendHandler endSendHandler;

		// Token: 0x04003700 RID: 14080
		private OperationEndCallback endSendAckRequestedHandler;

		// Token: 0x04003701 RID: 14081
		private UniqueId id;

		// Token: 0x04003702 RID: 14082
		private MessageVersion messageVersion;

		// Token: 0x04003703 RID: 14083
		private object mutex = new object();

		// Token: 0x04003704 RID: 14084
		private static AsyncCallback onSendRetriesComplete = Fx.ThunkCallback(new AsyncCallback(ReliableOutputConnection.OnSendRetriesComplete));

		// Token: 0x04003705 RID: 14085
		private static AsyncCallback onSendRetryComplete = Fx.ThunkCallback(new AsyncCallback(ReliableOutputConnection.OnSendRetryComplete));

		// Token: 0x04003706 RID: 14086
		private ReliableMessagingVersion reliableMessagingVersion;

		// Token: 0x04003707 RID: 14087
		private Guard sendGuard = new Guard(int.MaxValue);

		// Token: 0x04003708 RID: 14088
		private SendHandler sendHandler;

		// Token: 0x04003709 RID: 14089
		private OperationWithTimeoutCallback sendAckRequestedHandler;

		// Token: 0x0400370A RID: 14090
		private static Action<object> sendRetries = new Action<object>(ReliableOutputConnection.SendRetries);

		// Token: 0x0400370B RID: 14091
		private TimeSpan sendTimeout;

		// Token: 0x0400370C RID: 14092
		private InterruptibleWaitObject shutdownHandle = new InterruptibleWaitObject(false);

		// Token: 0x0400370D RID: 14093
		private TransmissionStrategy strategy;

		// Token: 0x0400370E RID: 14094
		private bool terminated;

		// Token: 0x0400370F RID: 14095
		public ComponentFaultedHandler Faulted;

		// Token: 0x04003710 RID: 14096
		public ComponentExceptionHandler OnException;

		// Token: 0x02000DCE RID: 3534
		private sealed class AddAsyncResult : AsyncResult
		{
			// Token: 0x0600801B RID: 32795 RVA: 0x001DC73C File Offset: 0x001DA93C
			public AddAsyncResult(Message message, bool isLast, TimeSpan timeout, object state, ReliableOutputConnection connection, AsyncCallback callback, object asyncState) : base(callback, asyncState)
			{
				this.connection = connection;
				this.timeoutHelper = new TimeoutHelper(timeout);
				this.isLast = isLast;
				bool flag = false;
				IAsyncResult asyncResult;
				try
				{
					if (isLast)
					{
						if (state != null)
						{
							throw Fx.AssertAndThrow("The isLast overload does not take a state.");
						}
						asyncResult = this.connection.strategy.BeginAddLast(message, this.timeoutHelper.RemainingTime(), state, ReliableOutputConnection.AddAsyncResult.addCompleteStatic, this);
					}
					else
					{
						asyncResult = this.connection.strategy.BeginAdd(message, this.timeoutHelper.RemainingTime(), state, ReliableOutputConnection.AddAsyncResult.addCompleteStatic, this);
					}
				}
				catch (TimeoutException)
				{
					if (isLast)
					{
						this.connection.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.connection.id, SR.GetString("SequenceTerminatedAddLastToWindowTimedOut"), null));
					}
					throw;
				}
				catch (Exception exception)
				{
					if (!Fx.IsFatal(exception))
					{
						this.connection.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.connection.id, SR.GetString("SequenceTerminatedUnknownAddToWindowError"), null));
					}
					throw;
				}
				if (asyncResult.CompletedSynchronously)
				{
					flag = this.CompleteAdd(asyncResult);
				}
				if (flag)
				{
					base.Complete(true);
				}
			}

			// Token: 0x0600801C RID: 32796 RVA: 0x001DC868 File Offset: 0x001DAA68
			private static void AddComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableOutputConnection.AddAsyncResult addAsyncResult = (ReliableOutputConnection.AddAsyncResult)result.AsyncState;
					bool flag = false;
					Exception ex = null;
					try
					{
						flag = addAsyncResult.CompleteAdd(result);
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						ex = ex2;
					}
					if (flag || ex != null)
					{
						addAsyncResult.Complete(false, ex);
					}
				}
			}

			// Token: 0x0600801D RID: 32797 RVA: 0x001DC8C4 File Offset: 0x001DAAC4
			private bool CompleteAdd(IAsyncResult result)
			{
				MessageAttemptInfo attemptInfo = default(MessageAttemptInfo);
				this.validAdd = true;
				try
				{
					if (this.isLast)
					{
						attemptInfo = this.connection.strategy.EndAddLast(result);
					}
					else if (!this.connection.strategy.EndAdd(result, out attemptInfo))
					{
						this.validAdd = false;
						return true;
					}
				}
				catch (TimeoutException)
				{
					if (this.isLast)
					{
						this.connection.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.connection.id, SR.GetString("SequenceTerminatedAddLastToWindowTimedOut"), null));
					}
					throw;
				}
				catch (Exception exception)
				{
					if (!Fx.IsFatal(exception))
					{
						this.connection.RaiseFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.connection.id, SR.GetString("SequenceTerminatedUnknownAddToWindowError"), null));
					}
					throw;
				}
				if (this.connection.sendGuard.Enter())
				{
					bool flag = true;
					try
					{
						result = this.connection.beginSendHandler(attemptInfo, this.timeoutHelper.RemainingTime(), false, ReliableOutputConnection.AddAsyncResult.sendCompleteStatic, this);
						flag = false;
						goto IL_126;
					}
					catch (QuotaExceededException)
					{
						this.connection.RaiseFault(null, SequenceTerminatedFault.CreateQuotaExceededFault(this.connection.id));
						throw;
					}
					finally
					{
						if (flag)
						{
							this.connection.sendGuard.Exit();
						}
					}
					return true;
					IL_126:
					if (result.CompletedSynchronously)
					{
						this.CompleteSend(result);
						return true;
					}
					return false;
				}
				return true;
			}

			// Token: 0x0600801E RID: 32798 RVA: 0x001DCA40 File Offset: 0x001DAC40
			private void CompleteSend(IAsyncResult result)
			{
				try
				{
					this.connection.endSendHandler(result);
				}
				catch (QuotaExceededException)
				{
					this.connection.RaiseFault(null, SequenceTerminatedFault.CreateQuotaExceededFault(this.connection.id));
					throw;
				}
				finally
				{
					this.connection.sendGuard.Exit();
				}
			}

			// Token: 0x0600801F RID: 32799 RVA: 0x001DCAAC File Offset: 0x001DACAC
			private static void SendComplete(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					ReliableOutputConnection.AddAsyncResult addAsyncResult = (ReliableOutputConnection.AddAsyncResult)result.AsyncState;
					Exception exception = null;
					try
					{
						addAsyncResult.CompleteSend(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
					}
					addAsyncResult.Complete(false, exception);
				}
			}

			// Token: 0x06008020 RID: 32800 RVA: 0x001DCB00 File Offset: 0x001DAD00
			public static bool End(IAsyncResult result)
			{
				AsyncResult.End<ReliableOutputConnection.AddAsyncResult>(result);
				return ((ReliableOutputConnection.AddAsyncResult)result).validAdd;
			}

			// Token: 0x04004938 RID: 18744
			private static AsyncCallback addCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableOutputConnection.AddAsyncResult.AddComplete));

			// Token: 0x04004939 RID: 18745
			private ReliableOutputConnection connection;

			// Token: 0x0400493A RID: 18746
			private bool isLast;

			// Token: 0x0400493B RID: 18747
			private static AsyncCallback sendCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableOutputConnection.AddAsyncResult.SendComplete));

			// Token: 0x0400493C RID: 18748
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400493D RID: 18749
			private bool validAdd;
		}

		// Token: 0x02000DCF RID: 3535
		private class AlreadyCompletedTransferAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06008022 RID: 32802 RVA: 0x001DCB42 File Offset: 0x001DAD42
			public AlreadyCompletedTransferAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}

			// Token: 0x06008023 RID: 32803 RVA: 0x001DCB4C File Offset: 0x001DAD4C
			public void End()
			{
				AsyncResult.End<ReliableOutputConnection.AlreadyCompletedTransferAsyncResult>(this);
			}
		}
	}
}
