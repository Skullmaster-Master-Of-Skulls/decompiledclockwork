using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Serialization;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096B RID: 2411
	internal sealed class WsrmMessageInfo
	{
		// Token: 0x17001651 RID: 5713
		// (get) Token: 0x06005D7A RID: 23930 RVA: 0x00159925 File Offset: 0x00157B25
		public WsrmAcknowledgmentInfo AcknowledgementInfo
		{
			get
			{
				return this.acknowledgementInfo;
			}
		}

		// Token: 0x17001652 RID: 5714
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x0015992D File Offset: 0x00157B2D
		public WsrmAckRequestedInfo AckRequestedInfo
		{
			get
			{
				return this.ackRequestedInfo;
			}
		}

		// Token: 0x17001653 RID: 5715
		// (get) Token: 0x06005D7C RID: 23932 RVA: 0x00159935 File Offset: 0x00157B35
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17001654 RID: 5716
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x0015993D File Offset: 0x00157B3D
		public CloseSequenceInfo CloseSequenceInfo
		{
			get
			{
				return this.closeSequenceInfo;
			}
		}

		// Token: 0x17001655 RID: 5717
		// (get) Token: 0x06005D7E RID: 23934 RVA: 0x00159945 File Offset: 0x00157B45
		public CloseSequenceResponseInfo CloseSequenceResponseInfo
		{
			get
			{
				return this.closeSequenceResponseInfo;
			}
		}

		// Token: 0x17001656 RID: 5718
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x0015994D File Offset: 0x00157B4D
		public CreateSequenceInfo CreateSequenceInfo
		{
			get
			{
				return this.createSequenceInfo;
			}
		}

		// Token: 0x17001657 RID: 5719
		// (get) Token: 0x06005D80 RID: 23936 RVA: 0x00159955 File Offset: 0x00157B55
		public CreateSequenceResponseInfo CreateSequenceResponseInfo
		{
			get
			{
				return this.createSequenceResponseInfo;
			}
		}

		// Token: 0x17001658 RID: 5720
		// (get) Token: 0x06005D81 RID: 23937 RVA: 0x0015995D File Offset: 0x00157B5D
		// (set) Token: 0x06005D82 RID: 23938 RVA: 0x00159965 File Offset: 0x00157B65
		public Exception FaultException
		{
			get
			{
				return this.faultException;
			}
			set
			{
				if (this.faultException != null)
				{
					throw Fx.AssertAndThrow("FaultException can only be set once.");
				}
				this.faultException = value;
			}
		}

		// Token: 0x17001659 RID: 5721
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x00159981 File Offset: 0x00157B81
		public MessageFault FaultInfo
		{
			get
			{
				return this.faultInfo;
			}
		}

		// Token: 0x1700165A RID: 5722
		// (get) Token: 0x06005D84 RID: 23940 RVA: 0x00159989 File Offset: 0x00157B89
		// (set) Token: 0x06005D85 RID: 23941 RVA: 0x00159991 File Offset: 0x00157B91
		public Message FaultReply
		{
			get
			{
				return this.faultReply;
			}
			set
			{
				if (this.faultReply != null)
				{
					throw Fx.AssertAndThrow("FaultReply can only be set once.");
				}
				this.faultReply = value;
			}
		}

		// Token: 0x1700165B RID: 5723
		// (get) Token: 0x06005D86 RID: 23942 RVA: 0x001599AD File Offset: 0x00157BAD
		public Message Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x1700165C RID: 5724
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x001599B5 File Offset: 0x00157BB5
		public MessageFault MessageFault
		{
			get
			{
				return this.faultInfo;
			}
		}

		// Token: 0x1700165D RID: 5725
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x001599BD File Offset: 0x00157BBD
		public Exception ParsingException
		{
			get
			{
				return this.parsingException;
			}
		}

		// Token: 0x1700165E RID: 5726
		// (get) Token: 0x06005D89 RID: 23945 RVA: 0x001599C5 File Offset: 0x00157BC5
		public WsrmSequencedMessageInfo SequencedMessageInfo
		{
			get
			{
				return this.sequencedMessageInfo;
			}
		}

		// Token: 0x1700165F RID: 5727
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x001599CD File Offset: 0x00157BCD
		public TerminateSequenceInfo TerminateSequenceInfo
		{
			get
			{
				return this.terminateSequenceInfo;
			}
		}

		// Token: 0x17001660 RID: 5728
		// (get) Token: 0x06005D8B RID: 23947 RVA: 0x001599D5 File Offset: 0x00157BD5
		public TerminateSequenceResponseInfo TerminateSequenceResponseInfo
		{
			get
			{
				return this.terminateSequenceResponseInfo;
			}
		}

		// Token: 0x17001661 RID: 5729
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x001599DD File Offset: 0x00157BDD
		public WsrmUsesSequenceSSLInfo UsesSequenceSSLInfo
		{
			get
			{
				return this.usesSequenceSSLInfo;
			}
		}

		// Token: 0x17001662 RID: 5730
		// (get) Token: 0x06005D8D RID: 23949 RVA: 0x001599E5 File Offset: 0x00157BE5
		public WsrmUsesSequenceSTRInfo UsesSequenceSTRInfo
		{
			get
			{
				return this.usesSequenceSTRInfo;
			}
		}

		// Token: 0x17001663 RID: 5731
		// (get) Token: 0x06005D8E RID: 23950 RVA: 0x001599ED File Offset: 0x00157BED
		public WsrmHeaderFault WsrmHeaderFault
		{
			get
			{
				return this.faultInfo as WsrmHeaderFault;
			}
		}

		// Token: 0x06005D8F RID: 23951 RVA: 0x001599FA File Offset: 0x00157BFA
		public static Exception CreateInternalFaultException(Message faultReply, string message, Exception inner)
		{
			return new WsrmMessageInfo.InternalFaultException(faultReply, SR.GetString("WsrmMessageProcessingError", new object[]
			{
				message
			}), inner);
		}

		// Token: 0x06005D90 RID: 23952 RVA: 0x00159A18 File Offset: 0x00157C18
		private static Exception CreateWsrmRequiredException(MessageVersion messageVersion)
		{
			string @string = SR.GetString("WsrmRequiredExceptionString");
			string string2 = SR.GetString("WsrmRequiredFaultString");
			Message message = new WsrmRequiredFault(string2).CreateMessage(messageVersion, ReliableMessagingVersion.WSReliableMessaging11);
			return WsrmMessageInfo.CreateInternalFaultException(message, @string, new ProtocolException(@string));
		}

		// Token: 0x06005D91 RID: 23953 RVA: 0x00159A5A File Offset: 0x00157C5A
		public static WsrmMessageInfo Get(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, IChannel channel, ISession session, Message message)
		{
			return WsrmMessageInfo.Get(messageVersion, reliableMessagingVersion, channel, session, message, false);
		}

		// Token: 0x06005D92 RID: 23954 RVA: 0x00159A68 File Offset: 0x00157C68
		public static WsrmMessageInfo Get(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, IChannel channel, ISession session, Message message, bool csrOnly)
		{
			WsrmMessageInfo wsrmMessageInfo = new WsrmMessageInfo();
			wsrmMessageInfo.message = message;
			bool flag = true;
			try
			{
				flag = message.IsFault;
				MessageHeaders headers = message.Headers;
				string text = headers.Action;
				wsrmMessageInfo.action = text;
				bool flag2 = false;
				bool flag3 = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005;
				bool flag4 = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
				bool flag5 = false;
				if (text == WsrmIndex.GetCreateSequenceResponseActionString(reliableMessagingVersion))
				{
					wsrmMessageInfo.createSequenceResponseInfo = CreateSequenceResponseInfo.ReadMessage(messageVersion, reliableMessagingVersion, message, headers);
					WsrmMessageInfo.ValidateMustUnderstand(messageVersion, message);
					return wsrmMessageInfo;
				}
				if (csrOnly)
				{
					return wsrmMessageInfo;
				}
				if (text == WsrmIndex.GetTerminateSequenceActionString(reliableMessagingVersion))
				{
					wsrmMessageInfo.terminateSequenceInfo = TerminateSequenceInfo.ReadMessage(messageVersion, reliableMessagingVersion, message, headers);
					flag2 = true;
				}
				else if (text == WsrmIndex.GetCreateSequenceActionString(reliableMessagingVersion))
				{
					wsrmMessageInfo.createSequenceInfo = CreateSequenceInfo.ReadMessage(messageVersion, reliableMessagingVersion, session as ISecureConversationSession, message, headers);
					if (flag3)
					{
						WsrmMessageInfo.ValidateMustUnderstand(messageVersion, message);
						return wsrmMessageInfo;
					}
					flag5 = true;
				}
				else if (flag4)
				{
					if (text == "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequence")
					{
						wsrmMessageInfo.closeSequenceInfo = CloseSequenceInfo.ReadMessage(messageVersion, message, headers);
						flag2 = true;
					}
					else if (text == "http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse")
					{
						wsrmMessageInfo.closeSequenceResponseInfo = CloseSequenceResponseInfo.ReadMessage(messageVersion, message, headers);
						flag2 = true;
					}
					else if (text == WsrmIndex.GetTerminateSequenceResponseActionString(reliableMessagingVersion))
					{
						wsrmMessageInfo.terminateSequenceResponseInfo = TerminateSequenceResponseInfo.ReadMessage(messageVersion, message, headers);
						flag2 = true;
					}
				}
				string namespaceString = WsrmIndex.GetNamespaceString(reliableMessagingVersion);
				bool flag6 = messageVersion.Envelope == EnvelopeVersion.Soap11;
				bool flag7 = false;
				int num = -1;
				int num2 = -1;
				int num3 = -1;
				int num4 = -1;
				int num5 = -1;
				int num6 = -1;
				int num7 = -1;
				int num8 = -1;
				int num9 = -1;
				for (int i = 0; i < headers.Count; i++)
				{
					MessageHeaderInfo messageHeaderInfo = headers[i];
					if (messageVersion.Envelope.IsUltimateDestinationActor(messageHeaderInfo.Actor) && messageHeaderInfo.Namespace == namespaceString)
					{
						bool flag8 = true;
						if (flag5)
						{
							if (flag4 && messageHeaderInfo.Name == "UsesSequenceSSL")
							{
								if (num8 != -1)
								{
									num = i;
									break;
								}
								num8 = i;
							}
							else if (flag4 && messageHeaderInfo.Name == "UsesSequenceSTR")
							{
								if (num9 != -1)
								{
									num = i;
									break;
								}
								num9 = i;
							}
							else
							{
								flag8 = false;
							}
						}
						else if (messageHeaderInfo.Name == "Sequence")
						{
							if (num2 != -1)
							{
								num = i;
								break;
							}
							num2 = i;
						}
						else if (messageHeaderInfo.Name == "SequenceAcknowledgement")
						{
							if (num3 != -1)
							{
								num = i;
								break;
							}
							num3 = i;
						}
						else if (messageHeaderInfo.Name == "AckRequested")
						{
							if (num4 != -1)
							{
								num = i;
								break;
							}
							num4 = i;
						}
						else if (flag6 && messageHeaderInfo.Name == "SequenceFault")
						{
							if (num7 != -1)
							{
								num = i;
								break;
							}
							num7 = i;
						}
						else
						{
							flag8 = false;
						}
						if (flag8)
						{
							if (i > num5)
							{
								num5 = i;
							}
							if (num6 == -1)
							{
								num6 = i;
							}
						}
					}
				}
				if (num != -1)
				{
					Collection<MessageHeaderInfo> collection = new Collection<MessageHeaderInfo>();
					collection.Add(headers[num]);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MustUnderstandSoapException(collection, messageVersion.Envelope));
				}
				if (num5 > -1)
				{
					BufferedMessage bufferedMessage = message as BufferedMessage;
					if (bufferedMessage != null && bufferedMessage.Headers.ContainsOnlyBufferedMessageHeaders)
					{
						flag7 = true;
						using (XmlDictionaryReader readerAtHeader = headers.GetReaderAtHeader(num6))
						{
							for (int j = num6; j <= num5; j++)
							{
								MessageHeaderInfo messageHeaderInfo2 = headers[j];
								if (flag5)
								{
									if (flag4 && j == num8)
									{
										wsrmMessageInfo.usesSequenceSSLInfo = WsrmUsesSequenceSSLInfo.ReadHeader(readerAtHeader, messageHeaderInfo2);
										headers.UnderstoodHeaders.Add(messageHeaderInfo2);
									}
									else if (flag4 && j == num9)
									{
										wsrmMessageInfo.usesSequenceSTRInfo = WsrmUsesSequenceSTRInfo.ReadHeader(readerAtHeader, messageHeaderInfo2);
										headers.UnderstoodHeaders.Add(messageHeaderInfo2);
									}
									else
									{
										readerAtHeader.Skip();
									}
								}
								else if (j == num2)
								{
									wsrmMessageInfo.sequencedMessageInfo = WsrmSequencedMessageInfo.ReadHeader(reliableMessagingVersion, readerAtHeader, messageHeaderInfo2);
									headers.UnderstoodHeaders.Add(messageHeaderInfo2);
								}
								else if (j == num3)
								{
									wsrmMessageInfo.acknowledgementInfo = WsrmAcknowledgmentInfo.ReadHeader(reliableMessagingVersion, readerAtHeader, messageHeaderInfo2);
									headers.UnderstoodHeaders.Add(messageHeaderInfo2);
								}
								else if (j == num4)
								{
									wsrmMessageInfo.ackRequestedInfo = WsrmAckRequestedInfo.ReadHeader(reliableMessagingVersion, readerAtHeader, messageHeaderInfo2);
									headers.UnderstoodHeaders.Add(messageHeaderInfo2);
								}
								else
								{
									readerAtHeader.Skip();
								}
							}
						}
					}
				}
				if (num5 > -1 && !flag7)
				{
					flag7 = true;
					if (flag5)
					{
						if (num8 != -1)
						{
							using (XmlDictionaryReader readerAtHeader2 = headers.GetReaderAtHeader(num8))
							{
								MessageHeaderInfo messageHeaderInfo3 = headers[num8];
								wsrmMessageInfo.usesSequenceSSLInfo = WsrmUsesSequenceSSLInfo.ReadHeader(readerAtHeader2, messageHeaderInfo3);
								headers.UnderstoodHeaders.Add(messageHeaderInfo3);
							}
						}
						if (num9 == -1)
						{
							goto IL_5CB;
						}
						using (XmlDictionaryReader readerAtHeader3 = headers.GetReaderAtHeader(num9))
						{
							MessageHeaderInfo messageHeaderInfo4 = headers[num9];
							wsrmMessageInfo.usesSequenceSTRInfo = WsrmUsesSequenceSTRInfo.ReadHeader(readerAtHeader3, messageHeaderInfo4);
							headers.UnderstoodHeaders.Add(messageHeaderInfo4);
							goto IL_5CB;
						}
					}
					if (num2 != -1)
					{
						using (XmlDictionaryReader readerAtHeader4 = headers.GetReaderAtHeader(num2))
						{
							MessageHeaderInfo messageHeaderInfo5 = headers[num2];
							wsrmMessageInfo.sequencedMessageInfo = WsrmSequencedMessageInfo.ReadHeader(reliableMessagingVersion, readerAtHeader4, messageHeaderInfo5);
							headers.UnderstoodHeaders.Add(messageHeaderInfo5);
						}
					}
					if (num3 != -1)
					{
						using (XmlDictionaryReader readerAtHeader5 = headers.GetReaderAtHeader(num3))
						{
							MessageHeaderInfo messageHeaderInfo6 = headers[num3];
							wsrmMessageInfo.acknowledgementInfo = WsrmAcknowledgmentInfo.ReadHeader(reliableMessagingVersion, readerAtHeader5, messageHeaderInfo6);
							headers.UnderstoodHeaders.Add(messageHeaderInfo6);
						}
					}
					if (num4 != -1)
					{
						using (XmlDictionaryReader readerAtHeader6 = headers.GetReaderAtHeader(num4))
						{
							MessageHeaderInfo messageHeaderInfo7 = headers[num4];
							wsrmMessageInfo.ackRequestedInfo = WsrmAckRequestedInfo.ReadHeader(reliableMessagingVersion, readerAtHeader6, messageHeaderInfo7);
							headers.UnderstoodHeaders.Add(messageHeaderInfo7);
						}
					}
				}
				IL_5CB:
				if (flag5)
				{
					CreateSequenceInfo.ValidateCreateSequenceHeaders(messageVersion, session as ISecureConversationSession, wsrmMessageInfo);
					WsrmMessageInfo.ValidateMustUnderstand(messageVersion, message);
					return wsrmMessageInfo;
				}
				if (wsrmMessageInfo.sequencedMessageInfo == null && wsrmMessageInfo.action == null)
				{
					if (flag3)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("NoActionNoSequenceHeaderReason"), messageVersion.Addressing.Namespace, "Action", false));
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateWsrmRequiredException(messageVersion));
				}
				else
				{
					if (wsrmMessageInfo.sequencedMessageInfo == null && message.IsFault)
					{
						wsrmMessageInfo.faultInfo = MessageFault.CreateFault(message, 65536);
						WsrmHeaderFault wsrmHeaderFault;
						if (flag6)
						{
							if (WsrmHeaderFault.TryCreateFault11(reliableMessagingVersion, message, wsrmMessageInfo.faultInfo, num7, out wsrmHeaderFault))
							{
								wsrmMessageInfo.faultInfo = wsrmHeaderFault;
								wsrmMessageInfo.faultException = WsrmFault.CreateException(wsrmHeaderFault);
							}
						}
						else if (WsrmHeaderFault.TryCreateFault12(reliableMessagingVersion, message, wsrmMessageInfo.faultInfo, out wsrmHeaderFault))
						{
							wsrmMessageInfo.faultInfo = wsrmHeaderFault;
							wsrmMessageInfo.faultException = WsrmFault.CreateException(wsrmHeaderFault);
						}
						if (wsrmHeaderFault == null)
						{
							FaultConverter faultConverter = channel.GetProperty<FaultConverter>();
							if (faultConverter == null)
							{
								faultConverter = FaultConverter.GetDefaultFaultConverter(messageVersion);
							}
							if (!faultConverter.TryCreateException(message, wsrmMessageInfo.faultInfo, out wsrmMessageInfo.faultException))
							{
								wsrmMessageInfo.faultException = new ProtocolException(SR.GetString("UnrecognizedFaultReceived", new object[]
								{
									wsrmMessageInfo.faultInfo.Code.Namespace,
									wsrmMessageInfo.faultInfo.Code.Name,
									System.ServiceModel.FaultException.GetSafeReasonText(wsrmMessageInfo.faultInfo)
								}));
							}
						}
						flag2 = true;
					}
					if (!flag7 && !flag2)
					{
						if (flag3)
						{
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ActionNotSupportedException(SR.GetString("NonWsrmFeb2005ActionNotSupported", new object[]
							{
								text
							})));
						}
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateWsrmRequiredException(messageVersion));
					}
					else if (flag2 || WsrmUtilities.IsWsrmAction(reliableMessagingVersion, text))
					{
						WsrmMessageInfo.ValidateMustUnderstand(messageVersion, message);
					}
				}
			}
			catch (WsrmMessageInfo.InternalFaultException ex)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				}
				wsrmMessageInfo.FaultReply = ex.FaultReply;
				wsrmMessageInfo.faultException = ex.InnerException;
			}
			catch (CommunicationException ex2)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
				}
				if (flag)
				{
					wsrmMessageInfo.parsingException = ex2;
					return wsrmMessageInfo;
				}
				FaultConverter faultConverter2 = channel.GetProperty<FaultConverter>();
				if (faultConverter2 == null)
				{
					faultConverter2 = FaultConverter.GetDefaultFaultConverter(messageVersion);
				}
				if (faultConverter2.TryCreateFaultMessage(ex2, out wsrmMessageInfo.faultReply))
				{
					wsrmMessageInfo.faultException = new ProtocolException(SR.GetString("MessageExceptionOccurred"), ex2);
				}
				else
				{
					wsrmMessageInfo.parsingException = new ProtocolException(SR.GetString("MessageExceptionOccurred"), ex2);
				}
			}
			catch (XmlException ex3)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Information);
				}
				wsrmMessageInfo.parsingException = new ProtocolException(SR.GetString("MessageExceptionOccurred"), ex3);
			}
			return wsrmMessageInfo;
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x0015A3C4 File Offset: 0x001585C4
		private static void ValidateMustUnderstand(MessageVersion version, Message message)
		{
			Collection<MessageHeaderInfo> headersNotUnderstood = message.Headers.GetHeadersNotUnderstood();
			if (headersNotUnderstood != null && headersNotUnderstood.Count > 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MustUnderstandSoapException(headersNotUnderstood, version.Envelope));
			}
		}

		// Token: 0x0400378B RID: 14219
		private WsrmAcknowledgmentInfo acknowledgementInfo;

		// Token: 0x0400378C RID: 14220
		private WsrmAckRequestedInfo ackRequestedInfo;

		// Token: 0x0400378D RID: 14221
		private string action;

		// Token: 0x0400378E RID: 14222
		private CloseSequenceInfo closeSequenceInfo;

		// Token: 0x0400378F RID: 14223
		private CloseSequenceResponseInfo closeSequenceResponseInfo;

		// Token: 0x04003790 RID: 14224
		private CreateSequenceInfo createSequenceInfo;

		// Token: 0x04003791 RID: 14225
		private CreateSequenceResponseInfo createSequenceResponseInfo;

		// Token: 0x04003792 RID: 14226
		private Exception faultException;

		// Token: 0x04003793 RID: 14227
		private MessageFault faultInfo;

		// Token: 0x04003794 RID: 14228
		private Message faultReply;

		// Token: 0x04003795 RID: 14229
		private Message message;

		// Token: 0x04003796 RID: 14230
		private Exception parsingException;

		// Token: 0x04003797 RID: 14231
		private WsrmSequencedMessageInfo sequencedMessageInfo;

		// Token: 0x04003798 RID: 14232
		private TerminateSequenceInfo terminateSequenceInfo;

		// Token: 0x04003799 RID: 14233
		private TerminateSequenceResponseInfo terminateSequenceResponseInfo;

		// Token: 0x0400379A RID: 14234
		private WsrmUsesSequenceSSLInfo usesSequenceSSLInfo;

		// Token: 0x0400379B RID: 14235
		private WsrmUsesSequenceSTRInfo usesSequenceSTRInfo;

		// Token: 0x02000DEF RID: 3567
		[Serializable]
		private class InternalFaultException : ProtocolException
		{
			// Token: 0x060080E2 RID: 32994 RVA: 0x001DE7DE File Offset: 0x001DC9DE
			public InternalFaultException()
			{
			}

			// Token: 0x060080E3 RID: 32995 RVA: 0x001DE7E6 File Offset: 0x001DC9E6
			public InternalFaultException(Message faultReply, string message, Exception inner) : base(message, inner)
			{
				this.faultReply = faultReply;
			}

			// Token: 0x060080E4 RID: 32996 RVA: 0x001DE7F7 File Offset: 0x001DC9F7
			protected InternalFaultException(SerializationInfo info, StreamingContext context) : base(info, context)
			{
			}

			// Token: 0x17001C7E RID: 7294
			// (get) Token: 0x060080E5 RID: 32997 RVA: 0x001DE801 File Offset: 0x001DCA01
			public Message FaultReply
			{
				get
				{
					return this.faultReply;
				}
			}

			// Token: 0x0400497D RID: 18813
			private Message faultReply;
		}
	}
}
