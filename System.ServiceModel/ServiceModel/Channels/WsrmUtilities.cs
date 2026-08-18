using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200094A RID: 2378
	internal static class WsrmUtilities
	{
		// Token: 0x06005B64 RID: 23396 RVA: 0x0014F145 File Offset: 0x0014D345
		public static TimeSpan CalculateKeepAliveInterval(TimeSpan inactivityTimeout, int maxRetryCount)
		{
			return Ticks.ToTimeSpan(Ticks.FromTimeSpan(inactivityTimeout) / 2L / (long)maxRetryCount);
		}

		// Token: 0x06005B65 RID: 23397 RVA: 0x0014F158 File Offset: 0x0014D358
		internal static UniqueId NextSequenceId()
		{
			return new UniqueId();
		}

		// Token: 0x06005B66 RID: 23398 RVA: 0x0014F15F File Offset: 0x0014D35F
		internal static void AddAcknowledgementHeader(ReliableMessagingVersion reliableMessagingVersion, Message message, UniqueId id, SequenceRangeCollection ranges, bool final)
		{
			WsrmUtilities.AddAcknowledgementHeader(reliableMessagingVersion, message, id, ranges, final, -1);
		}

		// Token: 0x06005B67 RID: 23399 RVA: 0x0014F16D File Offset: 0x0014D36D
		internal static void AddAcknowledgementHeader(ReliableMessagingVersion reliableMessagingVersion, Message message, UniqueId id, SequenceRangeCollection ranges, bool final, int bufferRemaining)
		{
			message.Headers.Insert(0, new WsrmAcknowledgmentHeader(reliableMessagingVersion, id, ranges, final, bufferRemaining));
		}

		// Token: 0x06005B68 RID: 23400 RVA: 0x0014F187 File Offset: 0x0014D387
		internal static void AddAckRequestedHeader(ReliableMessagingVersion reliableMessagingVersion, Message message, UniqueId id)
		{
			message.Headers.Insert(0, new WsrmAckRequestedHeader(reliableMessagingVersion, id));
		}

		// Token: 0x06005B69 RID: 23401 RVA: 0x0014F19C File Offset: 0x0014D39C
		internal static void AddSequenceHeader(ReliableMessagingVersion reliableMessagingVersion, Message message, UniqueId id, long sequenceNumber, bool isLast)
		{
			message.Headers.Insert(0, new WsrmSequencedMessageHeader(reliableMessagingVersion, id, sequenceNumber, isLast));
		}

		// Token: 0x06005B6A RID: 23402 RVA: 0x0014F1B4 File Offset: 0x0014D3B4
		internal static void AssertWsrm11(ReliableMessagingVersion reliableMessagingVersion)
		{
			if (reliableMessagingVersion != ReliableMessagingVersion.WSReliableMessaging11)
			{
				throw Fx.AssertAndThrow("WS-ReliableMessaging 1.1 required.");
			}
		}

		// Token: 0x06005B6B RID: 23403 RVA: 0x0014F1CC File Offset: 0x0014D3CC
		internal static Message CreateAcknowledgmentMessage(MessageVersion version, ReliableMessagingVersion reliableMessagingVersion, UniqueId id, SequenceRangeCollection ranges, bool final, int bufferRemaining)
		{
			Message message = Message.CreateMessage(version, WsrmIndex.GetSequenceAcknowledgementActionHeader(version.Addressing, reliableMessagingVersion));
			WsrmUtilities.AddAcknowledgementHeader(reliableMessagingVersion, message, id, ranges, final, bufferRemaining);
			message.Properties.AllowOutputBatching = false;
			return message;
		}

		// Token: 0x06005B6C RID: 23404 RVA: 0x0014F208 File Offset: 0x0014D408
		internal static Message CreateAckRequestedMessage(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, UniqueId id)
		{
			Message message = Message.CreateMessage(messageVersion, WsrmIndex.GetAckRequestedActionHeader(messageVersion.Addressing, reliableMessagingVersion));
			WsrmUtilities.AddAckRequestedHeader(reliableMessagingVersion, message, id);
			message.Properties.AllowOutputBatching = false;
			return message;
		}

		// Token: 0x06005B6D RID: 23405 RVA: 0x0014F240 File Offset: 0x0014D440
		internal static Message CreateCloseSequenceResponse(MessageVersion messageVersion, UniqueId messageId, UniqueId inputId)
		{
			CloseSequenceResponse body = new CloseSequenceResponse(inputId);
			Message message = Message.CreateMessage(messageVersion, WsrmIndex.GetCloseSequenceResponseActionHeader(messageVersion.Addressing), body);
			message.Headers.RelatesTo = messageId;
			return message;
		}

		// Token: 0x06005B6E RID: 23406 RVA: 0x0014F274 File Offset: 0x0014D474
		internal static Message CreateCreateSequenceResponse(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, bool duplex, CreateSequenceInfo createSequenceInfo, bool ordered, UniqueId inputId, EndpointAddress acceptAcksTo)
		{
			CreateSequenceResponse createSequenceResponse = new CreateSequenceResponse(messageVersion.Addressing, reliableMessagingVersion);
			createSequenceResponse.Identifier = inputId;
			createSequenceResponse.Expires = createSequenceInfo.Expires;
			createSequenceResponse.Ordered = ordered;
			if (duplex)
			{
				createSequenceResponse.AcceptAcksTo = acceptAcksTo;
			}
			return Message.CreateMessage(messageVersion, ActionHeader.Create(WsrmIndex.GetCreateSequenceResponseAction(reliableMessagingVersion), messageVersion.Addressing), createSequenceResponse);
		}

		// Token: 0x06005B6F RID: 23407 RVA: 0x0014F2CF File Offset: 0x0014D4CF
		internal static Message CreateCSRefusedCommunicationFault(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, string reason)
		{
			return WsrmUtilities.CreateCSRefusedFault(messageVersion, reliableMessagingVersion, false, null, reason);
		}

		// Token: 0x06005B70 RID: 23408 RVA: 0x0014F2DB File Offset: 0x0014D4DB
		internal static Message CreateCSRefusedProtocolFault(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, string reason)
		{
			return WsrmUtilities.CreateCSRefusedFault(messageVersion, reliableMessagingVersion, true, null, reason);
		}

		// Token: 0x06005B71 RID: 23409 RVA: 0x0014F2E8 File Offset: 0x0014D4E8
		internal static Message CreateCSRefusedServerTooBusyFault(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, string reason)
		{
			FaultCode subCode = new FaultCode("ConnectionLimitReached", "http://schemas.microsoft.com/ws/2006/05/rm");
			subCode = new FaultCode("CreateSequenceRefused", WsrmIndex.GetNamespaceString(reliableMessagingVersion), subCode);
			return WsrmUtilities.CreateCSRefusedFault(messageVersion, reliableMessagingVersion, false, subCode, reason);
		}

		// Token: 0x06005B72 RID: 23410 RVA: 0x0014F324 File Offset: 0x0014D524
		private static Message CreateCSRefusedFault(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, bool isSenderFault, FaultCode subCode, string reason)
		{
			FaultCode code;
			if (messageVersion.Envelope == EnvelopeVersion.Soap11)
			{
				code = new FaultCode("CreateSequenceRefused", WsrmIndex.GetNamespaceString(reliableMessagingVersion));
			}
			else
			{
				if (messageVersion.Envelope != EnvelopeVersion.Soap12)
				{
					throw Fx.AssertAndThrow("Unsupported version.");
				}
				if (subCode == null)
				{
					subCode = new FaultCode("CreateSequenceRefused", WsrmIndex.GetNamespaceString(reliableMessagingVersion), subCode);
				}
				if (isSenderFault)
				{
					code = FaultCode.CreateSenderFaultCode(subCode);
				}
				else
				{
					code = FaultCode.CreateReceiverFaultCode(subCode);
				}
			}
			FaultReason reason2 = new FaultReason(SR.GetString("CSRefused", new object[]
			{
				reason
			}), CultureInfo.CurrentCulture);
			MessageFault fault = MessageFault.CreateFault(code, reason2);
			string faultActionString = WsrmIndex.GetFaultActionString(messageVersion.Addressing, reliableMessagingVersion);
			return Message.CreateMessage(messageVersion, fault, faultActionString);
		}

		// Token: 0x06005B73 RID: 23411 RVA: 0x0014F3D4 File Offset: 0x0014D5D4
		public static Exception CreateCSFaultException(MessageVersion version, ReliableMessagingVersion reliableMessagingVersion, Message message, IChannel innerChannel)
		{
			MessageFault messageFault = MessageFault.CreateFault(message, 65536);
			FaultCode code = messageFault.Code;
			FaultCode faultCode;
			if (version.Envelope == EnvelopeVersion.Soap11)
			{
				faultCode = code;
			}
			else
			{
				if (version.Envelope != EnvelopeVersion.Soap12)
				{
					throw Fx.AssertAndThrow("Unsupported version.");
				}
				faultCode = code.SubCode;
			}
			if (faultCode != null)
			{
				if (faultCode.Namespace == WsrmIndex.GetNamespaceString(reliableMessagingVersion) && faultCode.Name == "CreateSequenceRefused")
				{
					string safeReasonText = FaultException.GetSafeReasonText(messageFault);
					if (version.Envelope == EnvelopeVersion.Soap12)
					{
						FaultCode subCode = faultCode.SubCode;
						if (subCode != null && subCode.Namespace == "http://schemas.microsoft.com/ws/2006/05/rm" && subCode.Name == "ConnectionLimitReached")
						{
							return new ServerTooBusyException(safeReasonText);
						}
						if (code.IsSenderFault)
						{
							return new ProtocolException(safeReasonText);
						}
					}
					return new CommunicationException(safeReasonText);
				}
				if (faultCode.Namespace == version.Addressing.Namespace && faultCode.Name == "EndpointUnavailable")
				{
					return new EndpointNotFoundException(FaultException.GetSafeReasonText(messageFault));
				}
			}
			FaultConverter faultConverter = innerChannel.GetProperty<FaultConverter>();
			if (faultConverter == null)
			{
				faultConverter = FaultConverter.GetDefaultFaultConverter(version);
			}
			Exception result;
			if (faultConverter.TryCreateException(message, messageFault, out result))
			{
				return result;
			}
			return new ProtocolException(SR.GetString("UnrecognizedFaultReceivedOnOpen", new object[]
			{
				messageFault.Code.Namespace,
				messageFault.Code.Name,
				FaultException.GetSafeReasonText(messageFault)
			}));
		}

		// Token: 0x06005B74 RID: 23412 RVA: 0x0014F548 File Offset: 0x0014D748
		internal static Message CreateEndpointNotFoundFault(MessageVersion version, string reason)
		{
			FaultCode faultCode = new FaultCode("EndpointUnavailable", version.Addressing.Namespace);
			FaultCode code;
			if (version.Envelope == EnvelopeVersion.Soap11)
			{
				code = faultCode;
			}
			else
			{
				if (version.Envelope != EnvelopeVersion.Soap12)
				{
					throw Fx.AssertAndThrow("Unsupported version.");
				}
				code = FaultCode.CreateSenderFaultCode(faultCode);
			}
			FaultReason reason2 = new FaultReason(reason, CultureInfo.CurrentCulture);
			MessageFault fault = MessageFault.CreateFault(code, reason2);
			return Message.CreateMessage(version, fault, version.Addressing.DefaultFaultAction);
		}

		// Token: 0x06005B75 RID: 23413 RVA: 0x0014F5C3 File Offset: 0x0014D7C3
		internal static Message CreateTerminateMessage(MessageVersion version, ReliableMessagingVersion reliableMessagingVersion, UniqueId id)
		{
			return WsrmUtilities.CreateTerminateMessage(version, reliableMessagingVersion, id, -1L);
		}

		// Token: 0x06005B76 RID: 23414 RVA: 0x0014F5D0 File Offset: 0x0014D7D0
		internal static Message CreateTerminateMessage(MessageVersion version, ReliableMessagingVersion reliableMessagingVersion, UniqueId id, long last)
		{
			Message message = Message.CreateMessage(version, WsrmIndex.GetTerminateSequenceActionHeader(version.Addressing, reliableMessagingVersion), new TerminateSequence(reliableMessagingVersion, id, last));
			message.Properties.AllowOutputBatching = false;
			return message;
		}

		// Token: 0x06005B77 RID: 23415 RVA: 0x0014F608 File Offset: 0x0014D808
		internal static Message CreateTerminateResponseMessage(MessageVersion version, UniqueId messageId, UniqueId sequenceId)
		{
			Message message = Message.CreateMessage(version, WsrmIndex.GetTerminateSequenceResponseActionHeader(version.Addressing), new TerminateSequenceResponse(sequenceId));
			message.Properties.AllowOutputBatching = false;
			message.Headers.RelatesTo = messageId;
			return message;
		}

		// Token: 0x06005B78 RID: 23416 RVA: 0x0014F648 File Offset: 0x0014D848
		internal static UniqueId GetInputId(WsrmMessageInfo info)
		{
			if (info.TerminateSequenceInfo != null)
			{
				return info.TerminateSequenceInfo.Identifier;
			}
			if (info.SequencedMessageInfo != null)
			{
				return info.SequencedMessageInfo.SequenceID;
			}
			if (info.AckRequestedInfo != null)
			{
				return info.AckRequestedInfo.SequenceID;
			}
			if (info.WsrmHeaderFault != null && info.WsrmHeaderFault.FaultsInput)
			{
				return info.WsrmHeaderFault.SequenceID;
			}
			if (info.CloseSequenceInfo != null)
			{
				return info.CloseSequenceInfo.Identifier;
			}
			return null;
		}

		// Token: 0x06005B79 RID: 23417 RVA: 0x0014F6C8 File Offset: 0x0014D8C8
		internal static UniqueId GetOutputId(ReliableMessagingVersion reliableMessagingVersion, WsrmMessageInfo info)
		{
			if (info.AcknowledgementInfo != null)
			{
				return info.AcknowledgementInfo.SequenceID;
			}
			if (info.WsrmHeaderFault != null && info.WsrmHeaderFault.FaultsOutput)
			{
				return info.WsrmHeaderFault.SequenceID;
			}
			if (info.TerminateSequenceResponseInfo != null)
			{
				return info.TerminateSequenceResponseInfo.Identifier;
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				if (info.CloseSequenceInfo != null)
				{
					return info.CloseSequenceInfo.Identifier;
				}
				if (info.CloseSequenceResponseInfo != null)
				{
					return info.CloseSequenceResponseInfo.Identifier;
				}
				if (info.TerminateSequenceResponseInfo != null)
				{
					return info.TerminateSequenceResponseInfo.Identifier;
				}
			}
			return null;
		}

		// Token: 0x06005B7A RID: 23418 RVA: 0x0014F763 File Offset: 0x0014D963
		internal static bool IsWsrmAction(ReliableMessagingVersion reliableMessagingVersion, string action)
		{
			return action != null && action.StartsWith(WsrmIndex.GetNamespaceString(reliableMessagingVersion), StringComparison.Ordinal);
		}

		// Token: 0x06005B7B RID: 23419 RVA: 0x0014F777 File Offset: 0x0014D977
		public static void ReadEmptyElement(XmlDictionaryReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			reader.Read();
			reader.ReadEndElement();
		}

		// Token: 0x06005B7C RID: 23420 RVA: 0x0014F798 File Offset: 0x0014D998
		public static UniqueId ReadIdentifier(XmlDictionaryReader reader, ReliableMessagingVersion reliableMessagingVersion)
		{
			reader.ReadStartElement(XD.WsrmFeb2005Dictionary.Identifier, WsrmIndex.GetNamespace(reliableMessagingVersion));
			UniqueId result = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			return result;
		}

		// Token: 0x06005B7D RID: 23421 RVA: 0x0014F7C9 File Offset: 0x0014D9C9
		public static long ReadSequenceNumber(XmlDictionaryReader reader)
		{
			return WsrmUtilities.ReadSequenceNumber(reader, false);
		}

		// Token: 0x06005B7E RID: 23422 RVA: 0x0014F7D4 File Offset: 0x0014D9D4
		public static long ReadSequenceNumber(XmlDictionaryReader reader, bool allowZero)
		{
			long num = reader.ReadContentAsLong();
			if (num < 0L || (num == 0L && !allowZero))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(SR.GetString("InvalidSequenceNumber", new object[]
				{
					num
				})));
			}
			return num;
		}

		// Token: 0x06005B7F RID: 23423 RVA: 0x0014F820 File Offset: 0x0014DA20
		public static WsrmFault ValidateCloseSequenceResponse(ChannelReliableSession session, UniqueId messageId, WsrmMessageInfo info, long last)
		{
			string @string;
			string string2;
			if (info.CloseSequenceResponseInfo == null)
			{
				@string = SR.GetString("InvalidWsrmResponseSessionFaultedExceptionString", new object[]
				{
					"CloseSequence",
					info.Action,
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse"
				});
				string2 = SR.GetString("InvalidWsrmResponseSessionFaultedFaultString", new object[]
				{
					"CloseSequence",
					info.Action,
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse"
				});
			}
			else if (!object.Equals(messageId, info.CloseSequenceResponseInfo.RelatesTo))
			{
				@string = SR.GetString("WsrmMessageWithWrongRelatesToExceptionString", new object[]
				{
					"CloseSequence"
				});
				string2 = SR.GetString("WsrmMessageWithWrongRelatesToFaultString", new object[]
				{
					"CloseSequence"
				});
			}
			else
			{
				if (info.AcknowledgementInfo != null && info.AcknowledgementInfo.Final)
				{
					return WsrmUtilities.ValidateFinalAck(session, info, last);
				}
				@string = SR.GetString("MissingFinalAckExceptionString");
				string2 = SR.GetString("SequenceTerminatedMissingFinalAck");
			}
			UniqueId outputID = session.OutputID;
			return SequenceTerminatedFault.CreateProtocolFault(outputID, string2, @string);
		}

		// Token: 0x06005B80 RID: 23424 RVA: 0x0014F91C File Offset: 0x0014DB1C
		public static bool ValidateCreateSequence<TChannel>(WsrmMessageInfo info, ReliableChannelListenerBase<TChannel> listener, IChannel channel, out EndpointAddress acksTo) where TChannel : class, IChannel
		{
			acksTo = null;
			string text = null;
			if (info.CreateSequenceInfo.OfferIdentifier == null)
			{
				if (typeof(TChannel) == typeof(IDuplexSessionChannel))
				{
					text = SR.GetString("CSRefusedDuplexNoOffer", new object[]
					{
						listener.Uri
					});
				}
				else if (typeof(TChannel) == typeof(IReplySessionChannel))
				{
					text = SR.GetString("CSRefusedReplyNoOffer", new object[]
					{
						listener.Uri
					});
				}
			}
			else if (listener.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && typeof(TChannel) == typeof(IInputSessionChannel))
			{
				text = SR.GetString("CSRefusedInputOffer", new object[]
				{
					listener.Uri
				});
			}
			if (text != null)
			{
				info.FaultReply = WsrmUtilities.CreateCSRefusedProtocolFault(listener.MessageVersion, listener.ReliableMessagingVersion, text);
				info.FaultException = new ProtocolException(SR.GetString("ConflictingOffer"));
				return false;
			}
			if (listener.LocalAddresses != null)
			{
				Collection<EndpointAddress> collection = new Collection<EndpointAddress>();
				try
				{
					listener.LocalAddresses.GetMatchingValues(info.Message, collection);
				}
				catch (CommunicationException ex)
				{
					FaultConverter faultConverter = channel.GetProperty<FaultConverter>();
					if (faultConverter == null)
					{
						faultConverter = FaultConverter.GetDefaultFaultConverter(listener.MessageVersion);
					}
					Message faultReply;
					if (faultConverter.TryCreateFaultMessage(ex, out faultReply))
					{
						info.FaultReply = faultReply;
						info.FaultException = new ProtocolException(SR.GetString("MessageExceptionOccurred"), ex);
						return false;
					}
					throw;
				}
				if (collection.Count > 0)
				{
					EndpointAddress endpointAddress = collection[0];
					acksTo = new EndpointAddress(info.CreateSequenceInfo.To, endpointAddress.Identity, endpointAddress.Headers);
					return true;
				}
				info.FaultReply = WsrmUtilities.CreateEndpointNotFoundFault(listener.MessageVersion, SR.GetString("EndpointNotFound", new object[]
				{
					info.CreateSequenceInfo.To
				}));
				info.FaultException = new ProtocolException(SR.GetString("ConflictingAddress"));
				return false;
			}
			acksTo = new EndpointAddress(info.CreateSequenceInfo.To, new AddressHeader[0]);
			return true;
		}

		// Token: 0x06005B81 RID: 23425 RVA: 0x0014FB3C File Offset: 0x0014DD3C
		public static WsrmFault ValidateFinalAck(ChannelReliableSession session, WsrmMessageInfo info, long last)
		{
			WsrmAcknowledgmentInfo acknowledgementInfo = info.AcknowledgementInfo;
			WsrmFault wsrmFault = WsrmUtilities.ValidateFinalAckExists(session, acknowledgementInfo);
			if (wsrmFault != null)
			{
				return wsrmFault;
			}
			SequenceRangeCollection ranges = acknowledgementInfo.Ranges;
			if (last == 0L)
			{
				if (ranges.Count == 0)
				{
					return null;
				}
			}
			else if (ranges.Count == 1 && ranges[0].Lower == 1L && ranges[0].Upper == last)
			{
				return null;
			}
			return new InvalidAcknowledgementFault(session.OutputID, acknowledgementInfo.Ranges);
		}

		// Token: 0x06005B82 RID: 23426 RVA: 0x0014FBB4 File Offset: 0x0014DDB4
		public static WsrmFault ValidateFinalAckExists(ChannelReliableSession session, WsrmAcknowledgmentInfo ackInfo)
		{
			if (ackInfo == null || !ackInfo.Final)
			{
				string @string = SR.GetString("MissingFinalAckExceptionString");
				string string2 = SR.GetString("SequenceTerminatedMissingFinalAck");
				return SequenceTerminatedFault.CreateProtocolFault(session.OutputID, string2, @string);
			}
			return null;
		}

		// Token: 0x06005B83 RID: 23427 RVA: 0x0014FBF4 File Offset: 0x0014DDF4
		public static WsrmFault ValidateTerminateSequenceResponse(ChannelReliableSession session, UniqueId messageId, WsrmMessageInfo info, long last)
		{
			if (info.WsrmHeaderFault is UnknownSequenceFault)
			{
				return null;
			}
			string @string;
			string string2;
			if (info.TerminateSequenceResponseInfo == null)
			{
				@string = SR.GetString("InvalidWsrmResponseSessionFaultedExceptionString", new object[]
				{
					"TerminateSequence",
					info.Action,
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse"
				});
				string2 = SR.GetString("InvalidWsrmResponseSessionFaultedFaultString", new object[]
				{
					"TerminateSequence",
					info.Action,
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/TerminateSequenceResponse"
				});
			}
			else
			{
				if (object.Equals(messageId, info.TerminateSequenceResponseInfo.RelatesTo))
				{
					return WsrmUtilities.ValidateFinalAck(session, info, last);
				}
				@string = SR.GetString("WsrmMessageWithWrongRelatesToExceptionString", new object[]
				{
					"TerminateSequence"
				});
				string2 = SR.GetString("WsrmMessageWithWrongRelatesToFaultString", new object[]
				{
					"TerminateSequence"
				});
			}
			UniqueId outputID = session.OutputID;
			return SequenceTerminatedFault.CreateProtocolFault(outputID, string2, @string);
		}

		// Token: 0x06005B84 RID: 23428 RVA: 0x0014FCD4 File Offset: 0x0014DED4
		public static bool ValidateWsrmRequest(ChannelReliableSession session, WsrmRequestInfo info, IReliableChannelBinder binder, RequestContext context)
		{
			if (!(info is CloseSequenceInfo) && !(info is TerminateSequenceInfo))
			{
				throw Fx.AssertAndThrow("Method is meant for CloseSequence or TerminateSequence only.");
			}
			if (info.ReplyTo.Uri != binder.RemoteAddress.Uri)
			{
				string @string = SR.GetString("WsrmRequestIncorrectReplyToFaultString", new object[]
				{
					info.RequestName
				});
				string string2 = SR.GetString("WsrmRequestIncorrectReplyToExceptionString", new object[]
				{
					info.RequestName
				});
				WsrmFault wsrmFault = SequenceTerminatedFault.CreateProtocolFault(session.InputID, @string, string2);
				session.OnLocalFault(wsrmFault.CreateException(), wsrmFault, context);
				return false;
			}
			return true;
		}

		// Token: 0x06005B85 RID: 23429 RVA: 0x0014FD6C File Offset: 0x0014DF6C
		public static void WriteIdentifier(XmlDictionaryWriter writer, ReliableMessagingVersion reliableMessagingVersion, UniqueId sequenceId)
		{
			writer.WriteStartElement("r", XD.WsrmFeb2005Dictionary.Identifier, WsrmIndex.GetNamespace(reliableMessagingVersion));
			writer.WriteValue(sequenceId);
			writer.WriteEndElement();
		}

		// Token: 0x06005B86 RID: 23430 RVA: 0x0014FD98 File Offset: 0x0014DF98
		public static string UseStrings()
		{
			return "SequenceTerminatedUnsupportedTerminateSequence";
		}
	}
}
