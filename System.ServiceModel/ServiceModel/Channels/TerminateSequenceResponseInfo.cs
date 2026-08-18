using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000971 RID: 2417
	internal sealed class TerminateSequenceResponseInfo
	{
		// Token: 0x17001675 RID: 5749
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x0015A984 File Offset: 0x00158B84
		// (set) Token: 0x06005DBE RID: 23998 RVA: 0x0015A98C File Offset: 0x00158B8C
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

		// Token: 0x17001676 RID: 5750
		// (get) Token: 0x06005DBF RID: 23999 RVA: 0x0015A995 File Offset: 0x00158B95
		// (set) Token: 0x06005DC0 RID: 24000 RVA: 0x0015A99D File Offset: 0x00158B9D
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

		// Token: 0x06005DC1 RID: 24001 RVA: 0x0015A9A8 File Offset: 0x00158BA8
		public static TerminateSequenceResponseInfo ReadMessage(MessageVersion messageVersion, Message message, MessageHeaders headers)
		{
			if (headers.RelatesTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MissingRelatesToOnWsrmResponseReason", new object[]
				{
					DXD.Wsrm11Dictionary.TerminateSequenceResponse
				}), messageVersion.Addressing.Namespace, "RelatesTo", false));
			}
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					WsrmIndex.GetTerminateSequenceResponseActionString(ReliableMessagingVersion.WSReliableMessaging11)
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
			}
			TerminateSequenceResponseInfo terminateSequenceResponseInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				terminateSequenceResponseInfo = TerminateSequenceResponse.Create(readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			terminateSequenceResponseInfo.relatesTo = headers.RelatesTo;
			return terminateSequenceResponseInfo;
		}

		// Token: 0x040037AA RID: 14250
		private UniqueId identifier;

		// Token: 0x040037AB RID: 14251
		private UniqueId relatesTo;
	}
}
