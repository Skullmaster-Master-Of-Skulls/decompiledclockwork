using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200097A RID: 2426
	internal sealed class WsrmAckRequestedHeader : WsrmMessageHeader
	{
		// Token: 0x06005DE8 RID: 24040 RVA: 0x0015B49E File Offset: 0x0015969E
		public WsrmAckRequestedHeader(ReliableMessagingVersion reliableMessagingVersion, UniqueId sequenceID) : base(reliableMessagingVersion)
		{
			this.sequenceID = sequenceID;
		}

		// Token: 0x1700168B RID: 5771
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x0015B4AE File Offset: 0x001596AE
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.WsrmFeb2005Dictionary.AckRequested;
			}
		}

		// Token: 0x06005DEA RID: 24042 RVA: 0x0015B4BC File Offset: 0x001596BC
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString dictionaryNamespace = this.DictionaryNamespace;
			writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, dictionaryNamespace);
			writer.WriteValue(this.sequenceID);
			writer.WriteEndElement();
		}

		// Token: 0x040037BF RID: 14271
		private UniqueId sequenceID;
	}
}
