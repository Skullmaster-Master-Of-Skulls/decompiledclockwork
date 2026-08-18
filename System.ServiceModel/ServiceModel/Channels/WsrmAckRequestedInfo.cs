using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000979 RID: 2425
	internal sealed class WsrmAckRequestedInfo : WsrmHeaderInfo
	{
		// Token: 0x06005DE5 RID: 24037 RVA: 0x0015B403 File Offset: 0x00159603
		public WsrmAckRequestedInfo(UniqueId sequenceID, MessageHeaderInfo header) : base(header)
		{
			this.sequenceID = sequenceID;
		}

		// Token: 0x1700168A RID: 5770
		// (get) Token: 0x06005DE6 RID: 24038 RVA: 0x0015B413 File Offset: 0x00159613
		public UniqueId SequenceID
		{
			get
			{
				return this.sequenceID;
			}
		}

		// Token: 0x06005DE7 RID: 24039 RVA: 0x0015B41C File Offset: 0x0015961C
		public static WsrmAckRequestedInfo ReadHeader(ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader, MessageHeaderInfo header)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			reader.ReadStartElement();
			reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			UniqueId uniqueId = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && reader.IsStartElement(wsrmFeb2005Dictionary.MessageNumber, @namespace))
			{
				reader.ReadStartElement();
				WsrmUtilities.ReadSequenceNumber(reader, true);
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return new WsrmAckRequestedInfo(uniqueId, header);
		}

		// Token: 0x040037BE RID: 14270
		private UniqueId sequenceID;
	}
}
