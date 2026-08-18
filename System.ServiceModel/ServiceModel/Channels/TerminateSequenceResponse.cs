using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200095E RID: 2398
	internal sealed class TerminateSequenceResponse : BodyWriter
	{
		// Token: 0x06005D07 RID: 23815 RVA: 0x00157903 File Offset: 0x00155B03
		public TerminateSequenceResponse() : base(true)
		{
		}

		// Token: 0x06005D08 RID: 23816 RVA: 0x0015790C File Offset: 0x00155B0C
		public TerminateSequenceResponse(UniqueId identifier) : base(true)
		{
			this.identifier = identifier;
		}

		// Token: 0x17001639 RID: 5689
		// (get) Token: 0x06005D09 RID: 23817 RVA: 0x0015791C File Offset: 0x00155B1C
		// (set) Token: 0x06005D0A RID: 23818 RVA: 0x00157924 File Offset: 0x00155B24
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

		// Token: 0x06005D0B RID: 23819 RVA: 0x00157930 File Offset: 0x00155B30
		public static TerminateSequenceResponseInfo Create(XmlDictionaryReader reader)
		{
			TerminateSequenceResponseInfo terminateSequenceResponseInfo = new TerminateSequenceResponseInfo();
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			reader.ReadStartElement(DXD.Wsrm11Dictionary.TerminateSequenceResponse, @namespace);
			reader.ReadStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			terminateSequenceResponseInfo.Identifier = reader.ReadContentAsUniqueId();
			reader.ReadEndElement();
			while (reader.IsStartElement())
			{
				reader.Skip();
			}
			reader.ReadEndElement();
			return terminateSequenceResponseInfo;
		}

		// Token: 0x06005D0C RID: 23820 RVA: 0x0015799C File Offset: 0x00155B9C
		protected override void OnWriteBodyContents(XmlDictionaryWriter writer)
		{
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(ReliableMessagingVersion.WSReliableMessaging11);
			writer.WriteStartElement(DXD.Wsrm11Dictionary.TerminateSequenceResponse, @namespace);
			writer.WriteStartElement(XD.WsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(this.identifier);
			writer.WriteEndElement();
			writer.WriteEndElement();
		}

		// Token: 0x04003761 RID: 14177
		private UniqueId identifier;
	}
}
