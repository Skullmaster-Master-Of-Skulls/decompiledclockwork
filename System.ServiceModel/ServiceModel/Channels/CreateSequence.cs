using System;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200090A RID: 2314
	internal sealed class CreateSequence : BodyWriter
	{
		// Token: 0x0600584F RID: 22607 RVA: 0x0014464E File Offset: 0x0014284E
		private CreateSequence() : base(true)
		{
		}

		// Token: 0x06005850 RID: 22608 RVA: 0x00144657 File Offset: 0x00142857
		public CreateSequence(AddressingVersion addressingVersion, ReliableMessagingVersion reliableMessagingVersion, bool ordered, IClientReliableChannelBinder binder, UniqueId offerIdentifier) : base(true)
		{
			this.addressingVersion = addressingVersion;
			this.reliableMessagingVersion = reliableMessagingVersion;
			this.ordered = ordered;
			this.binder = binder;
			this.offerIdentifier = offerIdentifier;
		}

		// Token: 0x06005851 RID: 22609 RVA: 0x00144688 File Offset: 0x00142888
		public static CreateSequenceInfo Create(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, ISecureConversationSession securitySession, XmlDictionaryReader reader)
		{
			CreateSequenceInfo result;
			try
			{
				CreateSequenceInfo createSequenceInfo = new CreateSequenceInfo();
				WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
				XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
				reader.ReadStartElement(wsrmFeb2005Dictionary.CreateSequence, @namespace);
				createSequenceInfo.AcksTo = EndpointAddress.ReadFrom(messageVersion.Addressing, reader, wsrmFeb2005Dictionary.AcksTo, @namespace);
				if (reader.IsStartElement(wsrmFeb2005Dictionary.Expires, @namespace))
				{
					createSequenceInfo.Expires = new TimeSpan?(reader.ReadElementContentAsTimeSpan());
				}
				if (reader.IsStartElement(wsrmFeb2005Dictionary.Offer, @namespace))
				{
					reader.ReadStartElement();
					reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
					createSequenceInfo.OfferIdentifier = reader.ReadContentAsUniqueId();
					reader.ReadEndElement();
					bool flag = reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11;
					Wsrm11Dictionary wsrm11Dictionary = flag ? DXD.Wsrm11Dictionary : null;
					if (flag)
					{
						EndpointAddress endpointAddress = EndpointAddress.ReadFrom(messageVersion.Addressing, reader, wsrm11Dictionary.Endpoint, @namespace);
						if (endpointAddress.Uri != createSequenceInfo.AcksTo.Uri)
						{
							string @string = SR.GetString("CSRefusedAcksToMustEqualEndpoint");
							Message faultReply = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, @string);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply, @string, new ProtocolException(@string)));
						}
					}
					if (reader.IsStartElement(wsrmFeb2005Dictionary.Expires, @namespace))
					{
						createSequenceInfo.OfferExpires = new TimeSpan?(reader.ReadElementContentAsTimeSpan());
					}
					if (flag && reader.IsStartElement(wsrm11Dictionary.IncompleteSequenceBehavior, @namespace))
					{
						string a = reader.ReadElementContentAsString();
						if (a != "DiscardEntireSequence" && a != "DiscardFollowingFirstGap" && a != "NoDiscard")
						{
							string string2 = SR.GetString("CSRefusedInvalidIncompleteSequenceBehavior");
							Message faultReply2 = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, string2);
							throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply2, string2, new ProtocolException(string2)));
						}
					}
					while (reader.IsStartElement())
					{
						reader.Skip();
					}
					reader.ReadEndElement();
				}
				if (securitySession != null)
				{
					bool flag2 = false;
					while (reader.IsStartElement())
					{
						if (securitySession.TryReadSessionTokenIdentifier(reader))
						{
							flag2 = true;
							break;
						}
						reader.Skip();
					}
					if (!flag2)
					{
						string string3 = SR.GetString("CSRefusedRequiredSecurityElementMissing");
						Message faultReply3 = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, string3);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply3, string3, new ProtocolException(string3)));
					}
				}
				while (reader.IsStartElement())
				{
					reader.Skip();
				}
				reader.ReadEndElement();
				if (reader.IsStartElement())
				{
					string string4 = SR.GetString("CSRefusedUnexpectedElementAtEndOfCSMessage");
					Message faultReply4 = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, string4);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply4, string4, new ProtocolException(string4)));
				}
				result = createSequenceInfo;
			}
			catch (XmlException innerException)
			{
				string string5 = SR.GetString("CouldNotParseWithAction", new object[]
				{
					WsrmIndex.GetCreateSequenceActionString(reliableMessagingVersion)
				});
				Message faultReply5 = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, string5);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply5, string5, new ProtocolException(string5, innerException)));
			}
			return result;
		}

		// Token: 0x06005852 RID: 22610 RVA: 0x00144960 File Offset: 0x00142B60
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(this.reliableMessagingVersion);
			writer.WriteStartElement(wsrmFeb2005Dictionary.CreateSequence, @namespace);
			EndpointAddress localAddress = this.binder.LocalAddress;
			localAddress.WriteTo(this.addressingVersion, writer, wsrmFeb2005Dictionary.AcksTo, @namespace);
			if (this.offerIdentifier != null)
			{
				writer.WriteStartElement(wsrmFeb2005Dictionary.Offer, @namespace);
				writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
				writer.WriteValue(this.offerIdentifier);
				writer.WriteEndElement();
				if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
				{
					Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
					localAddress.WriteTo(this.addressingVersion, writer, wsrm11Dictionary.Endpoint, @namespace);
					writer.WriteStartElement(wsrm11Dictionary.IncompleteSequenceBehavior, @namespace);
					writer.WriteValue(this.ordered ? wsrm11Dictionary.DiscardFollowingFirstGap : wsrm11Dictionary.NoDiscard);
					writer.WriteEndElement();
				}
				writer.WriteEndElement();
			}
			ISecureConversationSession secureConversationSession = this.binder.GetInnerSession() as ISecureConversationSession;
			if (secureConversationSession != null)
			{
				secureConversationSession.WriteSessionTokenIdentifier(writer);
			}
			writer.WriteEndElement();
		}

		// Token: 0x04003623 RID: 13859
		private AddressingVersion addressingVersion;

		// Token: 0x04003624 RID: 13860
		private IClientReliableChannelBinder binder;

		// Token: 0x04003625 RID: 13861
		private UniqueId offerIdentifier;

		// Token: 0x04003626 RID: 13862
		private bool ordered;

		// Token: 0x04003627 RID: 13863
		private ReliableMessagingVersion reliableMessagingVersion;
	}
}
