using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096F RID: 2415
	internal sealed class CreateSequenceResponseInfo
	{
		// Token: 0x1700166F RID: 5743
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x0015A7B5 File Offset: 0x001589B5
		// (set) Token: 0x06005DAF RID: 23983 RVA: 0x0015A7BD File Offset: 0x001589BD
		public EndpointAddress AcceptAcksTo
		{
			get
			{
				return this.acceptAcksTo;
			}
			set
			{
				this.acceptAcksTo = value;
			}
		}

		// Token: 0x17001670 RID: 5744
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x0015A7C6 File Offset: 0x001589C6
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x0015A7CE File Offset: 0x001589CE
		public UniqueId Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x17001671 RID: 5745
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x0015A7D7 File Offset: 0x001589D7
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x0015A7DF File Offset: 0x001589DF
		public UniqueId RelatesTo
		{
			get
			{
				return this.relatesTo;
			}
			set
			{
				this.relatesTo = value;
			}
		}

		// Token: 0x06005DB4 RID: 23988 RVA: 0x0015A7E8 File Offset: 0x001589E8
		public static CreateSequenceResponseInfo ReadMessage(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, Message message, MessageHeaders headers)
		{
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					WsrmIndex.GetCreateSequenceResponseActionString(reliableMessagingVersion)
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
			}
			if (headers.RelatesTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MissingRelatesToOnWsrmResponseReason", new object[]
				{
					XD.WsrmFeb2005Dictionary.CreateSequenceResponse
				}), messageVersion.Addressing.Namespace, "RelatesTo", false));
			}
			CreateSequenceResponseInfo createSequenceResponseInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				createSequenceResponseInfo = CreateSequenceResponse.Create(messageVersion.Addressing, reliableMessagingVersion, readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			createSequenceResponseInfo.RelatesTo = headers.RelatesTo;
			return createSequenceResponseInfo;
		}

		// Token: 0x040037A5 RID: 14245
		private EndpointAddress acceptAcksTo;

		// Token: 0x040037A6 RID: 14246
		private UniqueId identifier;

		// Token: 0x040037A7 RID: 14247
		private UniqueId relatesTo;
	}
}
