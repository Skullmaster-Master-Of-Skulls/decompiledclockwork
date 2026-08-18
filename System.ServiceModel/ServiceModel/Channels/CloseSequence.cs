using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000908 RID: 2312
	internal sealed class CloseSequence : BodyWriter
	{
		// Token: 0x06005849 RID: 22601 RVA: 0x0014444F File Offset: 0x0014264F
		public CloseSequence(UniqueId identifier, long lastMsgNumber) : base(true)
		{
			this.identifier = identifier;
			this.lastMsgNumber = lastMsgNumber;
		}

		// Token: 0x0600584A RID: 22602 RVA: 0x00144468 File Offset: 0x00142668
		public static CloseSequenceInfo Create(XmlDictionaryReader reader)
		{
			CloseSequenceInfo closeSequenceInfo = new CloseSequenceInfo();
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
			reader.ReadStartElement(wsrm11Dictionary.CloseSequence, @namespace);
			reader.ReadStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			closeSequenceInfo.Identifier = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			if (reader.IsStartElement(wsrm11Dictionary.LastMsgNumber, @namespace))
			{
				reader.ReadStartElement();
				closeSequenceInfo.LastMsgNumber = WsrmUtilities.ReadSequenceNumber(reader, false);
				reader.ReadEndElement();
			}
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return closeSequenceInfo;
		}

		// Token: 0x0600584B RID: 22603 RVA: 0x00144500 File Offset: 0x00142700
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			Wsrm11Dictionary wsrm11Dictionary = DXD.Wsrm11Dictionary;
			writer.WriteStartElement(wsrm11Dictionary.CloseSequence, @namespace);
			writer.WriteStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(this.identifier);
			writer.WriteEndElement();
			if (this.lastMsgNumber > 0L)
			{
				writer.WriteStartElement(wsrm11Dictionary.LastMsgNumber, @namespace);
				writer.WriteValue(this.lastMsgNumber);
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
		}

		// Token: 0x04003620 RID: 13856
		private UniqueId identifier;

		// Token: 0x04003621 RID: 13857
		private long lastMsgNumber;
	}
}
