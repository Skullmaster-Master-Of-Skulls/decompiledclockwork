using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095D RID: 2397
	internal sealed class TerminateSequence : BodyWriter
	{
		// Token: 0x06005D03 RID: 23811 RVA: 0x001577B2 File Offset: 0x001559B2
		public TerminateSequence() : base(true)
		{
		}

		// Token: 0x06005D04 RID: 23812 RVA: 0x001577BB File Offset: 0x001559BB
		public TerminateSequence(ReliableMessagingVersion reliableMessagingVersion, UniqueId identifier, long last) : base(true)
		{
			this.reliableMessagingVersion = reliableMessagingVersion;
			this.identifier = identifier;
			this.lastMsgNumber = last;
		}

		// Token: 0x06005D05 RID: 23813 RVA: 0x001577DC File Offset: 0x001559DC
		public static TerminateSequenceInfo Create(ReliableMessagingVersion reliableMessagingVersion, XmlDictionaryReader reader)
		{
			TerminateSequenceInfo terminateSequenceInfo = new TerminateSequenceInfo();
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			reader.ReadStartElement(wsrmFeb2005Dictionary.TerminateSequence, @namespace);
			reader.ReadStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			terminateSequenceInfo.Identifier = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && reader.IsStartElement(DXD.Wsrm11Dictionary.LastMsgNumber, @namespace))
			{
				reader.ReadStartElement();
				terminateSequenceInfo.LastMsgNumber = WsrmUtilities.ReadSequenceNumber(reader, false);
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return terminateSequenceInfo;
		}

		// Token: 0x06005D06 RID: 23814 RVA: 0x00157878 File Offset: 0x00155A78
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(this.reliableMessagingVersion);
			writer.WriteStartElement(wsrmFeb2005Dictionary.TerminateSequence, @namespace);
			writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(this.identifier);
			writer.WriteEndElement();
			if (this.reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.lastMsgNumber > 0L)
			{
				writer.WriteStartElement(DXD.Wsrm11Dictionary.LastMsgNumber, @namespace);
				writer.WriteValue(this.lastMsgNumber);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x0400375E RID: 14174
		private UniqueId identifier;

		// Token: 0x0400375F RID: 14175
		private long lastMsgNumber;

		// Token: 0x04003760 RID: 14176
		private ReliableMessagingVersion reliableMessagingVersion;
	}
}
