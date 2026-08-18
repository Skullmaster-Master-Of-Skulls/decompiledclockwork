using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000975 RID: 2421
	internal sealed class WsrmSequencedMessageInfo : WsrmHeaderInfo
	{
		// Token: 0x06005DD1 RID: 24017 RVA: 0x0015ABE7 File Offset: 0x00158DE7
		private WsrmSequencedMessageInfo(UniqueId sequenceID, long sequenceNumber, bool lastMessage, MessageHeaderInfo header) : base(header)
		{
			this.sequenceID = sequenceID;
			this.sequenceNumber = sequenceNumber;
			this.lastMessage = lastMessage;
		}

		// Token: 0x17001680 RID: 5760
		// (get) Token: 0x06005DD2 RID: 24018 RVA: 0x0015AC06 File Offset: 0x00158E06
		public UniqueId SequenceID
		{
			get
			{
				return this.sequenceID;
			}
		}

		// Token: 0x17001681 RID: 5761
		// (get) Token: 0x06005DD3 RID: 24019 RVA: 0x0015AC0E File Offset: 0x00158E0E
		public long SequenceNumber
		{
			get
			{
				return this.sequenceNumber;
			}
		}

		// Token: 0x17001682 RID: 5762
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x0015AC16 File Offset: 0x00158E16
		public bool LastMessage
		{
			get
			{
				return this.lastMessage;
			}
		}

		// Token: 0x06005DD5 RID: 24021 RVA: 0x0015AC20 File Offset: 0x00158E20
		public static WsrmSequencedMessageInfo ReadHeader(ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader, MessageHeaderInfo header)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			reader.ReadStartElement();
			reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			UniqueId uniqueId = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			reader.ReadStartElement(wsrmFeb2005Dictionary.MessageNumber, @namespace);
			long num = WsrmUtilities.ReadSequenceNumber(reader);
			reader.ReadEndElement();
			bool flag = false;
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && reader.IsStartElement(wsrmFeb2005Dictionary.LastMessage, @namespace))
			{
				WsrmUtilities.ReadEmptyElement(reader);
				flag = true;
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return new WsrmSequencedMessageInfo(uniqueId, num, flag, header);
		}

		// Token: 0x040037B0 RID: 14256
		private UniqueId sequenceID;

		// Token: 0x040037B1 RID: 14257
		private long sequenceNumber;

		// Token: 0x040037B2 RID: 14258
		private bool lastMessage;
	}
}
