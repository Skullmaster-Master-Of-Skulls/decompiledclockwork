using System;
using System.Runtime;
using System.ServiceModel.Diagnostics.Application;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000929 RID: 2345
	internal abstract class ReliableDuplexSessionChannel : DuplexChannel, IDuplexSessionChannel, IDuplexChannel, IInputChannel, IChannel, ICommunicationObject, IOutputChannel, ISessionChannel<IDuplexSession>
	{
		// Token: 0x060059C3 RID: 22979 RVA: 0x00147D48 File Offset: 0x00145F48
		protected ReliableDuplexSessionChannel(ChannelManagerBase manager, IReliableFactorySettings settings, IReliableChannelBinder binder) : base(manager, binder.LocalAddress)
		{
			this.binder = binder;
			this.settings = settings;
			this.acknowledgementTimer = new IOThreadTimer(new Action<object>(this.OnAcknowledgementTimeoutElapsed), null, true);
			this.binder.Faulted += this.OnBinderFaulted;
			this.binder.OnException += this.OnBinderException;
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x060059C4 RID: 22980 RVA: 0x00147DCF File Offset: 0x00145FCF
		public IReliableChannelBinder Binder
		{
			get
			{
				return this.binder;
			}
		}

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x060059C5 RID: 22981 RVA: 0x00147DD7 File Offset: 0x00145FD7
		public override EndpointAddress LocalAddress
		{
			get
			{
				return this.binder.LocalAddress;
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x060059C6 RID: 22982 RVA: 0x00147DE4 File Offset: 0x00145FE4
		protected ReliableOutputConnection OutputConnection
		{
			get
			{
				return this.outputConnection;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x060059C7 RID: 22983 RVA: 0x00147DEC File Offset: 0x00145FEC
		protected UniqueId OutputID
		{
			get
			{
				return this.session.OutputID;
			}
		}

		// Token: 0x170015D9 RID: 5593
		// (get) Token: 0x060059C8 RID: 22984 RVA: 0x00147DF9 File Offset: 0x00145FF9
		protected ChannelReliableSession ReliableSession
		{
			get
			{
				return this.session;
			}
		}

		// Token: 0x170015DA RID: 5594
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x00147E01 File Offset: 0x00146001
		public override EndpointAddress RemoteAddress
		{
			get
			{
				return this.binder.RemoteAddress;
			}
		}

		// Token: 0x170015DB RID: 5595
		// (get) Token: 0x060059CA RID: 22986 RVA: 0x00147E0E File Offset: 0x0014600E
		protected IReliableFactorySettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x170015DC RID: 5596
		// (get) Token: 0x060059CB RID: 22987 RVA: 0x00147E16 File Offset: 0x00146016
		public override Uri Via
		{
			get
			{
				return this.RemoteAddress.Uri;
			}
		}

		// Token: 0x170015DD RID: 5597
		// (get) Token: 0x060059CC RID: 22988 RVA: 0x00147E23 File Offset: 0x00146023
		public IDuplexSession Session
		{
			get
			{
				return (IDuplexSession)this.session;
			}
		}

		// Token: 0x060059CD RID: 22989 RVA: 0x00147E30 File Offset: 0x00146030
		private void AddPendingAcknowledgements(Message message)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.pendingAcknowledgements > 0)
				{
					this.acknowledgementTimer.Cancel();
					this.acknowledgementScheduled = false;
					this.pendingAcknowledgements = 0;
					this.ackVersion += 1UL;
					int bufferRemaining = this.GetBufferRemaining();
					WsrmUtilities.AddAcknowledgementHeader(this.settings.ReliableMessagingVersion, message, this.session.InputID, this.inputConnection.Ranges, this.inputConnection.IsLastKnown, bufferRemaining);
				}
			}
		}

		// Token: 0x060059CE RID: 22990 RVA: 0x00147ED8 File Offset: 0x001460D8
		private IAsyncResult BeginCloseBinder(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.binder.BeginClose(timeout, MaskingMode.Handled, callback, state);
		}

		// Token: 0x060059CF RID: 22991 RVA: 0x00147EE9 File Offset: 0x001460E9
		private void CloseSequence(TimeSpan timeout)
		{
			this.CreateCloseRequestor();
			this.closeRequestor.Request(timeout);
		}

		// Token: 0x060059D0 RID: 22992 RVA: 0x00147EFE File Offset: 0x001460FE
		private IAsyncResult BeginCloseSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.CreateCloseRequestor();
			return this.closeRequestor.BeginRequest(timeout, callback, state);
		}

		// Token: 0x060059D1 RID: 22993 RVA: 0x00147F14 File Offset: 0x00146114
		private void EndCloseSequence(IAsyncResult result)
		{
			this.closeRequestor.EndRequest(result);
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x00147F23 File Offset: 0x00146123
		private void ConfigureRequestor(ReliableRequestor requestor)
		{
			requestor.MessageVersion = this.settings.MessageVersion;
			requestor.Binder = this.binder;
			requestor.SetRequestResponsePattern();
		}

		// Token: 0x060059D3 RID: 22995 RVA: 0x00147F48 File Offset: 0x00146148
		private Message CreateAcknowledgmentMessage()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.ackVersion += 1UL;
			}
			int bufferRemaining = this.GetBufferRemaining();
			Message result = WsrmUtilities.CreateAcknowledgmentMessage(this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.session.InputID, this.inputConnection.Ranges, this.inputConnection.IsLastKnown, bufferRemaining);
			if (TD.SequenceAcknowledgementSentIsEnabled())
			{
				TD.SequenceAcknowledgementSent(this.session.Id);
			}
			return result;
		}

		// Token: 0x060059D4 RID: 22996 RVA: 0x00147FF0 File Offset: 0x001461F0
		private void CreateCloseRequestor()
		{
			SendWaitReliableRequestor sendWaitReliableRequestor = new SendWaitReliableRequestor();
			this.ConfigureRequestor(sendWaitReliableRequestor);
			sendWaitReliableRequestor.TimeoutString1Index = "TimeoutOnClose";
			sendWaitReliableRequestor.MessageAction = WsrmIndex.GetCloseSequenceActionHeader(this.settings.MessageVersion.Addressing);
			sendWaitReliableRequestor.MessageBody = new CloseSequence(this.session.OutputID, this.outputConnection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.closeRequestor = sendWaitReliableRequestor;
			}
		}

		// Token: 0x060059D5 RID: 22997 RVA: 0x0014808C File Offset: 0x0014628C
		private void CreateTerminateRequestor()
		{
			SendWaitReliableRequestor sendWaitReliableRequestor = new SendWaitReliableRequestor();
			this.ConfigureRequestor(sendWaitReliableRequestor);
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			sendWaitReliableRequestor.MessageAction = WsrmIndex.GetTerminateSequenceActionHeader(this.settings.MessageVersion.Addressing, reliableMessagingVersion);
			sendWaitReliableRequestor.MessageBody = new TerminateSequence(reliableMessagingVersion, this.session.OutputID, this.outputConnection.Last);
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfClosed();
				this.terminateRequestor = sendWaitReliableRequestor;
				if (this.inputConnection.IsLastKnown)
				{
					this.session.CloseSession();
				}
			}
		}

		// Token: 0x060059D6 RID: 22998 RVA: 0x00148144 File Offset: 0x00146344
		private void EndCloseBinder(IAsyncResult result)
		{
			this.binder.EndClose(result);
		}

		// Token: 0x060059D7 RID: 22999 RVA: 0x00148154 File Offset: 0x00146354
		private int GetBufferRemaining()
		{
			int num = -1;
			if (this.settings.FlowControlEnabled)
			{
				num = this.settings.MaxTransferWindowSize - this.deliveryStrategy.EnqueuedCount;
				this.advertisedZero = (num == 0);
			}
			return num;
		}

		// Token: 0x060059D8 RID: 23000 RVA: 0x00148194 File Offset: 0x00146394
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IDuplexSessionChannel))
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
				return (T)((object)FaultConverter.GetDefaultFaultConverter(this.settings.MessageVersion));
			}
			return property2;
		}

		// Token: 0x060059D9 RID: 23001 RVA: 0x00148220 File Offset: 0x00146420
		private void InternalCloseOutputSession(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.outputConnection.Close(timeoutHelper.RemainingTime());
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CloseSequence(timeoutHelper.RemainingTime());
			}
			this.TerminateSequence(timeoutHelper.RemainingTime());
		}

		// Token: 0x060059DA RID: 23002 RVA: 0x00148274 File Offset: 0x00146474
		private IAsyncResult BeginInternalCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				new OperationWithTimeoutBeginCallback(this.outputConnection.BeginClose),
				flag ? new OperationWithTimeoutBeginCallback(this.BeginCloseSequence) : null,
				new OperationWithTimeoutBeginCallback(this.BeginTerminateSequence)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				new OperationEndCallback(this.outputConnection.EndClose),
				flag ? new OperationEndCallback(this.EndCloseSequence) : null,
				new OperationEndCallback(this.EndTerminateSequence)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x060059DB RID: 23003 RVA: 0x0014831C File Offset: 0x0014651C
		private void EndInternalCloseOutputSession(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x060059DC RID: 23004 RVA: 0x00148324 File Offset: 0x00146524
		protected virtual void OnRemoteActivity()
		{
			this.session.OnRemoteActivity(false);
		}

		// Token: 0x060059DD RID: 23005 RVA: 0x00148334 File Offset: 0x00146534
		private WsrmFault ProcessCloseOrTerminateSequenceResponse(bool close, WsrmMessageInfo info)
		{
			SendWaitReliableRequestor sendWaitReliableRequestor = close ? this.closeRequestor : this.terminateRequestor;
			if (sendWaitReliableRequestor == null)
			{
				string text = close ? "CloseSequence" : "TerminateSequence";
				string @string = SR.GetString("ReceivedResponseBeforeRequestFaultString", new object[]
				{
					text
				});
				string string2 = SR.GetString("ReceivedResponseBeforeRequestExceptionString", new object[]
				{
					text
				});
				return SequenceTerminatedFault.CreateProtocolFault(this.session.OutputID, @string, string2);
			}
			WsrmFault wsrmFault = close ? WsrmUtilities.ValidateCloseSequenceResponse(this.session, this.closeRequestor.MessageId, info, this.outputConnection.Last) : WsrmUtilities.ValidateTerminateSequenceResponse(this.session, this.terminateRequestor.MessageId, info, this.outputConnection.Last);
			if (wsrmFault != null)
			{
				return wsrmFault;
			}
			sendWaitReliableRequestor.SetInfo(info);
			return null;
		}

		// Token: 0x060059DE RID: 23006 RVA: 0x00148400 File Offset: 0x00146600
		protected void ProcessDuplexMessage(WsrmMessageInfo info)
		{
			bool flag = true;
			try
			{
				bool flag2 = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
				bool flag3 = this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
				bool flag4 = false;
				if (this.outputConnection != null && info.AcknowledgementInfo != null)
				{
					flag4 = (flag3 && info.AcknowledgementInfo.Final);
					int quotaRemaining = -1;
					if (this.settings.FlowControlEnabled)
					{
						quotaRemaining = info.AcknowledgementInfo.BufferRemaining;
					}
					this.outputConnection.ProcessTransferred(info.AcknowledgementInfo.Ranges, quotaRemaining);
				}
				this.OnRemoteActivity();
				bool flag5 = info.AckRequestedInfo != null;
				bool flag6 = false;
				bool flag7 = false;
				bool flag8 = false;
				ulong num = 0UL;
				WsrmFault wsrmFault = null;
				Message message = null;
				Exception ex = null;
				if (info.SequencedMessageInfo != null)
				{
					bool flag9 = false;
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (base.Aborted || base.State == CommunicationState.Faulted)
						{
							return;
						}
						long sequenceNumber = info.SequencedMessageInfo.SequenceNumber;
						bool isLast = flag2 && info.SequencedMessageInfo.LastMessage;
						if (!this.inputConnection.IsValid(sequenceNumber, isLast))
						{
							if (flag2)
							{
								wsrmFault = new LastMessageNumberExceededFault(this.ReliableSession.InputID);
							}
							else
							{
								message = new SequenceClosedFault(this.session.InputID).CreateMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion);
								flag6 = true;
								this.OnMessageDropped();
							}
						}
						else if (this.inputConnection.Ranges.Contains(sequenceNumber))
						{
							this.OnMessageDropped();
							flag5 = true;
						}
						else if (flag2 && info.Action == "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage")
						{
							this.inputConnection.Merge(sequenceNumber, isLast);
							if (this.inputConnection.AllAdded)
							{
								flag8 = true;
								if (this.outputConnection.CheckForTermination())
								{
									this.session.CloseSession();
								}
							}
						}
						else if (base.State == CommunicationState.Closing)
						{
							if (flag2)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedSessionClosedBeforeDone"), SR.GetString("SessionClosedBeforeDone"));
							}
							else
							{
								message = new SequenceClosedFault(this.session.InputID).CreateMessage(this.settings.MessageVersion, this.settings.ReliableMessagingVersion);
								flag6 = true;
								this.OnMessageDropped();
							}
						}
						else if (this.deliveryStrategy.CanEnqueue(sequenceNumber) && (this.Settings.Ordered || this.inputConnection.CanMerge(sequenceNumber)))
						{
							this.inputConnection.Merge(sequenceNumber, isLast);
							flag9 = this.deliveryStrategy.Enqueue(info.Message, sequenceNumber);
							flag = false;
							num = this.ackVersion;
							this.pendingAcknowledgements++;
							if (this.inputConnection.AllAdded)
							{
								flag8 = true;
								if (this.outputConnection.CheckForTermination())
								{
									this.session.CloseSession();
								}
							}
						}
						else
						{
							this.OnMessageDropped();
						}
						if (this.inputConnection.IsLastKnown || this.pendingAcknowledgements == this.settings.MaxTransferWindowSize)
						{
							flag5 = true;
						}
						bool flag11 = flag5 || (this.pendingAcknowledgements > 0 && wsrmFault == null);
						if (flag11 && !this.acknowledgementScheduled)
						{
							this.acknowledgementScheduled = true;
							this.acknowledgementTimer.Set(this.settings.AcknowledgementInterval);
						}
					}
					if (flag9)
					{
						base.Dispatch();
					}
				}
				else if (flag2 && info.TerminateSequenceInfo != null)
				{
					object thisLock2 = base.ThisLock;
					bool flag13;
					lock (thisLock2)
					{
						flag13 = !this.inputConnection.Terminate();
					}
					if (flag13)
					{
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedEarlyTerminateSequence"), SR.GetString("EarlyTerminateSequence"));
					}
				}
				else if (flag3)
				{
					if ((info.TerminateSequenceInfo != null && info.TerminateSequenceInfo.Identifier == this.session.InputID) || info.CloseSequenceInfo != null)
					{
						bool flag14 = info.TerminateSequenceInfo != null;
						WsrmRequestInfo wsrmRequestInfo = flag14 ? info.TerminateSequenceInfo : info.CloseSequenceInfo;
						long num2 = flag14 ? info.TerminateSequenceInfo.LastMsgNumber : info.CloseSequenceInfo.LastMsgNumber;
						if (!WsrmUtilities.ValidateWsrmRequest(this.session, wsrmRequestInfo, this.binder, null))
						{
							return;
						}
						bool flag15 = true;
						bool flag16 = true;
						object thisLock3 = base.ThisLock;
						lock (thisLock3)
						{
							if (!this.inputConnection.IsLastKnown)
							{
								if (flag14)
								{
									if (this.inputConnection.SetTerminateSequenceLast(num2, out flag15))
									{
										flag8 = true;
									}
									else if (flag15)
									{
										ex = new ProtocolException(SR.GetString("EarlyTerminateSequence"));
									}
								}
								else
								{
									flag8 = this.inputConnection.SetCloseSequenceLast(num2);
									flag15 = flag8;
								}
								if (flag8)
								{
									this.session.SetFinalAck(this.inputConnection.Ranges);
									if (this.terminateRequestor != null)
									{
										this.session.CloseSession();
									}
									this.deliveryStrategy.Dispose();
								}
							}
							else
							{
								flag16 = (num2 == this.inputConnection.Last);
								if (flag14 && flag16 && this.inputConnection.IsSequenceClosed)
								{
									flag7 = true;
								}
							}
						}
						if (!flag15)
						{
							string @string = SR.GetString("SequenceTerminatedSmallLastMsgNumber");
							string string2 = SR.GetString("SmallLastMsgNumberExceptionString");
							wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, @string, string2);
						}
						else if (!flag16)
						{
							string string3 = SR.GetString("SequenceTerminatedInconsistentLastMsgNumber");
							string string4 = SR.GetString("InconsistentLastMsgNumberExceptionString");
							wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, string3, string4);
						}
						else
						{
							message = (flag14 ? WsrmUtilities.CreateTerminateResponseMessage(this.settings.MessageVersion, wsrmRequestInfo.MessageId, this.session.InputID) : WsrmUtilities.CreateCloseSequenceResponse(this.settings.MessageVersion, wsrmRequestInfo.MessageId, this.session.InputID));
							flag6 = true;
						}
					}
					else if (info.TerminateSequenceInfo != null)
					{
						wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.InputID, SR.GetString("SequenceTerminatedUnsupportedTerminateSequence"), SR.GetString("UnsupportedTerminateSequenceExceptionString"));
					}
					else if (info.TerminateSequenceResponseInfo != null)
					{
						wsrmFault = this.ProcessCloseOrTerminateSequenceResponse(false, info);
					}
					else if (info.CloseSequenceResponseInfo != null)
					{
						wsrmFault = this.ProcessCloseOrTerminateSequenceResponse(true, info);
					}
					else if (flag4)
					{
						if (this.closeRequestor == null)
						{
							string string5 = SR.GetString("UnsupportedCloseExceptionString");
							string string6 = SR.GetString("SequenceTerminatedUnsupportedClose");
							wsrmFault = SequenceTerminatedFault.CreateProtocolFault(this.session.OutputID, string6, string5);
						}
						else
						{
							wsrmFault = WsrmUtilities.ValidateFinalAck(this.session, info, this.outputConnection.Last);
							if (wsrmFault == null)
							{
								this.closeRequestor.SetInfo(info);
							}
						}
					}
					else if (info.WsrmHeaderFault != null)
					{
						if (!(info.WsrmHeaderFault is UnknownSequenceFault))
						{
							throw Fx.AssertAndThrow("Fault must be UnknownSequence fault.");
						}
						if (this.terminateRequestor == null)
						{
							throw Fx.AssertAndThrow("In wsrm11, if we start getting UnknownSequence, terminateRequestor cannot be null.");
						}
						this.terminateRequestor.SetInfo(info);
					}
				}
				if (wsrmFault != null)
				{
					this.session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, null);
				}
				else
				{
					if (flag8)
					{
						ActionItem.Schedule(new Action<object>(this.ShutdownCallback), null);
					}
					if (message != null)
					{
						if (flag6)
						{
							WsrmUtilities.AddAcknowledgementHeader(this.settings.ReliableMessagingVersion, message, this.session.InputID, this.inputConnection.Ranges, true, this.GetBufferRemaining());
						}
						else if (flag5)
						{
							this.AddPendingAcknowledgements(message);
						}
					}
					else if (flag5)
					{
						object thisLock4 = base.ThisLock;
						lock (thisLock4)
						{
							if (num != 0UL && num != this.ackVersion)
							{
								return;
							}
							if (this.acknowledgementScheduled)
							{
								this.acknowledgementTimer.Cancel();
								this.acknowledgementScheduled = false;
							}
							this.pendingAcknowledgements = 0;
						}
						message = this.CreateAcknowledgmentMessage();
					}
					if (message != null)
					{
						using (message)
						{
							if (this.guard.Enter())
							{
								try
								{
									this.binder.Send(message, base.DefaultSendTimeout);
								}
								finally
								{
									this.guard.Exit();
								}
							}
						}
					}
					if (flag7)
					{
						object thisLock5 = base.ThisLock;
						lock (thisLock5)
						{
							this.inputConnection.Terminate();
						}
					}
					if (ex != null)
					{
						this.ReliableSession.OnRemoteFault(ex);
					}
				}
			}
			finally
			{
				if (flag)
				{
					info.Message.Close();
				}
			}
		}

		// Token: 0x060059DF RID: 23007
		protected abstract void ProcessMessage(WsrmMessageInfo info);

		// Token: 0x060059E0 RID: 23008 RVA: 0x00148D58 File Offset: 0x00146F58
		protected override void OnAbort()
		{
			if (this.outputConnection != null)
			{
				this.outputConnection.Abort(this);
			}
			if (this.inputConnection != null)
			{
				this.inputConnection.Abort(this);
			}
			this.guard.Abort();
			ReliableRequestor reliableRequestor = this.closeRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Abort(this);
			}
			reliableRequestor = this.terminateRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Abort(this);
			}
			this.session.Abort();
		}

		// Token: 0x060059E1 RID: 23009 RVA: 0x00148DC8 File Offset: 0x00146FC8
		private void OnAcknowledgementTimeoutElapsed(object state)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				this.acknowledgementScheduled = false;
				this.pendingAcknowledgements = 0;
				if (base.State == CommunicationState.Closing || base.State == CommunicationState.Closed || base.State == CommunicationState.Faulted)
				{
					return;
				}
			}
			if (this.guard.Enter())
			{
				try
				{
					using (Message message = this.CreateAcknowledgmentMessage())
					{
						this.binder.Send(message, base.DefaultSendTimeout);
					}
				}
				finally
				{
					this.guard.Exit();
				}
			}
		}

		// Token: 0x060059E2 RID: 23010 RVA: 0x00148E84 File Offset: 0x00147084
		protected IAsyncResult OnBeginCloseOutputSession(TimeSpan timeout, AsyncCallback callback, object state)
		{
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfNotOpened();
				base.ThrowIfFaulted();
				if (base.State != CommunicationState.Opened || this.closeOutputWaitObject != null)
				{
					flag = true;
				}
				else
				{
					this.closeOutputWaitObject = new InterruptibleWaitObject(false, true);
				}
			}
			if (flag)
			{
				return new CompletedAsyncResult(callback, state);
			}
			bool flag3 = true;
			IAsyncResult result;
			try
			{
				IAsyncResult asyncResult = this.BeginInternalCloseOutputSession(timeout, callback, state);
				flag3 = false;
				result = asyncResult;
			}
			finally
			{
				if (flag3)
				{
					this.session.OnLocalFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.session.OutputID, SR.GetString("CloseOutputSessionErrorReason"), null), null);
					this.closeOutputWaitObject.Fault(this);
				}
			}
			return result;
		}

		// Token: 0x060059E3 RID: 23011 RVA: 0x00148F54 File Offset: 0x00147154
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.ThrowIfCloseInvalid();
			OperationWithTimeoutBeginCallback operationWithTimeoutBeginCallback;
			OperationEndCallback operationEndCallback;
			if (this.outputConnection == null)
			{
				operationWithTimeoutBeginCallback = null;
				operationEndCallback = null;
			}
			else if (this.closeOutputWaitObject == null)
			{
				operationWithTimeoutBeginCallback = new OperationWithTimeoutBeginCallback(this.BeginInternalCloseOutputSession);
				operationEndCallback = new OperationEndCallback(this.EndInternalCloseOutputSession);
			}
			else
			{
				operationWithTimeoutBeginCallback = new OperationWithTimeoutBeginCallback(this.closeOutputWaitObject.BeginWait);
				operationEndCallback = new OperationEndCallback(this.closeOutputWaitObject.EndWait);
			}
			OperationWithTimeoutBeginCallback operationWithTimeoutBeginCallback2;
			OperationEndCallback operationEndCallback2;
			if (this.inputConnection == null)
			{
				operationWithTimeoutBeginCallback2 = null;
				operationEndCallback2 = null;
			}
			else
			{
				operationWithTimeoutBeginCallback2 = new OperationWithTimeoutBeginCallback(this.inputConnection.BeginClose);
				operationEndCallback2 = new OperationEndCallback(this.inputConnection.EndClose);
			}
			OperationWithTimeoutBeginCallback[] beginOperations = new OperationWithTimeoutBeginCallback[]
			{
				operationWithTimeoutBeginCallback,
				operationWithTimeoutBeginCallback2,
				new OperationWithTimeoutBeginCallback(this.guard.BeginClose),
				new OperationWithTimeoutBeginCallback(this.session.BeginClose),
				new OperationWithTimeoutBeginCallback(this.BeginCloseBinder),
				new OperationWithTimeoutBeginCallback(base.OnBeginClose)
			};
			OperationEndCallback[] endOperations = new OperationEndCallback[]
			{
				operationEndCallback,
				operationEndCallback2,
				new OperationEndCallback(this.guard.EndClose),
				new OperationEndCallback(this.session.EndClose),
				new OperationEndCallback(this.EndCloseBinder),
				new OperationEndCallback(base.OnEndClose)
			};
			return OperationWithTimeoutComposer.BeginComposeAsyncOperations(timeout, beginOperations, endOperations, callback, state);
		}

		// Token: 0x060059E4 RID: 23012 RVA: 0x001490A9 File Offset: 0x001472A9
		protected override IAsyncResult OnBeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.outputConnection.BeginAddMessage(message, timeout, null, callback, state);
		}

		// Token: 0x060059E5 RID: 23013 RVA: 0x001490BC File Offset: 0x001472BC
		private IAsyncResult OnBeginSendHandler(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException, AsyncCallback callback, object state)
		{
			if (attemptInfo.RetryCount > this.settings.MaxRetryCount)
			{
				this.session.OnLocalFault(new CommunicationException(SR.GetString("MaximumRetryCountExceeded"), this.maxRetryCountException), SequenceTerminatedFault.CreateMaxRetryCountExceededFault(this.session.OutputID), null);
				return new CompletedAsyncResult(callback, state);
			}
			this.session.OnLocalActivity();
			this.AddPendingAcknowledgements(attemptInfo.Message);
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = new ReliableBinderSendAsyncResult(callback, state);
			reliableBinderSendAsyncResult.Binder = this.binder;
			reliableBinderSendAsyncResult.MessageAttemptInfo = attemptInfo;
			reliableBinderSendAsyncResult.MaskingMode = (maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None);
			if (attemptInfo.RetryCount < this.settings.MaxRetryCount)
			{
				reliableBinderSendAsyncResult.MaskingMode |= MaskingMode.Handled;
				reliableBinderSendAsyncResult.SaveHandledException = false;
			}
			else
			{
				reliableBinderSendAsyncResult.SaveHandledException = true;
			}
			reliableBinderSendAsyncResult.Begin(timeout);
			return reliableBinderSendAsyncResult;
		}

		// Token: 0x060059E6 RID: 23014 RVA: 0x00149194 File Offset: 0x00147394
		private IAsyncResult OnBeginSendAckRequestedHandler(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.session.OnLocalActivity();
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = new ReliableBinderSendAsyncResult(callback, state);
			reliableBinderSendAsyncResult.Binder = this.binder;
			reliableBinderSendAsyncResult.MaskingMode = MaskingMode.Handled;
			reliableBinderSendAsyncResult.Message = WsrmUtilities.CreateAckRequestedMessage(this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.ReliableSession.OutputID);
			reliableBinderSendAsyncResult.Begin(timeout);
			return reliableBinderSendAsyncResult;
		}

		// Token: 0x060059E7 RID: 23015 RVA: 0x001491FC File Offset: 0x001473FC
		private void OnBinderException(IReliableChannelBinder sender, Exception exception)
		{
			if (exception is QuotaExceededException)
			{
				if (base.State == CommunicationState.Opening || base.State == CommunicationState.Opened || base.State == CommunicationState.Closing)
				{
					this.session.OnLocalFault(exception, SequenceTerminatedFault.CreateQuotaExceededFault(this.session.OutputID), null);
					return;
				}
			}
			else
			{
				base.EnqueueAndDispatch(exception, null, false);
			}
		}

		// Token: 0x060059E8 RID: 23016 RVA: 0x00149254 File Offset: 0x00147454
		private void OnBinderFaulted(IReliableChannelBinder sender, Exception exception)
		{
			this.binder.Abort();
			if (base.State == CommunicationState.Opening || base.State == CommunicationState.Opened || base.State == CommunicationState.Closing)
			{
				exception = new CommunicationException(SR.GetString("EarlySecurityFaulted"), exception);
				this.session.OnLocalFault(exception, null, null);
			}
		}

		// Token: 0x060059E9 RID: 23017 RVA: 0x001492A8 File Offset: 0x001474A8
		protected override void OnClose(TimeSpan timeout)
		{
			this.ThrowIfCloseInvalid();
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.outputConnection != null)
			{
				if (this.closeOutputWaitObject != null)
				{
					this.closeOutputWaitObject.Wait(timeoutHelper.RemainingTime());
				}
				else
				{
					this.InternalCloseOutputSession(timeoutHelper.RemainingTime());
				}
				this.inputConnection.Close(timeoutHelper.RemainingTime());
			}
			this.guard.Close(timeoutHelper.RemainingTime());
			this.session.Close(timeoutHelper.RemainingTime());
			this.binder.Close(timeoutHelper.RemainingTime(), MaskingMode.Handled);
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x060059EA RID: 23018 RVA: 0x0014934C File Offset: 0x0014754C
		protected void OnCloseOutputSession(TimeSpan timeout)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				base.ThrowIfNotOpened();
				base.ThrowIfFaulted();
				if (base.State != CommunicationState.Opened || this.closeOutputWaitObject != null)
				{
					return;
				}
				this.closeOutputWaitObject = new InterruptibleWaitObject(false, true);
			}
			bool flag2 = true;
			try
			{
				this.InternalCloseOutputSession(timeout);
				flag2 = false;
			}
			finally
			{
				if (flag2)
				{
					this.session.OnLocalFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.session.OutputID, SR.GetString("CloseOutputSessionErrorReason"), null), null);
					this.closeOutputWaitObject.Fault(this);
				}
				else
				{
					this.closeOutputWaitObject.Set();
				}
			}
		}

		// Token: 0x060059EB RID: 23019 RVA: 0x00149410 File Offset: 0x00147610
		protected override void OnClosed()
		{
			base.OnClosed();
			this.binder.Faulted -= this.OnBinderFaulted;
			if (this.deliveryStrategy != null)
			{
				this.deliveryStrategy.Dispose();
			}
		}

		// Token: 0x060059EC RID: 23020 RVA: 0x00149442 File Offset: 0x00147642
		protected override void OnClosing()
		{
			base.OnClosing();
			this.acknowledgementTimer.Cancel();
		}

		// Token: 0x060059ED RID: 23021 RVA: 0x00149456 File Offset: 0x00147656
		private void OnComponentFaulted(Exception faultException, WsrmFault fault)
		{
			this.session.OnLocalFault(faultException, fault, null);
		}

		// Token: 0x060059EE RID: 23022 RVA: 0x00149466 File Offset: 0x00147666
		private void OnComponentException(Exception exception)
		{
			this.ReliableSession.OnUnknownException(exception);
		}

		// Token: 0x060059EF RID: 23023 RVA: 0x00149474 File Offset: 0x00147674
		protected override void OnEndClose(IAsyncResult result)
		{
			OperationWithTimeoutComposer.EndComposeAsyncOperations(result);
		}

		// Token: 0x060059F0 RID: 23024 RVA: 0x0014947C File Offset: 0x0014767C
		protected void OnEndCloseOutputSession(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			bool flag = true;
			try
			{
				this.EndInternalCloseOutputSession(result);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					this.session.OnLocalFault(null, SequenceTerminatedFault.CreateCommunicationFault(this.session.OutputID, SR.GetString("CloseOutputSessionErrorReason"), null), null);
					this.closeOutputWaitObject.Fault(this);
				}
				else
				{
					this.closeOutputWaitObject.Set();
				}
			}
		}

		// Token: 0x060059F1 RID: 23025 RVA: 0x001494FC File Offset: 0x001476FC
		protected override void OnEndSend(IAsyncResult result)
		{
			if (!this.outputConnection.EndAddMessage(result))
			{
				this.ThrowInvalidAddException();
			}
		}

		// Token: 0x060059F2 RID: 23026 RVA: 0x00149514 File Offset: 0x00147714
		private void OnEndSendHandler(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			Exception ex;
			ReliableBinderSendAsyncResult.End(result, out ex);
			ReliableBinderSendAsyncResult reliableBinderSendAsyncResult = (ReliableBinderSendAsyncResult)result;
			if (reliableBinderSendAsyncResult.MessageAttemptInfo.RetryCount == this.settings.MaxRetryCount)
			{
				this.maxRetryCountException = ex;
			}
		}

		// Token: 0x060059F3 RID: 23027 RVA: 0x00149561 File Offset: 0x00147761
		private void OnEndSendAckRequestedHandler(IAsyncResult result)
		{
			ReliableBinderSendAsyncResult.End(result);
		}

		// Token: 0x060059F4 RID: 23028 RVA: 0x00149569 File Offset: 0x00147769
		protected override void OnFaulted()
		{
			this.session.OnFaulted();
			this.UnblockClose();
			base.OnFaulted();
		}

		// Token: 0x060059F5 RID: 23029 RVA: 0x00149582 File Offset: 0x00147782
		protected override void OnSend(Message message, TimeSpan timeout)
		{
			if (!this.outputConnection.AddMessage(message, timeout, null))
			{
				this.ThrowInvalidAddException();
			}
		}

		// Token: 0x060059F6 RID: 23030 RVA: 0x0014959C File Offset: 0x0014779C
		private void OnSendHandler(MessageAttemptInfo attemptInfo, TimeSpan timeout, bool maskUnhandledException)
		{
			using (attemptInfo.Message)
			{
				if (attemptInfo.RetryCount > this.settings.MaxRetryCount)
				{
					this.session.OnLocalFault(new CommunicationException(SR.GetString("MaximumRetryCountExceeded"), this.maxRetryCountException), SequenceTerminatedFault.CreateMaxRetryCountExceededFault(this.session.OutputID), null);
				}
				else
				{
					this.session.OnLocalActivity();
					this.AddPendingAcknowledgements(attemptInfo.Message);
					MaskingMode maskingMode = maskUnhandledException ? MaskingMode.Unhandled : MaskingMode.None;
					if (attemptInfo.RetryCount < this.settings.MaxRetryCount)
					{
						maskingMode |= MaskingMode.Handled;
						this.binder.Send(attemptInfo.Message, timeout, maskingMode);
					}
					else
					{
						try
						{
							this.binder.Send(attemptInfo.Message, timeout, maskingMode);
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							if (!this.binder.IsHandleable(ex))
							{
								throw;
							}
							this.maxRetryCountException = ex;
						}
					}
				}
			}
		}

		// Token: 0x060059F7 RID: 23031 RVA: 0x001496AC File Offset: 0x001478AC
		private void OnSendAckRequestedHandler(TimeSpan timeout)
		{
			this.session.OnLocalActivity();
			using (Message message = WsrmUtilities.CreateAckRequestedMessage(this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.ReliableSession.OutputID))
			{
				this.binder.Send(message, timeout, MaskingMode.Handled);
			}
		}

		// Token: 0x060059F8 RID: 23032 RVA: 0x00149718 File Offset: 0x00147918
		private static void OnReceiveCompletedStatic(IAsyncResult result)
		{
			if (result.CompletedSynchronously)
			{
				return;
			}
			ReliableDuplexSessionChannel reliableDuplexSessionChannel = (ReliableDuplexSessionChannel)result.AsyncState;
			try
			{
				if (reliableDuplexSessionChannel.HandleReceiveComplete(result))
				{
					reliableDuplexSessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableDuplexSessionChannel.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x060059F9 RID: 23033 RVA: 0x00149778 File Offset: 0x00147978
		private static void AsyncReceiveCompleteStatic(object state)
		{
			IAsyncResult asyncResult = (IAsyncResult)state;
			ReliableDuplexSessionChannel reliableDuplexSessionChannel = (ReliableDuplexSessionChannel)asyncResult.AsyncState;
			try
			{
				if (reliableDuplexSessionChannel.HandleReceiveComplete(asyncResult))
				{
					reliableDuplexSessionChannel.StartReceiving(true);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				reliableDuplexSessionChannel.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x060059FA RID: 23034 RVA: 0x001497D4 File Offset: 0x001479D4
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
					flag = this.inputConnection.Terminate();
				}
				if (!flag && this.Binder.State == CommunicationState.Opened)
				{
					Exception e = new CommunicationException(SR.GetString("EarlySecurityClose"));
					this.ReliableSession.OnLocalFault(e, null, null);
				}
				return false;
			}
			Message requestMessage = requestContext.RequestMessage;
			requestContext.Close();
			WsrmMessageInfo info = WsrmMessageInfo.Get(this.settings.MessageVersion, this.settings.ReliableMessagingVersion, this.binder.Channel, this.binder.GetInnerSession(), requestMessage);
			this.StartReceiving(false);
			this.ProcessMessage(info);
			return false;
		}

		// Token: 0x060059FB RID: 23035 RVA: 0x001498BC File Offset: 0x00147ABC
		protected override void OnOpened()
		{
			base.OnOpened();
		}

		// Token: 0x060059FC RID: 23036 RVA: 0x001498C4 File Offset: 0x00147AC4
		protected virtual void OnMessageDropped()
		{
		}

		// Token: 0x060059FD RID: 23037 RVA: 0x001498C8 File Offset: 0x00147AC8
		protected void SetConnections()
		{
			this.outputConnection = new ReliableOutputConnection(this.session.OutputID, this.settings.MaxTransferWindowSize, this.Settings.MessageVersion, this.Settings.ReliableMessagingVersion, this.session.InitiationTime, true, base.DefaultSendTimeout);
			ReliableOutputConnection reliableOutputConnection = this.outputConnection;
			reliableOutputConnection.Faulted = (ComponentFaultedHandler)Delegate.Combine(reliableOutputConnection.Faulted, new ComponentFaultedHandler(this.OnComponentFaulted));
			ReliableOutputConnection reliableOutputConnection2 = this.outputConnection;
			reliableOutputConnection2.OnException = (ComponentExceptionHandler)Delegate.Combine(reliableOutputConnection2.OnException, new ComponentExceptionHandler(this.OnComponentException));
			this.outputConnection.BeginSendHandler = new BeginSendHandler(this.OnBeginSendHandler);
			this.outputConnection.EndSendHandler = new EndSendHandler(this.OnEndSendHandler);
			this.outputConnection.SendHandler = new SendHandler(this.OnSendHandler);
			this.outputConnection.BeginSendAckRequestedHandler = new OperationWithTimeoutBeginCallback(this.OnBeginSendAckRequestedHandler);
			this.outputConnection.EndSendAckRequestedHandler = new OperationEndCallback(this.OnEndSendAckRequestedHandler);
			this.outputConnection.SendAckRequestedHandler = new OperationWithTimeoutCallback(this.OnSendAckRequestedHandler);
			this.inputConnection = new ReliableInputConnection();
			this.inputConnection.ReliableMessagingVersion = this.Settings.ReliableMessagingVersion;
			if (this.settings.Ordered)
			{
				this.deliveryStrategy = new OrderedDeliveryStrategy<Message>(this, this.settings.MaxTransferWindowSize, false);
			}
			else
			{
				this.deliveryStrategy = new UnorderedDeliveryStrategy<Message>(this, this.settings.MaxTransferWindowSize);
			}
			this.deliveryStrategy.DequeueCallback = new Action(this.OnDeliveryStrategyItemDequeued);
		}

		// Token: 0x060059FE RID: 23038 RVA: 0x00149A6C File Offset: 0x00147C6C
		protected void SetSession(ChannelReliableSession session)
		{
			session.UnblockChannelCloseCallback = new ChannelReliableSession.UnblockChannelCloseHandler(this.UnblockClose);
			this.session = session;
		}

		// Token: 0x060059FF RID: 23039 RVA: 0x00149A87 File Offset: 0x00147C87
		private void OnDeliveryStrategyItemDequeued()
		{
			if (this.advertisedZero)
			{
				this.OnAcknowledgementTimeoutElapsed(null);
			}
		}

		// Token: 0x06005A00 RID: 23040 RVA: 0x00149A98 File Offset: 0x00147C98
		protected void StartReceiving(bool canBlock)
		{
			IAsyncResult asyncResult;
			for (;;)
			{
				asyncResult = this.binder.BeginTryReceive(TimeSpan.MaxValue, ReliableDuplexSessionChannel.onReceiveCompleted, this);
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
			ActionItem.Schedule(ReliableDuplexSessionChannel.asyncReceiveComplete, asyncResult);
		}

		// Token: 0x06005A01 RID: 23041 RVA: 0x00149ADD File Offset: 0x00147CDD
		private void ShutdownCallback(object state)
		{
			base.Shutdown();
		}

		// Token: 0x06005A02 RID: 23042 RVA: 0x00149AE8 File Offset: 0x00147CE8
		private void TerminateSequence(TimeSpan timeout)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (this.outputConnection.CheckForTermination())
				{
					this.session.CloseSession();
				}
				Message message = WsrmUtilities.CreateTerminateMessage(this.settings.MessageVersion, reliableMessagingVersion, this.session.OutputID);
				this.binder.Send(message, timeout, MaskingMode.Handled);
				return;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CreateTerminateRequestor();
				this.terminateRequestor.Request(timeout);
				return;
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005A03 RID: 23043 RVA: 0x00149B74 File Offset: 0x00147D74
		private IAsyncResult BeginTerminateSequence(TimeSpan timeout, AsyncCallback callback, object state)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (this.outputConnection.CheckForTermination())
				{
					this.session.CloseSession();
				}
				Message message = WsrmUtilities.CreateTerminateMessage(this.settings.MessageVersion, reliableMessagingVersion, this.session.OutputID);
				return this.binder.BeginSend(message, timeout, MaskingMode.Handled, callback, state);
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.CreateTerminateRequestor();
				return this.terminateRequestor.BeginRequest(timeout, callback, state);
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005A04 RID: 23044 RVA: 0x00149C04 File Offset: 0x00147E04
		private void EndTerminateSequence(IAsyncResult result)
		{
			ReliableMessagingVersion reliableMessagingVersion = this.settings.ReliableMessagingVersion;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				this.binder.EndSend(result);
				return;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				this.terminateRequestor.EndRequest(result);
				return;
			}
			throw Fx.AssertAndThrow("Reliable messaging version not supported.");
		}

		// Token: 0x06005A05 RID: 23045 RVA: 0x00149C54 File Offset: 0x00147E54
		private void ThrowIfCloseInvalid()
		{
			bool flag = false;
			if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
			{
				if (this.deliveryStrategy.EnqueuedCount > 0 || this.inputConnection.Ranges.Count > 1)
				{
					flag = true;
				}
			}
			else if (this.settings.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.deliveryStrategy.EnqueuedCount > 0)
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

		// Token: 0x06005A06 RID: 23046 RVA: 0x00149D04 File Offset: 0x00147F04
		private void ThrowInvalidAddException()
		{
			if (base.State == CommunicationState.Opened)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SendCannotBeCalledAfterCloseOutputSession")));
			}
			if (base.State == CommunicationState.Faulted)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.GetTerminalException());
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(base.CreateClosedException());
		}

		// Token: 0x06005A07 RID: 23047 RVA: 0x00149D60 File Offset: 0x00147F60
		private void UnblockClose()
		{
			if (this.outputConnection != null)
			{
				this.outputConnection.Fault(this);
			}
			if (this.inputConnection != null)
			{
				this.inputConnection.Fault(this);
			}
			ReliableRequestor reliableRequestor = this.closeRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Fault(this);
			}
			reliableRequestor = this.terminateRequestor;
			if (reliableRequestor != null)
			{
				reliableRequestor.Fault(this);
			}
		}

		// Token: 0x0400366A RID: 13930
		private bool acknowledgementScheduled;

		// Token: 0x0400366B RID: 13931
		private IOThreadTimer acknowledgementTimer;

		// Token: 0x0400366C RID: 13932
		private ulong ackVersion = 1UL;

		// Token: 0x0400366D RID: 13933
		private bool advertisedZero;

		// Token: 0x0400366E RID: 13934
		private IReliableChannelBinder binder;

		// Token: 0x0400366F RID: 13935
		private InterruptibleWaitObject closeOutputWaitObject;

		// Token: 0x04003670 RID: 13936
		private SendWaitReliableRequestor closeRequestor;

		// Token: 0x04003671 RID: 13937
		private DeliveryStrategy<Message> deliveryStrategy;

		// Token: 0x04003672 RID: 13938
		private Guard guard = new Guard(int.MaxValue);

		// Token: 0x04003673 RID: 13939
		private ReliableInputConnection inputConnection;

		// Token: 0x04003674 RID: 13940
		private Exception maxRetryCountException;

		// Token: 0x04003675 RID: 13941
		private static AsyncCallback onReceiveCompleted = Fx.ThunkCallback(new AsyncCallback(ReliableDuplexSessionChannel.OnReceiveCompletedStatic));

		// Token: 0x04003676 RID: 13942
		private ReliableOutputConnection outputConnection;

		// Token: 0x04003677 RID: 13943
		private int pendingAcknowledgements;

		// Token: 0x04003678 RID: 13944
		private ChannelReliableSession session;

		// Token: 0x04003679 RID: 13945
		private IReliableFactorySettings settings;

		// Token: 0x0400367A RID: 13946
		private SendWaitReliableRequestor terminateRequestor;

		// Token: 0x0400367B RID: 13947
		private static Action<object> asyncReceiveComplete = new Action<object>(ReliableDuplexSessionChannel.AsyncReceiveCompleteStatic);
	}
}
