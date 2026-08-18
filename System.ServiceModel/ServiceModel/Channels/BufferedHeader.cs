using System;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009D1 RID: 2513
	internal class BufferedHeader : ReadableMessageHeader
	{
		// Token: 0x060062C4 RID: 25284 RVA: 0x0016FB6C File Offset: 0x0016DD6C
		public BufferedHeader(MessageVersion version, XmlBuffer buffer, int bufferIndex, string name, string ns, bool mustUnderstand, string actor, bool relay, bool isRefParam)
		{
			this.version = version;
			this.buffer = buffer;
			this.bufferIndex = bufferIndex;
			this.name = name;
			this.ns = ns;
			this.mustUnderstand = mustUnderstand;
			this.actor = actor;
			this.relay = relay;
			this.isRefParam = isRefParam;
		}

		// Token: 0x060062C5 RID: 25285 RVA: 0x0016FBC4 File Offset: 0x0016DDC4
		public BufferedHeader(MessageVersion version, XmlBuffer buffer, int bufferIndex, MessageHeaderInfo headerInfo)
		{
			this.version = version;
			this.buffer = buffer;
			this.bufferIndex = bufferIndex;
			this.actor = headerInfo.Actor;
			this.relay = headerInfo.Relay;
			this.name = headerInfo.Name;
			this.ns = headerInfo.Namespace;
			this.isRefParam = headerInfo.IsReferenceParameter;
			this.mustUnderstand = headerInfo.MustUnderstand;
		}

		// Token: 0x060062C6 RID: 25286 RVA: 0x0016FC3C File Offset: 0x0016DE3C
		public BufferedHeader(MessageVersion version, XmlBuffer buffer, XmlDictionaryReader reader, XmlAttributeHolder[] envelopeAttributes, XmlAttributeHolder[] headerAttributes)
		{
			this.streamed = true;
			this.buffer = buffer;
			this.version = version;
			MessageHeader.GetHeaderAttributes(reader, version, out this.actor, out this.mustUnderstand, out this.relay, out this.isRefParam);
			this.name = reader.LocalName;
			this.ns = reader.NamespaceURI;
			this.bufferIndex = buffer.SectionCount;
			XmlDictionaryWriter xmlDictionaryWriter = buffer.OpenSection(reader.Quotas);
			xmlDictionaryWriter.WriteStartElement("Envelope");
			if (envelopeAttributes != null)
			{
				XmlAttributeHolder.WriteAttributes(envelopeAttributes, xmlDictionaryWriter);
			}
			xmlDictionaryWriter.WriteStartElement("Header");
			if (headerAttributes != null)
			{
				XmlAttributeHolder.WriteAttributes(headerAttributes, xmlDictionaryWriter);
			}
			xmlDictionaryWriter.WriteNode(reader, false);
			xmlDictionaryWriter.WriteEndElement();
			xmlDictionaryWriter.WriteEndElement();
			buffer.CloseSection();
		}

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060062C7 RID: 25287 RVA: 0x0016FCFC File Offset: 0x0016DEFC
		public override string Actor
		{
			get
			{
				return this.actor;
			}
		}

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060062C8 RID: 25288 RVA: 0x0016FD04 File Offset: 0x0016DF04
		public override bool IsReferenceParameter
		{
			get
			{
				return this.isRefParam;
			}
		}

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060062C9 RID: 25289 RVA: 0x0016FD0C File Offset: 0x0016DF0C
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060062CA RID: 25290 RVA: 0x0016FD14 File Offset: 0x0016DF14
		public override string Namespace
		{
			get
			{
				return this.ns;
			}
		}

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060062CB RID: 25291 RVA: 0x0016FD1C File Offset: 0x0016DF1C
		public override bool MustUnderstand
		{
			get
			{
				return this.mustUnderstand;
			}
		}

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060062CC RID: 25292 RVA: 0x0016FD24 File Offset: 0x0016DF24
		public override bool Relay
		{
			get
			{
				return this.relay;
			}
		}

		// Token: 0x060062CD RID: 25293 RVA: 0x0016FD2C File Offset: 0x0016DF2C
		public override bool IsMessageVersionSupported(MessageVersion messageVersion)
		{
			if (messageVersion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("messageVersion"));
			}
			return messageVersion == this.version;
		}

		// Token: 0x060062CE RID: 25294 RVA: 0x0016FD50 File Offset: 0x0016DF50
		public override XmlDictionaryReader GetHeaderReader()
		{
			XmlDictionaryReader reader = this.buffer.GetReader(this.bufferIndex);
			if (this.streamed)
			{
				reader.MoveToContent();
				reader.Read();
				reader.Read();
				reader.MoveToContent();
			}
			return reader;
		}

		// Token: 0x04003935 RID: 14645
		private MessageVersion version;

		// Token: 0x04003936 RID: 14646
		private XmlBuffer buffer;

		// Token: 0x04003937 RID: 14647
		private int bufferIndex;

		// Token: 0x04003938 RID: 14648
		private string actor;

		// Token: 0x04003939 RID: 14649
		private bool relay;

		// Token: 0x0400393A RID: 14650
		private bool mustUnderstand;

		// Token: 0x0400393B RID: 14651
		private string name;

		// Token: 0x0400393C RID: 14652
		private string ns;

		// Token: 0x0400393D RID: 14653
		private bool streamed;

		// Token: 0x0400393E RID: 14654
		private bool isRefParam;
	}
}
