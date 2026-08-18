using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000909 RID: 2313
	internal sealed class CloseSequenceResponse : BodyWriter
	{
		// Token: 0x0600584C RID: 22604 RVA: 0x0014457D File Offset: 0x0014277D
		public CloseSequenceResponse(UniqueId identifier) : base(true)
		{
			this.identifier = identifier;
		}

		// Token: 0x0600584D RID: 22605 RVA: 0x00144590 File Offset: 0x00142790
		public static CloseSequenceResponseInfo Create(XmlDictionaryReader reader)
		{
			CloseSequenceResponseInfo closeSequenceResponseInfo = new CloseSequenceResponseInfo();
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			reader.ReadStartElement(DXD.Wsrm11Dictionary.CloseSequenceResponse, @namespace);
			reader.ReadStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			closeSequenceResponseInfo.Identifier = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return closeSequenceResponseInfo;
		}

		// Token: 0x0600584E RID: 22606 RVA: 0x001445FC File Offset: 0x001427FC
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			writer.WriteStartElement(DXD.Wsrm11Dictionary.CloseSequenceResponse, @namespace);
			writer.WriteStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(this.identifier);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x04003622 RID: 13858
		private UniqueId identifier;
	}
}
