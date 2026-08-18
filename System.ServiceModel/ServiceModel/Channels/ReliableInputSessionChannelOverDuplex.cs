using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092D RID: 2349
	internal sealed class ReliableInputSessionChannelOverDuplex : ReliableInputSessionChannel
	{
		// Token: 0x06005A50 RID: 23120 RVA: 0x0014AEB0 File Offset: 0x001490B0
		public ReliableInputSessionChannelOverDuplex(ReliableChannelListenerBase<IInputSessionChannel> listener, IServerReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID) : base(listener, binder, faultHelper, inputID)
		{
			this.acknowledgementInterval = listener.AcknowledgementInterval;
			this.acknowledgementTimer = new IOThreadTimer(new Action<object>(this.OnAcknowledgementTimeoutElapsed), null, true);
			base.DeliveryStrategy.DequeueCallback = new Action(this.OnDeliveryStrategyItemDequeued);
			if (binder.HasSession)
			{
				try
				{
					base.StartReceiving(false);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					base.ReliableSession.OnUnknownException(ex);
				}
			}
		}

		// Token: 0x06005A51 RID: 23121 RVA: 0x0014AF50 File Offset: 0x00149150
		protected override void AbortGuards()
		{
			this.guard.Abort();
		}

		// Token: 0x06005A52 RID: 23122 RVA: 0x0014AF5D File Offset: 0x0014915D
		protected override IAsyncResult BeginCloseGuards(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return this.guard.BeginClose(timeout, callback, state);
		}

		// Token: 0x06005A53 RID: 23123 RVA: 0x0014AF6D File Offset: 0x0014916D
		protected override void CloseGuards(TimeSpan timeout)
		{
			this.guard.Close(timeout);
		}

		// Token: 0x06005A54 RID: 23124 RVA: 0x0014AF7B File Offset: 0x0014917B
		protected override void EndCloseGuards(IAsyncResult result)
		{
			this.guard.EndClose(result);
		}

		// Token: 0x06005A55 RID: 23125 RVA: 0x0014AF8C File Offset: 0x0014918C
		protected override bool HandleReceiveComplete(IAsyncResult result)
		{
			RequestContext requestContext;
			if (!base.Binder.EndTryReceive(result, out requestContext))
			{
				return true;
			}
			if (requestContext == null)
			{
				bool flag = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					flag = base.Connection.Terminate();
				}
				if (!flag && base.Binder.State == CommunicationState.Opened)
				{
					Exception e = new CommunicationException(SR.GetString("EarlySecurityClose"));
					base.ReliableSession.OnLocalFault(e, null, null);
				}
				return false;
			}
			Message requestMessage = requestContext.RequestMessage;
			requestContext.Close();
			WsrmMessageInfo info = WsrmMessageInfo.Get(base.Listener.MessageVersion, base.Listener.ReliableMessagingVersion, base.Binder.Channel, base.Binder.GetInnerSession(), requestMessage);
			base.StartReceiving(false);
			this.ProcessMessage(info);
			return false;
		}

		// Token: 0x06005A56 RID: 23126 RVA: 0x0014B074 File Offset: 0x00149274
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
					using (Message message = base.CreateAcknowledgmentMessage())
					{
						base.Binder.Send(message, base.DefaultSendTimeout);
					}
				}
				finally
				{
					this.guard.Exit();
				}
			}
		}

		// Token: 0x06005A57 RID: 23127 RVA: 0x0014B130 File Offset: 0x00149330
		private void OnDeliveryStrategyItemDequeued()
		{
			if (base.AdvertisedZero)
			{
				this.OnAcknowledgementTimeoutElapsed(null);
			}
		}

		// Token: 0x06005A58 RID: 23128 RVA: 0x0014B141 File Offset: 0x00149341
		protected override void OnClosing()
		{
			base.OnClosing();
			this.acknowledgementTimer.Cancel();
		}

		// Token: 0x06005A59 RID: 23129 RVA: 0x0014B155 File Offset: 0x00149355
		protected override void OnQuotaAvailable()
		{
			this.OnAcknowledgementTimeoutElapsed(null);
		}

		// Token: 0x06005A5A RID: 23130 RVA: 0x0014B160 File Offset: 0x00149360
		public void ProcessDemuxedMessage(WsrmMessageInfo info)
		{
			try
			{
				this.ProcessMessage(info);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A5B RID: 23131 RVA: 0x0014B1A0 File Offset: 0x001493A0
		private void ProcessMessage(WsrmMessageInfo info)
		{
			bool flag = true;
			try
			{
				if (!base.ReliableSession.ProcessInfo(info, null))
				{
					flag = false;
				}
				else if (!base.ReliableSession.VerifySimplexProtocolElements(info, null))
				{
					flag = false;
				}
				else
				{
					base.ReliableSession.OnRemoteActivity(false);
					if (info.CreateSequenceInfo != null)
					{
						EndpointAddress acceptAcksTo;
						if (WsrmUtilities.ValidateCreateSequence<IInputSessionChannel>(info, base.Listener, base.Binder.Channel, out acceptAcksTo))
						{
							Message message = WsrmUtilities.CreateCreateSequenceResponse(base.Listener.MessageVersion, base.Listener.ReliableMessagingVersion, false, info.CreateSequenceInfo, base.Listener.Ordered, base.ReliableSession.InputID, acceptAcksTo);
							using (message)
							{
								if (base.Binder.AddressResponse(info.Message, message))
								{
									base.Binder.Send(message, base.DefaultSendTimeout);
								}
								return;
							}
						}
						base.ReliableSession.OnLocalFault(info.FaultException, info.FaultReply, null);
					}
					else
					{
						bool flag2 = false;
						bool flag3 = false;
						bool flag4 = info.AckRequestedInfo != null;
						bool flag5 = false;
						Message message3 = null;
						WsrmFault wsrmFault = null;
						Exception ex = null;
						bool flag6 = base.Listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
						bool flag7 = base.Listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
						if (info.SequencedMessageInfo != null)
						{
							object thisLock = base.ThisLock;
							lock (thisLock)
							{
								if (base.Aborted || base.State == CommunicationState.Faulted)
								{
									return;
								}
								long sequenceNumber = info.SequencedMessageInfo.SequenceNumber;
								bool isLast = flag6 && info.SequencedMessageInfo.LastMessage;
								if (!base.Connection.IsValid(sequenceNumber, isLast))
								{
									if (flag6)
									{
										wsrmFault = new LastMessageNumberExceededFault(base.ReliableSession.InputID);
									}
									else
									{
										message3 = new SequenceClosedFault(base.ReliableSession.InputID).CreateMessage(base.Listener.MessageVersion, base.Listener.ReliableMessagingVersion);
										flag4 = true;
										if (PerformanceCounters.PerformanceCountersEnabled)
										{
											PerformanceCounters.MessageDropped(this.perfCounterId);
										}
									}
								}
								else if (base.Connection.Ranges.Contains(sequenceNumber))
								{
									if (PerformanceCounters.PerformanceCountersEnabled)
									{
										PerformanceCounters.MessageDropped(this.perfCounterId);
									}
									flag4 = true;
								}
								else if (flag6 && info.Action == "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage")
								{
									base.Connection.Merge(sequenceNumber, isLast);
									if (base.Connection.AllAdded)
									{
										flag3 = true;
										base.ReliableSession.CloseSession();
									}
								}
								else if (base.State == CommunicationState.Closing)
								{
									if (flag6)
									{
										wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedSessionClosedBeforeDone"), SR.GetString("SessionClosedBeforeDone"));
									}
									else
									{
										message3 = new SequenceClosedFault(base.ReliableSession.InputID).CreateMessage(base.Listener.MessageVersion, base.Listener.ReliableMessagingVersion);
										flag4 = true;
										if (PerformanceCounters.PerformanceCountersEnabled)
										{
											PerformanceCounters.MessageDropped(this.perfCounterId);
										}
									}
								}
								else if (base.DeliveryStrategy.CanEnqueue(sequenceNumber) && (base.Listener.Ordered || base.Connection.CanMerge(sequenceNumber)))
								{
									base.Connection.Merge(sequenceNumber, isLast);
									flag2 = base.DeliveryStrategy.Enqueue(info.Message, sequenceNumber);
									flag = false;
									this.pendingAcknowledgements++;
									if (this.pendingAcknowledgements == base.Listener.MaxTransferWindowSize)
									{
										flag4 = true;
									}
									if (base.Connection.AllAdded)
									{
										flag3 = true;
										base.ReliableSession.CloseSession();
									}
								}
								else if (PerformanceCounters.PerformanceCountersEnabled)
								{
									PerformanceCounters.MessageDropped(this.perfCounterId);
								}
								if (base.Connection.IsLastKnown)
								{
									flag4 = true;
								}
								if (!flag4 && this.pendingAcknowledgements > 0 && !this.acknowledgementScheduled && wsrmFault == null)
								{
									this.acknowledgementScheduled = true;
									this.acknowledgementTimer.Set(this.acknowledgementInterval);
								}
								goto IL_638;
							}
						}
						if (flag6 && info.TerminateSequenceInfo != null)
						{
							object thisLock2 = base.ThisLock;
							bool flag10;
							lock (thisLock2)
							{
								flag10 = !base.Connection.Terminate();
							}
							if (flag10)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedEarlyTerminateSequence"), SR.GetString("EarlyTerminateSequence"));
							}
						}
						else if (flag7 && (info.TerminateSequenceInfo != null || info.CloseSequenceInfo != null))
						{
							bool flag11 = info.TerminateSequenceInfo != null;
							WsrmRequestInfo wsrmRequestInfo = flag11 ? info.TerminateSequenceInfo : info.CloseSequenceInfo;
							long num = flag11 ? info.TerminateSequenceInfo.LastMsgNumber : info.CloseSequenceInfo.LastMsgNumber;
							if (!WsrmUtilities.ValidateWsrmRequest(base.ReliableSession, wsrmRequestInfo, base.Binder, null))
							{
								return;
							}
							bool flag12 = true;
							bool flag13 = true;
							object thisLock3 = base.ThisLock;
							lock (thisLock3)
							{
								if (!base.Connection.IsLastKnown)
								{
									if (flag11)
									{
										if (base.Connection.SetTerminateSequenceLast(num, out flag12))
										{
											flag3 = true;
										}
										else if (flag12)
										{
											ex = new ProtocolException(SR.GetString("EarlyTerminateSequence"));
										}
									}
									else
									{
										flag3 = base.Connection.SetCloseSequenceLast(num);
										flag12 = flag3;
									}
									if (flag3)
									{
										base.ReliableSession.SetFinalAck(base.Connection.Ranges);
										base.DeliveryStrategy.Dispose();
									}
								}
								else
								{
									flag13 = (num == base.Connection.Last);
									if (flag11 && flag13 && base.Connection.IsSequenceClosed)
									{
										flag5 = true;
									}
								}
							}
							if (!flag12)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedSmallLastMsgNumber"), SR.GetString("SmallLastMsgNumberExceptionString"));
							}
							else if (!flag13)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedInconsistentLastMsgNumber"), SR.GetString("InconsistentLastMsgNumberExceptionString"));
							}
							else
							{
								message3 = (flag11 ? WsrmUtilities.CreateTerminateResponseMessage(base.Listener.MessageVersion, wsrmRequestInfo.MessageId, base.ReliableSession.InputID) : WsrmUtilities.CreateCloseSequenceResponse(base.Listener.MessageVersion, wsrmRequestInfo.MessageId, base.ReliableSession.InputID));
								flag4 = true;
							}
						}
						IL_638:
						if (wsrmFault != null)
						{
							base.ReliableSession.OnLocalFault(wsrmFault.CreateException(), wsrmFault, null);
						}
						else
						{
							if (flag4)
							{
								object thisLock4 = base.ThisLock;
								lock (thisLock4)
								{
									if (this.acknowledgementScheduled)
									{
										this.acknowledgementTimer.Cancel();
										this.acknowledgementScheduled = false;
									}
									this.pendingAcknowledgements = 0;
								}
								if (message3 != null)
								{
									base.AddAcknowledgementHeader(message3);
								}
								else
								{
									message3 = base.CreateAcknowledgmentMessage();
								}
							}
							if (message3 != null)
							{
								using (message3)
								{
									if (this.guard.Enter())
									{
										try
										{
											base.Binder.Send(message3, base.DefaultSendTimeout);
										}
										finally
										{
											this.guard.Exit();
										}
									}
								}
							}
							if (flag5)
							{
								object thisLock5 = base.ThisLock;
								lock (thisLock5)
								{
									base.Connection.Terminate();
								}
							}
							if (ex != null)
							{
								base.ReliableSession.OnRemoteFault(ex);
							}
							else
							{
								if (flag2)
								{
									base.Dispatch();
								}
								if (flag3)
								{
									ActionItem.Schedule(new Action<object>(base.ShutdownCallback), null);
								}
							}
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
			}
		}

		// Token: 0x0400368D RID: 13965
		private TimeSpan acknowledgementInterval;

		// Token: 0x0400368E RID: 13966
		private bool acknowledgementScheduled;

		// Token: 0x0400368F RID: 13967
		private IOThreadTimer acknowledgementTimer;

		// Token: 0x04003690 RID: 13968
		private Guard guard = new Guard(int.MaxValue);

		// Token: 0x04003691 RID: 13969
		private int pendingAcknowledgements;
	}
}
