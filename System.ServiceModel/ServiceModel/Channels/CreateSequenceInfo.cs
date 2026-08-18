using System;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096E RID: 2414
	internal sealed class CreateSequenceInfo : WsrmRequestInfo
	{
		// Token: 0x17001669 RID: 5737
		// (get) Token: 0x06005DA1 RID: 23969 RVA: 0x0015A5B0 File Offset: 0x001587B0
		// (set) Token: 0x06005DA2 RID: 23970 RVA: 0x0015A5B8 File Offset: 0x001587B8
		public EndpointAddress AcksTo
		{
			get
			{
				return this.acksTo;
			}
			set
			{
				this.acksTo = value;
			}
		}

		// Token: 0x1700166A RID: 5738
		// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x0015A5C1 File Offset: 0x001587C1
		// (set) Token: 0x06005DA4 RID: 23972 RVA: 0x0015A5C9 File Offset: 0x001587C9
		public TimeSpan? Expires
		{
			get
			{
				return this.expires;
			}
			set
			{
				this.expires = value;
			}
		}

		// Token: 0x1700166B RID: 5739
		// (get) Token: 0x06005DA5 RID: 23973 RVA: 0x0015A5D2 File Offset: 0x001587D2
		// (set) Token: 0x06005DA6 RID: 23974 RVA: 0x0015A5DA File Offset: 0x001587DA
		public TimeSpan? OfferExpires
		{
			get
			{
				return this.offerExpires;
			}
			set
			{
				this.offerExpires = value;
			}
		}

		// Token: 0x1700166C RID: 5740
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x0015A5E3 File Offset: 0x001587E3
		// (set) Token: 0x06005DA8 RID: 23976 RVA: 0x0015A5EB File Offset: 0x001587EB
		public UniqueId OfferIdentifier
		{
			get
			{
				return this.offerIdentifier;
			}
			set
			{
				this.offerIdentifier = value;
			}
		}

		// Token: 0x1700166D RID: 5741
		// (get) Token: 0x06005DA9 RID: 23977 RVA: 0x0015A5F4 File Offset: 0x001587F4
		public override string RequestName
		{
			get
			{
				return "CreateSequence";
			}
		}

		// Token: 0x1700166E RID: 5742
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x0015A5FB File Offset: 0x001587FB
		public Uri To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x06005DAB RID: 23979 RVA: 0x0015A604 File Offset: 0x00158804
		public static CreateSequenceInfo ReadMessage(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, ISecureConversationSession securitySession, Message message, MessageHeaders headers)
		{
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					WsrmIndex.GetCreateSequenceActionString(reliableMessagingVersion)
				});
				Message faultReply = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, @string);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply, @string, new ProtocolException(@string)));
			}
			CreateSequenceInfo createSequenceInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				createSequenceInfo = CreateSequence.Create(messageVersion, reliableMessagingVersion, securitySession, readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			createSequenceInfo.SetMessageId(messageVersion, headers);
			createSequenceInfo.SetReplyTo(messageVersion, headers);
			if (createSequenceInfo.AcksTo.Uri != createSequenceInfo.ReplyTo.Uri)
			{
				string string2 = SR.GetString("CSRefusedAcksToMustEqualReplyTo");
				Message faultReply2 = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, reliableMessagingVersion, string2);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply2, string2, new ProtocolException(string2)));
			}
			createSequenceInfo.to = message.Headers.To;
			if (createSequenceInfo.to == null && messageVersion.Addressing == AddressingVersion.WSAddressing10)
			{
				createSequenceInfo.to = messageVersion.Addressing.AnonymousUri;
			}
			return createSequenceInfo;
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x0015A728 File Offset: 0x00158928
		public static void ValidateCreateSequenceHeaders(MessageVersion messageVersion, ISecureConversationSession securitySession, WsrmMessageInfo info)
		{
			string text = null;
			if (info.UsesSequenceSSLInfo != null)
			{
				text = SR.GetString("CSRefusedSSLNotSupported");
			}
			else if (info.UsesSequenceSTRInfo != null && securitySession == null)
			{
				text = SR.GetString("CSRefusedSTRNoWSSecurity");
			}
			else if (info.UsesSequenceSTRInfo == null && securitySession != null)
			{
				text = SR.GetString("CSRefusedNoSTRWSSecurity");
			}
			if (text != null)
			{
				Message faultReply = WsrmUtilities.CreateCSRefusedProtocolFault(messageVersion, ReliableMessagingVersion.WSReliableMessaging11, text);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(WsrmMessageInfo.CreateInternalFaultException(faultReply, text, new ProtocolException(text)));
			}
		}

		// Token: 0x040037A0 RID: 14240
		private EndpointAddress acksTo = EndpointAddress.AnonymousAddress;

		// Token: 0x040037A1 RID: 14241
		private TimeSpan? expires;

		// Token: 0x040037A2 RID: 14242
		private TimeSpan? offerExpires;

		// Token: 0x040037A3 RID: 14243
		private UniqueId offerIdentifier;

		// Token: 0x040037A4 RID: 14244
		private Uri to;
	}
}
