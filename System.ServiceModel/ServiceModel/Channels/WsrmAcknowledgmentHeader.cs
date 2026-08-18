using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000978 RID: 2424
	internal sealed class WsrmAcknowledgmentHeader : WsrmMessageHeader
	{
		// Token: 0x06005DE1 RID: 24033 RVA: 0x0015B256 File Offset: 0x00159456
		public WsrmAcknowledgmentHeader(ReliableMessagingVersion reliableMessagingVersion, UniqueId sequenceID, SequenceRangeCollection ranges, bool final, int bufferRemaining) : base(reliableMessagingVersion)
		{
			this.sequenceID = sequenceID;
			this.ranges = ranges;
			this.final = final;
			this.bufferRemaining = bufferRemaining;
		}

		// Token: 0x17001689 RID: 5769
		// (get) Token: 0x06005DE2 RID: 24034 RVA: 0x0015B27D File Offset: 0x0015947D
		public override XmlDictionaryString DictionaryName
		{
			get
			{
				return XD.WsrmFeb2005Dictionary.SequenceAcknowledgement;
			}
		}

		// Token: 0x06005DE3 RID: 24035 RVA: 0x0015B28C File Offset: 0x0015948C
		protected override void OnWriteHeaderContents(XmlDictionaryWriter writer, MessageVersion messageVersion)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString dictionaryNamespace = this.DictionaryNamespace;
			WsrmAcknowledgmentHeader.WriteAckRanges(writer, base.ReliableMessagingVersion, this.sequenceID, this.ranges);
			if (base.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11 && this.final)
			{
				writer.WriteStartElement(DXD.Wsrm11Dictionary.Final, dictionaryNamespace);
				writer.WriteEndElement();
			}
			if (this.bufferRemaining != -1)
			{
				writer.WriteStartElement("netrm", wsrmFeb2005Dictionary.BufferRemaining, XD.WsrmFeb2005Dictionary.NETNamespace);
				writer.WriteValue(this.bufferRemaining);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06005DE4 RID: 24036 RVA: 0x0015B320 File Offset: 0x00159520
		internal static void WriteAckRanges(XmlDictionaryWriter writer, ReliableMessagingVersion reliableMessagingVersion, UniqueId sequenceId, SequenceRangeCollection ranges)
		{
			WsrmFeb2005Dictionary wsrmFeb2005Dictionary = XD.WsrmFeb2005Dictionary;
			XmlDictionaryString @namespace = WsrmIndex.GetNamespace(reliableMessagingVersion);
			writer.WriteStartElement(wsrmFeb2005Dictionary.Identifier, @namespace);
			writer.WriteValue(sequenceId);
			writer.WriteEndElement();
			if (ranges.Count == 0)
			{
				if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005)
				{
					ranges = ranges.MergeWith(0L);
				}
				else if (reliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11)
				{
					writer.WriteStartElement(DXD.Wsrm11Dictionary.None, @namespace);
					writer.WriteEndElement();
				}
			}
			for (int i = 0; i < ranges.Count; i++)
			{
				writer.WriteStartElement(wsrmFeb2005Dictionary.AcknowledgementRange, @namespace);
				writer.WriteStartAttribute(wsrmFeb2005Dictionary.Lower, null);
				writer.WriteValue(ranges[i].Lower);
				writer.WriteEndAttribute();
				writer.WriteStartAttribute(wsrmFeb2005Dictionary.Upper, null);
				writer.WriteValue(ranges[i].Upper);
				writer.WriteEndAttribute();
				writer.WriteEndElement();
			}
		}

		// Token: 0x040037BA RID: 14266
		private int bufferRemaining;

		// Token: 0x040037BB RID: 14267
		private bool final;

		// Token: 0x040037BC RID: 14268
		private SequenceRangeCollection ranges;

		// Token: 0x040037BD RID: 14269
		private UniqueId sequenceID;
	}
}
