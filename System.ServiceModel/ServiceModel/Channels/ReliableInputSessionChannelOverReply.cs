using System;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200092E RID: 2350
	internal sealed class ReliableInputSessionChannelOverReply : ReliableInputSessionChannel
	{
		// Token: 0x06005A5C RID: 23132 RVA: 0x0014B9F4 File Offset: 0x00149BF4
		public ReliableInputSessionChannelOverReply(ReliableChannelListenerBase<IInputSessionChannel> listener, IServerReliableChannelBinder binder, FaultHelper faultHelper, UniqueId inputID) : base(listener, binder, faultHelper, inputID)
		{
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

		// Token: 0x06005A5D RID: 23133 RVA: 0x0014BA48 File Offset: 0x00149C48
		protected override bool HandleReceiveComplete(IAsyncResult result)
		{
			RequestContext requestContext;
			bool flag = base.Binder.EndTryReceive(result, out requestContext);
			if (!flag)
			{
				return true;
			}
			if (requestContext == null)
			{
				bool flag2 = false;
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					flag2 = base.Connection.Terminate();
				}
				if (!flag2 && base.Binder.State == CommunicationState.Opened)
				{
					Exception e = new CommunicationException(SR.GetString("EarlySecurityClose"));
					base.ReliableSession.OnLocalFault(e, null, null);
				}
				return false;
			}
			WsrmMessageInfo info = WsrmMessageInfo.Get(base.Listener.MessageVersion, base.Listener.ReliableMessagingVersion, base.Binder.Channel, base.Binder.GetInnerSession(), requestContext.RequestMessage);
			base.StartReceiving(false);
			this.ProcessRequest(requestContext, info);
			return false;
		}

		// Token: 0x06005A5E RID: 23134 RVA: 0x0014BB2C File Offset: 0x00149D2C
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
				base.ReliableSession.OnUnknownException(ex);
			}
		}

		// Token: 0x06005A5F RID: 23135 RVA: 0x0014BB6C File Offset: 0x00149D6C
		private void ProcessRequest(RequestContext context, WsrmMessageInfo info)
		{
			bool flag = true;
			bool flag2 = true;
			try
			{
				if (!base.ReliableSession.ProcessInfo(info, context))
				{
					flag = false;
					flag2 = false;
				}
				else if (!base.ReliableSession.VerifySimplexProtocolElements(info, context))
				{
					flag = false;
					flag2 = false;
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
							try
							{
								using (message)
								{
									if (base.Binder.AddressResponse(info.Message, message))
									{
										context.Reply(message, base.DefaultSendTimeout);
									}
									goto IL_101;
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
						base.ReliableSession.OnLocalFault(info.FaultException, info.FaultReply, context);
						IL_101:
						flag = false;
						flag2 = false;
					}
					else
					{
						bool flag3 = false;
						bool flag4 = false;
						bool flag5 = false;
						WsrmFault wsrmFault = null;
						Message message3 = null;
						Exception ex = null;
						bool flag6 = base.Listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
						bool flag7 = base.Listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
						bool flag8 = info.AckRequestedInfo != null;
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
								}
								else if (flag6 && info.Action == "http://schemas.xmlsoap.org/ws/2005/02/rm/LastMessage")
								{
									base.Connection.Merge(sequenceNumber, isLast);
									flag4 = base.Connection.AllAdded;
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
										if (PerformanceCounters.PerformanceCountersEnabled)
										{
											PerformanceCounters.MessageDropped(this.perfCounterId);
										}
									}
								}
								else if (base.DeliveryStrategy.CanEnqueue(sequenceNumber) && (base.Listener.Ordered || base.Connection.CanMerge(sequenceNumber)))
								{
									base.Connection.Merge(sequenceNumber, isLast);
									flag3 = base.DeliveryStrategy.Enqueue(info.Message, sequenceNumber);
									flag4 = base.Connection.AllAdded;
									flag2 = false;
								}
								else if (PerformanceCounters.PerformanceCountersEnabled)
								{
									PerformanceCounters.MessageDropped(this.perfCounterId);
								}
								goto IL_5D0;
							}
						}
						if (flag6 && info.TerminateSequenceInfo != null)
						{
							object thisLock2 = base.ThisLock;
							bool flag11;
							lock (thisLock2)
							{
								flag11 = !base.Connection.Terminate();
							}
							if (!flag11)
							{
								return;
							}
							wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedEarlyTerminateSequence"), SR.GetString("EarlyTerminateSequence"));
						}
						else if (flag7 && (info.TerminateSequenceInfo != null || info.CloseSequenceInfo != null))
						{
							bool flag12 = info.TerminateSequenceInfo != null;
							WsrmRequestInfo wsrmRequestInfo = flag12 ? info.TerminateSequenceInfo : info.CloseSequenceInfo;
							long num = flag12 ? info.TerminateSequenceInfo.LastMsgNumber : info.CloseSequenceInfo.LastMsgNumber;
							if (!WsrmUtilities.ValidateWsrmRequest(base.ReliableSession, wsrmRequestInfo, base.Binder, context))
							{
								flag2 = false;
								flag = false;
								return;
							}
							bool flag13 = true;
							bool flag14 = true;
							object thisLock3 = base.ThisLock;
							lock (thisLock3)
							{
								if (!base.Connection.IsLastKnown)
								{
									if (flag12)
									{
										if (base.Connection.SetTerminateSequenceLast(num, out flag13))
										{
											flag4 = true;
										}
										else if (flag13)
										{
											ex = new ProtocolException(SR.GetString("EarlyTerminateSequence"));
										}
									}
									else
									{
										flag4 = base.Connection.SetCloseSequenceLast(num);
										flag13 = flag4;
									}
									if (flag4)
									{
										base.ReliableSession.SetFinalAck(base.Connection.Ranges);
										base.DeliveryStrategy.Dispose();
									}
								}
								else
								{
									flag14 = (num == base.Connection.Last);
									if (flag12 && flag14 && base.Connection.IsSequenceClosed)
									{
										flag5 = true;
									}
								}
							}
							if (!flag13)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedSmallLastMsgNumber"), SR.GetString("SmallLastMsgNumberExceptionString"));
							}
							else if (!flag14)
							{
								wsrmFault = SequenceTerminatedFault.CreateProtocolFault(base.ReliableSession.InputID, SR.GetString("SequenceTerminatedInconsistentLastMsgNumber"), SR.GetString("InconsistentLastMsgNumberExceptionString"));
							}
							else
							{
								message3 = (flag12 ? WsrmUtilities.CreateTerminateResponseMessage(base.Listener.MessageVersion, wsrmRequestInfo.MessageId, base.ReliableSession.InputID) : WsrmUtilities.CreateCloseSequenceResponse(base.Listener.MessageVersion, wsrmRequestInfo.MessageId, base.ReliableSession.InputID));
								flag8 = true;
							}
						}
						IL_5D0:
						if (wsrmFault != null)
						{
							base.ReliableSession.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
							flag2 = false;
							flag = false;
						}
						else
						{
							if (message3 != null && flag8)
							{
								base.AddAcknowledgementHeader(message3);
							}
							else if (message3 == null)
							{
								message3 = base.CreateAcknowledgmentMessage();
							}
							using (message3)
							{
								context.Reply(message3);
							}
							if (flag5)
							{
								object thisLock4 = base.ThisLock;
								lock (thisLock4)
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
								if (flag3)
								{
									base.Dispatch();
								}
								if (flag4)
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
				if (flag2)
				{
					info.Message.Close();
				}
				if (flag)
				{
					context.Close();
				}
			}
		}
	}
}
