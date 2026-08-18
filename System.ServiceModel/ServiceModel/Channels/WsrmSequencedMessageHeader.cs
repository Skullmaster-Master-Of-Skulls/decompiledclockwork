using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000976 RID: 2422
	internal sealed class WsrmSequencedMessageHeader : WsrmMessageHeader
	{
		// Token: 0x06005DD6 RID: 24022 RVA: 0x0015ACB7 File Offset: 0x00158EB7
		public WsrmSequencedMessageHeader(ReliableMessagingVersion reliableMessagingVersion, UniqueId sequenceID, long sequenceNumber, bool lastMessage) : base(reliableMessagingVersion)
		{
			this.sequenceID = sequenceID;
			this.sequenceNumber = sequenceNumber;
			this.lastMessage = lastMessage;
		}

		// Token: 0x17001683 RID: 5763
		// (get) Token: 0x06005DD7 RID: 24023 RVA: 0x0015ACD6 File Offset: 0x00158ED6
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.WsrmFeb2005Dictionary.Sequence;
			}
		}

		// Token: 0x17001684 RID: 5764
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x0015ACE2 File Offset: 0x00158EE2
		public override bool MustUnderstand
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06005DD9 RID: 24025 RVA: 0x0015ACE8 File Offset: 0x00158EE8
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString dictionaryNamespace = this.DictionaryNamespace;
			writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, dictionaryNamespace);
			writer.WriteValue(this.sequenceID);
			writer.WriteEndElement();
			writer.WriteStartElement(wsrmFeb2005Dictionary.MessageNumber, dictionaryNamespace);
			writer.WriteValue(this.sequenceNumber);
			writer.WriteEndElement();
			if (base.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005 && this.lastMessage)
			{
				writer.WriteStartElement(wsrmFeb2005Dictionary.LastMessage, dictionaryNamespace);
				writer.WriteEndElement();
			}
		}

		// Token: 0x040037B3 RID: 14259
		private bool lastMessage;

		// Token: 0x040037B4 RID: 14260
		private UniqueId sequenceID;

		// Token: 0x040037B5 RID: 14261
		private long sequenceNumber;
	}
}
