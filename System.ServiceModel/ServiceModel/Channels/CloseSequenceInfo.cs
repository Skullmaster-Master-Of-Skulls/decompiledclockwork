using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200096C RID: 2412
	internal sealed class CloseSequenceInfo : WsrmRequestInfo
	{
		// Token: 0x17001664 RID: 5732
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x0015A400 File Offset: 0x00158600
		// (set) Token: 0x06005D95 RID: 23957 RVA: 0x0015A408 File Offset: 0x00158608
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

		// Token: 0x17001665 RID: 5733
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x0015A411 File Offset: 0x00158611
		// (set) Token: 0x06005D97 RID: 23959 RVA: 0x0015A419 File Offset: 0x00158619
		public long LastMsgNumber
		{
			get
			{
				return this.lastMsgNumber;
			}
			set
			{
				this.lastMsgNumber = value;
			}
		}

		// Token: 0x17001666 RID: 5734
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x0015A422 File Offset: 0x00158622
		public override string RequestName
		{
			get
			{
				return "CloseSequence";
			}
		}

		// Token: 0x06005D99 RID: 23961 RVA: 0x0015A42C File Offset: 0x0015862C
		public static CloseSequenceInfo ReadMessage(MessageVersion messageVersion, Message message, MessageHeaders headers)
		{
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					"http://docs.oasis-open.org/ws-rx/wsrm/200702/CloseSequence"
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
			}
			CloseSequenceInfo closeSequenceInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				closeSequenceInfo = CloseSequence.Create(readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			closeSequenceInfo.SetMessageId(messageVersion, headers);
			closeSequenceInfo.SetReplyTo(messageVersion, headers);
			return closeSequenceInfo;
		}

		// Token: 0x0400379C RID: 14236
		private UniqueId identifier;

		// Token: 0x0400379D RID: 14237
		private long lastMsgNumber;
	}
}
