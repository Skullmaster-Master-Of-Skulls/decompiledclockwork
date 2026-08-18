using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000970 RID: 2416
	internal sealed class TerminateSequenceInfo : WsrmRequestInfo
	{
		// Token: 0x17001672 RID: 5746
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x0015A8C4 File Offset: 0x00158AC4
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x0015A8CC File Offset: 0x00158ACC
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

		// Token: 0x17001673 RID: 5747
		// (get) Token: 0x06005DB8 RID: 23992 RVA: 0x0015A8D5 File Offset: 0x00158AD5
		// (set) Token: 0x06005DB9 RID: 23993 RVA: 0x0015A8DD File Offset: 0x00158ADD
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

		// Token: 0x17001674 RID: 5748
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x0015A8E6 File Offset: 0x00158AE6
		public override string RequestName
		{
			get
			{
				return "TerminateSequence";
			}
		}

		// Token: 0x06005DBB RID: 23995 RVA: 0x0015A8F0 File Offset: 0x00158AF0
		public static TerminateSequenceInfo ReadMessage(MessageVersion messageVersion, ReliableMessagingVersion reliableMessagingVersion, Message message, MessageHeaders headers)
		{
			if (message.IsEmpty)
			{
				string @string = SR.GetString("NonEmptyWsrmMessageIsEmpty", new object[]
				{
					WsrmIndex.GetTerminateSequenceActionString(reliableMessagingVersion)
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new XmlException(@string));
			}
			TerminateSequenceInfo terminateSequenceInfo;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				terminateSequenceInfo = TerminateSequence.Create(reliableMessagingVersion, readerAtBodyContents);
				message.ReadFromBodyContentsToEnd(readerAtBodyContents);
			}
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
			{
				terminateSequenceInfo.SetMessageId(messageVersion, headers);
				terminateSequenceInfo.SetReplyTo(messageVersion, headers);
			}
			return terminateSequenceInfo;
		}

		// Token: 0x040037A8 RID: 14248
		private UniqueId identifier;

		// Token: 0x040037A9 RID: 14249
		private long lastMsgNumber;
	}
}
