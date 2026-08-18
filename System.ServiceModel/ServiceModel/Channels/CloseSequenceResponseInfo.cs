using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096D RID: 2413
	internal sealed class CloseSequenceResponseInfo
	{
		// Token: 0x17001667 RID: 5735
		// (get) Token: 0x06005D9B RID: 23963 RVA: 0x0015A4B8 File Offset: 0x001586B8
		// (set) Token: 0x06005D9C RID: 23964 RVA: 0x0015A4C0 File Offset: 0x001586C0
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

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06005D9D RID: 23965 RVA: 0x0015A4C9 File Offset: 0x001586C9
		// (set) Token: 0x06005D9E RID: 23966 RVA: 0x0015A4D1 File Offset: 0x001586D1
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

		// Token: 0x06005D9F RID: 23967 RVA: 0x0015A4DC File Offset: 0x001586DC
		public static CloseSequenceResponseInfo ReadMessage(MessageVersion messageVersion, Message message, MessageHeaders headers)
		{
			if (headers.RelatesTo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageHeaderException(SR.GetString("MissingRelatesToOnWsrmResponseReason", new object[]
				{
					DXD.Wsrm11Dictionary.CloseSequenceResponse
				}), messageVersion.Addressing.Namespace, "RelatesTo", false));
			}
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequenceResponse"
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
			}
			CloseSequenceResponseInfo closeSequenceResponseInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				closeSequenceResponseInfo = CloseSequenceResponse.Create(readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			closeSequenceResponseInfo.relatesTo = headers.RelatesTo;
			return closeSequenceResponseInfo;
		}

		// Token: 0x0400379E RID: 14238
		private UniqueId identifier;

		// Token: 0x0400379F RID: 14239
		private UniqueId relatesTo;
	}
}
