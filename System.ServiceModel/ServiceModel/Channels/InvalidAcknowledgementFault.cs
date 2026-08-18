using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000964 RID: 2404
	internal sealed class InvalidAcknowledgementFault : WsrmHeaderFault
	{
		// Token: 0x06005D5D RID: 23901 RVA: 0x00159322 File Offset: 0x00157522
		public InvalidAcknowledgementFault(UniqueId sequenceID, SequenceRangeCollection ranges) : base(true, "InvalidAcknowledgement", SR.GetString("InvalidAcknowledgementFaultReason"), SR.GetString("InvalidAcknowledgementReceived"), sequenceID, true, false)
		{
			this.ranges = ranges;
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x00159350 File Offset: 0x00157550
		public InvalidAcknowledgementFault(FaultCode code, FaultReason reason, XmlDictionaryReader detailReader, ReliableMessagingVersion reliableMessagingVersion) : base(code, "InvalidAcknowledgement", reason, true, false)
		{
			UniqueId sequenceID;
			bool flag;
			WsrmAcknowledgmentInfo.ReadAck(reliableMessagingVersion, detailReader, out sequenceID, out this.ranges, out flag);
			base.SequenceID = sequenceID;
			while (detailReader.IsStartElement())
			{
				detailReader.Skip();
			}
			detailReader.ReadEndElement();
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x0015939C File Offset: 0x0015759C
		protected override void OnWriteDetailContents(XmlDictionaryWriter writer)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			ReliableMessagingVersion reliableMessagingVersion = base.GetReliableMessagingVersion();
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			writer.WriteStartElement(wsrmFeb2005Dictionary.SequenceAcknowledgement, @namespace);
			WsrmAcknowledgmentHeader.WriteAckRanges(writer, reliableMessagingVersion, base.SequenceID, this.ranges);
			writer.WriteEndElement();
		}

		// Token: 0x04003789 RID: 14217
		private SequenceRangeCollection ranges;
	}
}
