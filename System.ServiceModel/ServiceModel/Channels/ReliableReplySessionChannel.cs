using System;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000955 RID: 2389
	internal sealed class ReliableReplySessionChannel : ReplyChannel, IReplySessionChannel, IReplyChannel, IChannel, ICommunicationObject, ISessionChannel<IInputSession>
	{
		// Token: 0x06005C19 RID: 23577 RVA: 0x00151E74 File Offset: 0x00150074
		public ReliableReplySessionChannel(ReliableChannelListenerBase<IReplySessionChannel> listener, IServerReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID, UniqueId outputID) : base(listener, binder.LocalAddress)
		{
			this.listener = listener;
			this.connection = new ReliableInputConnection();
			this.connection.ReliableMessagingVersion = this.listener.ReliableMessagingVersion;
			this.binder = binder;
			this.session = new ServerReliableSession(this, listener, binder, faultHelper, inputID, outputID);
			this.session.UnblockChannelCloseCallback = new ChannelReliableSession.UnblockChannelCloseHandler(this.UnblockClose);
			if (this.listener.Ordered)
			{
				this.deliveryStrategy = new OrderedDeliveryStrategy<RequestContext>(this, this.listener.MaxTransferWindowSize, true);
			}
			else
			{
				this.deliveryStrategy = new UnorderedDeliveryStrategy<RequestContext>(this, this.listener.MaxTransferWindowSize);
			}
			this.binder.Faulted += this.OnBinderFaulted;
			this.binder.OnException += this.OnBinderException;
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.messagingCompleteWaitObject = new InterruptibleWaitObject(false);
			}
			this.session.Open(TimeSpan.Zero);
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				this.perfCounterId = this.listener.Uri.ToString().ToUpperInvariant();
			}
			if (binder.HasSession)
			{
				try
				{
					this.StartReceiving(false);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					this.session.OnUnknownException(ex);
				}
			}
		}

		// Token: 0x1700161B RID: 5659
		// (get) Token: 0x06005C1A RID: 23578 RVA: 0x00152010 File Offset: 0x00150210
		public IServerReliableChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x1700161C RID: 5660
		// (get) Token: 0x06005C1B RID: 23579 RVA: 0x00152018 File Offset: 0x00150218
		private bool IsMessagingCompleted
		{
			get
			{
				object thisLock = base.ThisLock;
				bool result;
				lock (thisLock)
				{
					result = (this.connection.AllAdded && this.requestsByRequestSequenceNumber.Count == 0 && this.lastReplyAcked);
				}
				return result;
			}
		}

		// Token: 0x1700161D RID: 5661
		// (get) Token: 0x06005C1C RID: 23580 RVA: 0x00152078 File Offset: 0x00150278
		private MessageVersion MessageVersion
		{
			get
			{
				return this.listener.MessageVersion;
			}
		}

		// Token: 0x1700161E RID: 5662
		// (get) Token: 0x06005C1D RID: 23581 RVA: 0x00152088 File Offset: 0x00150288
		private int PendingRequestContexts
		{
			get
			{
				object thisLock = base.ThisLock;
				int result;
				lock (thisLock)
				{
					result = this.requestsByRequestSequenceNumber.Count - this.requestsByReplySequenceNumber.Count;
				}
				return result;
			}
		}

		// Token: 0x1700161F RID: 5663
		// (get) Token: 0x06005C1E RID: 23582 RVA: 0x001520DC File Offset: 0x001502DC
		public IInputSession Session
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x06005C1F RID: 23583 RVA: 0x001520E4 File Offset: 0x001502E4
		private void AbortContexts()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.contextAborted)
				{
					return;
				}
				this.contextAborted = true;
			}
			Dictionary<long, ReliableReplySessionChannel.ReliableRequestContext>.ValueCollection values = this.requestsByRequestSequenceNumber.Values;
			foreach (ReliableReplySessionChannel.ReliableRequestContext reliableRequestContext in values)
			{
				reliableRequestContext.Abort();
			}
			this.requestsByRequestSequenceNumber.Clear();
			this.requestsByReplySequenceNumber.Clear();
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && this.lastReply != null)
			{
				this.lastReply.Abort();
			}
		}

		// Token: 0x06005C20 RID: 23584 RVA: 0x001521B8 File Offset: 0x001503B8
		private void AddAcknowledgementHeader(Message message)
		{
			WsrmUtilities.AddAcknowledgementHeader(this.listener.ReliableMessagingVersion, message, this.session.InputID, this.connection.Ranges, this.connection.IsLastKnown, this.listener.MaxTransferWindowSize - this.deliveryStrategy.EnqueuedCount);
		}

		// Token: 0x06005C21 RID: 23585 RVA: 0x00152210 File Offset: 0x00150410
		private static void AsyncReceiveCompleteStatic(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			ReliableReplySessionChannel reliableReplySessionChannel = (ReliableReplySessionChannel)asyncResult.AsyncState;
			try
			{
				if (reliableReplySessionChannel.HandleReceiveComplete(asyncResult))
				{
					reliableReplySessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableReplySessionChannel.session.OnUnknownException(ex);
			}
		}

		// Token: 0x06005C22 RID: 23586 RVA: 0x0015226C File Offset: 0x0015046C
		private IAsyncResult BeginCloseBinder(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.binder.BeginClose(timeout, MaskingMode.Handled, callback, state);
		}

		// Token: 0x06005C23 RID: 23587 RVA: 0x00152280 File Offset: 0x00150480
		private IAsyncResult BeginCloseOutput(TimeSpan timeout, AsyncCallback callback, object state)
		{
			if (this.listener.ReliableMessagingVersion != ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfClosed();
					this.CreateCloseSequenceReplyHelper();
				}
				return this.closeSequenceReplyHelper.BeginWaitAndReply(timeout, callback, state);
			}
			ReliableReplySessionChannel.ReliableRequestContext reliableRequestContext = this.lastReply;
			if (reliableRequestContext == null)
			{
				return new ReliableReplySessionChannel.CloseOutputCompletedAsyncResult(callback, state);
			}
			return reliableRequestContext.BeginReplyInternal(null, timeout, callback, state);
		}

		// Token: 0x06005C24 RID: 23588 RVA: 0x00152304 File Offset: 0x00150504
		private IAsyncResult BeginUnregisterChannel(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.listener.OnReliableChannelBeginClose(this.session.InputID, this.session.OutputID, timeout, callback, state);
		}

		// Token: 0x06005C25 RID: 23589 RVA: 0x0015232C File Offset: 0x0015052C
		private Message CreateAcknowledgement(SequenceRangeCollection ranges)
		{
			return WsrmUtilities.CreateAcknowledgmentMessage(this.MessageVersion, this.listener.ReliableMessagingVersion, this.session.InputID, ranges, this.connection.IsLastKnown, this.listener.MaxTransferWindowSize - this.deliveryStrategy.EnqueuedCount);
		}

		// Token: 0x06005C26 RID: 23590 RVA: 0x00152380 File Offset: 0x00150580
		private Message CreateSequenceClosedFault()
		{
			Message message = new SequenceClosedFault(this.session.InputID).CreateMessage(this.listener.MessageVersion, this.listener.ReliableMessagingVersion);
			this.AddAcknowledgementHeader(message);
			return message;
		}

		// Token: 0x06005C27 RID: 23591 RVA: 0x001523C1 File Offset: 0x001505C1
		private bool CreateCloseSequenceReplyHelper()
		{
			if (base.State == CommunicationState.Faulted || base.Aborted)
			{
				return false;
			}
			if (this.closeSequenceReplyHelper == null)
			{
				this.closeSequenceReplyHelper = new ReliableReplySessionChannel.ReplyHelper(this, ReliableReplySessionChannel.CloseSequenceReplyProvider.Instance, true);
			}
			return true;
		}

		// Token: 0x06005C28 RID: 23592 RVA: 0x001523F1 File Offset: 0x001505F1
		private bool CreateTerminateSequenceReplyHelper()
		{
			if (base.State == CommunicationState.Faulted || base.Aborted)
			{
				return false;
			}
			if (this.terminateSequenceReplyHelper == null)
			{
				this.terminateSequenceReplyHelper = new ReliableReplySessionChannel.ReplyHelper(this, ReliableReplySessionChannel.TerminateSequenceReplyProvider.Instance, false);
			}
			return true;
		}

		// Token: 0x06005C29 RID: 23593 RVA: 0x00152424 File Offset: 0x00150624
		private void CloseOutput(TimeSpan timeout)
		{
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				ReliableReplySessionChannel.ReliableRequestContext reliableRequestContext = this.lastReply;
				if (reliableRequestContext != null)
				{
					reliableRequestContext.ReplyInternal(null, timeout);
					return;
				}
			}
			else
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfClosed();
					this.CreateCloseSequenceReplyHelper();
				}
				this.closeSequenceReplyHelper.WaitAndReply(timeout);
			}
		}

		// Token: 0x06005C2A RID: 23594 RVA: 0x0015249C File Offset: 0x0015069C
		private bool ContainsRequest(long requestSeqNum)
		{
			object thisLock = base.ThisLock;
			bool result;
			lock (thisLock)
			{
				bool flag2 = this.requestsByRequestSequenceNumber.ContainsKey(requestSeqNum);
				if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
				{
					result = (flag2 || (this.lastReply != null && this.lastReply.RequestSequenceNumber == requestSeqNum && !this.lastReplyAcked));
				}
				else
				{
					result = flag2;
				}
			}
			return result;
		}

		// Token: 0x06005C2B RID: 23595 RVA: 0x00152524 File Offset: 0x00150724
		private void EndCloseBinder(IAsyncResult result)
		{
			this.binder.EndClose(result);
		}

		// Token: 0x06005C2C RID: 23596 RVA: 0x00152532 File Offset: 0x00150732
		private void EndCloseOutput(IAsyncResult result)
		{
			if (this.listener.ReliableMessagingVersion != ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.closeSequenceReplyHelper.EndWaitAndReply(result);
				return;
			}
			if (result is ReliableReplySessionChannel.CloseOutputCompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			this.lastReply.EndReplyInternal(result);
		}

		// Token: 0x06005C2D RID: 23597 RVA: 0x0015256E File Offset: 0x0015076E
		private void EndUnregisterChannel(IAsyncResult result)
		{
			this.listener.OnReliableChannelEndClose(result);
		}

		// Token: 0x06005C2E RID: 23598 RVA: 0x0015257C File Offset: 0x0015077C
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IReplySessionChannel))
			{
				return (T)((object)this);
			}
			T property = base.GetProperty<T>();
			if (property != null)
			{
				return property;
			}
			T property2 = this.binder.Channel.GetProperty<T>();
			if (property2 == null && typeof(T) == typeof(FaultConverter))
			{
				return (T)((object)FaultConverter.GetDefaultFaultConverter(this.listener.MessageVersion));
			}
			return property2;
		}

		// Token: 0x06005C2F RID: 23599 RVA: 0x00152608 File Offset: 0x00150808
		private bool HandleReceiveComplete(IAsyncResult result)
		{
			RequestContext requestContext;
			if (!this.Binder.EndTryReceive(result, out requestContext))
			{
				return true;
			}
			if (requestContext == null)
			{
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					flag = this.connection.Terminate();
				}
				if (!flag && this.Binder.State == CommunicationState.Opened)
				{
					Exception e = new CommunicationException(SR.GetString("EarlySecurityClose"));
					this.session.OnLocalFault(e, null, null);
				}
				return false;
			}
			WsrmMessageInfo info = WsrmMessageInfo.Get(this.listener.MessageVersion, this.listener.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), requestContext.RequestMessage);
			this.StartReceiving(false);
			this.ProcessRequest(requestContext, info);
			return false;
		}

		// Token: 0x06005C30 RID: 23600 RVA: 0x001526E8 File Offset: 0x001508E8
		protected override void OnAbort()
		{
			if (this.closeSequenceReplyHelper != null)
			{
				this.closeSequenceReplyHelper.Abort();
			}
			this.connection.Abort(this);
			if (this.terminateSequenceReplyHelper != null)
			{
				this.terminateSequenceReplyHelper.Abort();
			}
			this.session.Abort();
			this.AbortContexts();
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.messagingCompleteWaitObject.Abort(this);
			}
			this.listener.OnReliableChannelAbort(this.session.InputID, this.session.OutputID);
			base.OnAbort();
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x00152780 File Offset: 0x00150980
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfCloseInvalid();
			bool flag = this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.BeginCloseOutput),
				flag ? new OperationWithTimeoutBeginCallback(this.connection.BeginClose) : new OperationWithTimeoutBeginCallback(this.BeginTerminateSequence),
				flag ? new OperationWithTimeoutBeginCallback(this.messagingCompleteWaitObject.BeginWait) : new OperationWithTimeoutBeginCallback(this.connection.BeginClose),
				new OperationWithTimeoutBeginCallback(this.session.BeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginCloseBinder),
				new OperationWithTimeoutBeginCallback(this.BeginUnregisterChannel),
				new OperationWithTimeoutBeginCallback(base.OnBeginClose)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.EndCloseOutput),
				flag ? new OperationEndCallback(this.connection.EndClose) : new OperationEndCallback(this.EndTerminateSequence),
				flag ? new OperationEndCallback(this.messagingCompleteWaitObject.EndWait) : new OperationEndCallback(this.connection.EndClose),
				new OperationEndCallback(this.session.EndClose),
				new OperationEndCallback(this.EndCloseBinder),
				new OperationEndCallback(this.EndUnregisterChannel),
				new OperationEndCallback(base.OnEndClose)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x06005C32 RID: 23602 RVA: 0x001528FE File Offset: 0x00150AFE
		private void OnBinderException(IReliableChannelBinder sender, Exception exception)
		{
			if (exception is QuotaExceededException)
			{
				this.session.OnLocalFault(exception, null, null);
				return;
			}
			base.EnqueueAndDispatch(exception, null, false);
		}

		// Token: 0x06005C33 RID: 23603 RVA: 0x00152920 File Offset: 0x00150B20
		private void OnBinderFaulted(IReliableChannelBinder sender, Exception exception)
		{
			this.binder.Abort();
			exception = new CommunicationException(SR.GetString("EarlySecurityFaulted"), exception);
			this.session.OnLocalFault(exception, null, null);
		}

		// Token: 0x06005C34 RID: 23604 RVA: 0x00152950 File Offset: 0x00150B50
		protected override void OnClose(TimeSpan timeout)
		{
			this.ThrowIfCloseInvalid();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseOutput(timeoutHelper.RemainingTime());
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.connection.Close(timeoutHelper.RemainingTime());
				this.messagingCompleteWaitObject.Wait(timeoutHelper.RemainingTime());
			}
			else
			{
				this.TerminateSequence(timeoutHelper.RemainingTime());
				this.connection.Close(timeoutHelper.RemainingTime());
			}
			this.session.Close(timeoutHelper.RemainingTime());
			this.binder.Close(timeoutHelper.RemainingTime(), MaskingMode.Handled);
			this.listener.OnReliableChannelClose(this.session.InputID, this.session.OutputID, timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06005C35 RID: 23605 RVA: 0x00152A2C File Offset: 0x00150C2C
		protected override void OnClosed()
		{
			this.deliveryStrategy.Dispose();
			this.binder.Faulted -= this.OnBinderFaulted;
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && this.lastReply != null)
			{
				this.lastReply.Abort();
			}
			base.OnClosed();
		}

		// Token: 0x06005C36 RID: 23606 RVA: 0x00152A86 File Offset: 0x00150C86
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x06005C37 RID: 23607 RVA: 0x00152A8E File Offset: 0x00150C8E
		protected override void OnFaulted()
		{
			this.session.OnFaulted();
			this.UnblockClose();
			base.OnFaulted();
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.SessionFaulted(this.perfCounterId);
			}
		}

		// Token: 0x06005C38 RID: 23608 RVA: 0x00152ABC File Offset: 0x00150CBC
		private static void OnReceiveCompletedStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReliableReplySessionChannel reliableReplySessionChannel = (ReliableReplySessionChannel)result.AsyncState;
			try
			{
				if (reliableReplySessionChannel.HandleReceiveComplete(result))
				{
					reliableReplySessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableReplySessionChannel.session.OnUnknownException(ex);
			}
		}

		// Token: 0x06005C39 RID: 23609 RVA: 0x00152B1C File Offset: 0x00150D1C
		private void OnTerminateSequenceCompleted()
		{
			if (this.session.Settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.connection.IsSequenceClosed)
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					this.connection.Terminate();
				}
			}
		}

		// Token: 0x06005C3A RID: 23610 RVA: 0x00152B88 File Offset: 0x00150D88
		private bool PrepareReply(ReliableReplySessionChannel.ReliableRequestContext context)
		{
			object thisLock = base.ThisLock;
			bool result;
			lock (thisLock)
			{
				if (base.Aborted || base.State == CommunicationState.Faulted || base.State == CommunicationState.Closed)
				{
					result = false;
				}
				else
				{
					long requestSequenceNumber = context.RequestSequenceNumber;
					bool flag2 = this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
					if (flag2 && this.connection.Last == requestSequenceNumber)
					{
						if (this.lastReply == null)
						{
							this.lastReply = context;
						}
						this.requestsByRequestSequenceNumber.Remove(requestSequenceNumber);
						if (!this.connection.AllAdded || base.State != CommunicationState.Closing)
						{
							return false;
						}
					}
					else
					{
						if (base.State == CommunicationState.Closing)
						{
							return false;
						}
						if (!context.HasReply)
						{
							this.requestsByRequestSequenceNumber.Remove(requestSequenceNumber);
							return true;
						}
					}
					if (this.nextReplySequenceNumber == 9223372036854775807L)
					{
						MessageNumberRolloverFault messageNumberRolloverFault = new MessageNumberRolloverFault(this.session.OutputID);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(messageNumberRolloverFault.CreateException());
					}
					long replySequenceNumber = this.nextReplySequenceNumber + 1L;
					this.nextReplySequenceNumber = replySequenceNumber;
					context.SetReplySequenceNumber(replySequenceNumber);
					if (flag2 && this.connection.Last == requestSequenceNumber)
					{
						if (!context.HasReply)
						{
							this.lastReplyAcked = true;
						}
						this.lastReplySequenceNumber = this.nextReplySequenceNumber;
						context.SetLastReply(this.lastReplySequenceNumber);
					}
					else if (context.HasReply)
					{
						this.requestsByReplySequenceNumber.Add(this.nextReplySequenceNumber, context);
					}
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06005C3B RID: 23611 RVA: 0x00152D38 File Offset: 0x00150F38
		private Message PrepareReplyMessage(long replySequenceNumber, bool isLast, SequenceRangeCollection ranges, Message reply)
		{
			this.AddAcknowledgementHeader(reply);
			WsrmUtilities.AddSequenceHeader(this.listener.ReliableMessagingVersion, reply, this.session.OutputID, replySequenceNumber, isLast);
			return reply;
		}

		// Token: 0x06005C3C RID: 23612 RVA: 0x00152D64 File Offset: 0x00150F64
		private void ProcessAcknowledgment(WsrmAcknowledgmentInfo info)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (!base.Aborted && base.State != CommunicationState.Faulted && base.State != CommunicationState.Closed)
				{
					if (this.requestsByReplySequenceNumber.Count > 0)
					{
						this.acked.Clear();
						foreach (KeyValuePair<long, ReliableReplySessionChannel.ReliableRequestContext> keyValuePair in this.requestsByReplySequenceNumber)
						{
							long num = keyValuePair.Key;
							if (info.Ranges.Contains(num))
							{
								this.acked.Add(num);
							}
						}
						for (int i = 0; i < this.acked.Count; i++)
						{
							long num = this.acked[i];
							this.requestsByRequestSequenceNumber.Remove(this.requestsByReplySequenceNumber[num].RequestSequenceNumber);
							this.requestsByReplySequenceNumber.Remove(num);
						}
						if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && !this.lastReplyAcked && this.lastReplySequenceNumber != -9223372036854775808L)
						{
							this.lastReplyAcked = info.Ranges.Contains(this.lastReplySequenceNumber);
						}
					}
				}
			}
		}

		// Token: 0x06005C3D RID: 23613 RVA: 0x00152EE4 File Offset: 0x001510E4
		private void ProcessAckRequested(RequestContext context)
		{
			try
			{
				using (Message message = this.CreateAcknowledgement(this.connection.Ranges))
				{
					context.Reply(message);
				}
			}
			finally
			{
				context.RequestMessage.Close();
				context.Close();
			}
		}

		// Token: 0x06005C3E RID: 23614 RVA: 0x00152F44 File Offset: 0x00151144
		private void ProcessShutdown11(RequestContext context, WsrmMessageInfo info)
		{
			bool flag = true;
			try
			{
				bool flag2 = info.TerminateSequenceInfo != null;
				WsrmRequestInfo info2 = flag2 ? info.TerminateSequenceInfo : info.CloseSequenceInfo;
				long num = flag2 ? info.TerminateSequenceInfo.LastMsgNumber : info.CloseSequenceInfo.LastMsgNumber;
				if (!WsrmUtilities.ValidateWsrmRequest(this.session, info2, this.binder, context))
				{
					flag = false;
				}
				else
				{
					bool flag3 = false;
					Exception ex = null;
					ReliableReplySessionChannel.ReplyHelper replyHelper = null;
					bool flag4 = true;
					bool flag5 = true;
					bool flag6 = true;
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (!this.connection.IsLastKnown)
						{
							if (this.requestsByRequestSequenceNumber.Count == 0)
							{
								if (flag2)
								{
									if (this.connection.SetTerminateSequenceLast(num, out flag5))
									{
										flag3 = true;
									}
									else if (flag5)
									{
										ex = new ProtocolException(SR.GetString("EarlyTerminateSequence"));
									}
								}
								else
								{
									flag3 = this.connection.SetCloseSequenceLast(num);
									flag5 = flag3;
								}
								if (flag3)
								{
									if (!this.CreateCloseSequenceReplyHelper())
									{
										return;
									}
									if (flag2)
									{
										replyHelper = this.closeSequenceReplyHelper;
									}
									this.session.SetFinalAck(this.connection.Ranges);
									this.deliveryStrategy.Dispose();
								}
							}
							else
							{
								flag4 = false;
							}
						}
						else
						{
							flag6 = (num == this.connection.Last);
						}
					}
					WsrmFault wsrmFault = null;
					if (!flag5)
					{
						string @string = SR.GetString("SequenceTerminatedSmallLastMsgNumber");
						string string2 = SR.GetString("SmallLastMsgNumberExceptionString");
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, @string, string2);
					}
					else if (!flag4)
					{
						string string3 = SR.GetString("SequenceTerminatedNotAllRepliesAcknowledged");
						string string4 = SR.GetString("NotAllRepliesAcknowledgedExceptionString");
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.OutputID, string3, string4);
					}
					else if (!flag6)
					{
						string string5 = SR.GetString("SequenceTerminatedInconsistentLastMsgNumber");
						string string6 = SR.GetString("InconsistentLastMsgNumberExceptionString");
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, string5, string6);
					}
					else if (ex != null)
					{
						Message message = WsrmUtilities.CreateTerminateMessage(this.MessageVersion, this.listener.ReliableMessagingVersion, this.session.OutputID);
						this.AddAcknowledgementHeader(message);
						using (message)
						{
							context.Reply(message);
						}
						this.session.OnRemoteFault(ex);
						return;
					}
					if (wsrmFault != null)
					{
						this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
						flag = false;
					}
					else
					{
						if (flag2)
						{
							if (replyHelper != null)
							{
								replyHelper.UnblockWaiter();
							}
							object thisLock2 = base.ThisLock;
							lock (thisLock2)
							{
								if (!this.CreateTerminateSequenceReplyHelper())
								{
									return;
								}
							}
						}
						ReliableReplySessionChannel.ReplyHelper replyHelper2 = flag2 ? this.terminateSequenceReplyHelper : this.closeSequenceReplyHelper;
						if (!replyHelper2.TransferRequestContext(context, info))
						{
							replyHelper2.Reply(context, info, base.DefaultSendTimeout, MaskingMode.All);
							if (flag2)
							{
								this.OnTerminateSequenceCompleted();
							}
						}
						else
						{
							flag = false;
						}
						if (flag3)
						{
							ActionItem.Schedule(new Action<object>(this.ShutdownCallback), null);
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					context.RequestMessage.Close();
					context.Close();
				}
			}
		}

		// Token: 0x06005C3F RID: 23615 RVA: 0x001532B8 File Offset: 0x001514B8
		public void ProcessDemuxedRequest(RequestContext context, WsrmMessageInfo info)
		{
			try
			{
				this.ProcessRequest(context, info);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				this.session.OnUnknownException(ex);
			}
		}

		// Token: 0x06005C40 RID: 23616 RVA: 0x001532F8 File Offset: 0x001514F8
		private void ProcessRequest(RequestContext context, WsrmMessageInfo info)
		{
			bool flag = true;
			bool flag2 = true;
			try
			{
				if (!this.session.ProcessInfo(info, context))
				{
					flag = false;
					flag2 = false;
				}
				else if (!this.session.VerifyDuplexProtocolElements(info, context))
				{
					flag = false;
					flag2 = false;
				}
				else
				{
					this.session.OnRemoteActivity(false);
					if (info.CreateSequenceInfo != null)
					{
						EndpointAddress acceptAcksTo;
						if (WsrmUtilities.ValidateCreateSequence<IReplySessionChannel>(info, this.listener, this.binder.Channel, out acceptAcksTo))
						{
							Message message = WsrmUtilities.CreateCreateSequenceResponse(this.listener.MessageVersion, this.listener.ReliableMessagingVersion, true, info.CreateSequenceInfo, this.listener.Ordered, this.session.InputID, acceptAcksTo);
							try
							{
								using (message)
								{
									if (this.Binder.AddressResponse(info.Message, message))
									{
										context.Reply(message, base.DefaultSendTimeout);
									}
									goto IL_F9;
								}
							}
							finally
							{
								if (context != null)
								{
									((IDisposable)context).Dispose();
								}
							}
						}
						this.session.OnLocalFault(info.FaultException, info.FaultReply, context);
						IL_F9:
						flag2 = false;
					}
					else
					{
						flag2 = false;
						if (info.AcknowledgementInfo != null)
						{
							this.ProcessAcknowledgment(info.AcknowledgementInfo);
							flag2 = (info.Action == WsrmIndex.GetSequenceAcknowledgementActionString(this.listener.ReliableMessagingVersion));
						}
						if (!flag2)
						{
							flag = false;
							if (info.SequencedMessageInfo != null)
							{
								this.ProcessSequencedMessage(context, info.Action, info.SequencedMessageInfo);
							}
							else if (info.TerminateSequenceInfo != null)
							{
								if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
								{
									this.ProcessTerminateSequenceFeb2005(context, info);
								}
								else
								{
									if (!(info.TerminateSequenceInfo.Identifier == this.session.InputID))
									{
										WsrmFault wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedUnsupportedTerminateSequence"), SR.GetString("UnsupportedTerminateSequenceExceptionString"));
										this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
										flag = false;
										flag2 = false;
										return;
									}
									this.ProcessShutdown11(context, info);
								}
							}
							else if (info.CloseSequenceInfo != null)
							{
								this.ProcessShutdown11(context, info);
							}
							else if (info.AckRequestedInfo != null)
							{
								this.ProcessAckRequested(context);
							}
						}
						if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && this.IsMessagingCompleted)
						{
							this.messagingCompleteWaitObject.Set();
						}
					}
				}
			}
			finally
			{
				if (flag)
				{
					info.Message.Close();
				}
				if (flag2)
				{
					context.Close();
				}
			}
		}

		// Token: 0x06005C41 RID: 23617 RVA: 0x001535A0 File Offset: 0x001517A0
		private void ProcessSequencedMessage(RequestContext context, string action, WsrmSequencedMessageInfo info)
		{
			ReliableReplySessionChannel.ReliableRequestContext reliableRequestContext = null;
			WsrmFault wsrmFault = null;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
			bool flag4 = this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			long sequenceNumber = info.SequenceNumber;
			bool isLast = flag3 && info.LastMessage;
			bool flag5 = flag3 && action == "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage";
			Message message = null;
			object thisLock = base.ThisLock;
			bool flag7;
			lock (thisLock)
			{
				if (base.Aborted || base.State == CommunicationState.Faulted || base.State == CommunicationState.Closed)
				{
					context.RequestMessage.Close();
					context.Abort();
					return;
				}
				flag7 = this.connection.Ranges.Contains(sequenceNumber);
				if (!this.connection.IsValid(sequenceNumber, isLast))
				{
					if (flag3)
					{
						wsrmFault = new LastMessageNumberExceededFault(this.session.InputID);
					}
					else
					{
						message = this.CreateSequenceClosedFault();
						if (PerformanceCounters.PerformanceCountersEnabled)
						{
							PerformanceCounters.MessageDropped(this.perfCounterId);
						}
					}
				}
				else if (flag7)
				{
					if (PerformanceCounters.PerformanceCountersEnabled)
					{
						PerformanceCounters.MessageDropped(this.perfCounterId);
					}
					if (!this.requestsByRequestSequenceNumber.TryGetValue(info.SequenceNumber, out reliableRequestContext))
					{
						if (this.lastReply != null && this.lastReply.RequestSequenceNumber == info.SequenceNumber)
						{
							reliableRequestContext = this.lastReply;
						}
						else
						{
							reliableRequestContext = new ReliableReplySessionChannel.ReliableRequestContext(context, info.SequenceNumber, this, true);
						}
					}
					reliableRequestContext.SetAckRanges(this.connection.Ranges);
				}
				else if (base.State == CommunicationState.Closing && !flag5)
				{
					if (flag3)
					{
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedSessionClosedBeforeDone"), SR.GetString("SessionClosedBeforeDone"));
					}
					else
					{
						message = this.CreateSequenceClosedFault();
						if (PerformanceCounters.PerformanceCountersEnabled)
						{
							PerformanceCounters.MessageDropped(this.perfCounterId);
						}
					}
				}
				else if (this.deliveryStrategy.CanEnqueue(sequenceNumber) && this.requestsByReplySequenceNumber.Count < this.listener.MaxTransferWindowSize && (this.listener.Ordered || this.connection.CanMerge(sequenceNumber)))
				{
					this.connection.Merge(sequenceNumber, isLast);
					reliableRequestContext = new ReliableReplySessionChannel.ReliableRequestContext(context, info.SequenceNumber, this, false);
					reliableRequestContext.SetAckRanges(this.connection.Ranges);
					if (!flag5)
					{
						flag = this.deliveryStrategy.Enqueue(reliableRequestContext, sequenceNumber);
						this.requestsByRequestSequenceNumber.Add(info.SequenceNumber, reliableRequestContext);
					}
					else
					{
						this.lastReply = reliableRequestContext;
					}
					flag2 = this.connection.AllAdded;
				}
				else if (PerformanceCounters.PerformanceCountersEnabled)
				{
					PerformanceCounters.MessageDropped(this.perfCounterId);
				}
			}
			if (wsrmFault != null)
			{
				this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
				return;
			}
			if (reliableRequestContext == null)
			{
				if (message != null)
				{
					using (message)
					{
						context.Reply(message);
					}
				}
				context.RequestMessage.Close();
				context.Close();
				return;
			}
			if (flag7 && reliableRequestContext.CheckForReplyOrAddInnerContext(context))
			{
				reliableRequestContext.SendReply(context, MaskingMode.All);
				return;
			}
			if (!flag7 && flag5)
			{
				reliableRequestContext.Close();
			}
			if (flag)
			{
				base.Dispatch();
			}
			if (flag2)
			{
				ActionItem.Schedule(new Action<object>(this.ShutdownCallback), null);
			}
		}

		// Token: 0x06005C42 RID: 23618 RVA: 0x00153918 File Offset: 0x00151B18
		private void ProcessTerminateSequenceFeb2005(RequestContext context, WsrmMessageInfo info)
		{
			bool flag = true;
			try
			{
				object thisLock = base.ThisLock;
				bool flag3;
				bool flag4;
				lock (thisLock)
				{
					flag3 = !this.connection.Terminate();
					flag4 = (this.requestsByRequestSequenceNumber.Count == 0);
				}
				WsrmFault wsrmFault = null;
				if (flag3)
				{
					wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedEarlyTerminateSequence"), SR.GetString("EarlyTerminateSequence"));
				}
				else if (!flag4)
				{
					wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedBeforeReplySequenceAcked"), SR.GetString("EarlyRequestTerminateSequence"));
				}
				if (wsrmFault != null)
				{
					this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
					flag = false;
				}
				else
				{
					Message message = WsrmUtilities.CreateTerminateMessage(this.MessageVersion, this.listener.ReliableMessagingVersion, this.session.OutputID);
					this.AddAcknowledgementHeader(message);
					using (message)
					{
						context.Reply(message);
					}
				}
			}
			finally
			{
				if (flag)
				{
					context.RequestMessage.Close();
					context.Close();
				}
			}
		}

		// Token: 0x06005C43 RID: 23619 RVA: 0x00153A5C File Offset: 0x00151C5C
		private void StartReceiving(bool canBlock)
		{
			IAsyncResult asyncResult;
			for (;;)
			{
				asyncResult = this.binder.BeginTryReceive(TimeSpan.MaxValue, ReliableReplySessionChannel.onReceiveCompleted, this);
				if (!asyncResult.CompletedSynchronously)
				{
					break;
				}
				if (!canBlock)
				{
					goto Block_1;
				}
				if (!this.HandleReceiveComplete(asyncResult))
				{
					return;
				}
			}
			return;
			Block_1:
			ActionItem.Schedule(ReliableReplySessionChannel.asyncReceiveComplete, asyncResult);
		}

		// Token: 0x06005C44 RID: 23620 RVA: 0x00153AA1 File Offset: 0x00151CA1
		private void ShutdownCallback(object state)
		{
			base.Shutdown();
		}

		// Token: 0x06005C45 RID: 23621 RVA: 0x00153AAC File Offset: 0x00151CAC
		private void TerminateSequence(TimeSpan timeout)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.CreateTerminateSequenceReplyHelper();
			}
			this.terminateSequenceReplyHelper.WaitAndReply(timeout);
			this.OnTerminateSequenceCompleted();
		}

		// Token: 0x06005C46 RID: 23622 RVA: 0x00153B08 File Offset: 0x00151D08
		private IAsyncResult BeginTerminateSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.CreateTerminateSequenceReplyHelper();
			}
			return this.terminateSequenceReplyHelper.BeginWaitAndReply(timeout, callback, state);
		}

		// Token: 0x06005C47 RID: 23623 RVA: 0x00153B60 File Offset: 0x00151D60
		private void EndTerminateSequence(IAsyncResult result)
		{
			this.terminateSequenceReplyHelper.EndWaitAndReply(result);
			this.OnTerminateSequenceCompleted();
		}

		// Token: 0x06005C48 RID: 23624 RVA: 0x00153B74 File Offset: 0x00151D74
		private void ThrowIfCloseInvalid()
		{
			bool flag = false;
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (this.PendingRequestContexts != 0 || this.connection.Ranges.Count > 1)
				{
					flag = true;
				}
			}
			else if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.PendingRequestContexts != 0)
			{
				flag = true;
			}
			if (flag)
			{
				WsrmFault wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedSessionClosedBeforeDone"), SR.GetString("SessionClosedBeforeDone"));
				this.session.OnLocalFault(null, wsrmFault, null);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(wsrmFault.CreateException());
			}
		}

		// Token: 0x06005C49 RID: 23625 RVA: 0x00153C18 File Offset: 0x00151E18
		private void UnblockClose()
		{
			this.AbortContexts();
			if (this.listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.messagingCompleteWaitObject.Fault(this);
			}
			else
			{
				if (this.closeSequenceReplyHelper != null)
				{
					this.closeSequenceReplyHelper.Fault();
				}
				if (this.terminateSequenceReplyHelper != null)
				{
					this.terminateSequenceReplyHelper.Fault();
				}
			}
			this.connection.Fault(this);
		}

		// Token: 0x0400371B RID: 14107
		private List<long> acked = new List<long>();

		// Token: 0x0400371C RID: 14108
		private static Action<object> asyncReceiveComplete = new Action<object>(ReliableReplySessionChannel.AsyncReceiveCompleteStatic);

		// Token: 0x0400371D RID: 14109
		private IServerReliableChannelBinder binder;

		// Token: 0x0400371E RID: 14110
		private ReliableReplySessionChannel.ReplyHelper closeSequenceReplyHelper;

		// Token: 0x0400371F RID: 14111
		private ReliableInputConnection connection;

		// Token: 0x04003720 RID: 14112
		private bool contextAborted;

		// Token: 0x04003721 RID: 14113
		private DeliveryStrategy<RequestContext> deliveryStrategy;

		// Token: 0x04003722 RID: 14114
		private ReliableReplySessionChannel.ReliableRequestContext lastReply;

		// Token: 0x04003723 RID: 14115
		private bool lastReplyAcked;

		// Token: 0x04003724 RID: 14116
		private long lastReplySequenceNumber = long.MinValue;

		// Token: 0x04003725 RID: 14117
		private ReliableChannelListenerBase<IReplySessionChannel> listener;

		// Token: 0x04003726 RID: 14118
		private InterruptibleWaitObject messagingCompleteWaitObject;

		// Token: 0x04003727 RID: 14119
		private long nextReplySequenceNumber;

		// Token: 0x04003728 RID: 14120
		private static AsyncCallback onReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(ReliableReplySessionChannel.OnReceiveCompletedStatic));

		// Token: 0x04003729 RID: 14121
		private string perfCounterId;

		// Token: 0x0400372A RID: 14122
		private Dictionary<long, ReliableReplySessionChannel.ReliableRequestContext> requestsByRequestSequenceNumber = new Dictionary<long, ReliableReplySessionChannel.ReliableRequestContext>();

		// Token: 0x0400372B RID: 14123
		private Dictionary<long, ReliableReplySessionChannel.ReliableRequestContext> requestsByReplySequenceNumber = new Dictionary<long, ReliableReplySessionChannel.ReliableRequestContext>();

		// Token: 0x0400372C RID: 14124
		private ServerReliableSession session;

		// Token: 0x0400372D RID: 14125
		private ReliableReplySessionChannel.ReplyHelper terminateSequenceReplyHelper;

		// Token: 0x02000DD0 RID: 3536
		private class CloseOutputCompletedAsyncResult : CompletedAsyncResult
		{
			// Token: 0x06008024 RID: 32804 RVA: 0x001DCB55 File Offset: 0x001DAD55
			public CloseOutputCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
			{
			}
		}

		// Token: 0x02000DD1 RID: 3537
		private class ReliableRequestContext : RequestContextBase
		{
			// Token: 0x06008025 RID: 32805 RVA: 0x001DCB60 File Offset: 0x001DAD60
			public ReliableRequestContext(RequestContext context, long requestSequenceNumber, ReliableReplySessionChannel channel, bool outcome) : base(context.RequestMessage, channel.DefaultCloseTimeout, channel.DefaultSendTimeout)
			{
				this.channel = channel;
				this.requestSequenceNumber = requestSequenceNumber;
				this.outcomeKnown = outcome;
				if (!outcome)
				{
					this.innerContexts.Add(context);
				}
			}

			// Token: 0x06008026 RID: 32806 RVA: 0x001DCBB8 File Offset: 0x001DADB8
			public bool CheckForReplyOrAddInnerContext(RequestContext innerContext)
			{
				object thisLock = base.ThisLock;
				bool result;
				lock (thisLock)
				{
					if (this.outcomeKnown)
					{
						result = true;
					}
					else
					{
						this.innerContexts.Add(innerContext);
						result = false;
					}
				}
				return result;
			}

			// Token: 0x17001C69 RID: 7273
			// (get) Token: 0x06008027 RID: 32807 RVA: 0x001DCC10 File Offset: 0x001DAE10
			public bool HasReply
			{
				get
				{
					return this.bufferedReply != null;
				}
			}

			// Token: 0x17001C6A RID: 7274
			// (get) Token: 0x06008028 RID: 32808 RVA: 0x001DCC1B File Offset: 0x001DAE1B
			public long RequestSequenceNumber
			{
				get
				{
					return this.requestSequenceNumber;
				}
			}

			// Token: 0x06008029 RID: 32809 RVA: 0x001DCC24 File Offset: 0x001DAE24
			private void AbortInnerContexts()
			{
				for (int i = 0; i < this.innerContexts.Count; i++)
				{
					this.innerContexts[i].Abort();
					this.innerContexts[i].RequestMessage.Close();
				}
				this.innerContexts.Clear();
			}

			// Token: 0x0600802A RID: 32810 RVA: 0x001DCC7C File Offset: 0x001DAE7C
			internal IAsyncResult BeginReplyInternal(Message reply, TimeSpan timeout, AsyncCallback callback, object state)
			{
				bool flag = true;
				bool flag2 = true;
				IAsyncResult result;
				try
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.ranges == null)
						{
							throw Fx.AssertAndThrow("this.ranges != null");
						}
						if (base.Aborted)
						{
							flag = false;
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("RequestContextAborted")));
						}
						if (this.outcomeKnown)
						{
							flag = false;
							flag2 = false;
						}
						else
						{
							if (reply != null && this.bufferedReply == null)
							{
								this.bufferedReply = reply.CreateBufferedCopy(int.MaxValue);
							}
							if (!this.channel.PrepareReply(this))
							{
								flag = false;
								flag2 = false;
							}
							else
							{
								this.outcomeKnown = true;
							}
						}
					}
					if (!flag2)
					{
						result = new ReliableReplySessionChannel.ReliableRequestContext.ReplyCompletedAsyncResult(callback, state);
					}
					else
					{
						IAsyncResult asyncResult = new ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult(this, timeout, callback, state);
						flag = false;
						result = asyncResult;
					}
				}
				finally
				{
					if (flag)
					{
						this.AbortInnerContexts();
						this.Abort();
					}
				}
				return result;
			}

			// Token: 0x0600802B RID: 32811 RVA: 0x001DCD78 File Offset: 0x001DAF78
			internal void EndReplyInternal(IAsyncResult result)
			{
				if (result is ReliableReplySessionChannel.ReliableRequestContext.ReplyCompletedAsyncResult)
				{
					CompletedAsyncResult.End(result);
					return;
				}
				bool flag = true;
				try
				{
					ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult.End(result);
					this.innerContexts.Clear();
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.AbortInnerContexts();
						this.Abort();
					}
				}
			}

			// Token: 0x0600802C RID: 32812 RVA: 0x001DCDCC File Offset: 0x001DAFCC
			protected override void OnAbort()
			{
				object thisLock = base.ThisLock;
				bool flag2;
				lock (thisLock)
				{
					flag2 = this.outcomeKnown;
					this.outcomeKnown = true;
				}
				if (!flag2)
				{
					this.AbortInnerContexts();
				}
				if (this.channel.ContainsRequest(this.requestSequenceNumber))
				{
					Exception e = new ProtocolException(SR.GetString("ReliableRequestContextAborted"));
					this.channel.session.OnLocalFault(e, null, null);
				}
			}

			// Token: 0x0600802D RID: 32813 RVA: 0x001DCE54 File Offset: 0x001DB054
			protected override IAsyncResult OnBeginReply(Message reply, TimeSpan timeout, AsyncCallback callback, object state)
			{
				return this.BeginReplyInternal(reply, timeout, callback, state);
			}

			// Token: 0x0600802E RID: 32814 RVA: 0x001DCE61 File Offset: 0x001DB061
			protected override void OnClose(TimeSpan timeout)
			{
				if (!base.ReplyInitiated)
				{
					this.OnReply(null, timeout);
				}
			}

			// Token: 0x0600802F RID: 32815 RVA: 0x001DCE73 File Offset: 0x001DB073
			protected override void OnEndReply(IAsyncResult result)
			{
				this.EndReplyInternal(result);
			}

			// Token: 0x06008030 RID: 32816 RVA: 0x001DCE7C File Offset: 0x001DB07C
			protected override void OnReply(Message reply, TimeSpan timeout)
			{
				this.ReplyInternal(reply, timeout);
			}

			// Token: 0x06008031 RID: 32817 RVA: 0x001DCE88 File Offset: 0x001DB088
			internal void ReplyInternal(Message reply, TimeSpan timeout)
			{
				bool flag = true;
				try
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.ranges == null)
						{
							throw Fx.AssertAndThrow("this.ranges != null");
						}
						if (base.Aborted)
						{
							flag = false;
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationObjectAbortedException(SR.GetString("RequestContextAborted")));
						}
						if (this.outcomeKnown)
						{
							flag = false;
							return;
						}
						if (reply != null && this.bufferedReply == null)
						{
							this.bufferedReply = reply.CreateBufferedCopy(int.MaxValue);
						}
						if (!this.channel.PrepareReply(this))
						{
							flag = false;
							return;
						}
						this.outcomeKnown = true;
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					for (int i = 0; i < this.innerContexts.Count; i++)
					{
						this.SendReply(this.innerContexts[i], MaskingMode.Handled, ref timeoutHelper);
					}
					this.innerContexts.Clear();
					flag = false;
				}
				finally
				{
					if (flag)
					{
						this.AbortInnerContexts();
						this.Abort();
					}
				}
			}

			// Token: 0x06008032 RID: 32818 RVA: 0x001DCFA4 File Offset: 0x001DB1A4
			public void SetAckRanges(SequenceRangeCollection ranges)
			{
				if (this.ranges == null)
				{
					this.ranges = ranges;
				}
			}

			// Token: 0x06008033 RID: 32819 RVA: 0x001DCFB5 File Offset: 0x001DB1B5
			public void SetLastReply(long sequenceNumber)
			{
				this.replySequenceNumber = sequenceNumber;
				this.isLastReply = true;
				if (this.bufferedReply == null)
				{
					this.bufferedReply = Message.CreateMessage(this.channel.MessageVersion, "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage").CreateBufferedCopy(int.MaxValue);
				}
			}

			// Token: 0x06008034 RID: 32820 RVA: 0x001DCFF4 File Offset: 0x001DB1F4
			public void SendReply(RequestContext context, MaskingMode maskingMode)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(base.DefaultSendTimeout);
				this.SendReply(context, maskingMode, ref timeoutHelper);
			}

			// Token: 0x06008035 RID: 32821 RVA: 0x001DD018 File Offset: 0x001DB218
			private void SendReply(RequestContext context, MaskingMode maskingMode, ref TimeoutHelper timeoutHelper)
			{
				if (!this.outcomeKnown)
				{
					throw Fx.AssertAndThrow("this.outcomeKnown");
				}
				Message message;
				if (this.bufferedReply != null)
				{
					message = this.bufferedReply.CreateMessage();
					this.channel.PrepareReplyMessage(this.replySequenceNumber, this.isLastReply, this.ranges, message);
				}
				else
				{
					message = this.channel.CreateAcknowledgement(this.ranges);
				}
				this.channel.binder.SetMaskingMode(context, maskingMode);
				using (message)
				{
					context.Reply(message, timeoutHelper.RemainingTime());
				}
				context.Close(timeoutHelper.RemainingTime());
			}

			// Token: 0x06008036 RID: 32822 RVA: 0x001DD0C8 File Offset: 0x001DB2C8
			public void SetReplySequenceNumber(long sequenceNumber)
			{
				this.replySequenceNumber = sequenceNumber;
			}

			// Token: 0x0400493E RID: 18750
			private MessageBuffer bufferedReply;

			// Token: 0x0400493F RID: 18751
			private ReliableReplySessionChannel channel;

			// Token: 0x04004940 RID: 18752
			private List<RequestContext> innerContexts = new List<RequestContext>();

			// Token: 0x04004941 RID: 18753
			private bool isLastReply;

			// Token: 0x04004942 RID: 18754
			private bool outcomeKnown;

			// Token: 0x04004943 RID: 18755
			private SequenceRangeCollection ranges;

			// Token: 0x04004944 RID: 18756
			private long requestSequenceNumber;

			// Token: 0x04004945 RID: 18757
			private long replySequenceNumber;

			// Token: 0x02000F77 RID: 3959
			private class ReplyCompletedAsyncResult : CompletedAsyncResult
			{
				// Token: 0x060087E6 RID: 34790 RVA: 0x001F92D8 File Offset: 0x001F74D8
				public ReplyCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
				{
				}
			}

			// Token: 0x02000F78 RID: 3960
			private class ReplyAsyncResult : AsyncResult
			{
				// Token: 0x060087E7 RID: 34791 RVA: 0x001F92E2 File Offset: 0x001F74E2
				public ReplyAsyncResult(ReliableReplySessionChannel.ReliableRequestContext thisContext, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.context = thisContext;
					if (this.SendReplies())
					{
						base.Complete(true);
					}
				}

				// Token: 0x060087E8 RID: 34792 RVA: 0x001F930F File Offset: 0x001F750F
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult>(result);
				}

				// Token: 0x060087E9 RID: 34793 RVA: 0x001F9318 File Offset: 0x001F7518
				private void HandleReplyComplete(IAsyncResult result)
				{
					RequestContext requestContext = this.context.innerContexts[this.currentContext];
					try
					{
						requestContext.EndReply(result);
						requestContext.Close(this.timeoutHelper.RemainingTime());
						this.currentContext++;
					}
					finally
					{
						this.reply.Close();
						this.reply = null;
					}
				}

				// Token: 0x060087EA RID: 34794 RVA: 0x001F9388 File Offset: 0x001F7588
				private static void ReplyCompleteStatic(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult replyAsyncResult = null;
					bool flag = false;
					try
					{
						replyAsyncResult = (ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult)result.AsyncState;
						replyAsyncResult.HandleReplyComplete(result);
						flag = replyAsyncResult.SendReplies();
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						exception = ex;
						flag = true;
					}
					if (flag)
					{
						replyAsyncResult.Complete(false, exception);
					}
				}

				// Token: 0x060087EB RID: 34795 RVA: 0x001F93EC File Offset: 0x001F75EC
				private bool SendReplies()
				{
					while (this.currentContext < this.context.innerContexts.Count)
					{
						if (this.context.bufferedReply != null)
						{
							this.reply = this.context.bufferedReply.CreateMessage();
							this.context.channel.PrepareReplyMessage(this.context.replySequenceNumber, this.context.isLastReply, this.context.ranges, this.reply);
						}
						else
						{
							this.reply = this.context.channel.CreateAcknowledgement(this.context.ranges);
						}
						RequestContext requestContext = this.context.innerContexts[this.currentContext];
						this.context.channel.binder.SetMaskingMode(requestContext, MaskingMode.Handled);
						IAsyncResult asyncResult = requestContext.BeginReply(this.reply, this.timeoutHelper.RemainingTime(), ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult.replyCompleteStatic, this);
						if (!asyncResult.CompletedSynchronously)
						{
							return false;
						}
						this.HandleReplyComplete(asyncResult);
					}
					return true;
				}

				// Token: 0x04004F47 RID: 20295
				private ReliableReplySessionChannel.ReliableRequestContext context;

				// Token: 0x04004F48 RID: 20296
				private int currentContext;

				// Token: 0x04004F49 RID: 20297
				private Message reply;

				// Token: 0x04004F4A RID: 20298
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004F4B RID: 20299
				private static AsyncCallback replyCompleteStatic = Fx.ThunkCallback(new AsyncCallback(ReliableReplySessionChannel.ReliableRequestContext.ReplyAsyncResult.ReplyCompleteStatic));
			}
		}

		// Token: 0x02000DD2 RID: 3538
		private class ReplyHelper
		{
			// Token: 0x06008037 RID: 32823 RVA: 0x001DD0D1 File Offset: 0x001DB2D1
			internal ReplyHelper(ReliableReplySessionChannel channel, ReliableReplySessionChannel.ReplyProvider replyProvider, bool throwTimeoutOnWait)
			{
				this.channel = channel;
				this.replyProvider = replyProvider;
				this.throwTimeoutOnWait = throwTimeoutOnWait;
				this.waitHandle = new InterruptibleWaitObject(false, this.throwTimeoutOnWait);
			}

			// Token: 0x17001C6B RID: 7275
			// (get) Token: 0x06008038 RID: 32824 RVA: 0x001DD107 File Offset: 0x001DB307
			private object ThisLock
			{
				get
				{
					return this.channel.ThisLock;
				}
			}

			// Token: 0x06008039 RID: 32825 RVA: 0x001DD114 File Offset: 0x001DB314
			internal void Abort()
			{
				this.Cleanup(true);
			}

			// Token: 0x0600803A RID: 32826 RVA: 0x001DD120 File Offset: 0x001DB320
			private void Cleanup(bool abort)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.canTransfer = false;
				}
				if (abort)
				{
					this.waitHandle.Abort(this.channel);
					return;
				}
				this.waitHandle.Fault(this.channel);
			}

			// Token: 0x0600803B RID: 32827 RVA: 0x001DD188 File Offset: 0x001DB388
			internal void Fault()
			{
				this.Cleanup(false);
			}

			// Token: 0x0600803C RID: 32828 RVA: 0x001DD194 File Offset: 0x001DB394
			internal void Reply(RequestContext context, WsrmMessageInfo info, TimeSpan timeout, MaskingMode maskingMode)
			{
				using (Message message = this.replyProvider.Provide(this.channel, info))
				{
					this.channel.binder.SetMaskingMode(context, maskingMode);
					context.Reply(message, timeout);
				}
			}

			// Token: 0x0600803D RID: 32829 RVA: 0x001DD1EC File Offset: 0x001DB3EC
			private IAsyncResult BeginReply(TimeSpan timeout, AsyncCallback callback, object state)
			{
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.canTransfer = false;
				}
				if (this.requestContext == null)
				{
					return new ReliableReplySessionChannel.ReplyHelper.ReplyCompletedAsyncResult(callback, state);
				}
				this.asyncMessage = this.replyProvider.Provide(this.channel, this.info);
				bool flag2 = true;
				IAsyncResult result;
				try
				{
					this.channel.binder.SetMaskingMode(this.requestContext, MaskingMode.Handled);
					IAsyncResult asyncResult = this.requestContext.BeginReply(this.asyncMessage, timeout, callback, state);
					flag2 = false;
					result = asyncResult;
				}
				finally
				{
					if (flag2)
					{
						this.asyncMessage.Close();
						this.asyncMessage = null;
					}
				}
				return result;
			}

			// Token: 0x0600803E RID: 32830 RVA: 0x001DD2B4 File Offset: 0x001DB4B4
			private void EndReply(IAsyncResult result)
			{
				ReliableReplySessionChannel.ReplyHelper.ReplyCompletedAsyncResult replyCompletedAsyncResult = result as ReliableReplySessionChannel.ReplyHelper.ReplyCompletedAsyncResult;
				if (replyCompletedAsyncResult != null)
				{
					replyCompletedAsyncResult.End();
					return;
				}
				try
				{
					this.requestContext.EndReply(result);
				}
				finally
				{
					if (this.asyncMessage != null)
					{
						this.asyncMessage.Close();
					}
				}
			}

			// Token: 0x0600803F RID: 32831 RVA: 0x001DD308 File Offset: 0x001DB508
			internal bool TransferRequestContext(RequestContext requestContext, WsrmMessageInfo info)
			{
				RequestContext requestContext2 = null;
				WsrmMessageInfo wsrmMessageInfo = null;
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					if (!this.canTransfer)
					{
						return false;
					}
					requestContext2 = this.requestContext;
					wsrmMessageInfo = this.info;
					this.requestContext = requestContext;
					this.info = info;
				}
				this.waitHandle.Set();
				if (requestContext2 != null)
				{
					wsrmMessageInfo.Message.Close();
					requestContext2.Close();
				}
				return true;
			}

			// Token: 0x06008040 RID: 32832 RVA: 0x001DD394 File Offset: 0x001DB594
			internal void UnblockWaiter()
			{
				this.TransferRequestContext(null, null);
			}

			// Token: 0x06008041 RID: 32833 RVA: 0x001DD3A0 File Offset: 0x001DB5A0
			internal void WaitAndReply(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.waitHandle.Wait(timeoutHelper.RemainingTime());
				object thisLock = this.ThisLock;
				lock (thisLock)
				{
					this.canTransfer = false;
					if (this.requestContext == null)
					{
						return;
					}
				}
				this.Reply(this.requestContext, this.info, timeoutHelper.RemainingTime(), MaskingMode.Handled);
			}

			// Token: 0x06008042 RID: 32834 RVA: 0x001DD420 File Offset: 0x001DB620
			internal IAsyncResult BeginWaitAndReply(TimeSpan timeout, AsyncCallback callback, object state)
			{
				OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
				{
					new OperationWithTimeoutBeginCallback(this.waitHandle.BeginWait),
					new OperationWithTimeoutBeginCallback(this.BeginReply)
				};
				OperationEndCallback[] endOperations = new OperationEndCallback[]
				{
					new OperationEndCallback(this.waitHandle.EndWait),
					new OperationEndCallback(this.EndReply)
				};
				return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
			}

			// Token: 0x06008043 RID: 32835 RVA: 0x001DD48B File Offset: 0x001DB68B
			internal void EndWaitAndReply(IAsyncResult result)
			{
				OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
			}

			// Token: 0x04004946 RID: 18758
			private Message asyncMessage;

			// Token: 0x04004947 RID: 18759
			private bool canTransfer = true;

			// Token: 0x04004948 RID: 18760
			private ReliableReplySessionChannel channel;

			// Token: 0x04004949 RID: 18761
			private WsrmMessageInfo info;

			// Token: 0x0400494A RID: 18762
			private ReliableReplySessionChannel.ReplyProvider replyProvider;

			// Token: 0x0400494B RID: 18763
			private RequestContext requestContext;

			// Token: 0x0400494C RID: 18764
			private bool throwTimeoutOnWait;

			// Token: 0x0400494D RID: 18765
			private InterruptibleWaitObject waitHandle;

			// Token: 0x02000F79 RID: 3961
			private class ReplyCompletedAsyncResult : CompletedAsyncResult
			{
				// Token: 0x060087ED RID: 34797 RVA: 0x001F950D File Offset: 0x001F770D
				internal ReplyCompletedAsyncResult(AsyncCallback callback, object state) : base(callback, state)
				{
				}

				// Token: 0x060087EE RID: 34798 RVA: 0x001F9517 File Offset: 0x001F7717
				public void End()
				{
					AsyncResult.End<ReliableReplySessionChannel.ReplyHelper.ReplyCompletedAsyncResult>(this);
				}
			}
		}

		// Token: 0x02000DD3 RID: 3539
		private abstract class ReplyProvider
		{
			// Token: 0x06008044 RID: 32836
			internal abstract Message Provide(ReliableReplySessionChannel channel, WsrmMessageInfo info);
		}

		// Token: 0x02000DD4 RID: 3540
		private class CloseSequenceReplyProvider : ReliableReplySessionChannel.ReplyProvider
		{
			// Token: 0x06008046 RID: 32838 RVA: 0x001DD49B File Offset: 0x001DB69B
			private CloseSequenceReplyProvider()
			{
			}

			// Token: 0x17001C6C RID: 7276
			// (get) Token: 0x06008047 RID: 32839 RVA: 0x001DD4A3 File Offset: 0x001DB6A3
			internal static ReliableReplySessionChannel.ReplyProvider Instance
			{
				get
				{
					if (ReliableReplySessionChannel.CloseSequenceReplyProvider.instance == null)
					{
						ReliableReplySessionChannel.CloseSequenceReplyProvider.instance = new ReliableReplySessionChannel.CloseSequenceReplyProvider();
					}
					return ReliableReplySessionChannel.CloseSequenceReplyProvider.instance;
				}
			}

			// Token: 0x06008048 RID: 32840 RVA: 0x001DD4BC File Offset: 0x001DB6BC
			internal override Message Provide(ReliableReplySessionChannel channel, WsrmMessageInfo requestInfo)
			{
				Message message = WsrmUtilities.CreateCloseSequenceResponse(channel.MessageVersion, requestInfo.CloseSequenceInfo.MessageId, channel.session.InputID);
				channel.AddAcknowledgementHeader(message);
				return message;
			}

			// Token: 0x0400494E RID: 18766
			private static ReliableReplySessionChannel.CloseSequenceReplyProvider instance = new ReliableReplySessionChannel.CloseSequenceReplyProvider();
		}

		// Token: 0x02000DD5 RID: 3541
		private class TerminateSequenceReplyProvider : ReliableReplySessionChannel.ReplyProvider
		{
			// Token: 0x0600804A RID: 32842 RVA: 0x001DD4FF File Offset: 0x001DB6FF
			private TerminateSequenceReplyProvider()
			{
			}

			// Token: 0x17001C6D RID: 7277
			// (get) Token: 0x0600804B RID: 32843 RVA: 0x001DD507 File Offset: 0x001DB707
			internal static ReliableReplySessionChannel.ReplyProvider Instance
			{
				get
				{
					if (ReliableReplySessionChannel.TerminateSequenceReplyProvider.instance == null)
					{
						ReliableReplySessionChannel.TerminateSequenceReplyProvider.instance = new ReliableReplySessionChannel.TerminateSequenceReplyProvider();
					}
					return ReliableReplySessionChannel.TerminateSequenceReplyProvider.instance;
				}
			}

			// Token: 0x0600804C RID: 32844 RVA: 0x001DD520 File Offset: 0x001DB720
			internal override Message Provide(ReliableReplySessionChannel channel, WsrmMessageInfo requestInfo)
			{
				Message message = WsrmUtilities.CreateTerminateResponseMessage(channel.MessageVersion, requestInfo.TerminateSequenceInfo.MessageId, channel.session.InputID);
				channel.AddAcknowledgementHeader(message);
				return message;
			}

			// Token: 0x0400494F RID: 18767
			private static ReliableReplySessionChannel.TerminateSequenceReplyProvider instance = new ReliableReplySessionChannel.TerminateSequenceReplyProvider();
		}
	}
}
